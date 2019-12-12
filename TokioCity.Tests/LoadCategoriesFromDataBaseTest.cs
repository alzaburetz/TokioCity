using NUnit.Framework;

using TokioCity.Services;
using TokioCity.ViewModels;
using TokioCity;
using TokioCity.Models;

using FakeItEasy;
using Xamarin;
using Xamarin.Forms.PlatformConfiguration;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Moq;
using Moq.Internals;
using Moq.Protected;
using System.Net;

    

using System.Net.Http;
using System.Collections.Generic;

using System.Threading.Tasks;
using Newtonsoft.Json;

using RichardSzalay.MockHttp;


namespace TokioCity.Tests
{
    public interface IHttpProvider
    { 
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }

    public class HttpClientProvider: IHttpProvider
    {
        private readonly HttpClient _httpClient;
        public HttpClientProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _httpClient.GetAsync(requestUri);
        }
    }
    public class RequestMock
    {
        private readonly IHttpProvider _httpProvider;

        public RequestMock(IHttpProvider httpProvider)
        {
            _httpProvider = httpProvider;
        }
        public async Task<List<Category>> LoadCategs()
        {
            var response = await _httpProvider.GetAsync("data/app_categs.php?version=1.8");
            var responseData = await response.Content.ReadAsStringAsync();
            var categs = JsonConvert.DeserializeObject<List<Category>>(responseData);
            return categs; 
        }

    }
    public class LoadCategoriesFromDataBaseTest
    {
        public IDataBase database { get; set; }
        public System.Net.Http.HttpClient client { get; set; }
        public List<Category> categories { get; set; }
        [SetUp]
        public void SetUp()
        {
            var platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = platformServicesFake;
            client = new System.Net.Http.HttpClient();
            database = A.Fake<DataBaseService>((te) => DependencyService.Get<IDataBase>());

            client.BaseAddress = new System.Uri("http://www.tokio-city.ru");
        }


        [Test]
        public async Task LoadCategoriesNotNull()
        {
            var mockClient = new MockHttpMessageHandler();
            var client = A.Fake<HttpClient>();

            var Client = client;
            Client.BaseAddress = new System.Uri("https://www.tokyo-city.ru");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var response = await Client.GetAsync("data/app_categs_full20.php?version=1.8");
            var json = await response.Content.ReadAsStringAsync();
            var categs = JsonConvert.DeserializeObject<List<Category>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Assert.NotNull(categs);
            database.WriteAll<Category>("Categories", categs);
            Assert.That(database.GetRecordCount<Category>("Categories") > 0);
            Assert.That(database.GetItem<Category>("Categories", LiteDB.Query.Where("id", x => x.AsInt32 == 49)).subcategories.Count > 0);
        }

        [Test]
        public void LoadSingleCategoryNotNull()
        {
            var category = database.GetItem<Category>("Categories", LiteDB.Query.Where("id", x => x.AsInt32 == 211));
            Assert.NotNull(category);
            Assert.That(category.subcategories.Count > 0);
        }
    }
}
