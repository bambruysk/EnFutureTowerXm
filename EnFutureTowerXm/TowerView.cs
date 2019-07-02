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

namespace EnFutureTowerXm
{
    class TowerView
    {
        public ImageView imageView;

        public TowerView ( ImageView view)
        {
            imageView = view;
            imageView.SetImageResource(Resource.Mipmap.test);
        }

        internal void OnHPChanged(object sender, TowerHPChangeEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}