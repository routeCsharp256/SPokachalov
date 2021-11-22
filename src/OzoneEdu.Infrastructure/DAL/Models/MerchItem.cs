using System;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public class MerchItem
    {
        public long MerchItemId { get; set; }
        public long MerchPackId { get; set; }
        public long CustomerId { get; set; }
        public long StatusId { get; set; }
        public long IssueTypeId { get; set; }
        public DateTime MerchItemOrderDate { get; set; }
        public DateTime? MerchItemConfirmDate { get; set; }
        public string  MerchItemDescription { get; set; }
    }
}