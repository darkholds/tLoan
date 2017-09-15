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
    public class LoanListFragment : ListFragment
    {
        List<TableItem> listItem = new List<TableItem>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View rootView = inflater.Inflate(Resource.Layout.LoanList, container, false);

            return rootView;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            List<LoanApplication> loans = new LoanApplication().GetAllLoan();
            foreach (LoanApplication l in loans)
            {
                listItem.Add(new TableItem(l.Id,l.MemberInfo.LastName + ", " + l.MemberInfo.FirstName, "Amount: " + l.Amount + ", Balance: " + l.Balance,"Date of Loan: " + l.DateApproved));
            }

            ListAdapter = new LoanScreenAdapter(Activity, listItem);
            ListView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        public class LoanScreenAdapter : BaseAdapter<TableItem>
        {
            List<TableItem> items;
            Activity context;
            public LoanScreenAdapter(Activity context, List<TableItem> items) : base()
            {
                this.context = context;
                this.items = items;
            }
            public override long GetItemId(int position)
            {
                return position;
            }
            public override TableItem this[int position]
            {
                get { return items[position]; }
            }
            public override int Count
            {
                get { return items.Count; }
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var item = items[position];
                View view = convertView;
                if (view == null) // no view to re-use, create new
                    view = context.LayoutInflater.Inflate(Resource.Layout.Loanlist_row, null);
                view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Heading;
                view.FindViewById<TextView>(Resource.Id.Text2).Text = item.SubHead1;
                view.FindViewById<TextView>(Resource.Id.Text3).Text = item.SubHead2;

                return view;
            }
        }

        public class TableItem
        {
            public string Heading { get; set; }
            public int Id { get; set; }
            public string SubHead1 { get; set; }
            public string SubHead2 { get; set; }

            public TableItem(int Id, string heading, string subhead1, string subhead2)
            {
                this.Id = Id;
                Heading = heading;
                SubHead1 = subhead1;
                SubHead2 = subhead2;
            }
        }
    }
}