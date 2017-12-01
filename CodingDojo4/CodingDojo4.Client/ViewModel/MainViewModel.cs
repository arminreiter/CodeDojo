using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Net.Sockets;
using System.Collections.ObjectModel;
using CodingDojo4.Core;
using System.Windows.Input;

namespace CodingDojo4.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Logic.Client _client;
        private bool isConnected = false;
        
        public string UserName { get; set; }
        public string Message { get; set; }
        public ObservableCollection<string> Messages { get; set; }

        #region Commands

        public RelayCommand ConnectCommand { get => new RelayCommand(Connect, () => !isConnected); }
        public RelayCommand SendCommand { get => new RelayCommand(Send, () => !String.IsNullOrEmpty(Message) && isConnected); }
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Messages = new ObservableCollection<string>();
            _client = new Logic.Client();
            _client.MessageReceived += _client_MessageReceived;
            _client.ClientDisconnected += _client_ClientDisconnected;
        }

        private void Send()
        {
            _client.SendMessage(UserName + ": " + Message);
            Messages.Add("YOU: " + Message);
        }
        
        private void Connect()
        {
            isConnected = _client.Connect(Globals.IP, Globals.PORT);
            if (isConnected)
                Messages.Add("Connection successful!");
            else
                Messages.Add("Connection could not be established!");
        }        

        private void _client_MessageReceived(object sender, string e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(e);
            });
        }

        private void _client_ClientDisconnected(object sender, EventArgs e)
        {
            isConnected = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }
}