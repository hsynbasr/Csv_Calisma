using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ConsoleTables;

namespace FilmProjesi
{
    class Program
    {
        static List<Movie> movies = new List<Movie>();
        static List<Movie> moviesFilter = new List<Movie>();
        static List<string> withoutRepeat = new List<string>();

        static void Main(string[] args)
        {

            DataParcalama();
            bool devam = true;
            while (devam)
            {
                Console.WriteLine("(1)  - Yıl bilgisine göre listeleme. (Örn. 2002 tarihli filmler)");
                Console.WriteLine("(2)  - Ülke bilgisine göre listeleme (Örn. Ülkesi UK olan filmler)");
                Console.WriteLine("(3)  - Süre bilgisine göre listeleme (Örn. Süresi ....'den fazla/az eşit olan filmler)");
                Console.WriteLine("(4)  - Imdb skoruna göre listeleme (Örn. Imdb skoru ....'den fazla/az eşit olan filmler)");
                Console.WriteLine("(5)  - Tür bilgisine göre listeleme (Örn. Adventure türündeki filmler)");
                Console.WriteLine("(6)  - Film adı üzerinden sorgulanan film listesi (Örn. Adında .... geçen filmler filmler)");
                Console.WriteLine("(7)  - Yönetmen Listesi");
                Console.WriteLine("(8)  - Ülkelerin Listesi");
                Console.WriteLine("(9)  - Türlerin Listelenmesi");
                Console.WriteLine("(10) - İstatistiksel Bilgiler");
                Console.WriteLine("(X)  - ÇIKIŞ ");
                string AnaMenu = Console.ReadLine();
                switch (AnaMenu)
                {
                    case "1":
                        Movie.YilaGoreListeleme(movies, moviesFilter);
                        EkranaYazdirma();
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "2":
                        Movie.UlkeyeGoreSiralama(movies, moviesFilter);
                        EkranaYazdirma();
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "3":
                        Movie.SureBilgisineGoreSıralama(movies, moviesFilter);
                        EkranaYazdirma();
                        break;
                    case "4":
                        Movie.ImdbBilgisineGoreSiralama(movies, moviesFilter);
                        EkranaYazdirma();
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "5":
                        Movie.TurBilgisineGoreSiralama(movies, moviesFilter);
                        EkranaYazdirma();
                        break;
                    case "6":
                        Movie.FilmAdinaGoreSiralama(movies, moviesFilter);
                        EkranaYazdirma();
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "7":
                        Movie.YonetmenListeleme(movies);
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "8":
                        Movie.UlkeListeleme(movies);
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "9":
                        Movie.TurlerinListelenmesi(movies);
                        Console.WriteLine("Devam etmek için bir tuşa basınız.");
                        Console.ReadKey();
                        break;
                    case "10":
                        Console.WriteLine("(1) - Yıllara göre film sayılar");
                        Console.WriteLine("(2) - Ülkelere göre film sayıları");
                        Console.WriteLine("(3) - Türlere göre film sayıları");
                        string istatistikMenu = Console.ReadLine();
                        switch (istatistikMenu)
                        {
                            case "1":
                                Movie.YillaraGoreFilmSayisi(movies);
                                Console.WriteLine("Devam etmek için bir tuşa basınız.");
                                Console.ReadKey();
                                break;
                            case "2":
                                Movie.UlkelereGoreFilmSayisi(movies);
                                Console.WriteLine("Devam etmek için bir tuşa basınız.");
                                Console.ReadKey();
                                break;
                            case "3":
                                Movie.TurlereGoreFilmSayisi(movies);
                                Console.WriteLine("Devam etmek için bir tuşa basınız.");
                                Console.ReadKey();
                                break;
                            default:
                                Console.WriteLine("Hatalı seçim!!!");
                                break;
                        }
                        break;
                    case "X":
                        devam = false; break;
                    default:
                        Console.WriteLine("Hatalı giriş!!!");
                        break;
                }
            }
        }
        public static void DataParcalama()
        {
            string[] films = File.ReadAllLines("movie_database.csv");
            foreach (var item in films)
            {
                Movie film = new Movie();
                string[] filmInformation = item.Split(';');
                film.directorName = filmInformation[0];
                if (int.TryParse(filmInformation[1], out int durationSayi))
                    film.duration = durationSayi;
                film.actor_2_Name = filmInformation[2];
                film.genres = filmInformation[3];
                film.actor_1_Name = filmInformation[4];
                film.movieTitle = filmInformation[5];
                film.numVotedUsers = filmInformation[6];
                film.actor_3_Name = filmInformation[7];
                film.movieImdbLink = filmInformation[8];
                film.numUserForReviews = filmInformation[9];
                film.language = filmInformation[10];
                film.country = filmInformation[11].ToUpper();
                if (int.TryParse(filmInformation[12], out int yearSayi))
                    film.title_year = yearSayi;
                if (DoubleCevrim(filmInformation[13], out double imdbSayi))
                {
                    if (imdbSayi > 10)
                        film.imdb_score = (imdbSayi / 10);
                    else
                        film.imdb_score = (imdbSayi);
                }
                movies.Add(film);
            }
        }
        public static bool DoubleCevrim(string s, out double sayi)
        {
            try
            {
                sayi = Convert.ToDouble(s);
                return true;
            }
            catch (System.Exception)
            {
                sayi = 0;
                return false;
            }
        }

        public static void EkranaYazdirma()
        {
            Console.WriteLine("Listeleniyor...");
            Thread.Sleep(1000);
            Console.Clear();
            // 
            Console.WriteLine("=============================================");
            var tabloOpsiyon = new ConsoleTableOptions
            {
                Columns = new[] { "-Film Adı-", "-Yönetmen-", "-İmdb Puanı-", "-Film Süresi-", " Film Türü" }, //,"İmdb Puanı", " Film Süresi", " Film Türü"
                EnableCount = true
            };
            var filmTable = new ConsoleTable(tabloOpsiyon);

            foreach (var film in moviesFilter)
            {
                filmTable.AddRow(film.movieTitle, film.directorName, film.imdb_score, film.duration, film.genres); //, film.imdb_score, film.duration, " Film Türü"
            }

            filmTable.Write();

            // Console.WriteLine("===================================================");
            // foreach (var item in moviesFilter)
            // {
            //     Console.WriteLine($"---({item.movieTitle}) -- Yönetmen: {item.directorName} -- Imdb Score:{item.imdb_score}/10 - Genres: {item.genres}  -- Film Süresi :{item.duration}");
            // }
            // Console.WriteLine("===================================================");
            // Console.WriteLine($"{moviesFilter.Count} Film Listelendi");
            // Console.WriteLine("===================================================");
            // moviesFilter.Clear();
        }



    }
}

