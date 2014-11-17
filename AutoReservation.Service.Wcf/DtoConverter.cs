using System;
using System.Collections.Generic;
using System.Linq;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal;

namespace AutoReservation.Service.Wcf
{
    public static class DtoConverter
    {
        #region Auto
        private static Auto GetAutoInstance(AutoDto dto)
        {
            if (dto.AutoKlasse == AutoKlasse.Standard) { return new StandardAuto(); }
            if (dto.AutoKlasse == AutoKlasse.Mittelklasse) { return new MittelklasseAuto(); }
            if (dto.AutoKlasse == AutoKlasse.Luxusklasse) { return new LuxusklasseAuto(); }
            throw new NotImplementedException("Unknown AutoDto implementation.");
        }
        public static Auto ConvertToEntity(this AutoDto dto)
        {
            if (dto == null) { return null; }

            Auto auto = GetAutoInstance(dto);
            auto.Id = dto.Id;
            auto.Marke = dto.Marke;
            auto.Tagestarif = dto.Tagestarif;

            if (auto is LuxusklasseAuto)
            {
                ((LuxusklasseAuto)auto).Basistarif = dto.Basistarif;
            }
            return auto;
        }
        public static AutoDto ConvertToDto(this Auto auto)
        {
            if (auto == null) { return null; }

            AutoDto dto = new AutoDto
            {
                Id = auto.Id,
                Marke = auto.Marke,
                Tagestarif = auto.Tagestarif,
            };

            if (auto is StandardAuto) { dto.AutoKlasse = AutoKlasse.Standard; }
            if (auto is MittelklasseAuto) { dto.AutoKlasse = AutoKlasse.Mittelklasse; }
            if (auto is LuxusklasseAuto)
            {
                dto.AutoKlasse = AutoKlasse.Luxusklasse;
                dto.Basistarif = ((LuxusklasseAuto)auto).Basistarif;
            }


            return dto;
        }
        public static List<Auto> ConvertToEntities(this IEnumerable<AutoDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<AutoDto> ConvertToDtos(this IEnumerable<Auto> autos)
        {
            return ConvertGenericList(autos, ConvertToDto);
        }
        #endregion
        #region Kunde
        public static Kunde ConvertToEntity(this KundeDto dto)
        {
            if (dto == null) { return null; }

            return new Kunde
            {
                Id = dto.Id,
                Nachname = dto.Nachname,
                Vorname = dto.Vorname,
                Geburtsdatum = dto.Geburtsdatum
            };
        }
        public static KundeDto ConvertToDto(this Kunde kunde)
        {
            if (kunde == null) { return null; }

            return new KundeDto
            {
                Id = kunde.Id,
                Nachname = kunde.Nachname,
                Vorname = kunde.Vorname,
                Geburtsdatum = kunde.Geburtsdatum
            };
        }
        public static List<Kunde> ConvertToEntities(this IEnumerable<KundeDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<KundeDto> ConvertToDtos(this IEnumerable<Kunde> kunden)
        {
            return ConvertGenericList(kunden, ConvertToDto);
        }
        #endregion
        #region Reservation
        public static Reservation ConvertToEntity(this ReservationDto dto)
        {
            if (dto == null) { return null; }

            Reservation reservation = new Reservation
            {
                ReservationNr = dto.ReservationNr,
                Von = dto.Von,
                Bis = dto.Bis,
                AutoId = dto.Auto.Id,
                KundeId = dto.Kunde.Id
            };

            return reservation;
        }
        public static ReservationDto ConvertToDto(this Reservation reservation)
        {
            if (reservation == null) { return null; }

            return new ReservationDto
            {
                ReservationNr = reservation.ReservationNr,
                Von = reservation.Von,
                Bis = reservation.Bis,
                Auto = ConvertToDto(reservation.Auto),
                Kunde = ConvertToDto(reservation.Kunde)
            };
        }
        public static List<Reservation> ConvertToEntities(this IEnumerable<ReservationDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<ReservationDto> ConvertToDtos(this IEnumerable<Reservation> reservationen)
        {
            return ConvertGenericList(reservationen, ConvertToDto);
        }
        #endregion

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(s => converter(s)).ToList();
        }
    }

 
}
