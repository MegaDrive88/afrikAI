﻿namespace afrikAI { 
    public class Program {
        static void Main() {
            Console.CursorVisible = false;
            Console.WriteLine("      _         ___          _   __            _       _____  \r\n     / \\      .' ..]        (_) [  |  _       / \\     |_   _| \r\n    / _ \\    _| |_  _ .--.  __   | | / ]     / _ \\      | |   \r\n   / ___ \\  '-| |-'[ `/'`\\][  |  | '' <     / ___ \\     | |   \r\n _/ /   \\ \\_  | |   | |     | |  | |`\\ \\  _/ /   \\ \\_  _| |_  \r\n|____| |____|[___] [___]   [___][__|  \\_]|____| |____||_____| \r\n");
            Console.WriteLine("Nyomjon meg egy billentyűt a továbblépéshez");
            Console.ReadKey(true);
            //int height = 10;
            //int width = 10;
            //TileManager tm = new TileManager(width, height);
            //tm.DrawTiles();
            Menu menu = new Menu(new[] { "Sivatag betöltése fájlból",
                                         "Sivatag random generálása",
                                         "Sivatagvarázsló megnyitása" });
            //Game game = new Game(10, 10);
            InputHandler i = new InputHandler(menu);
            i.HandleMenuInput();

        }
    }
}
// class tile
// class "zebra" : tile - jobbra le
// 