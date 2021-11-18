using System;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public class MerchItem
    {
        public long Id { get; set; }
        public long MerchPackId { get; set; }
        public long CustomerId { get; set; }
        public long StatusId { get; set; }
        public long IssueTypeid { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string  Description { get; set; }
    }
}