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
using uhack;

namespace uhacapp
{
    public class NewMemberFragment : Fragment
    {
        EditText txtLName;
        EditText txtFName;
        EditText txtMName;
        EditText txtDOB;
        EditText txtSex;
        EditText txtCivilStatus;
        EditText txtContactNo;
        EditText txtAddress;
        View rootView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            rootView = inflater.Inflate(Resource.Layout.NewMember, container, false);
            txtLName = rootView.FindViewById<EditText>(Resource.Id.txtLN);
            txtFName = rootView.FindViewById<EditText>(Resource.Id.txtFN);
            txtMName = rootView.FindViewById<EditText>(Resource.Id.txtMN);
            txtDOB = rootView.FindViewById<EditText>(Resource.Id.txtDOB);
            txtSex = rootView.FindViewById<EditText>(Resource.Id.txtSex);
            txtCivilStatus = rootView.FindViewById<EditText>(Resource.Id.txtCS);
            txtContactNo = rootView.FindViewById<EditText>(Resource.Id.txtCN);
            txtAddress = rootView.FindViewById<EditText>(Resource.Id.txtAd);
            Button btnSave = rootView.FindViewById<Button>(Resource.Id.btnSaveNewMember);
            Button btnBack = rootView.FindViewById<Button>(Resource.Id.btnBackNewMember);

            btnSave.Click += new EventHandler(btnSave_Click);
            btnBack.Click += new EventHandler(btnBack_Click);
            return rootView;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            fragTrans.Replace(Resource.Id.fragment_container, home, "Home");
            fragTrans.Commit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Member member = new Member();
            bool result = member.SaveMember(txtFName.Text, txtLName.Text, txtMName.Text, txtDOB.Text, txtSex.Text, txtAddress.Text, txtContactNo.Text, txtCivilStatus.Text);
            //member.SaveMember(txtFName.Text, txtLName.Text, txtMName.Text, txtDOB.Text, txtSex.Text, txtAddress.Text, txtContactNo.Text, txtCivilStatus.Text);
            if (result)
            {
                this.Activity.RunOnUiThread(() =>
                {
                    TextToast(Convert.ToString("Member is added successfully!"));
                });
            }
            else {
                this.Activity.RunOnUiThread(() =>
                {
                    TextToast(Convert.ToString("Add failed!"));
                });
            }      
        }
        public void TextToast(string textToDisplay)
        {
            Toast.MakeText(this.Activity, textToDisplay, ToastLength.Long).Show();
        }
    }
}