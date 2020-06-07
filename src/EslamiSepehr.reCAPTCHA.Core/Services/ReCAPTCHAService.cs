using EslamiSepehr.reCAPTCHA.Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EslamiSepehr.reCAPTCHA.Core.Interfaces
{
    public class ReCAPTCHAService : IReCAPTCHAService
    {
        private readonly reCAPTCHAOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        public ReCAPTCHAService(IOptions<reCAPTCHAOptions> options, IHttpClientFactory clientFactory)
        {
            _options = options.Value;
            _clientFactory = clientFactory;
        }

        public async Task<bool> IsValidAsync(HttpRequest request)
        {
            var gRecaptchaResponse = request.Form["g-recaptcha-response"];
            var gRequest = new HttpRequestMessage(HttpMethod.Get,
                $"https://www.google.com/recaptcha/api/siteverify?secret={_options.SecretKey}&response={gRecaptchaResponse}");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(gRequest);
            if (!response.IsSuccessStatusCode)
                return false;

            var JSONres = await response.Content.ReadAsStringAsync();
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
                return false;

            return true;
        }
    }
}
