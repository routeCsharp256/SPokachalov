using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchCustomer"/>
    /// </summary>
    public  interface IMerchItemCustomerRepository : IRepository<IMerchItemCustomerRepository>
    {
        /// <summary>
        /// Получить получателя мерч пака по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект заявки</returns>
        Task<MerchCustomer> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить получателя мерч пака по адресу почты
        /// </summary>
        /// <param name="mailCustomer">Номер заявки</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект заявки</returns>
        Task<MerchCustomer> FindByMailAsync(MailCustomer mailCustomer,
            CancellationToken cancellationToken = default);
    }
}