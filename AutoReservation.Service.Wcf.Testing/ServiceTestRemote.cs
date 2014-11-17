using AutoReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestRemote : ServiceTestBase
    {
        private static ServiceHost serviceHost;
        
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(AutoReservationService));
            serviceHost.Open();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            if (serviceHost.State != CommunicationState.Closed)
                serviceHost.Close();
        }

        private IAutoReservationService target;
        protected override IAutoReservationService Target
        {
            get
            {
                if (target == null)
                {
                    ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    target = channelFactory.CreateChannel();
                }
                return target;
            }
        }

    }
}