using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Charadas_2._0
{
    internal class Nombres
    {

        public int Id_Nombre { get; set; }
        public string Nombre1 { get; set; }
        public int id_categoria { get; set; }

        public static implicit operator Nombres(Task<Nombres> v)
        {
            throw new NotImplementedException();
        }
    }
}