using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Charadas_2._0.Models
{
    public class MyItem
    {
        public Bitmap imagen { get; set; }
        public int Icon { get; set; }
        public String Descripcion { get; set; }
        public int Gidcategoria { get; set; }

        public MyItem (int Icon, String Descripcion)
        {
            this.Icon = Icon;
            this.Descripcion = Descripcion;


        }

        public MyItem(Bitmap imagen, string descripcion,int idcategoria)
        {
            this.imagen = imagen;
            Descripcion = descripcion;
            Gidcategoria = idcategoria;
        }
    }
}