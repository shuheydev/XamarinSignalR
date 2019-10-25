using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSignalR.Models;

namespace XamarinSignalR.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private HubConnection _hubConnection;

        private string _userMessage;
        public string UserMessage
        {
            get
            {
                return _userMessage;
            }
            set
            {
                if (_userMessage == value) { return; }
                _userMessage = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MessageModel> _messages;
        public ObservableCollection<MessageModel> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                if (_messages == value) { return; }
                _messages = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }

        public MainPageViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            SendMessageCommand = new Command(async () => { await SendMessage(); });
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://signalrcorepractice20191006060526.azurewebsites.net/chathub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string, string>("Receive", (message, from) =>
            {
                Messages.Add(new MessageModel { User = from, Message = message });
            });
        }

        private async Task Disconnect()
        {
            await _hubConnection.StopAsync();
            Messages.Add(new MessageModel { User = "Xamarin", Message = "Disconnected " });
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();
            Messages.Add(new MessageModel { User = "Xamarin", Message = "Connection Started" });
        }

        private async Task SendMessage()
        {
            await _hubConnection.InvokeAsync("send", UserMessage, "Xamarin");
            UserMessage = "";
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
