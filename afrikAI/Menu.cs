namespace afrikAI
{
	public class Menu
	{
		public int choice;
        private InputHandler inputHandler;
        private string[] options;
        public Menu(string[] _options) {
            options = _options;
            inputHandler = new(this);
            Show(options);
		}
		public void MenuMove(int direction)
		{
            int d = direction == - 1 ? options.Length - 1 : options.Length + 1;
            (_, int top) = Console.GetCursorPosition();
            Console.Write(options[top]);
            top = (top + d) % options.Length;
            Console.SetCursorPosition(0, top);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(options[top]);
            Console.ResetColor();
            Console.SetCursorPosition(0, top);
            inputHandler.HandleMenuInput();
		}
        public int Confrim()
		{
            (_, int top) = Console.GetCursorPosition();
            Console.Clear();
            return top;
        }
        public void Exit()
		{
            Console.Write("csontkovacs");
		}
        private void Show(string[] options) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            foreach (string option in options) { 
                Console.WriteLine(option);
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, 0);
        }
        private void LoadFromFileMenu() {
            string[] files = Directory.GetFiles("./saved_deserts");
            //if (files.Length != 0) {
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i][16..];
            }
            List<string> temp = files.ToList();
            temp.Add("Vissza");
            files = temp.ToArray();
            Menu m = new Menu(files);
            inputHandler = new InputHandler(m);
            inputHandler.HandleMenuInput();
            //}
            //else {
            //    //Console.WriteLine("Nincsenek mentett sivatagok");
            //    Menu m = new Menu(new[] { "Vissza" });
            //    inputHandler = new InputHandler(m);
            //    inputHandler.HandleMenuInput();
            //}
        }
        private void GenRandomMenu() {

        }
        private void EditorMenu() {

        }
        public void MainMenuSwitch() {
            switch (choice) {
                case 0:
                    LoadFromFileMenu();
                    break;
                case 1:
                    GenRandomMenu();
                    break;
                case 2:
                    EditorMenu();
                    break;
            }
        }

        // sivatag fájlból:
        //		fájlok kilistázás
        //				algoritmus választása
        // random generálás
        //		szél hossz falak száma vizek oroszlánok száma
        //				algoritmus választása
        // editor
        //		szél hossz
        //				falak elhelyezése; víz elheyez; oroszlán elhelyez; név
        //						algor választ.
    }
}
