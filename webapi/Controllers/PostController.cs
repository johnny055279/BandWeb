using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;
using webapi.Entities;
using webapi.Extensions;
using webapi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PostController : BaseController
    {

        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public PostController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsAsync()
        {
            var posts = await unitOfWork.PostRepository.GetPostsAsync();

            return Ok(mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsAsync(int id)
        {
            var posts = await unitOfWork.PostRepository.GetPostsAsync();

            return Ok(mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePostAsync(PostDto postDto)
        {
            var post = new Post(postDto.Title, postDto.Content);

            unitOfWork.PostRepository.CreatePostAsync(post);

            if (!await unitOfWork.Complete()) return BadRequest("fail to create post");

            return Ok(mapper.Map(post, postDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostCommentDto>> UpdatePost(int id, [FromBody] PostUpdateDto postUpdateDto)
        {
            var post = await unitOfWork.PostRepository.GetPostByIdAsync(id);

            mapper.Map(postUpdateDto, post);

            unitOfWork.PostRepository.UpdatePost(post);

            if (!await unitOfWork.Complete()) return BadRequest("can't not update post");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult> DeletePost(int id)
        {
            await unitOfWork.PostRepository.DeletePost(id);

            if (!await unitOfWork.Complete()) return BadRequest("fail to delete post");

            return NoContent();
        }

        [HttpPost("{id}/comment")]
        public async Task<ActionResult<PostCommentDto>> LeaveComment(int id, [FromBody] PostCommentDto postCommentDto)
        {

            var user = await unitOfWork.UserRepository.GetUserByNameAsync(User.GetUserName());

            var post = await unitOfWork.PostRepository.GetPostByIdAsync(id);

            var postComment = new PostComment(post.Id, user.Id, postCommentDto.Comment)
            {
                User = user,

                Post = post
            };

            unitOfWork.PostRepository.LeaveCommentAsync(postComment);

            if (!await unitOfWork.Complete()) return BadRequest("can't leave comment");

            return Ok(postCommentDto);
        }

        [HttpPost("{id}/like")]
        public async Task<ActionResult<PostDto>> PostLike(int id)
        {
            var post = await unitOfWork.PostRepository.GetPostByIdAsync(id);

            var user = await unitOfWork.UserRepository.GetUserByNameAsync(User.GetUserName());

            var postLike = await unitOfWork.PostRepository.GetPostLike(user.Id, post.Id);

            if(postLike != null)
            {
                post.Likes = post.Likes -= 1;

                unitOfWork.PostRepository.PostDislike(postLike, post);
            }
            else
            {
                post.Likes = post.Likes += 1;

                var newPostLike = new PostLike(post.Id, user.Id)
                {
                    User = user,
                    Post = post
                };

                unitOfWork.PostRepository.PostLike(newPostLike, post);
            }

            if (!await unitOfWork.Complete()) return BadRequest("fail to set postlike");

            return Ok(mapper.Map<PostDto>(post));
        }
    }
}

