using System;
using System.Collections.Generic;
using Android.Support.V4.App;
using WordPressRestApiStandard;
using WordPressRestApiStandard.Models;

namespace shinyichen
{
    public class PostPagerAdapter : FragmentPagerAdapter
    {

        List<Post> posts;

        public PostPagerAdapter(FragmentManager fm, List<Post> posts) : base(fm)
        {
            this.posts = posts;
        }

        public override int Count {
            get {
                return posts.Count;
            }
        }

        public override Fragment GetItem(int position)
        {
            Post post = posts[position];
            return PostFragment.newInstance(post.Id, post.Title.Rendered, post.Content.Rendered);
        }
    }
}
