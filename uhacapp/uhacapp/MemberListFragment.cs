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
    public class MemberListFragment : ListFragment
    {
        List<TableItem> listItem = new List<TableItem>();
        string sourcebtn;

        public MemberListFragment() { }

        public static MemberListFragment newInstance(string src)
        {
            MemberListFragment f = new MemberListFragment();
            Bundle args = new Bundle();
            args.PutString("source", src);
            f.Arguments = args;
            return f;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            sourcebtn = Arguments.GetString("source");
           
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View rootView= inflater.Inflate(Resource.Layout.Member, container, false);
            return rootView;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            Dictionary<int, string> members = new Member().GetAllMember();
            members.ToList();
            foreach(KeyValuePair<int, string> s in members)
            {
                listItem.Add(new TableItem(s.Key,s.Value));
            }
           
            ListAdapter = new MemberScreenAdapter(Activity, listItem);
            ListView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            Bundle bundle = new Bundle();
            FragmentTransaction fragTrans;
            fragTrans = FragmentManager.BeginTransaction();
            bundle.PutInt("idmember", listItem[e.Position].Id);
            if (sourcebtn.Equals("Apply"))
            {                        
                LoanFragment loan = new LoanFragment();
                loan.Arguments = bundle;
                fragTrans.Replace(Resource.Id.fragment_container, loan, "");
            }
            else if (sourcebtn.Equals("Pay"))
            {
                PaymentFragment pay = new PaymentFragment();
                pay.Arguments = bundle;
                fragTrans.Replace(Resource.Id.fragment_container, pay, "");
            }
       
            fragTrans.AddToBackStack(null);
            fragTrans.Commit();
        }

        public class MemberScreenAdapter : BaseAdapter<TableItem>
        {
            List<TableItem> items;
            Activity context;
            public MemberScreenAdapter(Activity context, List<TableItem> items) : base()
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
                    view = context.LayoutInflater.Inflate(Resource.Layout.Member_row, null);
                view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Heading;
                return view;
            }
        }

        public class TableItem
        {
            public string Heading { get; set; }
            public int Id { get; set; }

            public TableItem(int Id, string heading)
            {
                this.Id = Id;
                Heading = heading;
            }
        }
    }
}