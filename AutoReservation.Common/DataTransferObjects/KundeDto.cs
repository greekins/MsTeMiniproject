using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase
    {
        [DataMember] 
        private DateTime geburtsDatum;
        [DataMember] 
        private int id;
        [DataMember]
        private string nachname;
        [DataMember]
        private string vorname;

        public DateTime Geburtsdatum
        {
            get { return geburtsDatum; }
            set
            {
                geburtsDatum = value;
                RaisePropertyChanged();
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged();
            }
        }
        public string Vorname
        {
            get { return vorname; }
            set
            {
                vorname = value;
                RaisePropertyChanged();
            }
        }
        public string Nachname
        {
            get { return nachname; }
            set
            {
                nachname = value;
                RaisePropertyChanged();
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new KundeDto
            {
                Id = Id,
                Nachname = Nachname,
                Vorname = Vorname,
                Geburtsdatum = Geburtsdatum
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}",
                Id,
                Nachname,
                Vorname,
                Geburtsdatum);
        }

    }
}
