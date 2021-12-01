using System;
using System.Collections.Generic;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public class MerchItem
    {
        public long MerchItemId { get; set; }
        public long MerchPackId { get; set; }
        public MerchPack MerchPack { get; set; }
        
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public long StatusId { get; set; }
        public MerchStatus Status { get; set; }
        
        public long IssueTypeId { get; set; }
        public IssueType IssueType { get; set; }

        public DateTime MerchItemOrderDate { get; set; }
        public DateTime? MerchItemConfirmDate { get; set; }
        public string  MerchItemDescription { get; set; }
        public List<Sku> MerchSkusList { get; set; }

        public MerchItem()
        {
            MerchSkusList = new List<Sku>();
        }

    }
}