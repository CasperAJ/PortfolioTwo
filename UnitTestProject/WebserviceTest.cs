using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class WebserviceTest
    {
        private const string UsersApi = "http://localhost:5000/api/users";
        private const string CommentsApi = "http://localhost:5000/api/comments";

        [Fact]
        public void ApiUsers_GetUsersByValidId_OK()
        {
            var (user, statusCode) = GetObject($"{UsersApi}/2");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("user02", user["userName"]);

        }

        [Fact]
        public void ApiUsers_PostWithUsers_Created()
        {
            var newUser = new
            {
                userName = "Kastanie3",
                password = "test3",
                email = "test@email.dk3"
            };

            var (user, statusCode) = PostData(UsersApi, newUser);

            Assert.Equal(HttpStatusCode.Created, statusCode);

        }

        [Fact]
        public void ApiUsers_PutWithValidUsers_Ok()
        {
            var putUser = new
            {
                userName = "Put",
                password = "TestPut",
                email = "PutEmail@email.com"
            };

            var (user, _) = GetObject($"{UsersApi}/1");

            var update = new
            {
                id = user["id"],
                userName = user["username"] + "Updated",
                password = user["password"] + "Updated",
                email = user["email"] + "Updated"

            };

            var statusCode = PutData($"{UsersApi}/{user["id"]}", update);

            Assert.Equal(HttpStatusCode.OK, statusCode);

        }

        [Fact]
        public void ApiComments_GetAllComments_OK()
        {
            var (data, statusCode) = GetArray(CommentsApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(32042, data.Count);
            Assert.Equal(120, data.First()["id"]);
        }

        [Fact]
        public void ApiComments_ValidId_OK()
        {
            var (comment, statusCode) = GetObject($"{CommentsApi}/120");

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }


        // Helpers
        (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }


        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }
    }
}
