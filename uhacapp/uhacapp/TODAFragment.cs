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
using System.Net;

namespace uhacapp
{
    public class TODAFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View rootView= inflater.Inflate(Resource.Layout.BillsPay, container, false);
            Button btnSave = rootView.FindViewById<Button>(Resource.Id.btnSave);

            btnSave.Click += new EventHandler(btnSave_Click);
            return rootView;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            WebClient w = new WebClient();
            //w.up
        }
    }
}