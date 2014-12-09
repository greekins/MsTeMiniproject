using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using System.Runtime.Serialization;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        List<KundeDto> Kunden
        {
            [OperationContract]
            get;
        }
        List<AutoDto> Autos
        {
            [OperationContract]
            get;
        }
        List<ReservationDto> Reservationen
        {
            [OperationContract]
            get;
        }

        //Kunde
        [OperationContract, FaultContract(typeof(KundeDto))]
        void UpdateKunde(KundeDto modified, KundeDto original);
        [OperationContract]
        KundeDto GetKunde(int id);
        [OperationContract]
        void DeleteKunde(KundeDto kunde);
        [OperationContract]
        void InsertKunde(KundeDto kunde);

        //Auto
        [OperationContract]
        AutoDto GetAuto(int id);
        [OperationContract, FaultContract(typeof(AutoDto))]
        void UpdateAuto(AutoDto modified, AutoDto original);
        [OperationContract]
        void DeleteAuto(AutoDto auto);
        [OperationContract]
        void InsertAuto(AutoDto auto);
        
        //Reservation
        [OperationContract, FaultContract(typeof(ReservationDto))]
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        [OperationContract]
        ReservationDto GetReservation(int id);
        [OperationContract]
        void DeleteReservation(ReservationDto reservation);
        [OperationContract]
        void InsertReservation(ReservationDto reservation);
        
    }
}
