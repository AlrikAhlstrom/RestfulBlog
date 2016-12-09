using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BlogService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBlogService
    {
        [WebGet (UriTemplate = "GetPost/{id}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [FaultContract(typeof(PostFault))]
        Post GetPost(string id);

        [WebInvoke(Method = "POST", UriTemplate = "/UpdatePost", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [FaultContract(typeof (PostFault))]
        UpdatePostResponse UpdatePost(UpdatePostRequest request);

        //[OperationContract]
        //[FaultContract(typeof (PostFault))]
        //bool CreatePost(ref Post post, ref string message);

        // TODO: Add your service operations here
    }

    public class UpdatePostRequest
    {
        public Post Post { get; set; }
    }

    public class UpdatePostResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public Post Post { get; set; }
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "BlogService.ContractType".
    [DataContract]
    public class Post
    {
        [DataMember]
        public int PostId { get; set; }
        [DataMember]
        public string PostTitle { get; set; }
        [DataMember]
        public string PostContent { get; set; }
        [DataMember]
        public DateTime PostDateTime { get; set; }
        [DataMember]
        public byte[] RowVersion { get; set; }

    }

    [DataContract]
    public class PostFault
    {
        public PostFault(string msg)
        {
            FaultMessage = msg;
        }

        [DataMember]
        public string FaultMessage;
    }
}
