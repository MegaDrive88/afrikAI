namespace afrikAI
{
    public class Menu {
        private InputHandler inputHandler;
        private MenuItem[] options;
        private List<string> rowsEntered = new List<string>(); //- csak a bevitt adatok, stringként
        //private string errorMsg = string.Empty; //ha nem felel meg a szam a korlatoknak
        public Menu() {
            options = new[] {
                new MenuItem("Sivatag betöltése fájlból", "option", () => LoadFromFileMenu()),
                new MenuItem("Sivatag random generálása", "option", () => GenRandomMenu()),
                new MenuItem("Sivatagvarázsló megnyitása", "option", () => EditorMenu()),
                new MenuItem("Kilépés", "option", () => Exit()),
            };
            inputHandler = new InputHandler(this);
            Show();
        }
		public void MenuMove(int direction) {
            int d = direction == - 1 ? options.Length - 1 : options.Length + 1;
            (_, int top) = Console.GetCursorPosition();
            Console.SetCursorPosition(0, top);
            Console.Write(options[top].Text);
            top = (top + d) % options.Length;
            Console.SetCursorPosition(0, top);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(options[top].Text);
            Console.ResetColor();
            Console.SetCursorPosition(0, top);
            inputHandler.HandleMenuInput();
        }
        public void Confirm() {
            (_, int top) = Console.GetCursorPosition();
            try {
                options[top].Action.Invoke();
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
        public void GetUserInput(ConsoleKeyInfo cki) {
            if (rowsEntered.Count == 0) rowsEntered = new List<string>(new string[options.Length]);
            (int left, int top) = Console.GetCursorPosition();
            if ((options[top].Type == "numericInput" && char.IsNumber(cki.KeyChar)) || options[top].Type == "anyInput") {
                if (rowsEntered[top] is null) rowsEntered[top] = "";
                if (left == 0) left = options[top].Text.Length + 1 + rowsEntered[top].Length;
                Console.SetCursorPosition(left, top);
                rowsEntered[top] += cki.KeyChar;
                Console.Write(cki.KeyChar);
            }
            inputHandler.HandleMenuInput();
        }
        public void DeleteLastChar() {
            (int left, int top) = Console.GetCursorPosition();
            if (left == 0) left = options[top].Text.Length + 1 + rowsEntered[top].Length;
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
            foreach (MenuItem option in options) { 
                Console.WriteLine(option.Text);
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, 0);
        }
        private void LoadFromFileMenu() {
            string[] files = Directory.GetFiles("./saved_deserts");
            List<MenuItem> temp = new List<MenuItem>();
            for (int i = 0; i < files.Length; i++) {
                if (files[i].Contains(".txt")) temp.Add(new MenuItem(files[i][16..(files[i].Length - 4)], "option", null));
            }
            temp.Add(new MenuItem("Vissza", "option", () => Back()));
            options = temp.ToArray();
            Show();
            inputHandler.HandleMenuInput();
        }
        private void GenRandomMenu() {
            // szél, mag, lion, wall
            options = new[] {
                new MenuItem("Szélesség:", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Magasság:", "numericInput", () => inputHandler.HandleMenuInput()), //
                new MenuItem("Oroszlánok száma:", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Falak száma:", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Generálás", "option", () => CheckEnteredNumbers()),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void EditorMenu() {
            options = new[] {
                new MenuItem("Szélesség:", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Magasság:", "numericInput", () => inputHandler.HandleMenuInput()), //
                new MenuItem("Tovább", "option", () => CheckEnteredNumbers()),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        public void Back() {
            options = new[] {
                new MenuItem("Sivatag betöltése fájlból", "option", () => LoadFromFileMenu()),
                new MenuItem("Sivatag random generálása", "option", () => GenRandomMenu()),
                new MenuItem("Sivatagvarázsló megnyitása", "option", () => EditorMenu()),
                new MenuItem("Kilépés", "option", () => Exit()),
            };
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
