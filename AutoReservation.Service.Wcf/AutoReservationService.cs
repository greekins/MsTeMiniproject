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
                component.UpdateKunde(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch(LocalOptimisticConcurrencyException<Kunde> e) 
            {
                throw new FaultException<KundeDto>(e.MergedEntity.ConvertToDto());
            }
        }

        public System.Collections.Generic.List<KundeDto> GetKunden()
        {
            return component.GetKunden().ConvertToDtos();
        }

        public KundeDto GetKunde(int id)
        {
            return component.GetKunde(id).ConvertToDto();
        }

        public void DeleteKunde(int id)
        {
            component.DeleteKunde(id);
        }

        public void InsertKunde(KundeDto kunde)
        {
            component.InsertKunde(kunde.ConvertToEntity());
        }

        public System.Collections.Generic.List<AutoDto> GetAutos()
        {
            return component.GetAutos().ConvertToDtos();
        }

        public AutoDto GetAuto(int id)
        {
            return component.GetAuto(id).ConvertToDto();
        }

        public void UpdateAuto(AutoDto modified, AutoDto original)
        {
            try
            {
                component.UpdateAuto(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Auto> e)
            {
                throw new FaultException<AutoDto>(e.MergedEntity.ConvertToDto());
            }
        }

        public void DeleteAuto(int id)
        {
            component.DeleteAuto(id);
        }

        public void InsertAuto(AutoDto auto)
        {
            component.InsertAuto(auto.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            try
            {
                component.UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Reservation> e)
            {
                throw new FaultException<ReservationDto>(e.MergedEntity.ConvertToDto());
            }
        }

        public System.Collections.Generic.List<ReservationDto> GetReservationen()
        {
            return component.GetReservationen().ConvertToDtos();
        }

        public Common.DataTransferObjects.ReservationDto GetReservation(int id)
        {
            return component.GetReservation(id).ConvertToDto();
        }

        public void DeleteReservation(int id)
        {
            component.DeleteReservation(id);
        }

        public void InsertReservation(ReservationDto reservation)
        {
            component.InsertReservation(reservation.ConvertToEntity());
        }
    }
}