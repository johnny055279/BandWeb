using System;
using System.Threading.Tasks;
using webapi.Data;

namespace webapi.Helper
{
	public class DropdownResult
	{
		

        public async Task<T> GetDropdownList<T, TType>(Func<Task<T>> func, TType anyType)
        {
            var result = anyType switch
            {
                "city" => await func(),

                _ => throw new NotImplementedException()
            };

            return result;
        }
    }
}

