using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo3.ViewModel
{
    public class ItemVm : BaseViewModel
    {
        private ItemBase _item;

        public ItemBase Item
        {
            get => _item;
            set
            {
                _item = value;
                NotifyPropertyChanged();
            }
        }

        private string _tmp = "asdf";

        public string Tmp
        {
            get { return _tmp; }
            set { _tmp = value; NotifyPropertyChanged(); }
        }

        public object Value
        {
            get
            {
                if (_item is ISensor sensor)
                    return sensor.SensorValue;
                
                if (_item is IActuator actuator)
                    return actuator.ActuatorValue;

                return null;
            }
            set
            {
                if (_item is ISensor sensor)
                    sensor.SensorValue = value;

                if (_item is IActuator actuator)
                    actuator.ActuatorValue = value;

                NotifyPropertyChanged("Value");
            }
        }

        public Type GetValueType()
        {
            if (_item is ISensor sensor)
                return sensor.SensorValueType;

            if (_item is IActuator actuator)
                return actuator.ActuatorValueType;

            return typeof(object);
        }

        public ItemVm(ISensor sensor)
        {
            _item = sensor as ItemBase;
        }

        public ItemVm(IActuator actuator)
        {
            _item = actuator as ItemBase;
        }
        
    }

}
