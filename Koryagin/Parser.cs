using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Koryagin
{
    public class Parser
    {
        readonly RichTextBox input;
        public List<string> words = new List<string>();
        Dictionary<string, string> variables = new Dictionary<string, string>();
        int currentRow = 0;
        int current = 0;
        bool wasSets = false;
        bool wasLinks = false;
        bool wasOperators = false;

        public Parser(RichTextBox input)
        {
            words = new List<string>();
            variables = new Dictionary<string, string>();
            this.input = input;
            foreach (var line in input.Lines)
            {
                foreach (var word in line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                    words.Add(word);
                words.Add(Environment.NewLine);
            }
            words.RemoveAt(words.Count - 1);
            words.Add(" \x9");
        }

        public string Parse()
        {
            if (IsCurrentEqualTo("Начало")) current++;
            else Error("Язык должен начинаться с \"Начало\"");

            ProcessSets();
            ProcessLinks();
            ProcessOperator();

            if (!wasSets) Error("Пропущены Множества");
            if (!wasLinks) Error("Пропущены Звенья");
            if (!wasOperators) Error("Пропущен Оператор");

            return GenerateResult();
        }
        private void ProcessSets()
        {
            while (IsCurrentEqualTo("Первое") || IsCurrentEqualTo("Второе") || IsCurrentEqualTo("Третье"))
            {
                checkSet();
                wasSets = true;
            }
        }
        private void ProcessLinks()
        {
            if (!wasSets)
                Error("Пропущены Множества");
            while (IsCurrentEqualTo("Сочетаемое"))
            {
                checkLink();
                wasLinks = true;
            }
        }
        private void ProcessOperator()
        {
            if (!wasLinks)
                Error("Пропущены Звенья");
            checkOperator();
            wasOperators = true;
        }
        private void checkSet()
        {
            bool wereElements = false;
            if (IsCurrentEqualTo("Первое"))
            {
                current++;
                foreach (var word in words.Skip(current))
                {
                    if ((word == "Первое" || word == "Второе" || word == "Третье" || word == "Сочетаемое"))
                    {
                        if (!wereElements) Error("Отсутствует хотя бы одно целое число");
                        //current++;
                        break;
                    }

                    if (Typer.GetTypeOf(word) == Type.Int)
                        wereElements = true;

                    switch (Typer.GetTypeOf(word))
                    {
                        case Type.NewLine:
                            currentRow++;
                            break;
                        case Type.Float:
                            Error("В множестве после слова \"Первое\" недопустимо вещественное число");
                            break;
                        case Type.Variable:
                            Error("В множестве после слова \"Первое\" недопустима переменная");
                            break;
                        case Type.Comma:
                            Error("В множестве после слова \"Первое\" недопустима запятая");
                            break;
                        case Type.Operator:
                            Error("Знак математической операции недопустим в контексте множества");
                            break;
                        case Type.WTF:
                            Error($"Не удалось определить тип слова \"{word}\" в контексте множества");
                            break;
                        default:
                            break;
                    }

                    current++;
                }
            }
            else if (IsCurrentEqualTo("Второе"))
            {
                bool isPrevComma = true;

                current++;
                foreach (var word in words.Skip(current))
                {
                    if ((word == "Первое" || word == "Второе" || word == "Третье" || word == "Сочетаемое"))
                    {
                        if (!wereElements) Error("В множестве отсутствует хотя бы одно вещественное число");
                        break;
                    }

                    if (Typer.GetTypeOf(word) == Type.Float)
                    {
                        wereElements = true;
                        if (!isPrevComma)
                        {
                            Error("В множестве после слова \"Второе\" вещественные числа должны разделяться запятыми");
                            current++;
                            break;
                        }
                        isPrevComma = false;
                    }

                    if (Typer.GetTypeOf(word) == Type.Comma)
                    {
                        if (isPrevComma)
                        {
                            Error("Пропущено вещественное число");
                            current++;
                            break;
                        }
                        isPrevComma = true;
                    }

                    switch (Typer.GetTypeOf(word))
                    {
                        case Type.NewLine:
                            currentRow++;
                            break;
                        case Type.Int:
                            Error("В множестве после слова \"Второе\" недопустимо целое число");
                            break;
                        case Type.Variable:
                            Error("В множестве после слова \"Второе\" недопустима переменная");
                            break;
                        case Type.Operator:
                            Error("Знак математической операции недопустим в контексте множества");
                            break;
                        case Type.WTF:
                            Error($"Не удалось определить тип слова \"{word}\" в контексте множества");
                            break;
                        default:
                            break;
                    }

                    current++;
                }
            }
            else if (IsCurrentEqualTo("Третье"))
            {
                bool isPrevComma = true;

                current++;

                foreach (var word in words.Skip(current))
                {
                    if ((word == "Первое" || word == "Второе" || word == "Третье" || word == "Сочетаемое"))
                    {
                        if (!wereElements) Error("В множестве отсутствует хотя бы одна переменная");
                        break;
                    }

                    if (Typer.GetTypeOf(word) == Type.Variable)
                    {
                        wereElements = true;
                        if (!isPrevComma)
                        {
                            Error("В множестве после слова \"Третье\" переменные должны разделяться запятыми");
                            current++;
                            break;
                        }
                        isPrevComma = false;
                    }

                    if (Typer.GetTypeOf(word) == Type.Comma)
                    {
                        if (isPrevComma)
                        {
                            Error("Пропущена переменная");
                            current++;
                            break;
                        }
                        isPrevComma = true;
                    }

                    switch (Typer.GetTypeOf(word))
                    {
                        case Type.NewLine:
                            currentRow++;
                            break;
                        case Type.Int:
                            Error("В множестве после слова \"Третье\" недопустимо целое число");
                            break;
                        case Type.Float:
                            Error("В множестве после слова \"Третье\" недопустимо вещественное число");
                            break;
                        case Type.Operator:
                            Error("Знак математической операции недопустим в контексте множества");
                            break;
                        case Type.WTF:
                            Error($"Не удалось определить тип слова \"{word}\" в контексте множества");
                            break;
                        default:
                            break;
                    }
                    current++;
                }
            }
            else Error("Множество должно начинаться с \"Первое:\" или с \"Второе:\" или с \"Третье:\"");
        }
        private void checkLink()
        {
            if (!IsCurrentEqualTo("Сочетаемое")) Error("Звенья должны начинаться со слова \"Сочетаемое\"");

            bool isPrevComma = true;
            bool wereElements = false;

            current++;

            foreach (var word in words.Skip(current))
            {
                if ((isPrevComma == false && word != ",") || word == "Сочетаемое")
                {
                    if (!wereElements) Error("В звене отсутствует хотя бы одно вещественное число");
                    break;
                }

                if (Typer.GetTypeOf(word) == Type.Float)
                {
                    //Тут проверить есть ли следующая запятая
                    wereElements = true;
                    if (!isPrevComma)
                    {
                        Error("В звене вещественные числа должны разделяться запятыми");
                        current++;
                        break;
                    }
                    isPrevComma = false;
                }

                if (Typer.GetTypeOf(word) == Type.Comma)
                {
                    //
                    if (isPrevComma)
                    {
                        Error("Пропущено вещественное число");
                        current++;
                        break;
                    }
                    isPrevComma = true;
                }

                switch (Typer.GetTypeOf(word))
                {
                    case Type.NewLine:
                        currentRow++;
                        break;
                    case Type.Int:
                        Error("В звене недопустимо целое число");
                        break;
                    case Type.Variable:
                        Error("В звене недопустима переменная");
                        break;
                    case Type.Operator:
                        Error("Знак математической операции недопустим в контексте звена");
                        break;
                    case Type.WTF:
                        Error($"Не удалось определить тип слова \"{word}\" в контексте звена");
                        break;
                    default:
                        break;
                }

                current++;
            }
        }

        private void checkOperator()
        {


            CheckMarks();

            string varName = checkVar();
            var segment = CheckMath();

            string result = string.Empty;

            var expression = string.Join(" ", words
                .Skip(segment.Item1)
                .Take(segment.Item2 - segment.Item1 + 1)
                .ToList())
                .Replace("\t", "").Replace(Environment.NewLine, "");

            foreach (var key in variables.Keys)
                expression = expression.Replace(key, variables[key]);
            expression = ComputeFunctions(expression);
            expression = ComputePow(expression);
            try
            {
                result = Convert.ToDouble(new DataTable().Compute(expression.Replace(",", "."), "")).ToString();
            }
            catch (OverflowException)
            {
                Skip(1, false);
                Error("Возникла ошибка в процессе вычислений. Полученные вычисления превысили Int64");
            }
            catch (DivideByZeroException)
            {
                Skip(1, false);
                Error("Возникла ошибка в процессе вычислений. Деление на ноль");
            }
            catch (EvaluateException)
            {
                Skip(1, false);
                Error("Возникла ошибка в процессе вычислений. Полученные вычисления превысили Int64");
            }
            catch (SyntaxErrorException e)
            {
                Error(e.Message);
            }
            variables.Add(varName, result);
        }
        private string checkVar()
        {
            var name = string.Empty;
            if (IsCurrentTypeEqualTo(Type.Variable))
            {
                name = words[current];
                current++;
                if (IsCurrentEqualTo("="))
                {
                    current++;
                    if (IsCurrentTypeEqualTo(Type.Operator) && (words[current] == "-") == false)
                    {
                        Error($"Правая часть не может начинаться с знака математической операции \"{words[current]}\"");
                    }
                    if (IsCurrentTypeEqualTo(Type.Comma))
                    {
                        Error($"Правая часть не может начинаться с запятой");
                    }
                    if (IsCurrentTypeEqualTo(Type.WTF))
                    {
                        Error($"В правой части неизвестный символ: \"{words[current]}\"");
                    }
                    if (current > words.Count)
                    {
                        Error("Пропущена правая часть");
                    }
                }
                else Error("После переменной должно идти \"=\"");
            }
            else Error("Пропущена переменная");

            return name;
        }
        private Tuple<int, int> CheckMath()
        {
            int start = current;
            

            foreach (var word in words.Skip(current))
            {
                if (word == " \x9" || IsNextEqualTo(":") || IsNextEqualTo("="))
                {
                    if (word != " \x9")
                    {
                        if (IsNextEqualTo(":") && Typer.GetTypeOf(word) != Type.Int)
                        {
                            Skip(1, true);
                            Error("Пропущена метка");
                        }
                        if (IsNextEqualTo("=") && Typer.GetTypeOf(word) != Type.Variable)
                        {
                            Skip(1, true);
                            Error("Пропущена переменная");
                        }
                    }

                    Skip(1, false);
                    switch (Typer.GetTypeOf(words[current]))
                    {
                        case Type.Operator:
                            Error("Правая часть не может заканчиваться знаком математической операции");
                            break;
                        case Type.Function:
                            Error("Правая часть не может заканчиваться функцией");
                            break;
                        default:
                            break;
                    }
                    var end = current;
                    Skip(1, true);

                    return Tuple.Create(start, end);
                }

                var prevWord = GetPrev();
                var prevType = Typer.GetTypeOf(prevWord);

                switch (Typer.GetTypeOf(word))
                {
                    case Type.Variable:
                        Error("Значение переменной не определено");
                        break;
                    case Type.Int:
                        switch (prevType)
                        {
                            case Type.Int:
                                Error("Два и более числа не могут стоять подряд");
                                break;
                            case Type.Variable:
                                Error("Переменная и число не могут стоять подряд");
                                break;
                            default:
                                break;
                        }
                        break;
                    case Type.Operator:
                        switch (prevType)
                        {
                            case Type.Operator:
                                if ((word == "!" && prevWord != "!") == false)
                                    Error("Два и более знака математической операции не могут идти подряд");
                                break;
                            case Type.Function:
                                if (word != "-")
                                    Error($"Знак математической операции \"{word}\" не может идти после функции");
                                break;
                            default:
                                break;
                        }
                        break;
                    case Type.Function:
                        switch (prevType)
                        {
                            case Type.Variable:
                                Error("Функция не может идти после переменной");
                                break;
                            case Type.Int:
                                Error("Функция не может идти после числа");
                                break;
                            default:
                                break;
                        }
                        break;
                    case Type.Float:
                        Error("Вещественное число недопустимо в контексте правой части");
                        break;
                    case Type.Comma:
                        Error("Запятая недопустима в контексте правой части");
                        break;
                    case Type.WTF:
                        if(word != " \x9")
                        {
                            Error($"Не удалось определить тип слова \"{word}\"");
                        }                            
                        break;
                    case Type.NewLine:
                        currentRow++;
                        break;
                    default:
                        break;
                }
                current++;
            }

            current--;
            Error("В правой части встречен конец строки");
            return null;
        }

        private void CheckMarks()
        {
            bool wasMarks = false;

            foreach (var word in words.Skip(current))
            {
                if (word == ":")
                {
                    if (wasMarks) break;
                    else Error("Отсутствует хотя бы одна метка");
                }

                switch (Typer.GetTypeOf(word))
                {
                    case Type.Int:
                        wasMarks = true;
                        break;
                    case Type.Variable:
                        Error($"Переменная \"{word}\" недопустима в контексте меток");
                        break;
                    case Type.Float:
                        Error($"Вещественное число \"{word}\" недопустимо в контексте меток");
                        break;
                    case Type.Operator:
                        Error("Знак математической операции недопустим в контексте меток");
                        break;
                    case Type.Comma:
                        Error("Запятая недопустима в контексте меток");
                        break;
                    case Type.NewLine:
                        currentRow++;
                        break;
                    case Type.WTF:
                        Error($"Не удалось определить тип слова \"{word}\" в контексте меток");
                        break;
                    default:
                        break;
                }
                current++;
            }
            current++;
        }

        private string ComputeFunctions(string expression)
        {
            var words = expression.Split(' ').ToList();
            var start = 0;
            while (start < words.Count && !Typer.IsFunction(words[start])) start++;
            if (start == words.Count) return expression;

            var end = start;
            while (end < words.Count && Typer.IsFunction(words[end])) end++;

            var negative = false;
            if (words[end] == "-")
            {
                negative = true;
                words.RemoveAt(end);
            }
            if (words[end][0] == '-')
            {
                negative = true;
                words[end] = words[end].Substring(1);
            }

            var list = new List<string>();
            for (var i = end; i >= start; i--)
                list.Add(words[i]);

            var fmt = new NumberFormatInfo();
            fmt.NegativeSign = "−";
            fmt.NumberGroupSeparator = ".";
            var value = double.Parse(list[0],
                NumberStyles.AllowDecimalPoint,
                NumberFormatInfo.InvariantInfo);
            if (negative) value = -value;

            foreach (var fun in list.Skip(1))
            {
                if (fun == "sin") value = Math.Sin(value);
                else if (fun == "cos") value = Math.Cos(value);
                else value = Math.Abs(value);
            }

            words.RemoveRange(start, end - start + 1);
            words.Insert(start, value.ToString().Replace(",", "."));

            return ComputeFunctions(string.Join(" ", words));
        }
        string ComputePow(string expression)
        {
            var result = "";
            List<string> expr = new List<string>(expression.Split(' '));
            for (int j = 0; j<expr.Count; j++)
            {
                if (Typer.GetTypeOf(expr[j]) == Type.Float)
                {
                    expr[j] = expr[j].Replace('.', ',');
                }
            }
            int i = 0;
            while (i < expr.Count)
            {
                if (expr[i] == "^")
                {
                    var x1 = Convert.ToDouble(expr[i - 1]);
                    var x2 = Convert.ToDouble(expr[i + 1]);
                    expr[i] = Math.Pow(x1, x2).ToString();
                    expr.RemoveAt(i - 1);
                    expr.RemoveAt(i);
                    i--;
                }
                i++;
            }

            result = string.Join(" ", expr.ToArray());
            return result;
        }

        private bool IsCurrentEqualTo(string value)
        {
            if (current >= words.Count)
            {
                Skip(1, false);
            }

            if (IsCurrentTypeEqualTo(Type.NewLine)
                || IsCurrentTypeEqualTo(Type.Tab))
            {
                if (IsCurrentTypeEqualTo(Type.NewLine)) currentRow++;
                current++;
                return IsCurrentEqualTo(value);
            }

            if (words[current] != value)
                return false;
            return true;
        }

        private bool IsCurrentTypeEqualTo(Type typeToFind)
        {
            if (current >= words.Count)
            {
                Skip(1, false);
            }

            if (Typer.GetTypeOf(words[current]) != typeToFind)
                return false;
            return true;
        }

        private bool IsNextEqualTo(string value)
        {
            var count = 0;
            current++;
            while (IsCurrentTypeEqualTo(Type.NewLine) || IsCurrentTypeEqualTo(Type.Tab))
            {
                current++;
                count++;
            }
            var result = words[current] == value;
            current--;
            for (int i = 0; i < count; i++)
                current--;
            return result;
        }


        private void Skip(int count, bool forvard)
        {
            for (int i = 0; i < count; i++)
            {
                current += forvard ? 1 : -1;
                while (IsCurrentTypeEqualTo(Type.NewLine) || IsCurrentTypeEqualTo(Type.Tab))
                {
                    if (IsCurrentTypeEqualTo(Type.NewLine)) currentRow += forvard ? 1 : -1;
                    current += forvard ? 1 : -1;
                }
            }
        }

        public void Error(string msg)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < current; i++) sb.Append(words[i] + " ");
            var str = sb.ToString();
            var ind = currentRow > 0 ? str.LastIndexOf(Environment.NewLine) + 3 : 0;
            var substr = str.Substring(ind);

            var index = input.GetFirstCharIndexFromLine(currentRow);
            index += substr.Contains("\t") ? substr.Length + 1 : substr.Length;
            var length = words[current < words.Count ? current : current - 1].Length;
            throw new MyException(index, length, msg);
        }

        private string GenerateResult()
        {
            var result = "Результаты вычислений:";
            foreach (var key in variables.Keys)
                result += $"{Environment.NewLine}{key} = {variables[key]}";
            return result;
        }

        private string GetPrev()
        {
            var count = 0;
            current--;
            while (IsCurrentTypeEqualTo(Type.NewLine) || IsCurrentTypeEqualTo(Type.Tab))
            {
                current--;
                count++;
            }
            var result = words[current];
            current++;
            for (int i = 0; i < count; i++)
                current++;
            return result;
        }

    }

}