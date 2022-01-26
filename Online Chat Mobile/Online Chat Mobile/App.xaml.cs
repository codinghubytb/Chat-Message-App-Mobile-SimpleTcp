using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Online_Chat_Mobile.Services;
using Online_Chat_Mobile.View;

namespace Online_Chat_Mobile
{
    public partial class App : Application
    {
        #region Variable

        public static ClassServer server { get; set; }
        public static ClassClient client { get; set; }
        public static ClassIP classIP { get; set; } = new ClassIP();
        public static Color ColorFront { get; set; } = Color.Black;
        public static Color ColorBack { get; set; } = Color.PapayaWhip;

        public static Color FrameColorFront { get; set; } = Color.White;

        #endregion

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = ColorBack,
                BarTextColor = ColorFront
            };

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
