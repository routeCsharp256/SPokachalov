using System.Collections.Generic;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public class CreateStockItemAvailableRequestCommand : IRequest
    {
        public List<long> SkuList { get; set; }
    }
}