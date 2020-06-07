using EslamiSepehr.reCAPTCHA.Core.Options;
using EslamiSepehr.reCAPTCHA.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EslamiSepehr.reCAPTCHA.Core.Extensions
{
    public static class reCaptchaExtensions
    {
        public static void AddEsReCAPTCHA(this IServiceCollection services,
                                          Action<reCAPTCHAOptions> configureOptions)
        {
            configureOptions(new reCAPTCHAOptions());
            services.Configure(configureOptions);

            services.AddHttpClient();
            services.AddTransient<IReCAPTCHAService, ReCAPTCHAService>();
        }
    }
}
