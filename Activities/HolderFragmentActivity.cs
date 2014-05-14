using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using Android.App;
using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;


namespace SIM
{
	[Activity (Label = "HolderFragmentActivity")]			
	public class HolderFragmentActivity : Activity
	{
		LeftDrawerMenuAdapter _leftDrawerMenuAdapter;
		RelativeLayout _rightDrawer;
		ExpandableListView _leftDrawer;
		private NavigationDrawerToggleListener  _DrawerToggle;
		private DrawerLayout _DrawerLayout;

		List<String> listDataHeader;
		Dictionary<String, List<String>> listDataChild;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
				
			SetContentView (Resource.Layout.activity_fragment_holder);
			_DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			_DrawerLayout.SetScrimColor(50);
			_DrawerLayout.SetDrawerShadow(Resource.Drawable.nav_bar_shadow, (int)GravityFlags.Left);

			ActionBar.SetDisplayHomeAsUpEnabled(true);
			ActionBar.SetHomeButtonEnabled(true);

			_DrawerToggle = new NavigationDrawerToggleListener (this, _DrawerLayout,
				Resource.Drawable.ic_action_ic_drawer,
				Resource.String.app_name,
				Resource.String.app_name);

			//You can alternatively use _drawer.DrawerClosed here
			_DrawerToggle.DrawerClosed += delegate
			{
				ActionBar.Title = Resources.GetString(Resource.String.app_name);
				InvalidateOptionsMenu();
			};

			//You can alternatively use _drawer.DrawerOpened here
			_DrawerToggle.DrawerOpened += delegate
			{
				ActionBar.Title =  Resources.GetString(Resource.String.app_name);

				InvalidateOptionsMenu();

			};


			/*
			if (null == savedInstanceState)
				SelectItem(0);*/

			_DrawerLayout.SetDrawerListener(_DrawerToggle);

			//Define Left and  right Drawer menu
			_leftDrawer = FindViewById<ExpandableListView>(Resource.Id.left_drawer);
			_rightDrawer = FindViewById<RelativeLayout>(Resource.Id.right_drawer);

			// preparing list data
			PrepareListData();
			_leftDrawer = FindViewById<ExpandableListView> (Resource.Id.left_drawer);
			_leftDrawer.SetAdapter (new LeftDrawerMenuAdapter (this, listDataChild));
			//_leftDrawer.SetOnChildClickListener(new ExpandableListView.IOnChildClickListener

		}


		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);
			_DrawerToggle.SyncState();
		}
		public override void OnConfigurationChanged(Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			_DrawerToggle.OnConfigurationChanged(newConfig);

		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.main, menu);
			return base.OnCreateOptionsMenu(menu);
		}
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (_DrawerToggle.OnOptionsItemSelected(item))
				return true;

			switch (item.ItemId)
			{
			case Resource.Id.action_submenu:
				{
					/*
					var intent = new Intent(Intent.ActionWebSearch);
					intent.PutExtra(SearchManager.Query, ActionBar.Title);

					if ((intent.ResolveActivity(PackageManager)) != null)
						StartActivity(intent);
					else
						Toast.MakeText(this, Resource.String.app_not_available, ToastLength.Long).Show();
					return true;*/

					if(_DrawerLayout.IsDrawerOpen(_rightDrawer))
						_DrawerLayout.CloseDrawer(_rightDrawer);
					else
						_DrawerLayout.OpenDrawer(_rightDrawer);
					return true;

				}
		
			default:
				return base.OnOptionsItemSelected(item);
			}
		}




		/*
     * Preparing the list data
     */
		private void PrepareListData() {


			String[] mainModule = Resources.GetStringArray (Resource.Array.module_main);
			String[] inventoryModule = Resources.GetStringArray (Resource.Array.module_inventory_management);
			String[] shippingReceivingModule = Resources.GetStringArray (Resource.Array.module_shipping_receiving);
			String[] administrationModule = Resources.GetStringArray (Resource.Array.module_administration);
			String[] lookupsModule = Resources.GetStringArray (Resource.Array.module_lookups);
			String[] reportsModule = Resources.GetStringArray (Resource.Array.module_reports);

			listDataHeader = new List<String>() ;
			listDataChild = new Dictionary<String, List<String>>();

			foreach (String module in mainModule) {
				listDataHeader.Add(module);
			}

			List<String> inventoryManagementChild = new List<String>() ;
			foreach (String item in inventoryModule) {
				inventoryManagementChild.Add(item);
			}

			List<String> shippingReceivingChild =  new List<String>() ;
			foreach (String item in shippingReceivingModule) {
				shippingReceivingChild.Add(item);
			}

			List<String> administrationChild =  new List<String>() ;
			foreach (String item in administrationModule) {
				administrationChild.Add(item);
			}

			List<String> lookupsChild =  new List<String>() ;
			foreach (String item in lookupsModule) {
				lookupsChild.Add(item);
			}

			List<String> reportsChild =  new List<String>() ;
			foreach (String item in reportsModule) {
				reportsChild.Add(item);
			}

			listDataChild.Add(listDataHeader[0], inventoryManagementChild); // Header, Child data
			listDataChild.Add(listDataHeader[1], shippingReceivingChild);
			listDataChild.Add(listDataHeader[2], administrationChild);
			listDataChild.Add(listDataHeader[3], lookupsChild);
			listDataChild.Add(listDataHeader[4], reportsChild);
			
		}

	}
}

