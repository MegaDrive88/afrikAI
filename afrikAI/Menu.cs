namespace afrikAI
{
	public class Menu
	{
        private InputHandler inputHandler;
        private string[] options;
        private Dictionary<string, Action> allOptions; 
        public Menu(string[] _options) {
            options = _options;
            inputHandler = new(this);
            allOptions = new Dictionary<string, Action> {
                { "Sivatag betöltése fájlból", () => LoadFromFileMenu() },
                { "Sivatag random generálása", () => GenRandomMenu() },
                { "Sivatagvarázsló megnyitása", () => EditorMenu() },
                { "Vissza", () => Back()}
                // ezt kell bővíteni
            };
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
        public void Confirm() {
            (_, int top) = Console.GetCursorPosition();
            try {
                allOptions[options[top]].Invoke();
            }
            catch {
                //fájl lett kiválasztva
            }
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
            List<string> temp = files.ToList();
            temp.Remove(@"./saved_deserts\te.st");
            for (int i = 0; i < temp.Count; i++) {
                temp[i] = temp[i][16..(temp[i].Length - 4)];
            }
            temp.Add("Vissza");
            files = temp.ToArray();
            options = files;
            Show(options);
            inputHandler.HandleMenuInput();
        }
        private void GenRandomMenu() {

        }
        private void EditorMenu() {

        }
        private void Back() {
            
        }
        //private / public dolgokat rendezni!!!

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
