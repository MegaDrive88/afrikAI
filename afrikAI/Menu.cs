namespace afrikAI
{
    public class Menu {
        private InputHandler inputHandler;
        private MenuItem[] options;
        private List<string> rowsEntered = new List<string>(); //- csak a bevitt adatok, stringként
        private string errorMsg = string.Empty; //ha nem felel meg a szam a korlatoknak
        public Menu() {
            inputHandler = new InputHandler(this);
            Back();
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
            //try {
                options[top].Action.Invoke();
            //}
            //catch {
            //    using (StreamReader sr = new StreamReader($"./saved_deserts\\{options[top].Text}.txt")) {
            //        Console.Clear();
            //        // Console.WriteLine(sr.ReadToEnd());
            //    }
            //}
        }
        public void Exit() {
            Environment.Exit(0);
        }
        public void GetUserInput(ConsoleKeyInfo cki) {
            //delerrors
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
                if (files[i].Contains(".txt")) temp.Add(new MenuItem(files[i][16..(files[i].Length - 4)], "option"));
            }
            temp.Add(new MenuItem("Vissza", "option", () => Back()));
            options = temp.ToArray();
            Show();
            inputHandler.HandleMenuInput();
        }
        private void GenRandomMenu() {
            // szél, mag, lion, wall
            options = new[] {
                new MenuItem("Szélesség", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Magasság", "numericInput", () => inputHandler.HandleMenuInput()), //
                new MenuItem("Oroszlánok száma", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Falak száma", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Generálás", "option", () => CheckRandomGenNumbers()),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void EditorMenu() {
            options = new[] {
                new MenuItem("Szélesség", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Magasság", "numericInput", () => inputHandler.HandleMenuInput()), //
                new MenuItem("Tovább", "option", () => CheckEditorNumbers()),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void Back() {
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
        private void CheckRandomGenNumbers() {
            // korlátok? else handlemenuinp
            List<string> nums = rowsEntered.Take(4).ToList();
            // bool megfelel
            if (rowsEntered.Count == 0 || !nums.All(x => x is not null)) {
                Console.SetCursorPosition(options[4].Text.Length + 1, 4);
                errorMsg = "Adjon meg pontosan 4 számot!";
                ShowError(4);
                inputHandler.HandleMenuInput();
                return;
            }
            if (int.Parse(nums[0]) > 100) { // teszt feltételek, jövőben változhat
                Console.SetCursorPosition(options[0].Text.Length + 2 + nums[0].Length, 0);
                errorMsg = "A szélesség nem lehet 100-nál nagyobb!";
                ShowError(4);
            }
            if (int.Parse(nums[1]) > 50) {
                Console.SetCursorPosition(options[1].Text.Length + 2 + nums[1].Length, 1);
                errorMsg = "A magasság nem lehet 50-nél nagyobb!";
                ShowError(4);
            }
            if (int.Parse(nums[2]) > int.Parse(nums[0]) * int.Parse(nums[1]) / 4) { // nem fer intbe xdddd
                Console.SetCursorPosition(options[2].Text.Length + 2 + nums[2].Length, 2);
                errorMsg = "Az oroszlánok száma maximum a terület negyede lehet!";
                ShowError(4);
            }
            if (int.Parse(nums[3]) > int.Parse(nums[0]) * int.Parse(nums[1]) / 2) {
                Console.SetCursorPosition(options[3].Text.Length + 2 + nums[3].Length, 3);
                errorMsg = "A falak száma maximum a terület fele lehet!";
                ShowError(4);
            }
            inputHandler.HandleMenuInput();
            //else Console.Clear();
        }
        private void CheckEditorNumbers() {
            // korlátok? else handlemenuinp
            _ = rowsEntered;
        }
        private void ShowError(int cursorPos) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(errorMsg);
            Console.ResetColor();
            Console.SetCursorPosition(0, cursorPos);
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
