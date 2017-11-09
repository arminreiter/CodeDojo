using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Threading;

namespace CodingDojo3.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string CurrentDate { get; private set; }
        public string CurrentTime { get; private set; }

        public MainViewModel()
        {
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(250); // 4 times per second
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
            CurrentTime = DateTime.Now.ToLocalTime().ToLongTimeString();
            NotifyPropertyChanged("CurrentDate");
            NotifyPropertyChanged("CurrentTime");
        }

        #region INotifyPropertyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
