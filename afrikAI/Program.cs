namespace afrikAI { 
    public class Program {
        static void Main() {
            //int height = 10;
            //int width = 10;
            //TileManager tm = new TileManager(width, height);
            //tm.DrawTiles();
            Menu menu = new Menu(new[] {"Sivatag betöltése fájlból",
                                        "Sivatag random generálása",
                                        "Sivatagvarázsló megnyitása"});
            //Game game = new Game(10, 10);
            InputHandler i = new InputHandler(menu);
            i.HandleMenuInput();

        }
    }
}
// class tile
// class "zebra" : tile - jobbra le
// 