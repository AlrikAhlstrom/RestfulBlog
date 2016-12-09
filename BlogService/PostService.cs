using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BlogLogic;
using BlogBDO;

namespace BlogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PostService : IBlogService
    {
        PostLogic postLogic = new PostLogic();


        public Post GetPost(string id)
        {
            return GetPost(Convert.ToInt32(id));
        }

        public UpdatePostResponse UpdatePost(UpdatePostRequest request)
        {
            var post = request.Post;
            var message = "";
            
            var result = UpdatePost(ref post, ref message);
            var response = new UpdatePostResponse()
            {
                Result = result,
                Message = message,
                Post = post
            };
            return response;

        }

        public Post GetPost(int id)
        {
            PostBDO postBDO = null;
            try
            {
                postBDO = postLogic.GetPost(id);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                var reason = "GetPost Exception";
                throw new FaultException<PostFault>(new PostFault(msg), reason);
            }

            if (postBDO == null)
            {
                var msg = string.Format("No post found for id {0}", id);
                var reason = "GetPost Empty Post";
                throw new FaultException<PostFault>(new PostFault(msg), reason);
            }

            var post = new Post();
            TranslatePostBDOToPostDTO(postBDO, post);
            return post;
        }

        public bool UpdatePost(ref Post post, ref string message)
        {
            var result = true;

            if (string.IsNullOrEmpty(post.PostTitle))
            {
                message = "Title cannot be empty";
                result = false;
            }

            else if (string.IsNullOrEmpty(post.PostContent))
            {
                message = "Content cannot be empty";
                result = false;
            }
            else
            {
                    try
                    {
                        var postBDO = new PostBDO();
                        TranslatePostDTOtoPostBDO(post, postBDO);
                        result = postLogic.UpdatePost(ref postBDO, ref message);
                        post.RowVersion = postBDO.RowVersion;
                    }
                    catch (Exception e)
                    {
                        var msg = e.Message;
                        throw new FaultException<PostFault>(new PostFault(msg), msg);
                    }
            }
            return result;
        }

        public bool CreatePost(ref Post post, ref string message)
        {
            var result = true;

            if (string.IsNullOrEmpty(post.PostContent))
            {
                message = "Content cannot be empty";
                result = false;
            }

            else
            {
                try
                {
                    var postBDO = new PostBDO();
                    TranslatePostDTOtoPostBDO(post, postBDO);
                    result = postLogic.CreatePost(ref postBDO, ref message);
                }
                catch (Exception e)
                {
                    var msg = e.Message;
                    throw new FaultException<PostFault>(new PostFault(msg), msg);
                }
            }
            return result;
        }

        // BDO -> DTO
        private void TranslatePostBDOToPostDTO(PostBDO postBDO, Post post)
        {
            post.PostId = postBDO.PostId;
            post.PostTitle = postBDO.PostTitle;
            post.PostContent = postBDO.PostContent;
            post.PostDateTime = postBDO.PostDateTime;
            post.RowVersion = postBDO.RowVersion;
        }

        //DTO -> BDO

        private void TranslatePostDTOtoPostBDO(Post post, PostBDO postBDO)
        {
            postBDO.PostId = post.PostId;
            postBDO.PostTitle = post.PostTitle;
            postBDO.PostContent = post.PostContent;
            postBDO.PostDateTime = post.PostDateTime;
            postBDO.RowVersion = post.RowVersion;
        }
    }
}
