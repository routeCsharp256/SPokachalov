using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    /// <summary>
    /// Репозиторий для управления <see cref="MerchPack"/>
    /// </summary>
    public interface IMerchPackRepository : IRepository<MerchPack>
    {
        /// <summary>
        /// Найти набор товаров (MerchPack) по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор набора товаров (MerchPack)</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Найти набор товаров (MerchPack) по типу набора (MerchType)
        /// </summary>
        /// <param name="id">Тип набора товаров (MerchType)</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<MerchPack> FindByMerchTypeAsync(MerchType merchType, CancellationToken cancellationToken = default);
    }
}