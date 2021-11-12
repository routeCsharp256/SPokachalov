using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.CustomerAggregate;
using Xunit;

namespace OzonEdu.MerchApi.Domain.Tests
{
    public sealed class MechCustomerTest
    {
        [Fact]
        public void CreateMechCustomer()
        {
            var customer = new MerchCustomer(new MailCustomer("qwerty@ya.ru"), new NameCustomer("Иванов И.И."));
            Assert.NotNull(customer);
        }

        [Fact]
        public void NotValidMailMechCustomer()
        {
            Assert.Throws<NotValidMailException>(() =>
            {
                var customer = new MerchCustomer(new MailCustomer("qwertyya.ru"), new NameCustomer("Иванов И.И."));
            });
        }

        [Fact]
        public void NotValidMentorMailMechCustomer()
        {
            Assert.Throws<NotValidMailException>(() =>
            {
                var customer = new MerchCustomer(new MailCustomer("qwerty@ya.ru"), new NameCustomer("Иванов И.И."));
                customer.SetMentorMail(new MailCustomer("hasdgfhjadgshja"));
            });
        }
    }
}