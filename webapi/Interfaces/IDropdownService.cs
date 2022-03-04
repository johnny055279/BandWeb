using System.Threading.Tasks;
using webapi.DTOs;

namespace webapi.Interfaces
{
    public interface IDropdownService
    {
        Task<DropDownDto> GetCityListAsync();
    }
}