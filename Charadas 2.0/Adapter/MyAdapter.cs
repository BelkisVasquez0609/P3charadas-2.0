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
          
            if (itemList.Count== 1) { return 0; }//si solo hay un item que lo despliegue 
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
        public MyAdapter (Context context , List<MyItem> itemList)
        {
            this.context = context;
            this.itemList = itemList;

        }
        public override int ItemCount => itemList.Count();
        public String[] mColors = { "#3F51B5", "#FF9800", "#009688", "#673AB7","#3F51B5", "#FF9800", "#009688","#009688", "#3F51B5" };
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            //arreglar esto, el color 
            
            holder.ItemView.SetBackgroundColor(Color.ParseColor(mColors[position % 9])); // 4 can be replaced by mColors.length
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.img_icon.SetImageResource(itemList[position].Icon);
            myViewHolder.txt_description.Text= itemList[position].Descripcion;
            myViewHolder.SetOnClick(new Categoria(context, itemList[position]));

            

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            
            View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.layout_Categorias, parent, false);
            return new MyViewHolder(itemView);
        }

        private class MyViewHolder : RecyclerView.ViewHolder,View.IOnClickListener
        {
            public TextView txt_description;
            public ImageView img_icon;
            ListaCard listener;
            

            public void SetOnClick (ListaCard listaCard)
            {
                this.listener = listener;
            }
            public MyViewHolder(View itemView) : base(itemView)
            {
                txt_description = itemView.FindViewById<TextView>(Resource.Id.txt_description);
                img_icon = itemView.FindViewById<ImageView>(Resource.Id.img_icon);
                itemView.SetOnClickListener(this);
            }

            public void OnClick(View v)
            {
                listener.OnListaCard(v, AdapterPosition);
            }
        }

        private class Categoria : ListaCard
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
}