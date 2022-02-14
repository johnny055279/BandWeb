using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Repositories
{
	public class PostRepository : IPostRepository
	{
        private readonly DataContext dataContext;

        public PostRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task CreatePostAsync(Post post)
        {
           await dataContext.AddAsync(post);
        }

        public async Task DeleteComment(int id)
        {
            var postComment = await dataContext.PostComments.FindAsync(id);

            dataContext.Remove(postComment);
        }

        public async Task DeletePost(int id)
        {
            var post = await dataContext.Posts.FindAsync(id);

            dataContext.Remove(post);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await dataContext.Posts.FindAsync(id);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await dataContext.Posts.ToListAsync();
        }

        public async Task<PostLike> GetPostLike(int userId, int postId)
        {
            return await dataContext.PostLikes.FindAsync(userId, postId);
        }

        public void LeaveCommentAsync(PostComment postComment)
        {
            dataContext.Add(postComment);
        }

        public void PostDislike(PostLike postLike, Post post)
        {
            dataContext.Remove(postLike);

            dataContext.Entry<Post>(post).State = EntityState.Modified;
        }

        public void PostLike(PostLike postLike, Post post)
        {
            dataContext.Add(postLike);

            dataContext.Entry<Post>(post).State = EntityState.Modified;
        }

        public void UpdatePost(Post post)
        {
            dataContext.Entry<Post>(post).State = EntityState.Modified;
        }
    }
}

