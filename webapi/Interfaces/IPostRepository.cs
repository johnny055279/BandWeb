using System;
using System.Threading.Tasks;

namespace webapi.Interfaces
{
	public interface IPostRepository
	{
		Task GetPostsAsync();
	}
}

