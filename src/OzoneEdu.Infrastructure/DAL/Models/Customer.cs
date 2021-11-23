namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public sealed class Customer
    {
        public long CustomerId { get; set; }
        public string CustomerMail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMentorMail { get; set; }
        public string CustomerMentorName { get; set; }
    }
}