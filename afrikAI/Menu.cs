#pragma warning disable

namespace afrikAI
{
    public class Menu {
        private InputHandler inputHandler;
        private MenuItem[] options;
        private List<string> rowsEntered = new List<string>();
        private string errorMsg = string.Empty;
        private const string PATH = "./saved_deserts\\";
        private int skipLines = 0;
        public Menu() {
            inputHandler = new InputHandler(this);
            Back();
        }
        public void MenuMove(int direction) {
            int d = direction == - 1 ? options.Length - 1 : options.Length + 1;
            (_, int top) = Console.GetCursorPosition();
            Console.SetCursorPosition(0, top);
            Console.Write(options[top - skipLines].Text);
            top = (top - skipLines + d) % options.Length + skipLines;
            Console.SetCursorPosition(0, top);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(options[top - skipLines].Text);
            Console.ResetColor();
            Console.SetCursorPosition(0, top);
            inputHandler.HandleMenuInput();
        }
        public void Confirm() {
            (_, int top) = Console.GetCursorPosition();
            top -= skipLines;
            try {
                options[top].Action.Invoke();
            }
            catch {
                string path = $"{options[top].Text}.txt";
                using (StreamReader sr = new StreamReader(PATH + path)) {
                    Console.Clear();
                    LaunchFromFile(path);
                }
            }
        }
        public void Exit() {
            Environment.Exit(0);
        }
        public void GetUserInput(ConsoleKeyInfo cki) {
            ClearErrors();
            if (rowsEntered.Count == 0) rowsEntered = new List<string>(new string[options.Length]);
            (int left, int top) = Console.GetCursorPosition();
            top -= skipLines;
            if ((options[top].Type == "numericInput" && char.IsNumber(cki.KeyChar)) || options[top].Type == "anyInput") {
                if (rowsEntered[top] is null) rowsEntered[top] = "";
                if (left == 0) left = options[top].Text.Length + 1 + rowsEntered[top].Length;
                Console.SetCursorPosition(left, top + skipLines);
                rowsEntered[top] += cki.KeyChar;
                Console.Write(cki.KeyChar);
            }
            inputHandler.HandleMenuInput();
        }
        public void DeleteLastChar() {
            ClearErrors();
            (int left, int top) = Console.GetCursorPosition();
            top -= skipLines;
            try {
                _ = rowsEntered[top];
            }
            catch {
                inputHandler.HandleMenuInput();
                return;
            }
            if (left == 0) left = options[top].Text.Length + 1 + rowsEntered[top].Length;
            if (rowsEntered[top].Length > 0) {
                Console.SetCursorPosition(left - 1, top + skipLines);
                Console.Write(' ');
                rowsEntered[top] = rowsEntered[top][..^1];
                Console.SetCursorPosition(left - 1, top + skipLines);
            }
            inputHandler.HandleMenuInput();
        }
        private void Show() {
            Console.Clear();
            Console.SetCursorPosition(0, skipLines);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            foreach (MenuItem option in options) { 
                Console.WriteLine(option.Text);
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, skipLines);
        }
        private void LoadFromFileMenu() {
            string[] files = Directory.GetFiles("./saved_deserts");
            List<MenuItem> temp = new List<MenuItem>();
            for (int i = 0; i < files.Length; i++) {
                temp.Add(new MenuItem(files[i][16..(files[i].Length - 4)], "option"));
            }
            if (temp.Count == 0) skipLines = 1;
            temp.Add(new MenuItem("Vissza", "option", () => Back()));
            options = temp.ToArray();
            Show();
            if (skipLines == 1) {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Nincsenek mentett sivatagok!");
            }
            inputHandler.HandleMenuInput();
        }
        private void GenRandomMenu() {
            // szél, mag, lion, wall
            options = new[] {
                new MenuItem("Szélesség", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Magasság", "numericInput", () => inputHandler.HandleMenuInput()), //
                new MenuItem("Oroszlánok száma", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Falak száma", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Használni kívánt algoritmus (lehetséges:)", "anyInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Generálás", "option", () => CheckRandomGenNumbers()),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void EditorMenu() {
            options = new[] {
                new MenuItem("Szélesség", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Magasság", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Mentési név", "anyInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Használni kívánt algoritmus (lehetséges:)", "anyInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Tovább", "option", () => CheckEditorNumbers()),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void Back() {
            skipLines = 0;
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
        private void CheckRandomGenNumbers() { // nem general startot meg endet
            List<string> nums = rowsEntered.Take(4).ToList();
            bool megfelel = true;
            if (rowsEntered.Count == 0 || nums.Contains(null) || nums.Contains("") || rowsEntered[4] is null) {
                Console.SetCursorPosition(options[5].Text.Length + 1, 5 + skipLines);
                errorMsg = "Adja meg az összes adatot!";
                ShowError(5);
                inputHandler.HandleMenuInput();
                return;
            }
            if (nums[0].Length > 2 || int.Parse(nums[0]) > 60 || int.Parse(nums[0]) < 5) { // teszt feltételek, jövőben változhat
                Console.SetCursorPosition(options[0].Text.Length + 2 + nums[0].Length, skipLines);
                errorMsg = "A szélesség 60 és 5 közé kell essen!";
                megfelel = ShowError(5);
                inputHandler.HandleMenuInput();
                if (nums[0].Length > 2) return;
            }
            if (nums[1].Length > 2 || int.Parse(nums[1]) > 25 || int.Parse(nums[1]) < 4) {
                Console.SetCursorPosition(options[1].Text.Length + 2 + nums[1].Length, 1 + skipLines);
                errorMsg = "A szélesség 25 és 4 közé kell essen!";
                megfelel = ShowError(5);
                inputHandler.HandleMenuInput();
                if (nums[1].Length > 2) return;
            }
            if (nums[2].Length > 3 || int.Parse(nums[2]) > 375 || int.Parse(nums[2]) > int.Parse(nums[0]) * int.Parse(nums[1]) / 4) {
                Console.SetCursorPosition(options[2].Text.Length + 2 + nums[2].Length, 2 + skipLines);
                errorMsg = "Az oroszlánok száma maximum a terület negyede lehet!";
                megfelel = ShowError(5);
            }
            if (nums[3].Length > 3 || int.Parse(nums[3]) > 750 || int.Parse(nums[3]) > int.Parse(nums[0]) * int.Parse(nums[1]) / 2) {
                Console.SetCursorPosition(options[3].Text.Length + 2 + nums[3].Length, 3 + skipLines);
                errorMsg = "A falak száma maximum a terület fele lehet!";
                megfelel = ShowError(5);
            }
            if (rowsEntered[4] == "" || !Statics.PathFindingStrategys.PathfindingStrategies.Contains(rowsEntered[4])) { //lehet h mad nem is kell
                Console.SetCursorPosition(options[4].Text.Length + 2 + rowsEntered[4].Length, 4 + skipLines);
                errorMsg = "Hibás stratégianév!";
                megfelel = ShowError(5);
            }
            if (megfelel) {
                Console.Clear();
                ProceedToGame();
            }
            inputHandler.HandleMenuInput();
        }
        private void CheckEditorNumbers() {
            List<string> nums = rowsEntered.Take(2).ToList();
            bool megfelel = true;
            if (rowsEntered.Count == 0 || nums.Contains(null) || nums.Contains("") || rowsEntered[3] is null || rowsEntered[2] is null) {
                Console.SetCursorPosition(options[3].Text.Length + 1, 3 + skipLines);
                errorMsg = "Adja meg az összes adatot!";
                ShowError(4);
                inputHandler.HandleMenuInput();
                return;
            }
            if (nums[0].Length > 2 || int.Parse(nums[0]) > 60 || int.Parse(nums[0]) < 5) { // teszt feltételek, jövőben változhat
                Console.SetCursorPosition(options[0].Text.Length + 2 + nums[0].Length, 0 + skipLines);
                errorMsg = "A szélesség 60 és 5 közé kell essen!";
                megfelel = ShowError(4);
                inputHandler.HandleMenuInput();
                if (nums[0].Length > 2) return;
            }
            if (nums[1].Length > 2 || int.Parse(nums[1]) > 25 || int.Parse(nums[1]) < 4) {
                Console.SetCursorPosition(options[1].Text.Length + 2 + nums[1].Length, 1 + skipLines);
                errorMsg = "A szélesség 25 és 4 közé kell essen!";
                megfelel = ShowError(4);
                inputHandler.HandleMenuInput();
                if (nums[1].Length > 2) return;
            }
            if (rowsEntered[3] == "" || !Statics.PathFindingStrategys.PathfindingStrategies.Contains(rowsEntered[3])) {
                Console.SetCursorPosition(options[2].Text.Length + 2 + nums[2].Length, 2 + skipLines);
                errorMsg = "Hibás stratégianév!";
                megfelel = ShowError(4);
            }
            if (rowsEntered[2] == "" || !rowsEntered[2].All(x => !Statics.FileValidation.invalidFilenameCharacters.Contains(x)) || File.Exists($"{PATH}{rowsEntered[2]}.txt")) {
                Console.SetCursorPosition(options[2].Text.Length + 2 + rowsEntered[2].Length, 2 + skipLines);
                errorMsg = "A fájlnév helytelen vagy már létezik ilyen fájl!";
                megfelel = ShowError(4);
            }
            // if nincs olyan strategy - a strategyk listája legyen public ha lehet
            if (megfelel) {
                Console.Clear();
                ProceedToEditor();
            }
            inputHandler.HandleMenuInput();
        }
        private bool ShowError(int cursorPos) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(errorMsg);
            Console.ResetColor();
            Console.SetCursorPosition(0, cursorPos + skipLines);
            return false;
        }
        private void ClearErrors() {
            (int left, int top) = Console.GetCursorPosition();
            for (int i = 0; i < options.Length; i++) {
                try {
                    if (rowsEntered[i] is not null) {
                        Console.SetCursorPosition(options[i].Text.Length + 2 + rowsEntered[i].Length, i + skipLines);
                        Console.Write(new string(' ', Console.WindowWidth - (options[i].Text.Length + 2 + rowsEntered[i].Length)));
                    }
                    else {
                        Console.SetCursorPosition(options[i].Text.Length + 1, i + skipLines);
                        Console.Write(new string(' ', Console.WindowWidth - (options[i].Text.Length + 1)));
                    }
                }
                catch {
                    Console.SetCursorPosition(options[i].Text.Length + 1, i + skipLines);
                    Console.Write(new string(' ', Console.WindowWidth - (options[i].Text.Length + 1)));
                }
            }
            Console.SetCursorPosition(left, top);
        }
        private void ProceedToGame() {
            List<int> inputNums = rowsEntered.Take(4).ToList().ConvertAll(new Converter<string, int>(int.Parse));
            Game game = new Game(new TileGeneratorData(inputNums[0], inputNums[1], inputNums[3], inputNums[2]), rowsEntered[4]);
            game.StartOld();
        }
        private void LaunchFromFile(string _path) {
            options = new[] {
                new MenuItem("Szerkesztés", "option", () => { 
                    TileEditor te = new TileEditor(_path);
                    
                }),
                new MenuItem("Futtatás", "option", () => {
                    options = new[] {
                        new MenuItem("Használni kívánt algoritmus (lehetséges:)", "anyInput", () => inputHandler.HandleMenuInput()),
                        new MenuItem("Futtatás", "option", () => { }),
                    };
                    Show();
                    inputHandler.HandleMenuInput();
                    Console.Clear();
                    // van e ilyen context? - if
                    Game game = new Game(_path, rowsEntered[0]); // x = a megengedett????
                    game.StartOld();
                }),
                new MenuItem("Vissza", "option", () => Back()),
            };
            Show();
            inputHandler.HandleMenuInput();
        }
        private void ProceedToEditor() {
            List<int> inputNums = rowsEntered.Take(2).ToList().ConvertAll(new Converter<string, int>(int.Parse));
            using (StreamWriter sw = new($"{PATH}{rowsEntered[2]}.txt")) {
                for (int i = 0; i < inputNums[1]; i++) {
                    sw.WriteLine(string.Concat(Enumerable.Repeat("0 ", inputNums[0])));
                }
            }
            TileEditor te = new TileEditor($"{rowsEntered[2]}.txt"); // v a mentes - ADDIG NE LEHESSEN MENTENI, AMIG NINCS ZEBRA MEG VIZ!!!
        }
        //private / public dolgokat rendezni!!!

        // sivatag fájlból:
        //		fájlok kilistázás
        //          szerkeszt?
        //				algoritmus választása
        // random generálás
        //		szél hossz falak száma vizek oroszlánok száma
        //				algoritmus választása
        // editor
        //		szél hossz
        //				falak elhelyezése; víz elheyez; oroszlán elhelyez; név
        //						algor választ.
        // post game: szeretné menteni a sivatagot?...
    }
}
