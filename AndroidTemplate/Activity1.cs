using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.IO;
using Microsoft.Xna.Framework;

namespace AndroidTemplate
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private GameManager _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new GameManager();
            _game._inputDialog = new InputDialog(this);
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);
            _game.Run();

            /*ContextWrapper wrapper = new ContextWrapper(this);
            wrapper.FilesDir.CreateNewFile();
            FileOutputStream outputStream = new FileOutputStream(wrapper.FilesDir.AbsoluteFile);
            outputStream.Write(150);

            _game.testOutput = wrapper.FilesDir.AbsolutePath;*/
        }

        protected override void OnResume()
        {
            base.OnResume();

            // When we resume (which also seems to happen on startup), hide the system UI to go to full screen mode.
            HideSystemUI();
        }

        private void HideSystemUI()
        {
            //Hiding Navigation and Status Bars and making them transient.
            if(Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                //This method only works after API Level 30
                Window.InsetsController.Hide(WindowInsets.Type.NavigationBars() | WindowInsets.Type.StatusBars());
                Window.InsetsController.SystemBarsBehavior = 2;
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                //This method only works after API Level 30 and before 19
                View decorView = Window.DecorView;
                var uiOptions = (int)decorView.SystemUiVisibility;
                var newUiOptions = (int)uiOptions;

                //newUiOptions |= (int)SystemUiFlags.LowProfile;
                newUiOptions |= (int)SystemUiFlags.Fullscreen;
                newUiOptions |= (int)SystemUiFlags.HideNavigation;
                newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;

                decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
            }
            this.Immersive = true;            
        }
    }
}
