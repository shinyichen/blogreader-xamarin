﻿
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Webkit;
using static Android.Webkit.WebSettings;

namespace shinyichen
{
    public class PostFragment : Fragment
    {
        private WebView postWebView;

        public PostFragment() {
            
        }

        public static PostFragment newInstance(int id, string title, string content)
        {
            PostFragment f = new PostFragment();
            Bundle bundle = new Bundle();
            bundle.PutInt(Constants.ARG_ID, id);
            bundle.PutString(Constants.ARG_TITLE, title);
            bundle.PutString(Constants.ARG_CONTENT, content);
            f.Arguments = bundle;
            return f;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            string id = Arguments.GetString(Constants.ARG_ID);
            string title = Arguments.GetString(Constants.ARG_TITLE);
            string content = Arguments.GetString(Constants.ARG_CONTENT);
            View view = inflater.Inflate(Resource.Layout.Post, null); // NOTE: using container as second arguement will get error
			postWebView = view.FindViewById<WebView>(Resource.Id.postAWebView);
			postWebView.Settings.JavaScriptEnabled = true;
			postWebView.Settings.LoadWithOverviewMode = true;
			postWebView.Settings.UseWideViewPort = true;
            postWebView.Settings.SetLayoutAlgorithm(LayoutAlgorithm.TextAutosizing);
			postWebView.LoadData(content, "text/html; charset=UTF-8", null);
            return view;
        }
    }
}
