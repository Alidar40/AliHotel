using Microsoft.Extensions.DependencyInjection;
using AliHotel.Domain.Interfaces;
using AliHotel.Domain.Services;

namespace AliHotel.Domain
{
    /// <summary>
    /// Static class for services registration
    /// </summary>
    public static class DomainServices
    {
        /// <summary>
        /// Extension method, which adds services in collection
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection service)
        {
            service.AddScoped<IOrderService, OrderService>();

            return service;
        }
    }
}
