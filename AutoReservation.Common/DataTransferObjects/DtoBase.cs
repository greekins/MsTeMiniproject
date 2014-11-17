using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public abstract class DtoBase : INotifyPropertyChanged, ICloneable
    {
        public abstract string Validate();
        public abstract object Clone();

        private PropertyChangedEventHandler propertyChangedEvent;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChangedEvent += value; }
            remove { propertyChangedEvent -= value; }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
           if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
