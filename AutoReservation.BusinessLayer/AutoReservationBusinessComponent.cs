using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        //private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        //{
        //    var databaseValue = context.Entry(original).GetDatabaseValues();
        //    context.Entry(original).CurrentValues.SetValues(databaseValue);

        //    throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        //}

    }
}