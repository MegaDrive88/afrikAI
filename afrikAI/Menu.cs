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
        public Menu(bool init = true) {
            inputHandler = new InputHandler(this);
            if (init) Back();
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
            if (options[top].Action is not null)
                options[top].Action.Invoke();
            else {
                string path = $"{options[top].Text}.txt";
                Console.Clear();
                LaunchFromFile(path);
            }
        }
        public void Exit() {
            Back();
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
            if (rowsEntered.Count == 0) {
                inputHandler.HandleMenuInput();
                return;
            }
            if (rowsEntered[top] is not null) {
                if (left == 0) left = options[top].Text.Length + 1 + rowsEntered[top].Length;
                if (rowsEntered[top].Length > 0) {
                    Console.SetCursorPosition(left - 1, top + skipLines);
                    Console.Write(' ');
                    rowsEntered[top] = rowsEntered[top][..^1];
                    Console.SetCursorPosition(left - 1, top + skipLines);
                }
            }
            inputHandler.HandleMenuInput();
        }

        public int[] getCordInput(int width, int height, string inputMessage = "") {
            Console.ResetColor();
            rowsEntered = new List<string>();
            skipLines = height + 3;
            int[] coords = new int[] { 0, 0 };
            options = new[] {
                new MenuItem("x", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("y", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Bevitel", "option", () => {
                    if (rowsEntered.Count != 0 && rowsEntered[0] is not null && rowsEntered[1] is not null && rowsEntered[0] != "" && rowsEntered[1] != "" &&
                    rowsEntered[0].Length <= 2 && rowsEntered[1].Length <= 2 && int.Parse(rowsEntered[0]) - 1 >= 0 && int.Parse(rowsEntered[1]) - 1 >= 0 &&
                    int.Parse(rowsEntered[0]) - 1 < width && int.Parse(rowsEntered[1]) - 1 < height){
                        coords[0] = int.Parse(rowsEntered[0]) - 1;
                        coords[1] = int.Parse(rowsEntered[1]) - 1;
                    }
                    else {
                        errorMsg = "Hibás koordináta!";
                        Console.SetCursorPosition(8, Console.CursorTop);
                        ShowError(skipLines - height - 1);
                        inputHandler.HandleMenuInput();
                    }
                }),
            };
            Show(false);
            Console.SetCursorPosition(0, height + 1);
            Console.WriteLine(new string(' ', 60));
            Console.WriteLine(inputMessage);
            inputHandler.HandleMenuInput();
            return coords;
        }
        private void Show(bool clear = true) {
            if (clear) Console.Clear();
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
                new MenuItem("Magasság", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Oroszlánok száma", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Falak száma", "numericInput", () => inputHandler.HandleMenuInput()),
                new MenuItem("Víz mezők száma", "numericInput", () => inputHandler.HandleMenuInput()),
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
                new MenuItem("Kilépés", "option", () => Environment.Exit(0)),
            };
            rowsEntered = new List<string>();
            Show();
            inputHandler.HandleMenuInput();
        }
        private void CheckRandomGenNumbers() {
            List<string> nums = rowsEntered.Take(5).ToList();
            bool megfelel = true;
            if (rowsEntered.Count == 0 || nums.Contains(null) || nums.Contains("")) {
                Console.SetCursorPosition(options[5].Text.Length + 1, 5 + skipLines);
                errorMsg = "Adja meg az összes adatot!";
                ShowError(5);
                inputHandler.HandleMenuInput();
                return;
            }
            if (nums[0].Length > 2 || int.Parse(nums[0]) > 60 || int.Parse(nums[0]) < 5) {
                Console.SetCursorPosition(options[0].Text.Length + 2 + nums[0].Length, skipLines);
                errorMsg = "A szélesség 60 és 5 közé kell essen!";
                megfelel = ShowError(5);
                inputHandler.HandleMenuInput(); // kulonben nem tudna teruletet szamolni
                if (nums[0].Length > 2) return;
            }
            if (nums[1].Length > 2 || int.Parse(nums[1]) > 25 || int.Parse(nums[1]) < 4) {
                Console.SetCursorPosition(options[1].Text.Length + 2 + nums[1].Length, 1 + skipLines);
                errorMsg = "A szélesség 25 és 4 közé kell essen!";
                megfelel = ShowError(5);
                inputHandler.HandleMenuInput();
                if (nums[1].Length > 2) return;
            }
            if (nums[3].Length > 3 || int.Parse(nums[3]) > int.Parse(nums[0]) * int.Parse(nums[1]) / 4) {
                Console.SetCursorPosition(options[3].Text.Length + 2 + nums[3].Length, 3 + skipLines);
                errorMsg = $"A falak száma maximum {int.Parse(nums[0]) * int.Parse(nums[1]) / 4} lehet!";
                megfelel = ShowError(5);
            }
            if (nums[2].Length > 3 || int.Parse(nums[2]) > (int.Parse(nums[0]) * int.Parse(nums[1]) - int.Parse(nums[3])) / 4) {
                Console.SetCursorPosition(options[2].Text.Length + 2 + nums[2].Length, 2 + skipLines);
                errorMsg = $"Az oroszlánok száma maximum a földes terület negyede lehet";
                megfelel = ShowError(5);
            }
            if (nums[4].Length > 3 || int.Parse(nums[4]) > 
                (int.Parse(nums[0]) * int.Parse(nums[1]) - int.Parse(nums[3]) - int.Parse(nums[2]) - (int.Parse(nums[0]) * int.Parse(nums[1]) / 2)) / 4) {
                Console.SetCursorPosition(options[4].Text.Length + 2 + nums[4].Length, 4 + skipLines);
                errorMsg = $"A víz mezők száma maximum a földes terület és a félterület különbsége lehet";
                megfelel = ShowError(5);
            }
            if (megfelel) {
                Console.Clear();
                rowsEntered[5] = StrategySelector(); // geniusz
                ProceedToGame();
            }
            inputHandler.HandleMenuInput();
        }
        private void CheckEditorNumbers() {
            List<string> nums = rowsEntered.Take(2).ToList();
            bool megfelel = true;
            if (rowsEntered.Count == 0 || nums.Contains(null) || nums.Contains("") || rowsEntered[2] is null) {
                Console.SetCursorPosition(options[3].Text.Length + 1, 3 + skipLines);
                errorMsg = "Adja meg az összes adatot!";
                ShowError(3);
                inputHandler.HandleMenuInput();
                return;
            }
            if (nums[0].Length > 2 || int.Parse(nums[0]) > 60 || int.Parse(nums[0]) < 5) {
                Console.SetCursorPosition(options[0].Text.Length + 2 + nums[0].Length, 0 + skipLines);
                errorMsg = "A szélesség 60 és 5 közé kell essen!";
                megfelel = ShowError(3);
                inputHandler.HandleMenuInput();
                if (nums[0].Length > 2) return;
            }
            if (nums[1].Length > 2 || int.Parse(nums[1]) > 25 || int.Parse(nums[1]) < 4) {
                Console.SetCursorPosition(options[1].Text.Length + 2 + nums[1].Length, 1 + skipLines);
                errorMsg = "A szélesség 25 és 4 közé kell essen!";
                megfelel = ShowError(3);
                inputHandler.HandleMenuInput();
                if (nums[1].Length > 2) return;
            }
            if (rowsEntered[2] == "" || !rowsEntered[2].All(x => !Statics.FileValidation.invalidFilenameCharacters.Contains(x)) || File.Exists($"{PATH}{rowsEntered[2]}.txt")) {
                Console.SetCursorPosition(options[2].Text.Length + 2 + rowsEntered[2].Length, 2 + skipLines);
                errorMsg = "A fájlnév helytelen vagy már létezik ilyen fájl!";
                megfelel = ShowError(3);
            }
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
            List<int> inputNums = rowsEntered.Take(5).ToList().ConvertAll(new Converter<string, int>(int.Parse));
            Game game = new Game(new TileGeneratorData(inputNums[0], inputNums[1], inputNums[3], inputNums[2], inputNums[4]), rowsEntered[5]);
            game.Start();
        }
        private string StrategySelector() { // ujrahasznalhato
            string strat = string.Empty;
            skipLines = 1;
            List<MenuItem> temp = new List<MenuItem>();
            foreach (string strategy in Statics.PathFindingStrategys.PathfindingStrategies) {
                temp.Add(new MenuItem(strategy, "option", () => { strat = strategy; }));
            }
            options = temp.ToArray();
            Show();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Válassza ki a használni kívánt útkeresőt: ");
            inputHandler.HandleMenuInput();
            Console.Clear();
            return strat;
        }
        private void LaunchFromFile(string _path) {
            options = new[] {
                new MenuItem("Szerkesztés", "option", () => { 
                    TileEditor te = new TileEditor(_path);
                }),
                new MenuItem("Futtatás", "option", () => {
                    Game game = new Game(_path, StrategySelector());
                    game.Start();
                }),
                new MenuItem("Vissza", "option", () => LoadFromFileMenu()),
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
            TileEditor te = new TileEditor($"{rowsEntered[2]}.txt");
        }
        // sivatag fájlból:
        //		fájlok kilistázás
        //          szerkeszt?
        //				algoritmus választása
        // random generálás
        //		szél hossz falak száma vizek oroszlánok száma
        //				algoritmus választása
        // editor
        //		szél hossz
        //				falak elhelyezése; víz elheyez; oroszlán elhelyez;
        //						algor választ.
    }
}