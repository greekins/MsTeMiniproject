using AutoReservation.TestEnvironment;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        private ReservationViewModel reservationViewModel;
        private KundeViewModel kundeViewModel;
        private AutoViewModel autoViewModel;


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            autoViewModel = new AutoViewModel();
            kundeViewModel = new KundeViewModel();
            reservationViewModel = new ReservationViewModel();
        }

        [TestMethod]
        public void AutosLoadTest()
        {
            Assert.IsTrue(autoViewModel.LoadCommand.CanExecute(null));
            autoViewModel.LoadCommand.Execute(null);
            Assert.AreEqual(3, autoViewModel.Autos.Count);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            Assert.IsTrue(kundeViewModel.LoadCommand.CanExecute(null));
            autoViewModel.LoadCommand.Execute(null);
            Assert.AreEqual(4, kundeViewModel.Kunden.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            Assert.IsTrue(reservationViewModel.LoadCommand.CanExecute(null));
            autoViewModel.LoadCommand.Execute(null);
            Assert.AreEqual(3, reservationViewModel.Reservationen.Count);
        }
    }
}