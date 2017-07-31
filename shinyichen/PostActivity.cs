
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

namespace shinyichen
{
    [Activity(Label = "PostActivity")]
    public class PostActivity : Activity
    {
        
        private TextView postTitleView;
        private WebView postWebView;

        // TODO slide post view

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Bundle bundle = Intent.GetBundleExtra("post");
            int id = bundle.GetInt("id");
            string title = bundle.GetString("title");
            string content = bundle.GetString("content");

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Post);
            postTitleView = FindViewById<TextView>(Resource.Id.postATitleView);
            postTitleView.Text = title;
            postWebView = FindViewById<WebView>(Resource.Id.postAWebView);
            postWebView.LoadData(content, "text/html; charset=UTF-8 ", null);
        }
    }
}
