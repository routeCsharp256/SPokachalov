using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzoneEdu.MerchandiseService.Services
{
    public class MerchService : IMerchService
    {
        /// <summary>
        /// Выдача мерча по запросу
        /// </summary>
        public Task<MerchItem> GetMerch(long userId, CancellationToken _)
        {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Возвращает информацию о выданных мерчах  по сотруднику
        /// </summary>
        public Task<List<MerchItem>> GetMerchesByUserId(long userId, CancellationToken _)
        {
            throw new System.NotImplementedException();
        }
    }
}