using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace uhacapp
{
    public class LoadFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.Splash, container, false);
            rootView.Click += new EventHandler(form_Click);
            return rootView;      
        }

        private void form_Click(object sender, EventArgs e)
        {
            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            fragTrans.Replace(Resource.Id.fragment_container, home, "Home");
            fragTrans.Commit();
        }
    }
}