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
using Android.Speech;

namespace Charadas_2._0
{
    [Activity(Label = "ActivityNombre", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
   
    public class ActivityNombre : Activity
    {

        public TextView x;
        private Button Siguente;
        private TextView TxtCountDown;
        private LinearLayout Layout;
        private int CountGB = 60;
        Timer timer;
        public Context context;
        Vector3 vector = new Vector3();
        public int Categoria = 0;
        Nombres Nom;
        int bueno;
        int malo;
        bool click = true;
        bool accel = true;
        public AlertDialog Alerta;

        readonly int VOICE = 0;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Nombre);
             timer = new Timer();
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
            timer.AutoReset = true;
            Layout.SetBackgroundColor(Color.DarkBlue);
            await SpeakNowDefaultSettings("Ponerse el Celular en la Cabeza");
            Thread.Sleep(5000);
            await Amen();
            }
            catch (Exception)
            {

            }
         
            try
            {
                x.Click += delegate
                {
                    if (click == true)
                    {
                        try
                        {
                            SpeakaerBool();
                        }
                        catch (Exception) {}

                    }
                };
            
          

            Siguente.Click += async delegate
            {
               
                Reinicio();
                click = true;
                await SpeakNowDefaultSettings("Ponerse el Celular en la Cabeza").ConfigureAwait(false);

                x.Text = "Pongaselo en la Cabeza";
                Thread.Sleep(5000);
                await Amen();
            };

            }
            catch (Exception) {}
        }

        private void Reinicio()
        {
            try
            {

            
            Alerta.SetMessage("CONTINUAR JUGANDO");
            Alerta.Show();
            Alerta.Cancel();
            Accelerometer.Start(SensorSpeed.Game);

            CountGB = 60;
            bueno = 0;
            malo = 0;

                OnResume();
               

            }
            catch (Exception)
            {

             
            }


        }

        private async void Accelerometer_ReadingChangedAsync(object sender, AccelerometerChangedEventArgs e)
        {
            try
            {

           
            vector.X = e.Reading.Acceleration.X;
            vector.Y = e.Reading.Acceleration.Y;
            vector.Z = e.Reading.Acceleration.Z;

                await GetVectorAsync();
            }
            catch (Exception) {}

        }
        
        public async Task GetVectorAsync()
        {
            try
            {
            if (vector.Z >= 0.7)
            {
                    if (accel == true)
                    {
                        accel = false;
                        Layout.SetBackgroundColor(Color.Red);
                        malo++;
                        await Amen();
                        await SpeakNowDefaultSettings("Respuesta Incorrecta");

                    }

                }
            else if (vector.Z <= -0.7)
            {
                    if (accel == true)
                    {
                        accel = false;
                        Layout.SetBackgroundColor(Color.Green);
                        bueno++;
                        await Amen();
                        await SpeakNowDefaultSettings("Respuesta Correcta");

                    }

                }
            else 
            {

                   accel = true;
                    Layout.SetBackgroundColor(Color.DarkSlateBlue);
            }

            }
            catch (Exception){}
        }

        protected override void OnResume()
        {
            try
            {
                base.OnResume();
                timer.Interval = 1000;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
              
            
            }
            catch (Exception)
            {}
           
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                Siguente.Visibility = ViewStates.Invisible;

                if (CountGB <= 60 && CountGB >= 1)//los 60 segundos
                {
                    CountGB--;
                    TxtCountDown.Text = "" + CountGB; //se actualiza el valor

                }
                else
                {
                    //await SpeakNowDefaultSettings("Buenos:" + bueno.ToString() + "  " + " - Malos: " + malo.ToString()).ConfigureAwait(false);

                    click = false;
                    TxtCountDown.Text = "¡¡¡SE ACABO EL TIEMPO!!!";
                    Accelerometer.Stop();

                    x.Text = "Buenos: " + bueno.ToString() + " - Malos: " + malo.ToString();
                    Siguente.Visibility = ViewStates.Visible;
                 
                    timer.Stop();
  


                }


            }
            catch (Exception)
            { }

        }

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

        public void SpeakaerBool()
        {
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                var alert = new AlertDialog.Builder(x.Context);
                alert.SetTitle("You don't seem to have a microphone to record with");
                alert.SetPositiveButton("OK", (sender, e) =>
                {
                    return;
                });
               // alert.Show();
            }
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            StartActivityForResult(voiceIntent, VOICE);


        }

        protected override async void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            try
            {
                if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput =  matches[0];
                        Alerta.SetMessage(textInput);
                        Alerta.Show();
                     
                        switch (matches[0])
                        {
                            case "bueno":
                                bueno++;
                                Layout.SetBackgroundColor(Color.Green);
                                await Amen();
                                Alerta.Cancel();
                                break;

                            case "malo":
                                malo++;
                                Layout.SetBackgroundColor(Color.Red);
                                await Amen();
                                Alerta.Cancel();
                                break;
                        }
                    }
                    else
                    {
                       
                         Alerta.Cancel();
                    }
                }
                base.OnActivityResult(requestCode, resultVal, data);
 
                }

            }
            catch (Exception)
            {}
        }

        public async Task SpeakNowDefaultSettings(string txt)
        {
            await TextToSpeech.SpeakAsync(txt);

            // This method will block until utterance finishes.
        }

    }
}