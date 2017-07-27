using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using WordPressRestApiStandard;
using WordPressRestApiStandard.Models;
using WordPressRestApiStandard.QueryModel;

namespace shinyichen
{
    public class PostListAdapter : RecyclerView.Adapter
    {
        private WordPressApiClient wpClient;
        private List<Post> posts;

        public PostListAdapter(List<Post> posts, WordPressApiClient wpClient)
        {
            this.posts = posts;
            this.wpClient = wpClient;
        }

        public override int ItemCount {
            get {
                return posts.Count;
            }
        }

        public override async void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PostViewHolder vh = holder as PostViewHolder;
            Post post = posts[position];
            vh.PostTitleView.Text = post.Title.Rendered;
            if (post.FeaturedMedia > 0)
            {
                Media media = await wpClient.GetMedia(new MediaQuery(), post.FeaturedMedia);
                string url = media.MediaDetails.Sizes.Thumbnail.SourceUrl;
                Log.Info("adapter", url);
                // TODO set image view
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PostCard, parent, false);
            PostViewHolder vh = new PostViewHolder(view);
            return vh;
        }
    }

    public class PostViewHolder : RecyclerView.ViewHolder 
    {
        public ImageView PostImageView { get; private set; }
        public TextView PostTitleView { get; private set; }

        public PostViewHolder(View view) : base(view)
        {
            PostImageView = view.FindViewById<ImageView>(Resource.Id.postImageView);
            PostTitleView = view.FindViewById<TextView>(Resource.Id.postTitleView);
        }
    }
}
