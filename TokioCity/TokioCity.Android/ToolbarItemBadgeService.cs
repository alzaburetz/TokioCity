using Plugin.CurrentActivity;
using TokioCity.Droid.Services;
using TokioCity.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ToolbarItemBadgeService))]
namespace TokioCity.Droid.Services
{
    public class ToolbarItemBadgeService : IToolbarItemBadgeService
    {
        public void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var Current = CrossCurrentActivity.Current.Activity;
                if (Current != null)
                {
                    var toolbar = MyShellToolbarAppearanceTracker.mytoolbar;
                    if (toolbar != null)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            var idx = page.ToolbarItems.IndexOf(item);
                            if (toolbar.Menu.Size() > idx)
                            {
                                var menuItem = toolbar.Menu.GetItem(idx);
                                BadgeDrawable.SetBadgeText(CrossCurrentActivity.Current.Activity, menuItem, value, backgroundColor.ToAndroid(), textColor.ToAndroid());
                            }
                        }
                    }
                }
                
            });
        }
    }
}