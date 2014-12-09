using AutoReservation.BusinessLayer;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects;
using System;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.Dal;
using System.Collections.Generic;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private AutoReservationBusinessComponent component;

        public AutoReservationService() 
        {
            component = new AutoReservationBusinessComponent();

        }

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }


        public void UpdateKunde(KundeDto modified, KundeDto original)
        {
            try
            {
                WriteActualMethod();
                component.UpdateKunde(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch(LocalOptimisticConcurrencyException<Kunde> e) 
            {
                throw new FaultException<KundeDto>(e.MergedEntity.ConvertToDto());
            }
        }

        List<KundeDto> IAutoReservationService.Kunden
        {
            get
            {
                WriteActualMethod();
                return component.GetKunden().ConvertToDtos();
            }
        }

        public KundeDto GetKunde(int id)
        {
            WriteActualMethod();
            return component.GetKunde(id).ConvertToDto();
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            component.DeleteKunde(DtoConverter.ConvertToEntity(kunde));
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            component.InsertKunde(kunde.ConvertToEntity());
        }

        List<AutoDto> IAutoReservationService.Autos
        {
            get
            {
                WriteActualMethod();
                return component.GetAutos().ConvertToDtos();
            }
        }

        public AutoDto GetAuto(int id)
        {
            WriteActualMethod();
            return component.GetAuto(id).ConvertToDto();
        }

        public void UpdateAuto(AutoDto modified, AutoDto original)
        {
            try
            {
                WriteActualMethod();
                component.UpdateAuto(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<AutoDto>(e.MergedEntity.ConvertToDto());
            }
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            component.DeleteAuto(DtoConverter.ConvertToEntity(auto));
        }

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            component.InsertAuto(auto.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            try
            {
                WriteActualMethod();
                component.UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<ReservationDto>(e.MergedEntity.ConvertToDto());
            }
        }

        List<ReservationDto> IAutoReservationService.Reservationen
        {
            get
            {
                WriteActualMethod();
                return component.GetReservationen().ConvertToDtos();
            }
        }

        public Common.DataTransferObjects.ReservationDto GetReservation(int id)
        {
            WriteActualMethod();
            return component.GetReservation(id).ConvertToDto();
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            component.DeleteReservation(DtoConverter.ConvertToEntity(reservation));
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            component.InsertReservation(reservation.ConvertToEntity());
        }
    }
}