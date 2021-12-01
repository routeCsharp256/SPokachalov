using System.Collections.Generic;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public sealed class MerchPackItem
    {
        public long MerchPackItemId { get; set; }
        public string MerchPackItemName { get; set; }
        
        public List<Sku> MerchPackItemSkusList { get; set; }

        public MerchPackItem()
        {
            MerchPackItemSkusList = new List<Sku>();
        }
    }
}