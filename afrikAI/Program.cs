namespace afrikAI { 
    public class Program {
        static void Main() {
            //Console.CursorVisible = false;
            //Console.ResetColor();
            //Console.WriteLine("      _         ___          _   __            _       _____  \r\n     / \\      .' ..]        (_) [  |  _       / \\     |_   _| \r\n    / _ \\    _| |_  _ .--.  __   | | / ]     / _ \\      | |   \r\n   / ___ \\  '-| |-'[ `/'`\\][  |  | '' <     / ___ \\     | |   \r\n _/ /   \\ \\_  | |   | |     | |  | |`\\ \\  _/ /   \\ \\_  _| |_  \r\n|____| |____|[___] [___]   [___][__|  \\_]|____| |____||_____| \r\n");
            //Console.WriteLine("Nyomjon meg egy billentyűt a továbblépéshez");
            //Console.ReadKey(true);
            //Menu menu = new Menu();
            Console.ResetColor();
            Console.Clear();
            Game g = new("genyo.txt", "Adam");
            g.Start();
            //TileManager tm = new("genyo.txt")
            //while (true) {
            //    g
            //}
        }
    }
}
// class tile
// class "zebra" : tile - jobbra le
// 