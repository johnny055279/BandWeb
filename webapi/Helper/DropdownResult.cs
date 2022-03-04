using System;
using System.Threading.Tasks;
using webapi.Data;
using webapi.DTOs;
using webapi.Interfaces;

namespace webapi.Helper
{
    public class DropdownResult : IDropdownResult
    {
        public async Task<DropDownDto> GetDropdownList<T, TType>(Func<Task<T>> func, TType anyType)
        {
            var result = anyType switch
            {
                "city" => await func(),

                _ => throw new NotImplementedException()
            };

            return result as DropDownDto;
        }
    }
}

