using System.Net;
using RestSharp;

namespace SeventhTask
{
    [Author("Ludmila")]
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// API test for Github where we check can we GET user's (in this test - my) repos.
        /// If HTTP Status Code is OK, then test is successfull.
        /// </summary>
        /// <param name="userUrl"></param>
        [TestCase("users/Artona17/repos")]
        public void CheckReponseStatus_WhenGettingUsersRepos(string userUrl)
        {
            RestClient client = new RestClient("https://api.github.com/");
            RestRequest request = new RestRequest(userUrl, Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}