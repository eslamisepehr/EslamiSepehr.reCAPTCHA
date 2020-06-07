using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EslamiSepehr.reCAPTCHA.Core.Interfaces
{
    public interface IReCAPTCHAService
    {
        Task<bool> IsValidAsync(HttpRequest request);
    }
}
