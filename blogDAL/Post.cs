//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace blogDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public System.DateTime PostDateTime { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual Comment Comment { get; set; }
    }
}
