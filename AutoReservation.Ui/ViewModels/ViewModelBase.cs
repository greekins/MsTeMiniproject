using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Factory;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoReservation.Ui.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly IAutoReservationService Service;
        private PropertyChangedEventHandler propertyChangedEvent;

        protected ViewModelBase()
        {
            if (!IsInDesignTime)
            {
                Service = Creator.GetCreator().CreateInstance();
                Load();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChangedEvent += value; }
            remove { propertyChangedEvent -= value; }
        }

        protected abstract void Load();

        private string errorText;
        public string ErrorText
        {
            get
            {
                return errorText;
            }
            set
            {
                if (errorText != value)
                {
                    errorText = value;
                    RaisePropertyChanged();
                }
            }
        }

        #region Helper Methods
        
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool IsInDesignTime
        {
            get
            {
                DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        #endregion

    }
}
