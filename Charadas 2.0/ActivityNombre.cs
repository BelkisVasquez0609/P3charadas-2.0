using System;
using System.Collections.Generic;

using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
//using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using Timer = System.Timers.Timer;
using Android.Support.V7.Widget;
using Charadas_2._0.Interface;
using Charadas_2._0.Models;
using Android.Graphics;


namespace Charadas_2._0
{
    [Activity(Label = "ActivityNombre", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
   
    public class ActivityNombre : Activity
    {

        public TextView x;
        private Button Siguente;
        private Button CambiarCat;
        private TextView TxtCountDown;
        private LinearLayout Layout;
        private int Count = 60;
        Timer timer;
        public Context context;
        Vector3 vector = new Vector3();
       
        static Random rand = new Random();

        int bueno;
        int malo;
     
        public AlertDialog Alerta;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Nombre);
            x = FindViewById<TextView>(Resource.Id.textView1);
            Layout = FindViewById<LinearLayout>(Resource.Id.linearlayaut);

            CambiarCat = FindViewById<Button>(Resource.Id.CambiarCat);
            CambiarCat.Visibility = ViewStates.Invisible;
            Siguente = FindViewById<Button>(Resource.Id.siguente);
            Siguente.Visibility = ViewStates.Invisible;

            TxtCountDown = FindViewById<TextView>(Resource.Id.txtCountDown);

            Thread.Sleep(2000);
            await Amen();
            Alerta = new AlertDialog.Builder(this).Create();
         
            if (Accelerometer.IsMonitoring)
                return;
            Accelerometer.ReadingChanged += Gyroscope_ReadingChanged;
            Accelerometer.Start(SensorSpeed.Game);


            //x.Click += async delegate
            //{
            //    await amen();
            //};
            //View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.activity_main, null, false);

            //    context = itemView.Context;
            CambiarCat.Click += delegate
            {
                base.OnCreate(savedInstanceState);
                //     (Application.Current).MainPage = new NavigationPage(new MainPage());
                //    await Android.Content.Res.Navigation.PopAsync();
                //View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.activity_main, null, false);

                //context = itemView.Context;
                //Alerta.SetMessage("CAMBIAR");
                //Alerta.Show();
                //SetContentView(Resource.Layout.activity_main);

                //var NxtAct = new Intent(Application.Context, typeof(MainActivity));
                //context.StartActivity(NxtAct);
                //Alerta.Cancel();
            };
            Siguente.Click += delegate
            {
                reinicio();
            };
        }

        private void reinicio()
        {
            Alerta.SetMessage("SIGUIENTE");
            Alerta.Show();
            Accelerometer.Start(SensorSpeed.Game);
            Count = 60;
            bueno = 0;
            malo = 0;
            CambiarCat.Visibility = ViewStates.Invisible;
            OnResume();
            // await Amen();
        }

        void Gyroscope_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            vector.X = e.Reading.Acceleration.X;
            vector.Y = e.Reading.Acceleration.Y;
            vector.Z = e.Reading.Acceleration.Z;

            GetVector();


        }

        public void GetVector()
        {
            if (vector.Z > 0.8)
            {
                Layout.SetBackgroundColor(Color.Red);
                x.Text = "¡Incorrecto!";
                malo++;

                //Alerta.SetMessage("Correcto");
                //Alerta.Show();



            }
            else if (vector.Z <= -0.8)
            {

                x.Text = "¡Correcto!";
                bueno++;

                Layout.SetBackgroundColor(Color.Green);

                //await Amen();
            }
            else if (vector.Z >= -0.7 && vector.Z <= 0.7)
            {

                x.Text = "Derecho";
                //await Amen();
                Layout.SetBackgroundColor(Color.DarkSlateBlue);
            }




        }
        protected override void OnResume()
        {
            base.OnResume();
            timer = new Timer();
            timer.Interval = 1000; // 1 segundo
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Siguente.Visibility = ViewStates.Invisible;
           // CambiarCat.Visibility = ViewStates.Invisible;
       
            if (Count <= 60 && Count >= 1)//los 60 segundos
            {
             
                Count--;
                
                RunOnUiThread(() =>
                {
                    TxtCountDown.Text = ""  + Count; //se actualiza el valor

                });
            }
            else
            {
               
                //View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.activity_main,null , false);
                TxtCountDown.Text = "¡¡¡SE ACABO EL TIEMPO!!!";
                
                Accelerometer.Stop();
                x.Text = "Buenos: " + bueno.ToString() + " - Malo: " + malo.ToString(); ;
                CambiarCat.Visibility = ViewStates.Visible;
                Siguente.Visibility = ViewStates.Visible;
               
                
                Alerta.Show();
                timer.Stop();
                

                //DialogoCategoria();
                //Alerta.SetMessage("Correcto");
                //Alerta.Show();


                //Thread.Sleep(5000);
                //context = itemView.Context;
                ////SetContentView(Resource.Layout.activity_main);


            }
            
        }
        
        public void DialogoCategoria()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Categoria");
            alert.SetMessage("Quieres seguir jugando en esta categoria?");
            alert.SetPositiveButton("Add", (senderAlert, args) =>
            {

                alert.SetTitle("Continuando en la categoria");
            });

            alert.SetNegativeButton("Substract", (senderAlert, args) =>
            {
                alert.SetTitle("Ir a main");
            });

            Dialog dialog = alert.Create();
            dialog.Show();

            Alerta.Show();
        }
        
        //Metodo para traer color random
        public static Color GetRandomColor()
        {

            int hue = rand.Next(255);
            Color color = Color.HSVToColor(
                new[] {
            hue,
            1.0f,
            1.0f,
                }
            );
            return color;
        }

        public async Task Amen()
        {
         
            WSClient client2 = new WSClient();
            Random random = new Random();           
            var i = random.Next(1, 200);
            var result = await client2.Get<Nombres>("https://api-charadas.azurewebsites.net/api/Nombres/" + i.ToString()).ConfigureAwait(false);

            x.Text = result.Nombre1;

           
            //Layout.SetBackgroundColor(GetRandomColor());


        }
    }
}