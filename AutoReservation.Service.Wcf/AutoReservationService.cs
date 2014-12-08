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
                component.UpdateKunde(DtoConverter.ConvertToEntity(modified), DtoConverter.ConvertToEntity(original));
            }
            catch(LocalOptimisticConcurrencyException<Kunde> e) 
            {
                throw new FaultException<KundeDto>(e.MergedEntity.ConvertToDto());
            }
        }

        public System.Collections.Generic.List<KundeDto> GetKunden()
        {
            throw new NotImplementedException();
        }

        public .KundeDto GetKunde(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteKunde(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<AutoDto> GetAutos()
        {
            throw new NotImplementedException();
        }

        public AutoDto GetAuto(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuto(AutoDto modified, AutoDto original)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuto(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<ReservationDto> GetReservationen()
        {
            throw new NotImplementedException();
        }

        public Common.DataTransferObjects.ReservationDto GetReservation(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }
    }
}