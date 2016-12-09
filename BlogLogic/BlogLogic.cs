using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blogDAL;
using BlogBDO;

namespace BlogLogic
{
    public class PostLogic
    {
        PostDAO postDAO = new PostDAO();

        public PostBDO GetPost(int id)
        {
            return postDAO.GetPost(id);
        }

        public bool UpdatePost(ref PostBDO postBDO, ref string message)
        {
            var postInDB = GetPost(postBDO.PostId);

            //invalid post to update
            if (postInDB == null)
            {
                message = "cannot get post for this ID";
                return false;
            }
            else
            {
                return postDAO.UpdatePost(ref postBDO, ref message);
            }
        }

        public bool CreatePost(ref PostBDO postBDO, ref string message)
        {
            

                return postDAO.CreatePost(ref postBDO, ref message);
            
        }
    }
}
