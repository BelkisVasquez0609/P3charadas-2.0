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
using Android.Support.V7.Widget;
using Android.Graphics;

namespace Charadas_2._0
{
    public class SpacesItemDecoration : RecyclerView.ItemDecoration
    {
        int space;
        public SpacesItemDecoration(int space)
        {
            this.space = space;
        }
        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            base.GetItemOffsets(outRect, view, parent, state);
            outRect.Top = outRect.Bottom = outRect.Left = outRect.Right = space;
        }
    }
}
