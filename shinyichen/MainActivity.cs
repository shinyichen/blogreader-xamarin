﻿using Android.App;
using Android.OS;
using System.Collections.Generic;

using WordPressRestApiStandard;
using WordPressRestApiStandard.Models;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Content;

using Newtonsoft.Json;

namespace shinyichen
{
    [Activity(Label = "android", MainLauncher = true, Icon = "@mipmap/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
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
            ActionBar.SetTitle(Resource.String.app_name);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // load posts
            WordPressApiClient client = new WordPressApiClient(url);

            if (posts == null || posts.Count == 0) { 
	            if (savedInstanceState != null && savedInstanceState.GetString("posts") != null)
	                posts = JsonConvert.DeserializeObject<List<Post>>(savedInstanceState.GetString("posts"));
	            else
	                posts = await client.GetPosts(new WordPressRestApiStandard.QueryModel.PostsQuery() { PerPage = 8 });
            }
            postListView = FindViewById<RecyclerView>(Resource.Id.postListView);
            layoutManager = new LinearLayoutManager(this);
            postListView.SetLayoutManager(layoutManager);

            postListAdapter = new PostListAdapter(posts, client, this.BaseContext);
            postListAdapter.ItemClick += ItemClickCallback;
            postListView.SetAdapter(postListAdapter);

            // improve the image flicking issues
            postListView.HasFixedSize = true;
            postListView.SetItemViewCacheSize(10);
            postListView.DrawingCacheEnabled = true;
            postListView.DrawingCacheQuality = DrawingCacheQuality.High;

            // TODO load more
            // TODO refresh

        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            string posts_str = JsonConvert.SerializeObject(posts);
            outState.PutString("posts", posts_str);
            base.OnSaveInstanceState(outState);
        }

        private void ItemClickCallback(object sender, int pos) {
            Post post = posts[pos];
            Bundle bundle = new Bundle();
            bundle.PutInt("id", post.Id);
            bundle.PutString("title", post.Title.Rendered);
            bundle.PutString("content", post.Content.Rendered);
            Intent intent = new Intent(this, typeof(PostActivity) );
            intent.PutExtra("post", bundle);
            StartActivity(intent);
        }

    }
}

