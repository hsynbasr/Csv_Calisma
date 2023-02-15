using System;
using System.Collections.Generic;
using System.Threading;

namespace FilmProjesi
{
    class Movie
    {
        public string movieTitle;
        public int duration;
        public string directorName;
        public string actor_1_Name;
        public string actor_2_Name;
        public string actor_3_Name;
        public string numVotedUsers;
        public string movieImdbLink;
        public string language;
        public string numUserForReviews;
        public string country;
        public int title_year;
        public double imdb_score;

        public string genres;
        public Movie()
        {
            movieTitle = "";
            directorName = "";
            actor_1_Name = "";
            actor_2_Name = "";
            actor_3_Name = "";
            numVotedUsers = "";
            movieImdbLink = "";
            language = "";
            numUserForReviews = "";
            country = "";
        }


        public static void YilaGoreListeleme(List<Movie> movieList, List<Movie> filter) //movies referans heap
        {
            Console.WriteLine("Yılı yazınız:  ");
            int yil = Convert.ToInt32(Console.ReadLine());
            foreach (var item in movieList)
            {
                if (yil == item.title_year)
                {
                    filter.Add(item);
                }
            }
        }
        public static void UlkeyeGoreSiralama(List<Movie> movieList, List<Movie> filter)
        {

            Console.WriteLine("Ülke Giriniz");
            string ulke = Console.ReadLine().ToUpper();
            foreach (var item in movieList)
            {
                if (ulke == item.country)
                {
                    filter.Add(item);
                }

            }

        }
        public static void SureBilgisineGoreSıralama(List<Movie> movieList, List<Movie> filter)
        {
            Console.WriteLine("Süre Giriniz :");
            string sure = Console.ReadLine();
            if (int.TryParse(sure, out int filmSure))
            {
                Console.WriteLine($"(1) Film Süresi {sure} dk olan filmler");
                Console.WriteLine($"(2) Film Süresi {sure} dk dan az olan filmler");
                Console.WriteLine($"(3) Film Süresi {sure} dk dan fazla olan filmler");
                string sureKarar = Console.ReadLine();
                switch (sureKarar)
                {
                    case "1":
                        foreach (var film in movieList)
                        {
                            if (film.duration == filmSure)
                            {
                                filter.Add(film);
                            }
                        }
                        break;
                    case "2":
                        foreach (var film in movieList)
                        {
                            if (film.duration < filmSure)
                            {
                                filter.Add(film);
                            }
                        }
                        break;
                    case "3":
                        foreach (var film in movieList)
                        {
                            if (film.duration > filmSure)
                            {
                                filter.Add(film);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Hatalı giriş!!!"); break;
                }

            }
        }
        public static void ImdbBilgisineGoreSiralama(List<Movie> movieList, List<Movie> filter)
        {
            Console.WriteLine("Imdb puan giriniz :");
            string imdb = Console.ReadLine();
            if (Program.DoubleCevrim(imdb, out double imdbSayi))
            {
                Console.WriteLine($"(1) imdb puanı {imdbSayi}/10 olan filmler");
                Console.WriteLine($"(2) imdb {imdbSayi}/10  dan az olan filmler");
                Console.WriteLine($"(2) imdb {imdbSayi}/10  dan fazla olan filmler");
                string imdbKarar = Console.ReadLine();
                switch (imdbKarar)
                {
                    case "1":
                        foreach (var film in movieList)
                        {
                            if (film.imdb_score == imdbSayi)
                            {
                                filter.Add(film);
                            }
                        }
                        break;
                    case "2":
                        foreach (var film in movieList)
                        {
                            if (film.imdb_score < imdbSayi)
                            {
                                filter.Add(film);
                            }
                        }
                        break;
                    case "3":
                        foreach (var film in movieList)
                        {
                            if (film.imdb_score > imdbSayi)
                            {
                                filter.Add(film);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Hatalı giriş!!!"); break;
                }
            }
        }
        public static void TurBilgisineGoreSiralama(List<Movie> movieList, List<Movie> filter)
        {
            Console.WriteLine("Hangi tür de film istersiniz?");
            string filmTur = Console.ReadLine().ToUpper();
            foreach (var film in movieList)
            {
                string[] turler = film.genres.Split('|');
                for (int i = 0; i < turler.Length; i++)
                {
                    if (turler[i].ToUpper() == filmTur)
                    {
                        filter.Add(film);
                    }
                }
            }
        }
        public static void FilmAdinaGoreSiralama(List<Movie> movieList, List<Movie> filter)
        {
            Console.WriteLine("Film Adı giriniz");
            string ad = Console.ReadLine().ToUpper();
            foreach (var item in movieList)
            {
                if (item.movieTitle.ToUpper().Contains(ad))
                {
                    filter.Add(item);
                }
            }
        }
        public static void YonetmenListeleme(List<Movie> movieList)
        {
            List<string> yonetmenler = new List<string>();
            foreach (var item in movieList)
            {
                int iceriyorMu = yonetmenler.IndexOf(item.directorName);
                if (iceriyorMu < 0 && item.directorName != "")
                {
                    yonetmenler.Add(item.directorName);
                    Console.WriteLine("-----" + item.directorName);
                }
            }
            Console.WriteLine("---------------------");
            Console.WriteLine($"Listelenen yönetmen sayisi : {yonetmenler.Count}");
        }
        public static void UlkeListeleme(List<Movie> movieList)
        {
            List<string> ulkeler = new List<string>();
            foreach (var item in movieList)
            {
                int iceriyorMu = ulkeler.IndexOf(item.country);
                if (iceriyorMu < 0 && item.country != "")
                {
                    ulkeler.Add(item.country);
                    Console.WriteLine("-----" + item.country + "-----");
                }
            }
            Console.WriteLine("---------------------");
            Console.WriteLine($"Listelenen Ulke sayisi : {ulkeler.Count}");
        }
        public static void TurlerinListelenmesi(List<Movie> movieList)
        {
            List<string> turler = new List<string>();
            foreach (var item in movieList)
            {
                string[] tur = item.genres.Split('|');
                for (int i = 0; i < tur.Length; i++)
                {
                    int iceriyorMu = turler.IndexOf(tur[i]);
                    if (iceriyorMu < 0)
                    {
                        turler.Add(tur[i]);
                        Console.WriteLine("--" + tur[i]);
                    }
                }
            }
            Console.WriteLine("---------------------");
            Console.WriteLine($"Listelenen Tür Sayisi : {turler.Count}");
        }
        public static void YillaraGoreFilmSayisi(List<Movie> movieList)
        {
            List<int> yil = new List<int>();

            foreach (var item in movieList)
            {
                int iceriyorMu = yil.IndexOf(item.title_year);
                if (iceriyorMu < 0 && item.title_year != 0)
                {
                    yil.Add(item.title_year);
                }
            }
            yil.Sort();
            int tekrar = 0;
            foreach (var item in yil)
            {
                foreach (var item1 in movieList)
                {
                    if (item == item1.title_year)
                    {
                        tekrar++;
                    }

                }
                Console.WriteLine($"{item} Yılına ait {tekrar} film vardır");
                tekrar = 0;
            }

        }
        public static void UlkelereGoreFilmSayisi(List<Movie> movieList)
        {
            List<string> ulke = new List<string>();
            foreach (var item in movieList)
            {
                int iceriyorMu = ulke.IndexOf(item.country);
                if (iceriyorMu < 0 && item.country != "")
                {
                    ulke.Add(item.country);
                }
            }
            ulke.Sort();
            int tekrar = 0;
            foreach (var item in ulke)
            {
                foreach (var item1 in movieList)
                {
                    if (item == item1.country)
                    {
                        tekrar++;
                    }
                }
                Console.WriteLine($"({item}) Ulkesine ait {tekrar} film vardır");
                tekrar = 0;
            }

        }
        public static void TurlereGoreFilmSayisi(List<Movie> movieList)
        {
            List<string> turler = new List<string>();
            foreach (var item in movieList)
            {
                string[] tur = item.genres.Split('|');
                for (int i = 0; i < tur.Length; i++)
                {
                    int iceriyorMu = turler.IndexOf(tur[i]);
                    if (iceriyorMu < 0)
                        turler.Add(tur[i]);
                }
            }
            int sayi = 0;
            Console.WriteLine("------------------------------------------");
            foreach (var item in turler)
            {
                foreach (var item1 in movieList)
                {
                    if (item1.genres.Contains(item))
                    {
                        sayi++;
                    }
                }
                Console.WriteLine($"--({item}) Film Türüne ait {sayi} film vardır");
                sayi = 0;
            }
            Console.WriteLine("--------------------------------------------");

        }


    }


}


