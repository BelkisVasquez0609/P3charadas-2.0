using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace Charadas_2._0
{
    class Gyroscopecharadas
    {
        Vector3 vector = new Vector3();
      

        readonly AlertDialog Alerta;
        public Gyroscopecharadas(Context x, TextView Nombre)
        {
          
            Alerta = new AlertDialog.Builder(x).Create();
            // Register for reading changes.
            // Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            if (Accelerometer.IsMonitoring)
                return;
            Accelerometer.ReadingChanged += Gyroscope_ReadingChanged;
            Accelerometer.ShakeDetected += Gyroscope_ShakeDetected;
            Accelerometer.Start(SensorSpeed.Game);

        }

        void Gyroscope_ShakeDetected(object sender,EventArgs e)
        {
            Accelerometer.Stop();
            for (int i = 0; i <= 5; i++)
            {
                if (i == 4)
                {
                    Thread.Sleep(5000);

                    Accelerometer.Start(SensorSpeed.Fastest);

                }
            }
        }
        void Gyroscope_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            vector.X = e.Reading.Acceleration.X;
            vector.Y = e.Reading.Acceleration.Y;
            vector.Z = e.Reading.Acceleration.Z;
         
            GetVector();


        }
        public async Task amen()
        {
            WSClient client2 = new WSClient();
            Random random = new Random();
            var i = random.Next(1, 266);
            var result = await client2.Get<Nombres>("https://api-charadas.azurewebsites.net/api/Nombres/" + i.ToString()).ConfigureAwait(false);

         //   this.Nombre.Text = result.Nombre1;

        }
        public async void GetVector()
        {
            if (vector.Z > 0.5)
            {
                Alerta.SetMessage("SI");
                Alerta.Show();
                await amen();
               
            }
            else if (vector.Z < -0.5)
            {
                Alerta.SetMessage("NO");
                Alerta.Show();
                await amen();

            }
            else
            {
                Alerta.Cancel();
               
            }
           
           
        }

       

        }
    }
