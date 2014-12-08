using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto : DtoBase
    {
        [DataMember]
        private DateTime von;
        [DataMember]
        private DateTime bis;
        [DataMember]
        private int reservationNr;
        [DataMember]
        private AutoDto auto;
        [DataMember]
        private KundeDto kunde;

        public AutoDto Auto
        {
            get { return auto; }
            set 
            {
                auto = value;
                RaisePropertyChanged();
            }
        }

        public KundeDto Kunde
        {
            get { return kunde; }
            set
            {
                kunde = value;
                RaisePropertyChanged();
            }
        }

        public DateTime Von
        {
            get { return von; }
            set
            {
                von = value;
                RaisePropertyChanged();
            }
        }
        public DateTime Bis
        {
            get { return bis; }
            set
            {
                bis = value;
                RaisePropertyChanged();
            }
        }
        public int ReservationNr
        {
            get { return reservationNr; }
            set
            {
                reservationNr = value;
                RaisePropertyChanged();
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (Von == DateTime.MinValue)
            {
                error.AppendLine("- Von-Datum ist nicht gesetzt.");
            }
            if (Bis == DateTime.MinValue)
            {
                error.AppendLine("- Bis-Datum ist nicht gesetzt.");
            }
            if (Von > Bis)
            {
                error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
            }
            if (auto == null)
            {
                error.AppendLine("- Auto ist nicht zugewiesen.");
            }
            else
            {
                string autoError = auto.Validate();
                if (!string.IsNullOrEmpty(autoError))
                {
                    error.AppendLine(autoError);
                }
            }
            if (kunde == null)
            {
                error.AppendLine("- Kunde ist nicht zugewiesen.");
            }
            else
            {
                string kundeError = kunde.Validate();
                if (!string.IsNullOrEmpty(kundeError))
                {
                    error.AppendLine(kundeError);
                }
            }


            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new ReservationDto
            {
                ReservationNr = ReservationNr,
                Von = Von,
                Bis = Bis,
                auto = (AutoDto)auto.Clone(),
                kunde = (KundeDto)kunde.Clone()
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                ReservationNr,
                Von,
                Bis,
                auto,
                kunde);
        }

    }
}
