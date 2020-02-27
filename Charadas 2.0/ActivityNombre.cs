using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
//using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using Timer = System.Timers.Timer;
using Android.Graphics;

namespace Charadas_2._0
{
    [Activity(Label = "ActivityNombre", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
   
    public class ActivityNombre : Activity
    {

        public TextView x;
        private Button Siguente;
        SensorSpeed speed = SensorSpeed.Game;
        private TextView TxtCountDown;
        private LinearLayout Layout;
        private int Count = 60;
        Timer timer;
        public Context context;
        Vector3 vector = new Vector3();
        public int Categoria = 0;
       // bool nuevapregunta = false;

        //  static Random rand = new Random();

        int bueno;
        int malo;
        bool click = true;
        public AlertDialog Alerta;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Nombre);

            try
            {

           
            x = FindViewById<TextView>(Resource.Id.textView1);
            Layout = FindViewById<LinearLayout>(Resource.Id.linearlayaut);
            Siguente = FindViewById<Button>(Resource.Id.siguente);
            Siguente.Visibility = ViewStates.Invisible;

            TxtCountDown = FindViewById<TextView>(Resource.Id.txtCountDown);
            Categoria = Convert.ToInt32(this.Intent.GetStringExtra("Categoria"));

            Alerta = new AlertDialog.Builder(this).Create();

            if (Accelerometer.IsMonitoring)
                return;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChangedAsync;
            Accelerometer.Start(SensorSpeed.Game);
            }
            catch (Exception)
            {

            }
            Thread.Sleep(5000);
            await Amen();

            TxtCountDown.Click += async delegate
                {
                    if (click == true)
                    {
                        await Amen();
                        bueno++;
                    }
                };
                x.Click += async delegate
                {
                    if (click == true)
                    {
                        await Amen();
                        malo++;

                    }
                };
            
          

            Siguente.Click += async delegate
            {
                Reinicio();
                click = true;
                x.Text = "Pongaselo en la Cabeza";
                Thread.Sleep(5000);
                await Amen();
            };
        }

        private void Reinicio()
        {
            try
            {

            
            Alerta.SetMessage("CONTINUAR JUGANDO");
            Alerta.Show();
            Alerta.Cancel();
            Accelerometer.Start(SensorSpeed.Game);
            
            Count = 60;
            bueno = 0;
            malo = 0;
            
            OnResume2();

            }
            catch (Exception)
            {

             
            }


        }

        private void OnResume2()
        {
            try
            {

            base.OnResume();
            timer = new Timer
            {
                Interval = 10000 
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            }
            catch (Exception)
            {

                
            }
        }

    

        private void Accelerometer_ReadingChangedAsync(object sender, AccelerometerChangedEventArgs e)
        {
            try
            {

           
            vector.X = e.Reading.Acceleration.X;
            vector.Y = e.Reading.Acceleration.Y;
            vector.Z = e.Reading.Acceleration.Z;
         
                GetVector();
            }
            catch (Exception)
            {

               
            }

        }


        public void GetVector()
        {
            try
            {

           

            if (vector.Z >= 1)
            {
                x.Text = "¡Incorrecto!";
                Layout.SetBackgroundColor(Color.Red);

             



            }
            else if (vector.Z <= -0.8)
            {

                x.Text = "¡Correcto!";

            
                Layout.SetBackgroundColor(Color.Green);


            }
            else if (vector.Z > -0.7 && vector.Z < 0.7)
            {


                Layout.SetBackgroundColor(Color.DarkSlateBlue);
            }

            }
            catch (Exception)
            {

               
            }
        }


        protected override void OnResume()
        {
            try
            {
                base.OnResume();
                timer = new Timer
                {
                    Interval = 1000 // 1 segundo
                };
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            
            }
            catch (Exception)
            {

            
            }
           
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
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
                Siguente.Visibility = ViewStates.Invisible;
                // CambiarCat.Visibility = ViewStates.Invisible;

                if (Count <= 60 && Count >= 1)//los 60 segundos
                {

                    Count--;

                    RunOnUiThread(() =>
                    {
                        TxtCountDown.Text = "" + Count; //se actualiza el valor

                    });
                }
                else
                {
                        click = false;
                    //View itemView = LayoutInflater.From(context).Inflate(Resource.Layout.activity_main,null , false);
                    TxtCountDown.Text = "¡¡¡SE ACABO EL TIEMPO!!!";

                    Accelerometer.Stop();
                    x.Text = "Buenos: " + bueno.ToString() + " - Malo: " + malo.ToString(); ;
                 
                    Siguente.Visibility = ViewStates.Visible;


                    Alerta.Show();
                    timer.Stop();

                }
            }

            }
            catch (Exception)
            {

               
            }

        }

        Nombres Nom;
        public async Task Amen()
        {
            try
            {

          
            WSClient client2 = new WSClient();
           
                 do
                {
                    Random random = new Random();

                    var i = random.Next(1, 265);

                    var result = await client2.Get<Nombres>("https://api-charadas.azurewebsites.net/api/Nombres/" + i.ToString()).ConfigureAwait(false);
                    
                    Nom = result;

                } while (Nom.id_categoria != Categoria);
              
              
              
              x.Text = Nom.Nombre1;
            


            }
            catch (Exception)
            {

               
            }

        }
    }
}