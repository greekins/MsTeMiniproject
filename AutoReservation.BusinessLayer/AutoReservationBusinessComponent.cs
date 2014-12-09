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
        public List<Kunde> GetKunden()
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var result = context.Kunden
                    .Include(k => k.Reservations)
                    .ToList();
                return result;
            }
        }

        public Kunde GetKunde(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var kunde = context.Kunden
                    .Include(k => k.Reservations)
                    .SingleOrDefault(k => k.Id == id);
                return kunde;
            }
        }

        public void DeleteKunde(int kundenNr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                Kunde kunde = context.Kunden.SingleOrDefault(r => r.Id == kundenNr);
                context.Kunden.Attach(kunde);
                context.Kunden.Remove(kunde);
                context.SaveChanges();
            }
        }

        public void InsertKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Add(kunde);
                context.SaveChanges();
            }
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
                context.Autos.Attach(auto);
                context.Autos.Remove(auto);
                context.SaveChanges();
            }
        }
        public void InsertAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Add(auto);
                context.SaveChanges();
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
                return context.Reservationen
                    .Include(r => r.Auto)
                    .Include(r => r.Kunde)
                    .ToList();
            }
        }
        public Reservation GetReservation(int reservationNr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                return context.Reservationen
                    .Include(r => r.Auto)
                    .Include(r => r.Kunde)
                    .SingleOrDefault(r => r.ReservationNr == reservationNr);
            }
        }
        public void DeleteReservation(int reservationNr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var reservation = context.Reservationen
                    .SingleOrDefault(r => r.ReservationNr == reservationNr);
                context.Reservationen.Attach(reservation);
                context.Reservationen.Remove(reservation);
                context.SaveChanges();
            }
        }
        public void InsertReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Add(reservation);
                context.SaveChanges();
            }
        }

    }
}