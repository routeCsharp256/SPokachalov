using System;
using MediatR;
using OzonEdu.Infrastructure.Commands;
using OzonEdu.Infrastructure.Handlers.DeliveryRequestAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.CustomerAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemRequest;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Infrastructure.Stubs;

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
            services.AddMediatR(typeof(CreateStockItemRequestCommand).Assembly);
            services.AddMediatR(typeof(CreateStockItemAvailableRequestCommand).Assembly);
            services.AddMediatR(typeof(CreateStockItemAvailableRequestCommand).Assembly);

            return services;
        }

        
        /// <summary>
        /// Добавление в DI контейнер инфраструктурных репозиториев
        /// </summary>
        /// <param name="services">Объект IServiceCollection</param>
        /// <returns>Объект <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMerchPackRepository, MerchPackRepository>();
            services.AddTransient<IMerchItemRepository, MerchItemRepository>();
            services.AddTransient<IMerchItemCustomerRepository, MerchItemCustomerRepository>();
            return services;
        }
    }
}