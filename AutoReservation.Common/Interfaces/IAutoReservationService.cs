using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using System.Runtime.Serialization;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        //Kunde
        [OperationContract]
        public void UpdateKunde(KundeDto modified, KundeDto original);
        [OperationContract]
        public void GetKunden();
        [OperationContract]
        public void GetKunde(int id);
        [OperationContract]
        public void DeleteKunde(int id);
        [OperationContract]
        public void InsertKunde(KundeDto kunde);

        //Auto
        [OperationContract]
        public void GetAutos();
        [OperationContract]
        public void GetAuto(int id);
        [OperationContract]
        public void UpdateAuto(AutoDto modified, AutoDto original);
        [OperationContract]
        public void DeleteAuto(int id);
        [OperationContract]
        public void InsertAuto(AutoDto auto);
        
        //Reservation
        [OperationContract]
        public void UpdateReservation(ReservationDto modified, ReservationDto original);
        [OperationContract]
        public void GetReservationen();
        [OperationContract]
        public void GetReservation(int id);
        [OperationContract]
        public void DeleteReservation(int id);
        [OperationContract]
        public void InsertReservation(ReservationDto reservation);
        
    }
}
