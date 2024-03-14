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
                { "Szélesség:", () => inputHandler.HandleMenuInput() },
                { "Magasság:", () => inputHandler.HandleMenuInput() },
                { "Oroszlánok száma:", () => inputHandler.HandleMenuInput() },
                { "Falak száma:", () => inputHandler.HandleMenuInput() },
                { "Generálás", () => Back() }, // generate random
                { "Vissza", () => Back() }

                // ezt kell bővíteni
            };
            Show();
        }
		public void MenuMove(int direction) {
            int d = direction == - 1 ? options.Length - 1 : options.Length + 1;
            (_, int top) = Console.GetCursorPosition();
            Console.SetCursorPosition(0, top);
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
                using (StreamReader sr = new StreamReader($"./saved_deserts\\{options[top]}.txt")) {
                    Console.Clear();
                    // Console.WriteLine(sr.ReadToEnd());
                }
            }
        }
        public void Exit() {
            Console.Write("csontkovacs");
        }
        public void EnterNumbers(ConsoleKey c) {
            (int left, int top) = Console.GetCursorPosition();
            if (new[] { "Szélesség:", "Magasság:", "Oroszlánok száma:", "Falak száma:" }.Contains(options[top])) {
                if (left == 0) left = options[top].Length + 1;
                Console.SetCursorPosition(left, top); // innen kireturnolni :(
                if ((int)c > 57) c -= '0';
                Console.Write((int)c - '0');
            }
            inputHandler.HandleMenuInput();
        }
        public void DeleteNumber() {
            Console.CursorVisible = true;
            (int left, int top) = Console.GetCursorPosition();
            //if (left == 0) {
            //    left = options[top].Length - 1;
            //    Console.WriteLine(left);
            //}
            // lehet el kéne menteni a számot?
            if (left > options[top].Length + 1) {
                Console.SetCursorPosition(left - 1, top);
                Console.Write(' ');
                Console.SetCursorPosition(left - 1, top);
            }
            inputHandler.HandleMenuInput();
            // ha left kisebb mint az opcio hossza akk ne
        }
        private void Show() {
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
            Show();
            inputHandler.HandleMenuInput();
        }
        private int[] GenRandomMenu() {
            // szél, mag, lion, wall
            options = new[] { "Szélesség:", "Magasság:", "Oroszlánok száma:", "Falak száma:", "Generálás", "Vissza"}; // menüpontok mögé lesz írva
            Show();
            inputHandler.HandleMenuInput();
            return new[] { 1, 1, 1, 1 };
        }
        private void EditorMenu() {

        }
        private void Back() {
            options = new[] { "Sivatag betöltése fájlból", "Sivatag random generálása", "Sivatagvarázsló megnyitása" };
            Show();
            inputHandler.HandleMenuInput();
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
