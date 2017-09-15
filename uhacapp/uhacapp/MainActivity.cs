using Android.App;
using Android.Widget;
using Android.OS;

namespace uhacapp
{
    [Activity(Label = "T-Loan", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            //if (bundle == null)
            //{
            //ISharedPreferences credential = PreferenceManager.GetDefaultSharedPreferences(this);
            //auth.uid = credential.GetString("sabsabuid", string.Empty);
            // auth.password = credential.GetString("sabsabpassword", string.Empty);

            //Bundle b = new Bundle();
            //b.PutString("uid", auth.uid);
            //b.PutString("pass", auth.password);
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();
            FragmentTransaction fragTrans;
            fragTrans = this.FragmentManager.BeginTransaction();
            LoadFragment splash = new LoadFragment();
            //login.Arguments = b;
            fragTrans.Add(Resource.Id.fragment_container, splash, "");
            fragTrans.Commit();
            //}
        }

        public void TextToast(string textToDisplay)
        {
            Toast.MakeText(this, textToDisplay, ToastLength.Long).Show();
        }
    }

    
}

