using DAL_Lascaux_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL_Lascaux_Demo.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
         
        }

        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaRoom> CinemaRooms { get; set; }
        private TimeSlot _timeSlot = new();

        /// <summary>
        /// Fills the database once
        /// </summary>
        /// <returns></returns>
        public async Task fillDb()
        {
            #region rooms
            CinemaRoom Room1 = new("Sala 1", 75, RoomTypes.Regular);
            CinemaRoom Room2 = new( "Sala 2", 150, RoomTypes.Regular);
            CinemaRoom Room3 = new("Sala 3", 65, RoomTypes.Regular);
            CinemaRoom Room4 = new("Sala 4", 100, RoomTypes.Regular);
            CinemaRoom Room5 = new("Sala 5", 160, RoomTypes.Regular);
            CinemaRoom Room6 = new("Sala 6", 80, RoomTypes.Regular);
            CinemaRoom Room7 = new("Sala 7", 200, RoomTypes.IMAX);
            CinemaRoom Room8 = new("Sala 8", 50, RoomTypes.Regular);
            CinemaRoom Room9 = new("Sala 9", 120, RoomTypes.Regular);
            CinemaRoom Room10 = new("Sala 10", 180, RoomTypes.Regular);
            CinemaRoom Room11 = new("Sala 11", 250, RoomTypes.IMAX);
            CinemaRoom Room12 = new("Sala 12", 75, RoomTypes.Regular);

            CinemaRooms.AddRange(Room1, Room2, Room3, Room4, Room5, Room6, Room7, Room8, Room9, Room10, Room11, Room12);
            #endregion

            #region movies
            
            //Film divisi per data di uscita

            //14 nov
            Movie WakandForever = new("Wakanda Forever", new DateTime(2022, 11, 14), "https://m.media-amazon.com/images/M/MV5BNTM4NjIxNmEtYWE5NS00NDczLTkyNWQtYThhNmQyZGQzMjM0XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_SX300.jpg");
            Movie Spirited = new("Spirited", new DateTime(2022, 11, 14), "https://m.media-amazon.com/images/M/MV5BNjRiOGMzOTEtNTJjMS00N2IxLWFmNjMtOTJlOWJlZjM3NTNhXkEyXkFqcGdeQXVyMTk3NDAwMzI@._V1_SX300.jpg");
            Movie EnolaHolmes2 = new("Enola Holmes 2", new DateTime(2022, 11, 14), "https://m.media-amazon.com/images/M/MV5BMDI1NWM1ZDItNDFhMi00YWRhLTg1YzItNTNhY2M2N2QzY2FkXkEyXkFqcGdeQXVyMTEyMjM2NDc2._V1_SX300.jpg");
            Movie TinderSwindler = new("The Tinder Swindler", new DateTime(2022, 11, 14), "https://m.media-amazon.com/images/M/MV5BMTkwMTg2YWYtOGU5MS00YTdhLTg4N2QtYzcyZDE0MTlmNDU3XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg");
            Movie Moonfall = new("Moonfall", new DateTime(2022, 11, 14), "https://m.media-amazon.com/images/M/MV5BZjk0OWZiN2ItNmQ2YS00NTJmLTg0MjItNzM4NzBkMWM1ZTRlXkEyXkFqcGdeQXVyMjMxOTE0ODA@._V1_SX300.jpg");

            //28 nov
            Movie Prey = new("Prey", new DateTime(2022, 11, 28), "https://m.media-amazon.com/images/M/MV5BMDBlMDYxMDktOTUxMS00MjcxLWE2YjQtNjNhMjNmN2Y3ZDA1XkEyXkFqcGdeQXVyMTM1MTE1NDMx._V1_SX300.jpg");
            Movie AdamProject = new("The Adam Project", new DateTime(2022, 11, 28), "https://m.media-amazon.com/images/M/MV5BOWM0YWMwMDQtMjE5NS00ZTIwLWE1NWEtODViMWZjMWI2OTU3XkEyXkFqcGdeQXVyMTEyMjM2NDc2._V1_SX300.jpg");
            //5 dic
            Movie TurningRed = new("Turning Red", new DateTime(2022, 12, 5), "https://m.media-amazon.com/images/M/MV5BOWYxZDMxYWUtNjNiZC00MDE1LWI2Y2QtNWZhNDAyMGY5ZjVhXkEyXkFqcGdeQXVyODE5NzE3OTE@._V1_SX300.jpg");

            //12 dic
            Movie Blonde = new("Blonde", new DateTime(2022, 12, 12), "https://m.media-amazon.com/images/M/MV5BNDk2YTA1MGYtMGNjMi00YTJlLWI1YjItMjBjOGJlZGIwZmYzXkEyXkFqcGdeQXVyODA0MjgyNzM@._V1_SX300.jpg");

            //19 dic
            Movie TheLostCity = new("The Lost City", new DateTime(2022, 12, 19), "https://m.media-amazon.com/images/M/MV5BMmIwYzFhODAtY2I1YS00ZDdmLTkyYWQtZjI5NDIwMDc2MjEyXkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_SX300.jpg");
            Movie BulletTrain = new("Bullet Train", new DateTime(2022, 12, 19), "https://m.media-amazon.com/images/M/MV5BMDU2ZmM2OTYtNzIxYy00NjM5LTliNGQtN2JmOWQzYTBmZWUzXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_SX300.jpg");

            Movie JackassForever = new("Jackass Forever", new DateTime(2022, 12, 19), "https://m.media-amazon.com/images/M/MV5BMGViZGEzZWUtY2JjZC00MjRlLWI2NDktM2VlZTA2NzcwMzE2XkEyXkFqcGdeQXVyNjc5NjEzNA@@._V1_SX300.jpg");
            Movie TopGun2 = new("Top Gun: Maverick", new DateTime(2022, 12, 19), "https://m.media-amazon.com/images/M/MV5BZWYzOGEwNTgtNWU3NS00ZTQ0LWJkODUtMmVhMjIwMjA1ZmQwXkEyXkFqcGdeQXVyMjkwOTAyMDU@._V1_SX300.jpg");
            Movie TheDivision = new("The Division", new DateTime(2022, 12, 19), "https://m.media-amazon.com/images/M/MV5BMzE0OGYwYTMtMDAwYy00OWIyLThmZjUtOWEyODU2OWQxNWJiXkEyXkFqcGdeQXVyMTkzODUwNzk@._V1_SX300.jpg");
            //26 dic
            Movie BlackAdam = new("Black Adam", new DateTime(2022, 12, 26), "https://m.media-amazon.com/images/M/MV5BYzZkOGUwMzMtMTgyNS00YjFlLTg5NzYtZTE3Y2E5YTA5NWIyXkEyXkFqcGdeQXVyMjkwOTAyMDU@._V1_SX300.jpg");
            Movie Morbius = new("Morbius", new DateTime(2022, 12, 26), "https://m.media-amazon.com/images/M/MV5BNTA3N2Q0ZTAtODJjNy00MmQzLWJlMmItOGFmNDI0ODgxN2QwXkEyXkFqcGdeQXVyMTM0NTUzNDIy._V1_SX300.jpg");

            Movies.AddRange(WakandForever, Spirited, EnolaHolmes2, Prey, AdamProject, TurningRed, Blonde,
                BulletTrain, TheLostCity, TinderSwindler, Moonfall, JackassForever, TopGun2, TheDivision, BlackAdam, Morbius);
            #endregion

            //14 nov start
            List<TimeSlot> s1 = TimeSlot.CreateMultiRoomSlot(WakandForever, 2, new(){ Room6, Room11 });
            List<TimeSlot> s10 = TimeSlot.CreateMultiRoomSlot(Moonfall, 2, new() { Room1, Room5 });
            List<TimeSlot> s4 = TimeSlot.CreateMultiRoomSlot(Prey, 3, new() { Room1, Room5, Room6, Room11 });
            List<TimeSlot> s9 = TimeSlot.CreateMultiRoomSlot(BulletTrain, 1, new() { Room1, Room5, Room6, Room11 });

            //14 nov start
            List<TimeSlot> s2 = TimeSlot.CreateMultiRoomSlot(Spirited, 3, new() { Room7, Room8 });
            List<TimeSlot> s11 = TimeSlot.CreateMultiRoomSlot(TinderSwindler, 3, new() { Room2, Room4 });
            List<TimeSlot> s5 = TimeSlot.CreateMultiRoomSlot(TurningRed, 1, new() { Room2, Room4, Room7, Room8 });
            List<TimeSlot> s7 = TimeSlot.CreateMultiRoomSlot(Blonde, 2, new() { Room2, Room4, Room7, Room8 });

            //14 nov start
            List<TimeSlot> s3 = TimeSlot.CreateMultiRoomSlot(EnolaHolmes2, 2, new() { Room3, Room9, Room10, Room12 });
            List<TimeSlot> s6 = TimeSlot.CreateMultiRoomSlot(AdamProject, 3, new() { Room3, Room9, Room10, Room12 });
            List<TimeSlot> s8 = TimeSlot.CreateMultiRoomSlot(TheLostCity, 1, new() { Room3, Room9, Room10, Room12 });

            TimeSlots.AddRange(s1);
            TimeSlots.AddRange(s2);
            TimeSlots.AddRange(s3);
            TimeSlots.AddRange(s4);
            TimeSlots.AddRange(s5);
            TimeSlots.AddRange(s6);
            TimeSlots.AddRange(s7);
            TimeSlots.AddRange(s8);
            TimeSlots.AddRange(s9);
            TimeSlots.AddRange(s10);
            TimeSlots.AddRange(s11);

            await SaveChangesAsync();
        }

    }
}