
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

namespace shinyichen
{
    [Activity(Label = "PostActivity", ParentActivity = typeof(MainActivity))]
    public class PostActivity : Activity
    {
        private WebView postWebView;

        // TODO slide post view

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Bundle bundle = Intent.GetBundleExtra("post");
            int id = bundle.GetInt("id");
            string title = bundle.GetString("title");
            ActionBar.Title = title;
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            string content = bundle.GetString("content");

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Post);
            postWebView = FindViewById<WebView>(Resource.Id.postAWebView);
            postWebView.Settings.JavaScriptEnabled = true;
            //postWebView.Settings.LoadWithOverviewMode = true;
            //postWebView.Settings.UseWideViewPort = true;
            postWebView.Settings.SetLayoutAlgorithm(LayoutAlgorithm.TextAutosizing);
            postWebView.LoadData(content, "text/html; charset=UTF-8", null);
        }
    }
}
