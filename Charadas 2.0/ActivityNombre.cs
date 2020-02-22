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
    [Activity(Label = "ActivityNombre")]
    public class ActivityNombre : Activity
    {
        public TextView x;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Nombre);
            x = FindViewById<TextView>(Resource.Id.textView1);

            await amen();
            // Create your application here

            x.Click += async delegate
            {
                await amen();
            };
        }

        public async Task amen()
        {
            WSClient client2 = new WSClient();
            Random random = new Random();           
            var i = random.Next(1, 266);
            var result = await client2.Get<Nombres>("https://api-charadas.azurewebsites.net/api/Nombres/" + i.ToString()).ConfigureAwait(false);

            x.Text = result.Nombre1;

        }
    }
}