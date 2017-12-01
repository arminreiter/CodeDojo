using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace CodingDojo4.Server.ViewModel
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
        const string IP = "127.0.0.1";
        const int PORT = 10100;

        private Logic.Server _server;

        private bool isConnected = false;
        public ObservableCollection<string> Users { get; private set; }
        public ObservableCollection<string> Messages { get; private set; }

        public int ReceivedMessages { get; set; }
        
        #region commands

        public RelayCommand StartCommand    { get => new RelayCommand(StartServer, () => !isConnected); }
        public RelayCommand StopCommand     { get => new RelayCommand(StopServer, () => isConnected); }
        public RelayCommand DropUserCommand { get => new RelayCommand(DropUser); }
        public RelayCommand SaveLogCommand  { get => new RelayCommand(SaveLog); }
        public RelayCommand OpenLogCommand  { get => new RelayCommand(OpenLog); }
        public RelayCommand DropLogCommand  { get => new RelayCommand(DropLog); }
                                            
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Users = new ObservableCollection<string>();
            Messages = new ObservableCollection<string>();
        }

        private void DropUser()
        {
        }

        private void SaveLog()
        {
        }

        private void OpenLog()
        {
        }

        private void DropLog()
        {
        }

        private void StartServer()
        {
            if (_server == null)
            {
                _server = new Logic.Server(IP, PORT);
                _server.MessageReceived += _server_MessageReceived;
            }
            _server.Start();
            isConnected = true;
        }

        private void _server_MessageReceived(object sender, string e)
        {
            if (e.Contains(":"))
            {
                string user = e.Substring(0, e.IndexOf(':'));
                if (!Users.Contains(user))
                    Users.Add(user);
            }
            Messages.Add(e);
        }

        private void StopServer()
        {
            _server.Stop();
            isConnected = false;
        }
    }
}