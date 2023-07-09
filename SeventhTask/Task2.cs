using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Net;

namespace SeventhTask
{
    [Author("Ludmila")]
    [TestFixture]
    public class JSONServerAPITests
    {
        /// <summary>
        /// Test where we try to POST some post on our fake JSON server.
        /// Got JSON Server from here - https://github.com/typicode/json-server#getting-started
        /// </summary>
        [Test]
        [Order(0)]
        public void POST_CreateNewPOSTItem()
        {
            RestClient client = new RestClient("http://localhost:3000/");
            RestRequest request = new RestRequest("posts", Method.Post);

            //Post class
            PostsModel post = new PostsModel()
            {
                id = "2",
                title = "test_post",
                author = "post test"
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(post);

            var response = client.Execute<PostsModel>(request);

            Assert.That(response.Data.title, Is.EqualTo("test_post"));
            Assert.That(response.Data.author, Is.EqualTo("post test"));
        }

        /// <summary>
        /// Test where we try to GET the post that we POSTed earlier and checking that it really is our post.
        /// </summary>

        [Test]
        [Order(1)]
        public void GET_CreatedPOSTItem()
        {
            RestClient client = new RestClient("http://localhost:3000/");
            RestRequest request = new RestRequest("posts/2", Method.Get);

            RestResponse response = client.Execute(request);

            var result = new JsonNetSerializer().Deserialize<Dictionary<string, string>>(response);

            Assert.That(result["title"], Is.EqualTo("test_post"));
            Assert.That(result["author"], Is.EqualTo("post test"));
        }

        /// <summary>
        /// Test where we try to DELETE the post that we POSTed earlier and checking that it really was deleted
        /// with getting HTTP Status Code of response.
        /// </summary>
        /// 
        [Test]
        [Order(2)]
        public void DELETE_CreatedPOSTItem()
        {
            RestClient client = new RestClient("http://localhost:3000/");
            RestRequest request = new RestRequest("posts/2", Method.Delete);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
