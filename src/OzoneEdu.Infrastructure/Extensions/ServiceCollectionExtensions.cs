using System;
using MediatR;
using OzonEdu.Infrastructure.Handlers.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Класс расширений для типа <see cref="IServiceCollection"/> для регистрации инфраструктурных сервисов
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Добавление в DI контейнер инфраструктурных сервисов
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        { 
            services.AddMediatR(typeof(CreateMerchItemCommandHandler).Assembly); 
            services.AddMediatR(typeof(CheckMerchItemCommandHandler).Assembly); 
            services.AddMediatR(typeof(GetIssuedMerchPacksQueryHandler).Assembly);
            return services;
        }

        
        
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<OzonEdu.MerchApi.Domain.Contracts.IUnitOfWork, OzonEdu.MerchApi.Infrastructure.DAL.Infrastructure.UnitOfWork>();

            services.AddScoped<IMerchPackRepository, OzonEdu.MerchApi.Infrastructure.DAL.Repositories.MerchPackRepository>();
            services.AddScoped<IMerchItemRepository, OzonEdu.MerchApi.Infrastructure.DAL.Repositories.MerchItemRepository>();
            services.AddScoped<IMerchItemCustomerRepository, OzonEdu.MerchApi.Infrastructure.DAL.Repositories.MerchItemCustomerRepository>();
            return services;
        }
    }

    public interface IUnitOfWork
    {
    }
}