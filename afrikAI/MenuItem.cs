namespace afrikAI {
    public class MenuItem {
        public string Text { get; private set; }
        public Action? Action { get; private set; } // null ha file
        public string Type { get; private set; } // anyInput, numericInput, option, info?
        public MenuItem(string _text, string _type, Action? _action = null) {
            Text = _text;
            Type = _type;
            if (Type != "option") Text += ":";
            Action = _action;
        }
    }
}
