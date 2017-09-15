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
    public class LoanFragment : Fragment
    {
        EditText txtAmount;
        EditText txtColateral;
        EditText txtComaker;
        EditText txtComAddress;
        EditText txtComContact;

        int id;

        public LoanFragment() { }
        public static LoanFragment newInstance(int idmember)
        {
            LoanFragment f = new LoanFragment();
            Bundle args = new Bundle();
            args.PutInt("idmember", idmember);
            f.Arguments = args;
            return f;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            id = Arguments.GetInt("idmember");
            // Create your fragment here
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View rootView = inflater.Inflate(Resource.Layout.Loan, container, false);
            txtAmount = rootView.FindViewById<EditText>(Resource.Id.txtAmount);
            txtColateral = rootView.FindViewById<EditText>(Resource.Id.txtCol);
            txtComaker = rootView.FindViewById<EditText>(Resource.Id.txtComaker);
            txtComContact = rootView.FindViewById<EditText>(Resource.Id.txtComContact);
            txtComAddress= rootView.FindViewById<EditText>(Resource.Id.txtComAddress);
            Button btnSave = rootView.FindViewById<Button>(Resource.Id.btnSave);
            Button btnBack = rootView.FindViewById<Button>(Resource.Id.btnBackLoan);

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
            Member m = new Member().GetMemberById(id);

            bool result = new LoanApplication().SaveLoanApplication(m.Id, Convert.ToDouble(txtAmount.Text), txtColateral.Text, txtComaker.Text, txtComAddress.Text, txtComContact.Text);
            if (result)
            {
                this.Activity.RunOnUiThread(() =>
                {
                    TextToast(Convert.ToString("Loan successfully added!"));
                });
            }
            else
            {
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