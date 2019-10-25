using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows;
using System;
using System.Threading.Tasks;
using XamarinSignalR.ViewModels;

namespace XamarinSignalR
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MainPageViewModel();
        }

    }
}
