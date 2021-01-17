﻿using Cinemaddict.Services;
using Cinemaddict.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Helper;

namespace Cinemaddict
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var auth = DependencyService.Get<IFirebaseAuthentication>();
            Current.Properties["auth"] = auth;
            DependencyService.Register<MockDataStore>();
            if(auth.IsSignIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
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
