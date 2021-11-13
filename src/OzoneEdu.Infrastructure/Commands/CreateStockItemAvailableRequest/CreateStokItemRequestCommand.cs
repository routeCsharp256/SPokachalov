using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public class CreateStockItemRequestCommand : IRequest<bool>
    {
        public List<long> SkuList { get; set; }
    }
}