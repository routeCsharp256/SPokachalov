using System.Collections.Generic;

namespace OzonEdu.MerchandiseService.Models
{
    public class MerchItemRequest
    {
        public List<long> Sku { get; set; }
        
        public int MerchCustomerId { get; set; }
    }
}