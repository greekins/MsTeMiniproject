using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }

        public void UpdateKunde(Kunde modified, Kunde original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Kunden.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {   
                    HandleDbConcurrencyException(context, original);
                }
                    
            }
            
        }
        public IEnumerable<Kunde> GetKunden()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                IEnumerable<Kunde> result = context.Kunden.ToList();
                return result;
            }
        }

        public Kunde GetKundeByNr(int kundenNr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                Kunde result = context.Kunden
                    .SingleOrDefault(r => r.Id == kundenNr);
                return result;
            }
        }

        public void DeleteKunde(int kundenNr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                Kunde kunde = context.Kunden
                    .SingleOrDefault(r => r.Id == kundenNr);
                context.Kunden.Remove(kunde);
            }
        }

        public void InsertKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Add(kunde);
            }
        }
    }
}