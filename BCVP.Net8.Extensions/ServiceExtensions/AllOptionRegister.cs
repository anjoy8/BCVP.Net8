using BCVP.Net8.Common.Option.Core;
using BCVP.Net8.Common.Option;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCVP.Net8.Extensions.ServiceExtensions
{
    public static class AllOptionRegister
    {
        public static void AddAllOptionRegister(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            foreach (var optionType in typeof(ConfigurableOptions).Assembly.GetTypes().Where(s =>
                         !s.IsInterface && typeof(IConfigurableOptions).IsAssignableFrom(s)))
            {
                services.AddConfigurableOptions(optionType);
            }
        }
    }
}
