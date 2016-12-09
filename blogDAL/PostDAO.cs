using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlogBDO;

namespace blogDAL
{
    public class PostDAO
    {
        public PostBDO GetPost(int id)
        {
            PostBDO postBDO = null;
            using (var BEntities = new BlogEntities())
            {
                var post = (from p in BEntities.Posts
                    where p.PostId == id
                    select p).FirstOrDefault();

                if (post != null)
                    postBDO = new PostBDO()
                    {
                        PostId = post.PostId,
                        PostTitle = post.PostTitle,
                        PostContent = post.PostContent,
                        PostDateTime = post.PostDateTime,
                        RowVersion = post.RowVersion
                    };
            }
            return postBDO;
        }

        public bool CreatePost(ref PostBDO postBDO, ref string message)
        {
            message = "post created";
            var ret = true;

            using (var BEntities = new BlogEntities())
            {

                var maxId = 0;
                var maxIdPost = BEntities.Posts.OrderByDescending(i => i.PostId).FirstOrDefault();
                if (maxIdPost != null)
                {
                    maxId = maxIdPost.PostId;
                }

                var postId = maxId + 1;

                //var postId = postBDO.PostId;

                Post postToDB = new Post
                {
                    PostId = postBDO.PostId,
                    PostTitle = postBDO.PostTitle,
                    PostContent = postBDO.PostContent,
                    PostDateTime = DateTime.Now
                };
                BEntities.Posts.Add(postToDB);

                var num = BEntities.SaveChanges();

                if (num != 1)
                {
                    ret = false;
                    message = "no post created";
                }
            }
            return ret;

        }

        public bool UpdatePost(ref PostBDO postBDO, ref string message)
        {
            message = "post updated successfully";
            var ret = true;

            using (var BEntities = new BlogEntities())
            {
                var postId = postBDO.PostId;
                Post postInDB = (from p in BEntities.Posts
                    where p.PostId == postId
                    select p).FirstOrDefault();

                //check post
                if (postInDB == null)
                {
                    throw new Exception("No post with ID " + postBDO.PostId + " found.");
                }

                //update post
                postInDB.PostTitle = postBDO.PostTitle;
                postInDB.PostContent = postBDO.PostContent;
                postInDB.RowVersion = postBDO.RowVersion;

                BEntities.Posts.Attach(postInDB);
                BEntities.Entry(postInDB).State = System.Data.Entity.EntityState.Modified;
                var num = BEntities.SaveChanges();

                postBDO.RowVersion = postInDB.RowVersion;

                if (num != 1)
                {
                    ret = false;
                    message = "no post is updated";
                }
            }
            return ret;
        }
    }


}
