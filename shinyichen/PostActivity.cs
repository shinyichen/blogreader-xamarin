
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using WordPressRestApiStandard.Models;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Newtonsoft.Json;

namespace shinyichen
{
    [Activity(Label = "PostActivity", ParentActivity = typeof(MainActivity))]
    public class PostActivity : FragmentActivity
    {
        // TODO slide post view

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Bundle bundle = Intent.GetBundleExtra(Constants.ARG_ARGUMENTS);
            string posts_str = bundle.GetString(Constants.ARG_POSTS);
            int selected = bundle.GetInt(Constants.ARG_SELECTED);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(posts_str);

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PostActivityLayout);

            PostPagerAdapter adapter = new PostPagerAdapter(SupportFragmentManager, posts);

            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.postViewPager);
            viewPager.Adapter = adapter;
            viewPager.CurrentItem = selected;
        }
    }
}
