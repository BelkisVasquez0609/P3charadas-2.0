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
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using Timer = System.Timers.Timer;

namespace Charadas_2._0
{
    [Activity(Label = "ActivityNombre", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class ActivityNombre : Activity
    {
        public TextView x;
        Button siguente;
        private TextView TxtCountDown;
        private int Count = 60;
        Timer timer;
        public Context context;
        Vector3 vector = new Vector3();

        int bueno;
        int malo;
     
        public AlertDialog Alerta;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Nombre);
            x = FindViewById<TextView>(Resource.Id.textView1);
             siguente = FindViewById<Button>(Resource.Id.buttonrd);
            siguente.Visibility = ViewStates.Invisible;

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
        }

        
        void Gyroscope_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            vector.X = e.Reading.Acceleration.X;
            vector.Y = e.Reading.Acceleration.Y;
            vector.Z = e.Reading.Acceleration.Z;

            GetVector();


        }

        public async void GetVector()
        {
            if (vector.Z >= 1)
            {
               
                Alerta.SetMessage("Correcto");
                Alerta.Show();
               
                await Amen();
                
                bueno++;

              
            }
            else if (vector.Z < -1)
            {
               
                Alerta.SetMessage("Incorrecto");
                
                Alerta.Show();
                await Amen();
                malo++;

                

            }
            else if (vector.Z > -0.2 && vector.Z < 0.2)
            {
               
                Alerta.Cancel();

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
                TxtCountDown.Text = "SE ACABO EL TIEMPO!!!";
                siguente.Visibility = 0;
                x.Text = "Buenos: " + bueno.ToString() + " - Malo: " + malo.ToString(); ;
                timer.Stop();
                Accelerometer.Stop();
               
                //Thread.Sleep(5000);
                //context = itemView.Context;
                ////SetContentView(Resource.Layout.activity_main);
                //var NxtAct = new Intent(Application.Context, typeof(MainActivity));
                //context.StartActivity(NxtAct);

            }
        }

        public async Task Amen()
        {
            WSClient client2 = new WSClient();
            Random random = new Random();           
            var i = random.Next(1, 200);
            var result = await client2.Get<Nombres>("https://api-charadas.azurewebsites.net/api/Nombres/" + i.ToString()).ConfigureAwait(false);

            x.Text = result.Nombre1;

        }
    }
}