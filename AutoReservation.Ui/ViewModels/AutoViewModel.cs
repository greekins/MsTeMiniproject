using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
    public class AutoViewModel : ViewModelBase
    {
        private readonly List<AutoDto> autosOriginal = new List<AutoDto>();
        private ObservableCollection<AutoDto> autos;
        public ObservableCollection<AutoDto> Autos
        {
            get
            {
                if (autos == null)
                {
                    autos = new ObservableCollection<AutoDto>();
                }
                return autos;
            }
        }

        private AutoDto selectedAuto;
        public AutoDto SelectedAuto
        {
            get { return selectedAuto; }
            set
            {
                if (selectedAuto != value)
                {
                    selectedAuto = value;
                    RaisePropertyChanged();
                }
            }
        }

        #region Load-Command

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand(
                        param => Load(),
                        param => CanLoad()
                    );
                }
                return loadCommand;
            }
        }

        protected override void Load()
        {
            Autos.Clear();
            autosOriginal.Clear();
            foreach (AutoDto auto in Service.Autos)
            {
                Autos.Add(auto);
                autosOriginal.Add((AutoDto)auto.Clone());
            }
            SelectedAuto = Autos.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(
                        param => SaveData(),
                        param => CanSaveData()
                    );
                }
                return saveCommand;
            }
        }

        private void SaveData()
        {
            foreach (AutoDto auto in Autos)
            {
                if (auto.Id == default(int))
                {
                    Service.InsertAuto(auto);
                }
                else
                {
                    AutoDto original = autosOriginal.FirstOrDefault(ao => ao.Id == auto.Id);
                    Service.UpdateAuto(auto, original);
                }
            }
            Load();
        }

        private bool CanSaveData()
        {
            if (Service == null)
            {
                return false;
            }

            StringBuilder errorText = new StringBuilder();
            foreach (AutoDto auto in Autos)
            {
                string error = auto.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(auto.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        #endregion

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(
                        param => New(),
                        param => CanNew()
                    );
                }
                return newCommand;
            }
        }

        private void New()
        {
            Autos.Add(new AutoDto());
        }

        private bool CanNew()
        {
            return Service != null;
        }

        #endregion

        #region Delete-Command

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(
                        param => Delete(),
                        param => CanDelete()
                    );
                }
                return deleteCommand;
            }
        }

        private void Delete()
        {
            Service.DeleteAuto(SelectedAuto);
            Load();
        }

        private bool CanDelete()
        {
            return
                SelectedAuto != null &&
                SelectedAuto.Id != default(int) &&
                Service != null;
        }

        #endregion

    }
}