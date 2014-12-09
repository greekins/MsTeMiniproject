using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Ui.Properties;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
    abstract class Creator
    {
        public abstract IAutoReservationService CreateInstance();
        public Creator GetCreator() {
            Type serviceLayerType = Type.GetType(Settings.Default.ServiceLayerType);
            if (serviceLayerType == null) { return new LocalDataAccessCreator(); }
            return (Creator)Activator.CreateInstance(serviceLayerType);
        }
    }
}
