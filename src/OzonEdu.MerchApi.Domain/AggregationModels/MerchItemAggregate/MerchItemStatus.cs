using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.StockApi.Domain.AggregatesModels.DeliveryRequestAggregate
{
    public sealed class MerchItemStatus : Enumeration
    {
        public static MerchItemStatus InWork = new(1, "InWork");
        public static MerchItemStatus Done = new(1, "Done");
        
        public MerchItemStatus(int id, string name) : base(id, name)
        {
        }
    }
}