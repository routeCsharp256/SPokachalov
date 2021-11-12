using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class FillingItemType : Enumeration
    {
        public static FillingItemType TShirt = new(1, nameof(TShirt));
        public static FillingItemType Sweatshirt = new(2, nameof(Sweatshirt));
        public static FillingItemType Notepad = new(3, nameof(Notepad));
        public static FillingItemType Bag = new(4, nameof(Bag));
        public static FillingItemType Pen = new(5, nameof(Pen));
        public static FillingItemType Socks = new(6, nameof(Socks));

        public FillingItemType(int id, string name) : base(id, name)
        {
        }
    }
}