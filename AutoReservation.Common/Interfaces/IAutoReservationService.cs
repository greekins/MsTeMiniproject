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
        void UpdateKunde(KundeDto modified, KundeDto original);
        [OperationContract]
        List<KundeDto> GetKunden();
        [OperationContract]
        KundeDto GetKunde(int id);
        [OperationContract]
        void DeleteKunde(int id);
        [OperationContract]
        void InsertKunde(KundeDto kunde);

        //Auto
        [OperationContract]
        List<AutoDto> GetAutos();
        [OperationContract]
        AutoDto GetAuto(int id);
        [OperationContract]
        void UpdateAuto(AutoDto modified, AutoDto original);
        [OperationContract]
        void DeleteAuto(int id);
        [OperationContract]
        void InsertAuto(AutoDto auto);
        
        //Reservation
        [OperationContract]
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        [OperationContract]
        List<ReservationDto> GetReservationen();
        [OperationContract]
        ReservationDto GetReservation(int id);
        [OperationContract]
        void DeleteReservation(int id);
        [OperationContract]
        void InsertReservation(ReservationDto reservation);
        
    }
}
