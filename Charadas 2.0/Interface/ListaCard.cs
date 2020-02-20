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

namespace Charadas_2._0.Interface
{
    public interface ListaCard
    {
        void OnListaCard(View view, int position);
    }
}