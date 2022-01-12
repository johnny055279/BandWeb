using System.Threading.Tasks;
using webapi.Entities;

namespace webapi.Interfaces
{
    public interface ITokenServices
    {
        Task<string> CreateToken(AppUser appUser);
    }
}