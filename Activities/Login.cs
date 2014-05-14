using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace SIM
{ 

	[Activity (MainLauncher = true)]
	public class Login : Activity
	{
	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.activity_login);

			Button btnLogin = FindViewById<Button> (Resource.Id.btnLogin);

			btnLogin.Click += delegate
			{

				StartActivity(typeof(HolderFragmentActivity));
				OverridePendingTransition(Resource.Animation.slide_right,Resource.Animation.slide_left);
			};

			// Get our button from the layout resource,
			// and attach an event to it
		
		}
	}
}


