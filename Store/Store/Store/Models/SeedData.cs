using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Store.Models;
using Store.Data;
namespace Store.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<StoreContext>>()))
            {
                // Look for any movies.
                if (context.Kniga.Any() || context.Avtor.Any())
                {
                    return; // DB has been seeded
                }
                context.Avtor.AddRange(
                new Avtor { /*Id = 1, */Ime = "Billy", Prezime = "Crystal",Pol="Masko", Nacionalnost="Srbin", DatumRagjanje = DateTime.Parse("1948-3-14") },
                new Avtor { /*Id = 2, */Ime = "Meg", Prezime = "Ryan", Pol = "Zensko", Nacionalnost = "Makedonec", DatumRagjanje = DateTime.Parse("1961-11-19") },
                new Avtor { /*Id = 3, */Ime = "Carrie", Prezime = "Fisher", Pol = "Zensko", Nacionalnost = "Bosanec", DatumRagjanje = DateTime.Parse("1956-10-21") },
                new Avtor { /*Id = 4, */Ime = "Bill", Prezime = "Murray", Pol = "Masko", Nacionalnost = "Srbin", DatumRagjanje = DateTime.Parse("1950-9-21") },
                new Avtor { /*Id = 5, */Ime = "Dan", Prezime = "Aykroyd", Pol = "Zensko", Nacionalnost = "Bosanec", DatumRagjanje = DateTime.Parse("1952-7-1") },
                new Avtor { /*Id = 6, */Ime = "Sigourney", Prezime = "Weaver", Pol = "Masko", Nacionalnost = "Bosanec", DatumRagjanje = DateTime.Parse("1949-11-8") }
                );
                context.SaveChanges();
                context.Kniga.AddRange(
                new Kniga
                {
                    //Id = 1,
                    Naslov = "When Harry Met Sally",
                    Godina=1999,
                    BrStrani=100,
                    Opis="Book1",
                    Zanr="Comedy",
                    Tirazh=11,
                    Izdavac="Koki1",
                    SlikaUrl= "https://edit.org/images/cat/book-covers-big-2019101610.jpg"
                },
                 new Kniga
                 {
                     //Id = 2,
                     Naslov = "Dracula",
                     Godina = 2000,
                     BrStrani = 110,
                     Opis = "Book2",
                     Zanr = "Horror",
                     Tirazh = 12,
                     Izdavac = "Koki2",
                     SlikaUrl = "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1493044059l/1427.jpg"
                 },
                  new Kniga
                  {
                      //Id = 3,
                      Naslov = "Bella",
                      Godina = 2001,
                      BrStrani = 120,
                      Opis = "Book3",
                      Zanr = "Drama",
                      Tirazh = 13,
                      Izdavac = "Koki3",
                      SlikaUrl = "https://cdn2.penguin.com.au/covers/original/9780141949055.jpg"
                  },
                   new Kniga
                   {
                       //Id = 4,
                       Naslov = "Sidartha",
                       Godina = 2002,
                       BrStrani = 130,
                       Opis = "Book4",
                       Zanr = "Fantasy",
                       Tirazh = 14,
                       Izdavac = "Koki4",
                       SlikaUrl = " https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1387151694i/17245.jpg"
                   }/*
                    new Kniga
                    {
                        //Id = 5,
                        Naslov = "Anna",
                        Godina = 2003,
                        BrStrani = 140,
                        Opis = "Book5",
                        Zanr = "Action",
                        Tirazh = 15,
                        Izdavac = "Koki5",
                        SlikaUrl = "https://m.media-amazon.com/images/I/81EU992zdwL._AC_UF1000,1000_QL80_.jpg "
                    }
                   */
                );
                context.SaveChanges();
                context.AvtorKniga.AddRange
               (
                new AvtorKniga { AvtorId = 1, KnigaId = 1 },
                new AvtorKniga { AvtorId = 2, KnigaId = 1 },
                new AvtorKniga { AvtorId = 3, KnigaId = 1 },
                new AvtorKniga { AvtorId = 4, KnigaId = 2 },
                new AvtorKniga { AvtorId = 5, KnigaId = 2 },
                new AvtorKniga { AvtorId = 6, KnigaId = 2 },
                new AvtorKniga { AvtorId = 4, KnigaId = 3 },
                new AvtorKniga { AvtorId = 5, KnigaId = 3 },
                new AvtorKniga { AvtorId = 6, KnigaId = 3 },
                new AvtorKniga { AvtorId = 1, KnigaId = 4 }
                //new AvtorKniga { AvtorId = 7, KnigaId = 4 }
                // new AvtorKniga { AvtorId = 1, KnigaId = 5 }

                );
                context.SaveChanges();

            }

        }

    }
}
