namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public sealed class Customer
    {
        public long Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string MentorMail { get; set; }
        public string MentorName { get; set; }
    }
}