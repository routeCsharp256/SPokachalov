using System.Collections.Generic;


namespace OzonEdu.MerchApi.Infrastructure.DAL.Models
{
    public sealed class MerchPack
    {
        public long  MerchPackId { get; set; }

        
        public string MerchPackName { get; set; }

        public List<MerchPackItem> MerchPackItems { get; set; }

        public MerchPack()
        {
            MerchPackItems = new List<MerchPackItem>();
        }

    }
}