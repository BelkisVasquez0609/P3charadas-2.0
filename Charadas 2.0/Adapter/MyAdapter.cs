using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Charadas_2._0.Interface;
using Charadas_2._0.Models;
using Android.Graphics;


namespace Charadas_2._0.Adapter
{
    public class MyAdapter : RecyclerView.Adapter
    {
        Context context;
        List<MyItem> itemList;

        public override int GetItemViewType(int position)
        {

            if (itemList.Count == 1) { return 0; }//si solo hay un item que lo despliegue 
                                                  //como si fuera una columna
            else
            {
                if (itemList.Count % Comun.NUM_OF_COLUM == 0)//si el tamano del item se puede dividir por el numero de columna, asignalo a un numero de columna
                    return 1;
                else

                    return (position > 1 && position == itemList.Count - 1) ? 0 : 1;
                // Si la posicion es la ultima, que lo ponga del tamano de la pantalla
            }
        }
        public MyAdapter(Context context, List<MyItem> itemList)
        {
            this.context = context;
            this.itemList = itemList;

        }
        public override int ItemCount => itemList.Count();
        public String[] cColors = { "#B1B1B1", "#7CAE0F", "#EDAE23", "#029DBC", "#47464C", "#CA0B10", "#E24F60", "#702254", "#EC4525" };
       // public String[] nColors = { "#B1B1B1", "#7CAE0F", "#EDAE23", "#029DBC", "#47464C", "#CA0B10", "#E24F60", "#702254", "#EC4525" };
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            MyViewHolder myViewHolder = holder as MyViewHolder;

            myViewHolder.BackgroundItem.SetBackgroundColor(Color.ParseColor(cColors[position % cColors.Length]));

           // myViewHolder.BackgroundNombre.SetBackgroundColor(Color.ParseColor(nColors[position%nColors.Length]));

            myViewHolder.img_icon.SetImageResource(itemList[position].Icon);
            myViewHolder.txt_description.Text = itemList[position].Descripcion;
            myViewHolder.SetOnClick(new Categoria(context, itemList[position]));
            myViewHolder.SetOnClickListeners();



        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.layout_Categorias, parent, false);
            return new MyViewHolder(itemView);
        }
        //AQUI E
       

    }
        public class MyViewHolder : RecyclerView.ViewHolder,View.IOnClickListener
        {
            public TextView txt_description;
            public ImageView img_icon;
            ListaCard listener;
            public LinearLayout BackgroundItem;
            public Button BotonJugar1;
            public  Context context;
            public LinearLayout BackgroundNombre;
        public void SetOnClick(ListaCard listaCard)
            {
                this.listener = listaCard;

            }
            public MyViewHolder(View itemView) : base(itemView)
            {
                txt_description = itemView.FindViewById<TextView>(Resource.Id.txt_description);
                img_icon = itemView.FindViewById<ImageView>(Resource.Id.img_icon);
                itemView.SetOnClickListener(this);
                BotonJugar1 = (Button)itemView.FindViewById(Resource.Id.BotonJugar);
                BackgroundItem = (LinearLayout)itemView.FindViewById(Resource.Id.LinearLayautCategorias);
              //  BackgroundNombre = (LinearLayout)itemView.FindViewById(Resource.Id.LinearLayautBackgroundNombre);

            context = itemView.Context;

                BotonJugar1.Click += delegate
                {
                    OnClick(itemView);
                   
                };

            }

           public  void SetOnClickListeners()
            {
                BotonJugar1.SetOnClickListener(this);
               

            }
        
        public void OnClick(View v)
            {
                listener.OnListaCard(v, AdapterPosition);
            // var m_activity = Intent(this, typeof(Nombre));
            var NxtAct = new Intent(Application.Context, typeof(ActivityNombre));
            context.StartActivity(NxtAct);



        }
       

    }

        public class Categoria : ListaCard
        {
            private Context context;
            private MyItem myItem;

            public Categoria(Context context, MyItem myItem)
            {
                this.context = context;
                this.myItem = myItem;
            }

            public void OnListaCard(View view, int position)
            {
                Toast.MakeText(context, "Clicked: " + myItem.Descripcion, ToastLength.Short).Show();
            }
        }
    
}