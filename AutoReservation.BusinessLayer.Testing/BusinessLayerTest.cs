using AutoReservation.Dal;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest_DefaultCase()
        {
            var original = Target.GetAuto(3) as LuxusklasseAuto;
            var modified = modifiedAuto(original, 200);
            Target.UpdateAuto(modified, original);
        }

       

        [ExpectedException(typeof(LocalOptimisticConcurrencyException<Auto>))]
        [TestMethod]
        public void UpdateAutoTest_CollisionCase()
        {
            var original1 = Target.GetAuto(3) as LuxusklasseAuto;
            var original2 = Target.GetAuto(3) as LuxusklasseAuto;

            var modified1 = modifiedAuto(original1, 200);
            var modified2 = modifiedAuto(original2, 300);
            
            Target.UpdateAuto(modified1, original1);
            Target.UpdateAuto(modified2, original2);
        }


        [TestMethod]
        public void UpdateKundeTest_DefaultCase()
        {
            var original = Target.GetKunde(1);
            var modified = modifiedKunde(original, "Trocken");
            Target.UpdateKunde(modified, original);
        }

       
        [TestMethod]
        [ExpectedException(typeof(LocalOptimisticConcurrencyException<Kunde>))]
        public void UpdateKundeTest_CollisionCase()
        {
            var original1 = Target.GetKunde(1);
            var original2 = Target.GetKunde(1);

            var modified1 = modifiedKunde(original1, "Trocken");
            var modified2 = modifiedKunde(original2, "Feucht");

            Target.UpdateKunde(modified1, original1);
            Target.UpdateKunde(modified2, original2);
        }



        [TestMethod]
        public void UpdateReservationTest_DefaultCase()
        {
            var original = Target.GetReservation(1);
            var modified = modifiedReservation(original, 1);
            Target.UpdateReservation(modified, original);
        }

      

        [TestMethod]
        [ExpectedException(typeof(LocalOptimisticConcurrencyException<Reservation>))]
        public void UpdateReservationTest_CollisionCase()
        {
            var original1 = Target.GetReservation(1);
            var original2 = Target.GetReservation(1);

            var modified1 = modifiedReservation(original1, 1);
            var modified2 = modifiedReservation(original2, 2);

            Target.UpdateReservation(modified1, original1);
            Target.UpdateReservation(modified2, original2);
        }

        private Reservation modifiedReservation(Reservation original, int addDays)
        {
            return new Reservation
            {
                ReservationNr = original.ReservationNr,
                KundeId = original.KundeId,
                AutoId = original.AutoId,
                Von = original.Von,
                Bis = original.Bis.AddDays(addDays)
            };
        }


        private Kunde modifiedKunde(Kunde original, string newNachname)
        {
            return new Kunde
            {
                Id = original.Id,
                Vorname = original.Vorname,
                Nachname = newNachname,
                Geburtsdatum = original.Geburtsdatum,
                Reservations = original.Reservations
            };
        }

        private Auto modifiedAuto(LuxusklasseAuto original, int newTagestarif)
        {
            return new LuxusklasseAuto
            {
                Id = original.Id,
                Marke = original.Marke,
                Basistarif = original.Basistarif,
                Tagestarif = newTagestarif
            };
        }

    }
}
