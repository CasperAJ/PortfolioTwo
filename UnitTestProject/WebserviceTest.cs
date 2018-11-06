using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortfolioTwo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class WebserviceTest
    {
        private const string UsersApi = "http://localhost:5000/api/users";
        private const string CommentsApi = "http://localhost:5000/api/comments";
        private const string SearchesApi = "http://localhost:5000/api/searches";
        
        string passwordHashed = "QNb/XsvJfJTFkZHEQcTSWcvfrtIGxLr0fJp4V/SGeOve/K336Lqv31jqigFxaAZ4wLa5AbBR6QhqWk0+VnVN7pF8y3PFH5VUzo3/XQCcfcXEDfxdwDp3f6sNlOj5FiR+QzQ2rRlMiUeQziLv+zY2bTxfEhLyr+CBwZq6m4HkQ4PdXnw6ZYNg6WSVtocMsNKN1N0y/Qe5xz6qzox93IeiCf8kqa2gEiQDCv59xewacE9a4Si8G8MWIBDXwtsKtTNitvzuyDn+YXSfZuIZVvLRomi1n1sOWFD+0eFk3B8v1S8lje1DU6K1ezB/kXJJ18JNQ5Y/nEVMmYuz5U2ZE68RIw==";
        string genSalt = "WRxECRy0cO/nKoZPmK5h0YHK3uQywJKRPqA35lGuDE2SMJW4x+PEP3wHfBrYVCjtVpltY0GbcCwB6rKx23cMZe4gAWCdENOlQGg5bmFNowDTgm5fwzHMYMXV40VK2+Y+jlEZFuEkgJ3hRJarjs3y2Q2CAhpmn7X8+jvaf9+yR7BAvX9rQscYsjF2a99fKhW7IP9dmJR6k+dpX6mpl9m7XXu8ZgkGwKYmIHYg3jsBPaoztHYKxceQlmASZtECzP5D+TgQIv/VM6puXiqjv/zPstW1XfgNDJbIM19eQ7pJVBmuhyvyUBKMfpPUwxkElUpWDP/3bJHw/Ud3c1c4Hn9Dvg==";
        

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
            var size = 256;
            var newUser = new
            {
                UserName = "UserTest22",
                password = passwordHashed,
                salt = genSalt,
                email = "UserTest22"
            };

            var (user, statusCode) = PostData(UsersApi, newUser);

            Assert.Equal(HttpStatusCode.Created, statusCode);

        }

        //[Fact]
        //public void ApiUsers_PutWithValidUsers_Ok()
        //{
        //    var putUser = new
        //    {
        //        userName = "Put",
        //        password = passwordHashed,
        //        salt = genSalt,
        //        email = "PutEmail@email.com"
        //    };

        //    var (user, _) = GetObject($"{UsersApi}/1");

        //    var update = new
        //    {
        //        id = user["id"],
        //        userName = user["username"] + "Updated",
        //        password = user["password"] + "Updated",
        //        salt = user["salt"] + "Updated",
        //        email = user["email"] + "Updated"

        //    };

        //    var statusCode = PutData($"{UsersApi}/{user["id"]}", update);

        //    Assert.Equal(HttpStatusCode.OK, statusCode);

        //}

        [Fact]
        public void ApiComments_GetAllComments_OK()
        {
            var (data, statusCode) = GetObject(CommentsApi);

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ApiComments_ValidId_OK()
        {
            var (comment, statusCode) = GetObject($"{CommentsApi}/120");

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ApiSearches_StringNotContained_EmptyListOfSearchesAndNotFound()
        {
            var (searches, statusCode) = GetObject($"{SearchesApi}/searchstring/GETJSONOBJECT");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
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

        private static RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        public static string GenerateSalt(int size)
        {
            var buffer = new byte[size];
            _rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

       
    }
}
