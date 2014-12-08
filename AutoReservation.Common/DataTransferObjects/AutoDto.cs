using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase
    {
        [DataMember]
        private int id;
        [DataMember]
        private string marke;
        [DataMember]
        private int tagestarif;
        [DataMember]
        private int basistarif;
        [DataMember]
        private  AutoKlasse autoKlasse;

        public AutoKlasse AutoKlasse
        {
            get { return autoKlasse; }
        }


        public string Marke
        {
            get { return marke; }
            set
            { 
                marke = value;
                RaisePropertyChanged();  
            }
        }
        public int Tagestarif
        {
            get { return tagestarif; }
            set
            { 
                tagestarif = value;
                RaisePropertyChanged();
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Basistarif
        {
            get { return basistarif; }
            set
            {
                basistarif= value;
                RaisePropertyChanged();
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (autoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new AutoDto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                autoKlasse = autoKlasse,
                Basistarif = Basistarif
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                Id,
                Marke,
                Tagestarif,
                Basistarif,
                autoKlasse);
        }

    }
}
