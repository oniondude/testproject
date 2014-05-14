using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SIM
{
	class LeftDrawerMenuAdapter : BaseExpandableListAdapter
	{

		Dictionary<string, List<string>> _dictGroup =null;
		List<string> _lstGroupID = null;
		Activity _activity;

		public LeftDrawerMenuAdapter (Activity activity,
			Dictionary<string, List<string>> dictGroup)
		{
			_dictGroup = dictGroup;
			_activity = activity;
			_lstGroupID = dictGroup.Keys.ToList();

		}

		#region implemented abstract members of BaseExpandableListAdapter
		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			return _dictGroup [_lstGroupID [groupPosition]] [childPosition];
		}
		public override long GetChildId (int groupPosition, int childPosition)
		{
			return childPosition;
		}
		public override int GetChildrenCount (int groupPosition)
		{
			return _dictGroup [_lstGroupID [groupPosition]].Count;
		}
		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			var item = _dictGroup [_lstGroupID [groupPosition]] [childPosition];

			if (convertView == null)
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.line_list_group_item, null);

			var textBox = convertView.FindViewById<TextView> (Resource.Id.lblListItem);
			textBox.SetText (item, TextView.BufferType.Normal);

			convertView.Click += delegate 
			{
				Toast.MakeText(_activity, item, ToastLength.Long).Show();
			};



			return convertView;
		}
		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			return _lstGroupID [groupPosition];
		}
		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}
		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			var item = _lstGroupID [groupPosition];

			if (convertView == null)
				convertView = _activity.LayoutInflater.Inflate (Resource.Layout.line_list_group, null);

			var textBox = convertView.FindViewById<TextView> (Resource.Id.lblListHeader);
			textBox.SetText (item, TextView.BufferType.Normal);

			return convertView;
		}
		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return true;
		}
		public override int GroupCount {
			get {
				return _dictGroup.Count;
			}
		}
		public override bool HasStableIds {
			get {
				return true;
			}
		}
		#endregion
	}
} 


