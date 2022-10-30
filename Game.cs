using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trzeci
{
    internal class Game
    {
        private Player dog = new Player { HP = 7, DMG = 5, Name = "dog", X = 86 };
        private Player cat = new Player { HP = 5, DMG = 7, Name = "cat", X = 5 };
        public readonly int y = 28;
        public int wind = 0;
        public bool turn_wind = true;

        //Poczatek rozgrywki
        public Player StartGame()
        {
            Random random = new Random();

            Console.Clear();
            while (true)
            {
                if (turn_wind == true)
                    wind = random.Next(-10, 10);

                Console.ForegroundColor = ConsoleColor.Blue;
                DisplayPlayer(cat);
                Console.ForegroundColor = ConsoleColor.Red;
                DisplayPlayer(dog);
                DisplayWind();

                //Gracz 1 - kot rozgrywa 
                int v = ThrowPower();
                DisplayThrowCat(v + wind, 5, 1);
                if (dog.HP <= 0) return cat;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Blue;
                DisplayPlayer(dog);
                Console.ForegroundColor = ConsoleColor.Red;
                DisplayPlayer(cat);
                DisplayWind();

                //Gracz 2 - pies rozgrywa 
                v = ThrowPower();
                DisplayThrowDog(v + wind, 81, 1);
                if (cat.HP <= 0) return dog;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
        //Koniec rozgrywki
        public void EndGame(Player player)
        {   //score

            Console.WriteLine("The winner is {0}", player.Name);//dodac score (kazda runda to iles tam punktow + dodatkowe punkty za trafienie)   
            Console.SetCursorPosition(45, 20); Console.WriteLine("Press 'Enter' to continue . . .");
        }
        public void DisplayPlayer(Player player)
        {
            Console.SetCursorPosition(player.X, y);
            Console.WriteLine("X");
            Console.SetCursorPosition(player.X, y + 1);
            Console.WriteLine(player.HP);
        }

        //Wyswietlanie wiatru
        public void DisplayWind()
        {
            if (turn_wind == false) return;
            Console.SetCursorPosition(45, 3);
            Console.WriteLine("Wiatr");
            Console.SetCursorPosition(45, 4);
            if (wind < 0)
            {
                for (int i = 0; i <= Math.Abs(wind) / 3; i++)//math abs wartosc bezwzglesdna
                    Console.Write("<");
            }
            else if (wind >= 0)
            {
                for (int i = 0; i <= wind / 3; i++)
                    Console.Write(">");
            }
        }
        //Pasek ladowania
        public int ThrowPower()
        {
            int i = 0;
            bool dol = false;

            Task enterCheck = Task.Run(() =>
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter) return;
            });

            while (true)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("\u2551");
                Console.Write(i + " / 50  ");
                System.Threading.Thread.Sleep(100);

                if (i == 50) dol = true;

                else if (i == 0) dol = false;

                if (dol == true) i--; else i++;

                if (enterCheck.IsCompleted) break;
            }
            //return i = 30;
            return i;
        }

        //Wyswietlenie rzutu kota
        public void DisplayThrowCat(int v, int x0, int y0)
        {
            //licze pixel co 0,1 sekudny

            int x = x0, y = y0, g = 10;//g przyspieszenie ziemskie
            double vy = 0.7 * v;// v*sin(alpha) //predkosc poczatkowa skladowej y
            double vx = 0.7 * v;// v*cos(alpha) //predkosc poczatkowa skladowej x
            int czas_wznoszenia = (int)(10 * vy / g);
            int czas_lotu = (int)(10 * 2 * vx / g);


            for (int t = 0; t <= czas_lotu; t++)
            {
                if (x < 0 || x > 100 || (this.y - y) < 0)
                {
                    x = (int)(x0 + vx * t / 10);
                    y = (int)(y0 + (vy * t / 10) - (g * t * t / 200));
                    continue;
                }
                Console.SetCursorPosition(x, this.y - y);
                Console.Write("*");
                System.Threading.Thread.Sleep(20);
                Console.SetCursorPosition(x, this.y - y);
                Console.WriteLine(" ");

                //zmieniam polozenie x i y wzgledem t
                x = (int)(x0 + vx * t / 10);
                y = (int)(y0 + (vy * t / 10) - (g * t * t / 200));
            }

            //DMG zadane psu

            int pkx = x;
            if (pkx >= 80 && pkx <= 90)
            {
                dog.HP -= cat.DMG;
                if (dog.HP <= 0) dog.HP = 0;
            }
            else
            {
                if (x < 0 || x > 100 || (this.y - y) < 0) Console.SetCursorPosition(45, 20);
                else Console.SetCursorPosition(x, this.y - y);//wyladoiwala opikla
                Console.WriteLine("Pudło ! ");
            }
        }

        //Wyswietlenie rzutu psa
        public void DisplayThrowDog(int v, int x0, int y0)
        {
            int x = x0, y = y0, g = 10;
            double vy = 0.7 * v;
            double vx = 0.7 * v;
            int czas_wznoszenia = (int)(10 * vy / g);
            int czas_lotu = (int)(10 * 2 * vx / g);


            for (int t = 0; t <= czas_lotu; t++)
            {
                if (x < 0 || x > 100 || (this.y - y) < 0)
                {
                    x = (int)(x0 - vx * t / 10);
                    y = (int)(y0 + (vy * t / 10) - (g * t * t / 200));
                    continue;
                }
                Console.SetCursorPosition(x, this.y - y);
                Console.Write("*");
                System.Threading.Thread.Sleep(20);
                Console.SetCursorPosition(x, this.y - y);
                Console.WriteLine(" ");

                x = (int)(x0 - vx * t / 10);
                y = (int)(y0 + (vy * t / 10) - (g * t * t / 200));
            }

            //DMG zadane kotu

            int pkx = x;

            if (pkx >= 3 && pkx <= 7)
            {
                cat.HP -= dog.DMG;
                if (cat.HP < 0) cat.HP = 0;
            }
            else
            {
                if (x < 0 || x > 100 || (this.y - y) < 0) Console.SetCursorPosition(45, 20);
                else Console.SetCursorPosition(x, this.y - y);//wyladoiwala opikla
                Console.WriteLine("Pudło ! ");
            }
        }
    }
}
