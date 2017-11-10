using Shared.BaseModels;
using Shared.Interfaces;
using System;

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

        public string Mode
        {
            get
            {
                
                if (_item is ISensor sensor)
                    return sensor.SensorMode.ToString();

                if (_item is IActuator actuator)
                    return actuator.ActuatorMode.ToString();

                return String.Empty;
            }
            set
            {
                try
                {
                    if (_item is ISensor sensor)
                        sensor.SensorMode = (SensorModeType)Enum.Parse(typeof(SensorModeType), value);

                    if (_item is IActuator actuator)
                        actuator.ActuatorMode = (ModeType)Enum.Parse(typeof(ModeType), value);

                    NotifyPropertyChanged();
                }
                catch (Exception) { } // quick and dirty 
            }
        }

        public Type ValueType
        {
            get
            {
                if (_item is ISensor sensor)
                    return sensor.SensorValueType;

                if (_item is IActuator actuator)
                    return actuator.ActuatorValueType;

                return typeof(object);
            }
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
