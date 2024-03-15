namespace afrikAI {
    public class MenuItem {
        public string Text { get; private set; }
        public Action Action { get; private set; }
        public string Type { get; private set; } // anyInput, numericInput, option
        public MenuItem(string _text, string _type, Action _action) {
            Text = _text;
            Type = _type;
            Action = _action;
        }
    }
}
