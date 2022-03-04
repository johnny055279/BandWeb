using System;
using System.Threading.Tasks;
using webapi.DTOs;

namespace webapi.Interfaces
{
    public interface IDropdownResult
    {
        Task<DropDownDto> GetDropdownList<T, TType>(Func<Task<T>> func, TType anyType);
    }
}