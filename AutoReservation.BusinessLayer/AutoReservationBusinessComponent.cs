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





        public List<Auto> GetAutos()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Autos.ToList();
            }
        }
        public Auto GetAuto(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var auto = context.Autos.SingleOrDefault(a => a.Id == id);
                return auto;
            }
        }
        public void UpdateAuto(Auto modified, Auto original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Autos.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }
        public void DeleteAuto(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var auto = context.Autos.SingleOrDefault(a => a.Id == id);
                context.Autos.Remove(auto);
            }
        }
        public void InsertAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Add(auto);
            }
        }


        public void UpdateReservation(Reservation modified, Reservation original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Reservationen.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }
        public List<Reservation> GetReservationen()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Reservationen.ToList();
            }
        }
        public Reservation GetReservation(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Reservationen.SingleOrDefault(r => r.ReservationNr == id);
            }
        }
        public void DeleteReservation(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var reservation = context.Reservationen.SingleOrDefault(r => r.ReservationNr == id);
                context.Reservationen.Remove(reservation);
            }
        }
        public void InsertReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Add(reservation);
            }
        }

    }
}