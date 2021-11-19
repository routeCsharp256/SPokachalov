using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces;
using OzonEdu.MerchApi.Infrastructure.DAL.Models;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Repositories
{
    public class MerchItemCustomerRepository : IMerchItemCustomerRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchItemCustomerRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker; 
        }
        public Task<IMerchItemCustomerRepository> CreateAsync(IMerchItemCustomerRepository itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IMerchItemCustomerRepository> UpdateAsync(IMerchItemCustomerRepository itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchCustomer> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT customer.id,customer.mail, customer.name, customer.mentormail, customer.mentorname
                from customers customer where id = @Id;";
            
            var parameters = new
            {
                Id = id
            };
            CommandDefinition commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var customer = await connection.QueryAsync<Customer>(commandDefinition);
            var merchCustomer =   customer.Select(
                x => new MerchCustomer((int)x.Id, new MailCustomer(x.Mail),
                    new NameCustomer(x.Name), new MailCustomer(x.MentorMail),
                    new NameCustomer(x.MentorName)));
          
            _changeTracker.Track(merchCustomer.First());
            return merchCustomer.FirstOrDefault();
        }

        public async Task<MerchCustomer> FindByMailAsync(MailCustomer mailCustomer, CancellationToken cancellationToken = default)
        {
            const string sql = @" 
                SELECT customer.id,customer.mail, customer.name, customer.mentormail, customer.mentorname
                from customers customer where mail = @mail;";
            
            var parameters = new
            {
                mail = mailCustomer.Value
            };
            
            CommandDefinition commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var customer = await connection.QueryAsync<Customer>(commandDefinition);
            var merchCustomer =   customer.Select(
                x => new MerchCustomer((int)x.Id, new MailCustomer(x.Mail),
                    new NameCustomer(x.Name), new MailCustomer(x.MentorMail),
                    new NameCustomer(x.MentorName)));
          
            _changeTracker.Track(merchCustomer.First());
            return merchCustomer.First();
        }
    }
}