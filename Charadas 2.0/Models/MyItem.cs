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

namespace Charadas_2._0.Models
{
    public class MyItem
    {
        public int Icon { get; set; }
        public String Descripcion { get; set; }

        public MyItem (int Icon, String Descripcion)
        {
            this.Icon = Icon;
            this.Descripcion = Descripcion;


        }

    }
}