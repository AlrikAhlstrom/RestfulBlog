using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBDO
{
    public class CommentBDO
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
