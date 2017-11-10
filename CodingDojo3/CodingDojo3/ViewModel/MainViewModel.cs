using CodingDojo3.Commands;
using CodingDojo3.Simulation;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                        ActuatorData.Add(new ItemVm(new Switch("Test", "just a test", "WZ", 17)));
                    },
                    () => true);
            }
        }

        public MainViewModel()
        {
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
            AddTimer();
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
