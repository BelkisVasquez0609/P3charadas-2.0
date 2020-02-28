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
using System.Timers;
using static Android.Provider.SyncStateContract;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net;
using Android.Webkit;
using Newtonsoft.Json.Linq;
using System.Threading;
using Android.Widget;
using Timer = System.Timers.Timer;
using System;
using System.IO;
using System.Drawing;
using Android.Graphics;
using Xamarin.Essentials;

namespace Charadas_2._0
{
    [Activity(Label = "CHARADAS",Icon = "@drawable/CHARADAS", Theme = "@style/splash", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        MyAdapter adapter;
        List<MyItem> itemList;
        
        public Android.App.AlertDialog Alerta;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.AppTheme);
            
            base.OnCreate(savedInstanceState);
            Thread.Sleep(2000);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            await SpeakNowDefaultSettings();
            await InitDataAsync();

            InitView();
            SetData();
            Alerta = new Android.App.AlertDialog.Builder(this).Create();

            //  var x = new Gyroscopecharadas(this);
            //x.ToggleGyroscope();
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
        }

        public async Task SpeakNowDefaultSettings()
        {
            await TextToSpeech.SpeakAsync("Bienvenidos a Charadas 2.0, Profesor Willis Polanco");

            // This method will block until utterance finishes.
        }
        public byte[] ImageToByteArray(System.Drawing.Image imagen)
        {
            System.IO.MemoryStream ms = new MemoryStream();
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream imagen = new MemoryStream(byteArrayIn);
            return Image.FromStream(imagen);
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

        Categoria cat;
        private async Task InitDataAsync()
        {
            itemList = new List<MyItem>();
        
            //aqui llamo la imagen, y aqui el titulo
            WSClient client = new WSClient();

           
            for (int i = 1; i <= 9; i++)
            {
                try
                {
                    var result = await client.Get<Categoria>("https://api-charadas.azurewebsites.net/api/categorias/" + i.ToString()).ConfigureAwait(false);

                    cat = result;


                    var img = BitmapFactory.DecodeByteArray(cat.imagen, 0, cat.imagen.Length); 
                    
           

                    itemList.Add(new MyItem(img, cat.Descripcion,cat.id));//Resource.Drawable.ANIMALS
                   
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