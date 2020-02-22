using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V7.Widget;
using Charadas_2._0.Adapter;
using Charadas_2._0.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static Android.Provider.SyncStateContract;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net;
using Android.Webkit;
using Newtonsoft.Json.Linq;
using System.Threading;
using Android.Widget;

namespace Charadas_2._0
{
    [Activity(Label = "CHARADAS",Icon = "@drawable/CHARADAS", Theme = "@style/splash", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        MyAdapter adapter;
        List<MyItem> itemList;


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.AppTheme);
            base.OnCreate(savedInstanceState);
            Thread.Sleep(2000);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            await InitDataAsync();
            InitView();
            SetData();

        }

        private void InitView()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
        }

       

        private void SetData()
        {
            adapter = new MyAdapter(this, itemList);

            recyclerView.HasFixedSize = true;
            GridLayoutManager layoutManager = new GridLayoutManager(this, Comun.NUM_OF_COLUM);
            layoutManager.Orientation = RecyclerView.Vertical;
            layoutManager.SetSpanSizeLookup(new MySpanSizeLookup(adapter));
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.AddItemDecoration(new SpacesItemDecoration(8));
            recyclerView.SetAdapter(adapter);
        }

      



        private async Task InitDataAsync()
        {
            itemList = new List<MyItem>();
            //Yo voy a enviar 7 por default 
            //aqui llamo la imagen, y aqui el titulo
            WSClient client = new WSClient();

           
            for (int i = 1; i <= 9; i++)
            {
                try
                {
                    var result = await client.Get<Categoria>("https://api-charadas.azurewebsites.net/api/categorias/" + i.ToString()).ConfigureAwait(false);

                    itemList.Add(new MyItem(Resource.Drawable.ANIMALS, result.Descripcion));

                }
                catch (System.Exception)
                {

                  
                }
              

            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private class MySpanSizeLookup : GridLayoutManager.SpanSizeLookup
        {
            private MyAdapter adapter;

            public MySpanSizeLookup(MyAdapter adapter)
            {
                this.adapter = adapter;
            }

            public override int GetSpanSize(int position)
            {
               switch(adapter.GetItemViewType(position))
                {
                    case 1: return 1;
                    case 0: return Comun.NUM_OF_COLUM;
                    default:return -1;
                }
            }
        }
    }

    public class WSClient
    {
        public async Task<T> Get<T>(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}