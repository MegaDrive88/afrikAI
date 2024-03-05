namespace afrikAI
{
	public class Menu
	{
		private int mainMenuChoice;
        private InputHandler inputHandler;
        private string[] options;
        public Menu(string[] _options) {
            options = _options;
            inputHandler = new(this);
            mainMenuChoice = Show(options);
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
        public void Confrim()
		{
            Console.Clear();
        }
        public void Exit()
		{

		}
        private int Show(string[] options) {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            foreach (string option in options) { 
                Console.WriteLine(option);
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, 0);
            return 0;
        }
        private void LoadFromFileMenu() {

        }
        private void GenRandomMenu() {

        }
        private void EditorMenu() {

        }
        private void MainMenuSwitch() {
            switch (mainMenuChoice) {
                case 0: // "Sivatag betöltése fájlból"
                    LoadFromFileMenu();
                    break;
                case 1: // "Sivatag random generálása"
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
