using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void AutosTest()
        {
            var autos = Target.GetAutos();
            Assert.AreEqual(3, autos.Count);
        }

        [TestMethod]
        public void KundenTest()
        {
            var kunden = Target.GetKunden();
            Assert.AreEqual(4, kunden.Count);
        }

        [TestMethod]
        public void ReservationenTest()
        {
            var reservationen = Target.GetReservationen();
            Assert.AreEqual(3, reservationen.Count);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAuto(1);
            Assert.IsNotNull(auto);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var auto = Target.GetAuto(1);
            Assert.IsNotNull(auto);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            var reservation = Target.GetReservation(1);
            Assert.IsNotNull(reservation);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            var reservation = Target.GetReservation(100);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            var auto = new AutoDto
            {
                Basistarif = 0,
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 100,
                Marke = "BMW 130i",
            };
            Target.InsertAuto(auto);
            var autoRetrieved = Target.GetAutos().FirstOrDefault(a =>
                a.Marke == auto.Marke);
            Assert.IsNotNull(autoRetrieved);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            var kunde = new KundeDto
            {
                Vorname = "Max",
                Nachname = "Muster",
                Geburtsdatum = DateTime.Parse("1950-12-31 00:00:00")
            };
            Target.InsertKunde(kunde);
            var kundeRetrieved = Target.GetKunden().FirstOrDefault(k => 
                k.Vorname == kunde.Vorname && 
                k.Nachname == kunde.Nachname);
            Assert.IsNotNull(kundeRetrieved);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            var auto = Target.GetAutos().First();
            var kunde = Target.GetKunden().Last();
            var reservation = new ReservationDto()
            {
                Auto = auto,
                Kunde = kunde,
                Von = DateTime.Now,
                Bis = DateTime.Now.AddDays(7)
            };
            Target.InsertReservation(reservation);
            var reservationRetrieved = Target.GetReservationen().FirstOrDefault(r =>
                r.Kunde.Id == kunde.Id &&
                r.Auto.Id == auto.Id);
            Assert.IsNotNull(reservationRetrieved);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            var original = Target.GetAutos().First();
            var modified = modifiedAuto(original, 200);
            Target.UpdateAuto(modified, original);
        }

        private AutoDto modifiedAuto(AutoDto original, int newTagestarif)
        {
            return new AutoDto
            {
                AutoKlasse = original.AutoKlasse,
                Id = original.Id,
                Marke = original.Marke,
                Basistarif = original.Basistarif,
                Tagestarif = newTagestarif
            };
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var original = Target.GetKunden().First();
            var modified = modifiedKunde(original, "TEST-NACHNAME-1");
            Target.UpdateKunde(modified, original);
        }

        private KundeDto modifiedKunde(KundeDto original,string newNachname)
        {
            return new KundeDto 
            {
                Id = original.Id,
                Nachname = newNachname,
                Vorname = original.Vorname,
                Geburtsdatum = original.Geburtsdatum
            };
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var original = Target.GetReservationen().First();
            var modified = modifiedReservation(original, 1);
            Target.UpdateReservation(modified, original);
        }

        private ReservationDto modifiedReservation(ReservationDto original, int addDays)
        {
 	        return new ReservationDto 
            {
                ReservationNr = original.ReservationNr,
                Auto = original.Auto,
                Kunde = original.Kunde,
                Von = original.Von,
                Bis = original.Bis.AddDays(addDays)
            };
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            var original1 = Target.GetAuto(1);
            var original2 = Target.GetAuto(1);

            var modified1 = modifiedAuto(original1, 200);
            var modified2 = modifiedAuto(original2, 300);

            Target.UpdateAuto(modified1, original1);
            Target.UpdateAuto(modified2, original2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>))]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            var original1 = Target.GetKunde(1);
            var original2 = Target.GetKunde(1);

            var modified1 = modifiedKunde(original1, "TEST-NACHNAME-1");
            var modified2 = modifiedKunde(original2, "TEST-NACHNAME-2");

            Target.UpdateKunde(modified1, original1);
            Target.UpdateKunde(modified2, original2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            var original1 = Target.GetReservation(1);
            var original2 = Target.GetReservation(1);

            var modified1 = modifiedReservation(original1, 1);
            var modified2 = modifiedReservation(original2, 2);

            Target.UpdateReservation(modified1, original1);
            Target.UpdateReservation(modified2, original2);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            var kunde = Target.GetKunde(1);
            Assert.IsNotNull(kunde);

            Target.DeleteKunde(1);
            Assert.IsNull(Target.GetKunde(1));
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            var auto = Target.GetAuto(1);
            Assert.IsNotNull(auto);

            Target.DeleteAuto(1);
            Assert.IsNull(Target.GetAuto(1));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            var reservation = Target.GetReservation(1);
            Assert.IsNotNull(reservation);

            Target.DeleteReservation(1);
            Assert.IsNull(Target.GetReservation(1));
        }
    }
}
