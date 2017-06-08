using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using StorageDroid.Services;
using Android;

namespace StorageDroid.Droid
{
	[Activity (Label = "StorageDroid.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

            button.Click += Button_Click;
		}

        private async void Button_Click(object sender, EventArgs e)
        {
            string emailRegistro = "merolhack@gmail.com";
            string codigoReto = "Reto7" + emailRegistro;

            StorageDroid.StorageService storageSvc = new StorageService();
            await storageSvc.performBlobOperation(emailRegistro);

			try
			{
				ServiceHelper serviceHelper = new ServiceHelper();
				string AndroidId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

				await serviceHelper.InsertarEntidad(emailRegistro, codigoReto, AndroidId);
			}
			catch (Exception exc)
			{
				string msgError = exc.Message;
			}
        }
    }
}


