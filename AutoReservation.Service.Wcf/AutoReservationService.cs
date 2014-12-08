using AutoReservation.BusinessLayer;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects;
using System;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.Dal;

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

        public System.Collections.Generic.List<KundeDto> GetKunden()
        {
            WriteActualMethod();
            return component.GetKunden().ConvertToDtos();
        }

        public KundeDto GetKunde(int id)
        {
            WriteActualMethod();
            return component.GetKunde(id).ConvertToDto();
        }

        public void DeleteKunde(int id)
        {
            WriteActualMethod();
            component.DeleteKunde(id);
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            component.InsertKunde(kunde.ConvertToEntity());
        }

        public System.Collections.Generic.List<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return component.GetAutos().ConvertToDtos();
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

        public void DeleteAuto(int id)
        {
            WriteActualMethod();
            component.DeleteAuto(id);
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

        public System.Collections.Generic.List<ReservationDto> GetReservationen()
        {
            WriteActualMethod();
            return component.GetReservationen().ConvertToDtos();
        }

        public Common.DataTransferObjects.ReservationDto GetReservation(int id)
        {
            WriteActualMethod();
            return component.GetReservation(id).ConvertToDto();
        }

        public void DeleteReservation(int id)
        {
            WriteActualMethod();
            component.DeleteReservation(id);
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            component.InsertReservation(reservation.ConvertToEntity());
        }
    }
}