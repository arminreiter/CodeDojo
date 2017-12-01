using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using CodingDojo4.Core;

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
        const string LOGFILE_EXT = ".log";
        const string LOGFILE_FOLDER = "Logs/";

        private Logic.Server _server;
        private bool isConnected = false;

        public ObservableCollection<string> Users { get; private set; }
        public ObservableCollection<string> Messages { get; private set; }
        public ObservableCollection<string> LogFiles
        {
            get
            {
                CheckLogFolder();
                var logfiles = System.IO.Directory.EnumerateFiles(LOGFILE_FOLDER, "*" + LOGFILE_EXT);
                var logfilesWithoutFolder = logfiles.Select(x => x.Remove(0, LOGFILE_FOLDER.Length));
                return new ObservableCollection<string>(logfilesWithoutFolder);
            }
        }

        public List<string> OpenLogFileContent { get; private set; }

        public string SelectedUser { get; set; }
        public string SelectedLog { get; set; }
        private string SelectedLogFile { get { return LOGFILE_FOLDER + SelectedLog; } }
        
        #region commands

        public RelayCommand StartCommand    { get => new RelayCommand(StartServer, () => !isConnected); }
        public RelayCommand StopCommand     { get => new RelayCommand(StopServer, () => isConnected); }
        public RelayCommand DropUserCommand { get => new RelayCommand(DropUser, () => !String.IsNullOrEmpty(SelectedUser)); }
        public RelayCommand SaveLogCommand  { get => new RelayCommand(SaveLog, () => Messages.Count > 0); }
        public RelayCommand OpenLogCommand  { get => new RelayCommand(OpenLog, () => !String.IsNullOrEmpty(SelectedLog)); }
        public RelayCommand DropLogCommand  { get => new RelayCommand(DropLog, () => !String.IsNullOrEmpty(SelectedLog)); }
                                            
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
            _server.DropUser(SelectedUser);
            Users.Remove(SelectedUser);
        }

        private void SaveLog()
        {
            CheckLogFolder();
            var messages = string.Join(Environment.NewLine, Messages.ToArray());
            System.IO.File.WriteAllText(LOGFILE_FOLDER + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + LOGFILE_EXT, messages);
            RaisePropertyChanged("LogFiles");
        }

        private void OpenLog()
        {
            if(System.IO.File.Exists(SelectedLogFile))
            {
                OpenLogFileContent = System.IO.File.ReadAllLines(SelectedLogFile).ToList();
                RaisePropertyChanged("OpenLogFileContent");
            }
        }

        private void DropLog()
        {
            if (System.IO.File.Exists(SelectedLogFile))
            {
                System.IO.File.Delete(SelectedLogFile);
                RaisePropertyChanged("LogFiles");
            }
        }

        private void StartServer()
        {
            if (_server == null)
            {
                _server = new Logic.Server(Globals.IP, Globals.PORT);
                _server.MessageReceived += _server_MessageReceived;
            }
            _server.Start();
            isConnected = true;
        }

        private void StopServer()
        {
            _server.Stop();
            isConnected = false;
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

        private void CheckLogFolder()
        {
            if (!System.IO.Directory.Exists(LOGFILE_FOLDER))
                System.IO.Directory.CreateDirectory(LOGFILE_FOLDER);
        }
    }
}