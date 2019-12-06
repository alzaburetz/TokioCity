using NUnit.Framework;

using TokioCity.Services;
using TokioCity.ViewModels;
using TokioCity;

using FakeItEasy;
using Xamarin;
using Xamarin.Forms.PlatformConfiguration;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TokioCity.Tests
{
    public class TestsFavorite
    {
        public IDataBase datastore;
        [SetUp]
        public void Setup()
        {
            var platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = platformServicesFake;
        }

        [Test]
        public void FavoriteModelNotNullAfterInitialization()
        {
            
            var fav = new FavoriteViewModel();
            //fav.LoadFavorites.Execute(null);

            Assert.IsNotNull(fav.favorites);
        }

        [Test]
        public void DataBaseNotNull()
        {
            var datastore = A.Fake<DataBaseService>((te) => DependencyService.Get<IDataBase>());
            Assert.IsNotNull(datastore);
        }
    }
}