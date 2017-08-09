
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using WordPressRestApiStandard.Models;
using static Android.Webkit.WebSettings;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Newtonsoft.Json;

namespace shinyichen
{
    [Activity(Label = "PostActivity", ParentActivity = typeof(MainActivity))]
    public class PostActivity : FragmentActivity
    {
        //private WebView postWebView;

        // TODO slide post view

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Bundle bundle = Intent.GetBundleExtra("args");
            string posts_str = bundle.GetString("posts");
            int selected = bundle.GetInt("selected");
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(posts_str);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            //string content = bundle.GetString("content");

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PostActivityLayout);

            PostPagerAdapter adapter = new PostPagerAdapter(SupportFragmentManager, posts);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.postViewPager);
            viewPager.Adapter = adapter;
            viewPager.CurrentItem = selected;
        }
    }
}
