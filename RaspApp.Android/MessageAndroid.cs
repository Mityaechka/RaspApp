
using Android.App;
using Android.Widget;
using RaspApp.Droid;
using RaspApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace RaspApp.Droid
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}