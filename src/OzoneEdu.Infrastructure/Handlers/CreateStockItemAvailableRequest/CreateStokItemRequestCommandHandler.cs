using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace OzonEdu.Infrastructure.Commands
{
    public class CreateStockItemRequestCommandHandler : IRequestHandler<CreateStockItemRequestCommand, bool>
    {
        public async Task<bool> Handle(CreateStockItemRequestCommand stockItems, CancellationToken cancellationToken)
        {
           
            ////
            /// Запрос на резервирование товарных позиций
            /// 
            return true;
        }
    }
}