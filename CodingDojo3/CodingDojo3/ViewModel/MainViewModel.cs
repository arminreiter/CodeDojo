using CodingDojo3.Simulation;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Threading;

namespace CodingDojo3.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public string CurrentDate { get { return DateTime.Now.ToLocalTime().ToShortDateString(); } }
        public string CurrentTime { get { return DateTime.Now.ToLocalTime().ToLongTimeString(); } }

        public ObservableCollection<ItemVm> SensorData { get; set; }
        public ObservableCollection<ItemVm> ActuatorData { get; set; }

        public RelayCommand AddActuatorCommand
        {
            get
            {
                return new RelayCommand(
                    (param) => 
                    {
                        //ActuatorData.Add(new ItemVm(new Switch("Test", "just a test", "WZ", 17)));
                        ActuatorData[2].Tmp = "12";
                        ActuatorData[2].Value = "12";
                    },
                    () => true);
            }
        }

        public MainViewModel()
        {
            AddTimer();
            SensorData = new ObservableCollection<ItemVm>();
            ActuatorData = new ObservableCollection<ItemVm>();
            Simulator sim = new Simulator(new List<ItemVm>());
            
            foreach (var item in sim.Items)
            {
                if (item.Item is ISensor)
                    SensorData.Add(item);
                else if (item.Item is IActuator)
                    ActuatorData.Add(item);
            }
        }

        private void AddTimer()
        {
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(250); // 4 times per second
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            NotifyPropertyChanged("CurrentDate");
            NotifyPropertyChanged("CurrentTime");
        }
    }
}
