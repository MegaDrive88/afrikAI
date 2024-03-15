namespace afrikAI
{
    public class Menu {
        private InputHandler inputHandler;
        private string[] options;
        private Dictionary<string, Action> allOptions;
        private List<string> rowsEntered = new List<string>(); //- csak a bevitt adatok, stringként
        //private string errorMsg = string.Empty; //ha nem felel meg a szam a korlatoknak
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
                { "Generálás", () => CheckEnteredNumbers() }, // generate random
                { "Vissza", () => Back() },
                { "Kilépés", () => Exit() }

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
            Environment.Exit(0);
        }
        public void GetUserInput(ConsoleKey c) {
            if (rowsEntered.Count == 0) rowsEntered = new List<string>(new string[options.Length]);
            (int left, int top) = Console.GetCursorPosition();
            if (new[] { "Szélesség:", "Magasság:", "Oroszlánok száma:", "Falak száma:" }.Contains(options[top])) {
                if (rowsEntered[top] is null) rowsEntered[top] = "";
                if (left == 0) left = options[top].Length + 1 + rowsEntered[top].Length;
                Console.SetCursorPosition(left, top);
                if ((int)c > 57) c -= 48;
                rowsEntered[top] += ((int)c - '0').ToString();
                Console.Write((int)c - '0');
            }
            inputHandler.HandleMenuInput();
        }
        public void DeleteLastChar() {
            (int left, int top) = Console.GetCursorPosition();
            if (left == 0) left = options[top].Length + 1 + rowsEntered[top].Length;
            if (rowsEntered[top].Length > 0) {
                Console.SetCursorPosition(left - 1, top);
                Console.Write(' ');
                rowsEntered[top] = rowsEntered[top][..^1];
                Console.SetCursorPosition(left - 1, top);
            }
            inputHandler.HandleMenuInput();
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
        private void GenRandomMenu() {
            // szél, mag, lion, wall
            options = new[] { "Szélesség:", "Magasság:", "Oroszlánok száma:", "Falak száma:", "Generálás", "Vissza"};
            Show();
            inputHandler.HandleMenuInput();
        }
        private void EditorMenu() {
            options = new[] { "x:", "y:", "z:", "Tovább", "Vissza" };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void Back() {
            options = new[] { "Sivatag betöltése fájlból", "Sivatag random generálása", "Sivatagvarázsló megnyitása", "Kilépés" };
            rowsEntered = new List<string>();
            Show();
            inputHandler.HandleMenuInput();
        }
        private void CheckEnteredNumbers() {
            // korlátok? else handlemenuinp
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
