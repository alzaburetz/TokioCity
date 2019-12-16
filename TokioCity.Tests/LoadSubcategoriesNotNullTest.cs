using NUnit.Framework;
using FakeItEasy;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Internals;

using TokioCity;
using TokioCity.ViewModels;
using TokioCity.Models;
using TokioCity.Services;

using System.Net.Http;

namespace TokioCity.Tests
{
    public class LoadSubcategoriesNotNullTest
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
        public void GetSubcategoriesNotNullTest()
        {
            var NoSubcategories = database.GetItem<CategorySimplified>("Categories", LiteDB.Query.Where("cat_id", x => x == 1553));
            var WithSubcategories = database.GetItem<CategorySimplified>("Categories", LiteDB.Query.Where("cat_id", x => x == 54));
            Assert.NotNull(NoSubcategories);
            Assert.NotNull(WithSubcategories);

            Assert.That(NoSubcategories.subcats.Count == 0);
            Assert.That(WithSubcategories.subcats.Count > 0);
        }

        [Test]
        public void CategoriesViewModelCommandsTest()
        {
            var client = A.Fake<HttpClient>();

            var Client = client;
            Client.BaseAddress = new System.Uri("https://www.tokyo-city.ru");
            var viewModel = new CategoriesViewModel(Client);

            Assert.NotNull(viewModel);

            viewModel.LoadCategoriesCommand.Execute(null);

            Assert.NotNull(viewModel.CatList);
        }
    }
}
