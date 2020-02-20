using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using Charadas_2._0.Adapter;
using Charadas_2._0.Models;
using System.Collections.Generic;
using System;

namespace Charadas_2._0
{
    [Activity(Label = "CHARADAS",Icon = "@drawable/CHARADAS", Theme = "@style/splash", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        MyAdapter adapter;
        List<MyItem> itemList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.AppTheme);
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitData();
            InitView();
            SetData();

        }

        private void InitView()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
        }

        private T FindViewById<T>(object recycler_view)
        {
            throw new NotImplementedException();
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

       

      

        private void InitData()
        {
            itemList = new List<MyItem>();
            //Yo voy a enviar 7 por default 
                                                     //aqui llamo la imagen, y aqui el titulo
            itemList.Add(new MyItem(Resource.Drawable.me_time, "Politica"));
            itemList.Add(new MyItem(Resource.Drawable.family_time, "Adivinanzas"));
            itemList.Add(new MyItem(Resource.Drawable.lovely_time , "Deportes"));
            itemList.Add(new MyItem(Resource.Drawable.team_time, "Acciones"));
            itemList.Add(new MyItem(Resource.Drawable.friends, "Animales"));
            itemList.Add(new MyItem(Resource.Drawable.calendar, "Objetos"));
            itemList.Add(new MyItem(Resource.Drawable.calendar, "Actores"));

            //Aqui vamos a tener 3 lineas de 2 filas (2*3) y 1 tamano entero
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
}