using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public class CreateStockItemAvailableRequestCommandHandler : IRequestHandler<CreateStockItemRequestCommand, bool>
    {
        public async Task<bool> Handle(CreateStockItemRequestCommand stockItems, CancellationToken cancellationToken)
        {
            foreach (var sku in stockItems.SkuList)
            {
                ////
                /// Запрос на проверку товарных позиций
                /// /// 
            }
          
            return true;
        }
    }

 
}