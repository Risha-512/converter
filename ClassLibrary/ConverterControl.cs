namespace Converter
{
    public class ConverterControl
    {
        public enum EditCommand
        {
            AddDigit = 1,
            Erase,
            AddDelim,
            Clear
        }

        public int originalBase { get; set; }
        public int resultBase { get; set; }
        
        public History history = new History();
        public Editor editor = new Editor();

        public ConverterControl()
        {
            originalBase = 10;
            resultBase = 16;
        }

        public string EditNumber(EditCommand type, string n = "")
        {
            switch (type)
            {
                case EditCommand.AddDigit:
                    if (n == "0")
                        return editor.AddZero().number;
                    else
                        return editor.AddDigit(n, originalBase).number;

                case EditCommand.Erase:
                    return editor.RemoveSymbolsFromPosition().number;

                case EditCommand.AddDelim:
                    return editor.AddDelim().number;

                case EditCommand.Clear:
                    return editor.Clear().number;

                default:
                    return editor.number;
            }
        }

        public string Convert()
        {
            var result = Converter.Convert(editor.number, originalBase, resultBase);
            history.AddEntry($"{editor.number}({originalBase}) = {result}({resultBase})");

            return result;
        }
    }
}
