﻿using Android.App;
using Android.OS;
using System.Collections.Generic;

using WordPressRestApiStandard;
using WordPressRestApiStandard.Models;
using Android.Support.V7.Widget;
using Android.Views;

namespace shinyichen
{
    [Activity(Label = "android", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
		private string url = "http://shinyichen.com/blog/wp-json/wp/v2/";
		const string username = "android";
		const string password = "hjTxS2W5%CRD(SusyrhigD1e";
        private List<Post> posts;

        private RecyclerView postListView;
        private RecyclerView.LayoutManager layoutManager;
        private PostListAdapter postListAdapter;


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // load posts
			WordPressApiClient client = new WordPressApiClient(url);

			posts = await client.GetPosts(new WordPressRestApiStandard.QueryModel.PostsQuery() { PerPage = 8 });

            postListView = FindViewById<RecyclerView>(Resource.Id.postListView);
            layoutManager = new LinearLayoutManager(this);
            postListView.SetLayoutManager(layoutManager);

            postListAdapter = new PostListAdapter(posts, client, this.BaseContext);
            postListView.SetAdapter(postListAdapter);

            // improve the image flicking issues
            postListView.HasFixedSize = true;
            postListView.SetItemViewCacheSize(10);
            postListView.DrawingCacheEnabled = true;
            postListView.DrawingCacheQuality = DrawingCacheQuality.High;

        }

    }
}

