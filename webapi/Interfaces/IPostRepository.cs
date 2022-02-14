using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.DTOs;
using webapi.Entities;

namespace webapi.Interfaces
{
	public interface IPostRepository
	{
		Task<IEnumerable<Post>> GetPostsAsync();

		Task<Post> GetPostByIdAsync(int id);

		void UpdatePost(Post post);

		Task DeletePost(int id);

		Task CreatePostAsync(Post post);

		void LeaveCommentAsync(PostComment postComment);

		Task DeleteComment(int id);

		void PostLike(PostLike postLike, Post post);

		void PostDislike(PostLike postLike, Post post);

		Task<PostLike> GetPostLike(int userId, int postId);
	}
}

