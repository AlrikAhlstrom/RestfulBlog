using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBDO
{
    public class PostBDO
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime PostDateTime { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
