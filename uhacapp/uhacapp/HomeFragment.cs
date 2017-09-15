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
    public class HomeFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View rootView = inflater.Inflate(Resource.Layout.Home, container, false);
            Button btnNew = rootView.FindViewById<Button>(Resource.Id.btnNew);
            Button btnApplication = rootView.FindViewById<Button>(Resource.Id.btnLoan);
            Button btnPayment = rootView.FindViewById<Button>(Resource.Id.btnPayment);
            Button btnTODAPayment = rootView.FindViewById<Button>(Resource.Id.btnPaymentBills);
            Button btnLoans=rootView.FindViewById<Button>(Resource.Id.btnLoans);


            btnNew.Click += new EventHandler(btnNew_Click);
            btnApplication.Click += new EventHandler(btnApplication_Click);
            btnPayment.Click += new EventHandler(btnPayment_Click);
            btnTODAPayment.Click += new EventHandler(btnTODAPayment_Click);
            btnLoans.Click += new EventHandler(btnLoans_Click);
            return rootView;
        }

        private void btnLoans_Click(object sender, EventArgs e)
        {
            //Bundle bundle = new Bundle();
            //bundle.PutString("source", "Pay");

            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            LoanListFragment loans = new LoanListFragment();
            //payment.Arguments = bundle;
            fragTrans.Replace(Resource.Id.fragment_container, loans, "Loans");
            fragTrans.AddToBackStack(null);
            fragTrans.Commit();
        }

        private void btnTODAPayment_Click(object sender, EventArgs e)
        {
            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            TODAFragment toda = new TODAFragment();
            fragTrans.Replace(Resource.Id.fragment_container, toda, "Bill Payment");
            fragTrans.AddToBackStack(null);
            fragTrans.Commit();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("source", "Pay");

            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            MemberListFragment payment = new MemberListFragment();
            payment.Arguments = bundle;
            fragTrans.Replace(Resource.Id.fragment_container, payment, "Loan Payment");
            fragTrans.AddToBackStack(null);
            fragTrans.Commit();
        }

        private void btnApplication_Click(object sender, EventArgs e)
        {
            Bundle bundle = new Bundle();
            bundle.PutString("source", "Apply");

            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            MemberListFragment loanapplication = new MemberListFragment();
            loanapplication.Arguments = bundle;
            fragTrans.Replace(Resource.Id.fragment_container, loanapplication, "Member List");
            fragTrans.AddToBackStack(null);
            fragTrans.Commit();      
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            NewMemberFragment newmember = new NewMemberFragment();
            fragTrans.Replace(Resource.Id.fragment_container, newmember, "New Member");
            fragTrans.Commit();
        }
    }
}