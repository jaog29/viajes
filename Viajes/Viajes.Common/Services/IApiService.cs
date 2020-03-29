using Viajes.Common.Models;
using System.Threading.Tasks;

namespace Viajes.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetTripAsync(int id,string urlBase, string servicePrefix, string controller);
    }

}
