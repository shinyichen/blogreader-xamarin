using System;
using System.Collections.Generic;
using System.Net;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using WordPressRestApiStandard;
using WordPressRestApiStandard.Models;
using WordPressRestApiStandard.QueryModel;

namespace shinyichen
{
    public class PostListAdapter : RecyclerView.Adapter
    {
        private WordPressApiClient wpClient;
        private List<Post> posts;
        private Android.Content.Context context;
        Picasso picasso;
        public event EventHandler<int> ItemClick;

        public PostListAdapter(List<Post> posts, WordPressApiClient wpClient, Android.Content.Context context)
        {
            this.posts = posts;
            this.wpClient = wpClient;
            this.context = context;
            picasso = Picasso.With(context);
            picasso.IndicatorsEnabled = true;
        }

        public override int ItemCount {
            get {
                return posts.Count;
            }
        }

        public override async void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PostViewHolder vh = holder as PostViewHolder;
            //Picasso.With(context).CancelRequest(vh.PostImageView);
            Post post = posts[position];
            vh.PostTitleView.Text = post.Title.Rendered;
            if (post.FeaturedMedia > 0)
            {
                Media media = await wpClient.GetMedia(new MediaQuery(), post.FeaturedMedia);
                string url = media.MediaDetails.Sizes.Medium.SourceUrl;
                Log.Info("adapter", url);

                // set image view
                //Picasso p = Picasso.With(context);
                picasso.Load(url).Into(vh.PostImageView);

                //Bitmap image = GetBitmapFromUrl(url);
                //vh.PostImageView.SetImageBitmap(image);
            } else {
                vh.PostImageView.SetImageDrawable(null);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PostCard, parent, false);
            PostViewHolder vh = new PostViewHolder(view, OnClick);
            return vh;
        }

		private Bitmap GetBitmapFromUrl(string url)
		{
			Bitmap image = null;

			WebClient webClient = new WebClient();
			var imageBytes = webClient.DownloadData(url);
			if (imageBytes != null && imageBytes.Length > 0)
			{
				Bitmap fullImage = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				int r = fullImage.Width / 200;
                if (r < 1)
                    return fullImage;
                else
                {
                    image = Bitmap.CreateScaledBitmap(fullImage, 200, fullImage.Height / r, true);
                    fullImage.Recycle();
                }

			}

			return image;
		}

        void OnClick(int position) {
            if (ItemClick != null) {
                ItemClick(this, position);
            }
        }
    }

    public class PostViewHolder : RecyclerView.ViewHolder 
    {
        public ImageView PostImageView { get; private set; }
        public TextView PostTitleView { get; private set; }

        public PostViewHolder(View view, Action<int> listener) : base(view)
        {
            PostImageView = view.FindViewById<ImageView>(Resource.Id.postImageView);
            PostTitleView = view.FindViewById<TextView>(Resource.Id.postTitleView);

            view.Click += (sender, e) => listener(base.Position);
        }
    }
}
