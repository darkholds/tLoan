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
    public class PaymentFragment : Fragment
    {
        EditText txtPayAmount;
        int id;

        public PaymentFragment() { }

        public static PaymentFragment newInstance(int idmember)
        {
            PaymentFragment f = new PaymentFragment();
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
            View rootView = inflater.Inflate(Resource.Layout.Payment, container, false);
            txtPayAmount = rootView.FindViewById<EditText>(Resource.Id.txtAmount);
            Button btnSave = rootView.FindViewById<Button>(Resource.Id.btnSave);

            btnSave.Click += new EventHandler(btnSave_Click);
            return rootView;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Member m = new Member().GetMemberById(id);

            bool result = new Payment().SavePayment(m.Id, Convert.ToDouble(txtPayAmount.Text));
            if (result)
            {
                this.Activity.RunOnUiThread(() =>
                {
                    TextToast(Convert.ToString("Payment successfully saved!"));
                });
            }
            else
            {
                this.Activity.RunOnUiThread(() =>
                {
                    TextToast(Convert.ToString("Payment failed!"));
                });
            }
        }

        public void TextToast(string textToDisplay)
        {
            Toast.MakeText(this.Activity, textToDisplay, ToastLength.Long).Show();
        }
    }
}