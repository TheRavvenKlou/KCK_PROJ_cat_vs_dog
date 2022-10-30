using trzeci;
class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Menu";
        Console.CursorVisible = false;
        Game game = new Game();
        bool exit = false;

        //      *INTERFACE PROGRAMU*
        Console.WriteLine("\n\n\n\n\n");
        string logo = @"                               _________         __                   ________                 
                                \_   ___ \_____ _/  |_  ___  ________  \______ \   ____   ____  
                               /    \  \/\__  \\   __\ \  \/ /  ___/   |    |  \ /  _ \ / ___\ 
                               \     \____/ __ \|  |    \   /\___ \    |    `   (  <_> ) /_/  >
                                \______  (____  /__|     \_//____  >  /_______  /\____/\___  / 
                                       \/     \/                 \/           \/      /_____/  ";
        Console.WriteLine(logo);


        while (Console.ReadKey().Key != ConsoleKey.Enter) { }

        List<string> menu = new List<string>
        {
            "Zacznij gre","Wiatr","Autorzy","Wyjdź"
        };
        int place_in_menu = 0;

        //Menu
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine(logo);
            for (int i = 0; i < 4; i++)
            {
                if (i == place_in_menu)
                    Console.ForegroundColor = ConsoleColor.Magenta;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                if (i == 1)
                {
                    if (game.turn_wind)
                    {
                        Console.SetCursorPosition(((Console.WindowWidth - menu[1].Length) / 2) - 4, Console.CursorTop);
                        Console.WriteLine("Wiatr wlaczony");
                    }
                    else
                    {
                        Console.SetCursorPosition(((Console.WindowWidth - menu[1].Length) / 2) - 4, Console.CursorTop);
                        Console.WriteLine("Wiatr wylaczony");
                    }
                }
                else
                {
                    Console.SetCursorPosition((Console.WindowWidth - menu[i].Length) / 2, Console.CursorTop);
                    Console.WriteLine(menu[i]);
                }


            }
            ConsoleKeyInfo input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.Escape:
                    exit = true;
                    break;
                case ConsoleKey.UpArrow:
                    place_in_menu--;
                    if (place_in_menu == -1) { place_in_menu = 3; }
                    break;
                case ConsoleKey.DownArrow:
                    place_in_menu++;
                    if (place_in_menu == 4) { place_in_menu = 0; }
                    break;
                case ConsoleKey.Enter:
                    if (place_in_menu == 0)
                    {
                        Player pomPlayer = game.StartGame();
                        game.EndGame(pomPlayer);
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }
                    if (place_in_menu == 1)
                    {
                        game.turn_wind = !game.turn_wind;
                    }

                    if (place_in_menu == 2)
                    {
                        Console.Clear();
                        string author = "Klaudia Mieczkowska";                      
                        Console.SetCursorPosition(((Console.WindowWidth - author.Length) / 2) - 4, Console.CursorTop); Console.WriteLine(author);Console.Read();
                    }

                    if (place_in_menu == 3)
                    {
                        exit = true;
                    }
                    break;
            }

        }
    }
}
