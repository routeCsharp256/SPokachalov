using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.Infrastructure.Queries.StockItemAggregate;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzoneEdu.MerchandiseService.Services
{
    public class MerchService : IMerchService
    {
        private readonly IMediator _mediator;

        public MerchService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Выдача мерча по запросу
        /// </summary>
        public async Task<bool> GetMerch(long userId, List<long> skuList, long merchTypeId, long issuedType,
            bool isDone,
            CancellationToken cancelationToken)
        {
            var command = new CreateMerchItemCommand()
            {
                Sku = skuList,
                MerchPackTypeId = merchTypeId,
                MerchCustomerId = userId,
                IssueTypeId = issuedType,
                IsDone = isDone
            };
            var res = await _mediator.Send(command, cancelationToken);
            return res;
        }

        /// <summary>
        /// Возвращает информацию о выданных мерчах  по сотруднику
        /// </summary>
        public async Task<List<MerchItem>> GetMerchesByUserId(long userId, CancellationToken cancelationToken)
        {
            var query = new GetIssuedMerchPacksQuery()
            {
                Id = (int) userId
            };
            var collection = await _mediator.Send(query, cancelationToken);
            var res = new List<MerchItem>();
            foreach (var item in collection)
            {
                res.Add(new MerchItem(item.Id, item.Pack.Type.Type.Name));
            }

            return res;
        }

       
        ///  Проверка
        public async Task<bool> CheckMerch(int userId, int packId, CancellationToken cancelationToken)
        {
            var command = new CheckMerchItemCommand()
            {
                MerchCustomerId = userId,
                MerchPackTypeId = packId
            };
            var res = await _mediator.Send(command, cancelationToken);
            return res;
        }

        // провкерка на доступность в стоке
        public async Task<bool> CheckAvailableStokMerch(List<long> skuList, CancellationToken cancelationToken)
        {
            var command = new CreateStockItemRequestCommand()
            {
                SkuList = skuList
            };
            var res = await _mediator.Send(command, cancelationToken);
            return res;
        }

        // зарезервировать  в стоке
        public async Task<bool> ReserveOnStokMerch(List<long> skuList, CancellationToken cancelationToken)
        {
            var command = new CreateStockItemRequestCommand()
            {
                SkuList = skuList
            };
            var res = await _mediator.Send(command, cancelationToken);
            return res;
        }
    }
}