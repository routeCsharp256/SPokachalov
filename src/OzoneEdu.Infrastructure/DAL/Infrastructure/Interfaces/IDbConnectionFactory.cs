using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.Interfaces
{
    /// <summary>
    /// Фабрика подключений к базе данных.
    /// </summary>
    public interface IDbConnectionFactory<TConnection> : IDisposable
    {
        /// <summary>
        /// Создать подключение к БД.
        /// </summary>
        /// <returns></returns>
        Task<TConnection> CreateConnection(CancellationToken token);
    }
}