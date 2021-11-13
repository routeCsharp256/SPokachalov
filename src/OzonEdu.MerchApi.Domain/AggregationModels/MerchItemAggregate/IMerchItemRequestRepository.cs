using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest
{
    /// <summary>
    /// Репозиторий для управления сущностью <see cref="MerchItem"/>
    /// </summary>
    public interface IMerchItemRepository : IRepository<MerchItem>
    {
        /// <summary>
        /// Получить Набор товаров (Merch Pack) по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заявки</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект заявки</returns>
        Task<MerchItem> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить Набор товаров (Merch Pack) по идентификатору заявителя
        /// </summary>
        /// <param name="requestNumber">Номер заявки</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект заявки</returns>
        Task<List<MerchItem>> FindByCustomerIdAsync(int id,
            CancellationToken cancellationToken = default);
    }
}