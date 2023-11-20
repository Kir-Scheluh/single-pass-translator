using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Koryagin
{
	public static class Typer
	{
		public static Type GetTypeOf(string value)
		{
			if (IsInt(value)) return Type.Int;
			if (IsFloat(value)) return Type.Float;
			if (IsVariable(value)) return Type.Variable;
			if (IsComma(value)) return Type.Comma;
			if (IsOperator(value)) return Type.Operator;
			if (IsNewLine(value)) return Type.NewLine;
			if (IsTab(value)) return Type.Tab;
			if (IsFunction(value)) return Type.Function;

			return Type.WTF;
		}

        public static bool IsFloat(string value)
        {
			return Regex.IsMatch(value, "\\d+(\\.\\d+)$");
		}

        public static bool IsComma(string value)
        {
			return value == ",";
		}

		public static bool IsFunction(string value)
		{
			string[] func = { "sin", "cos", "abs" };
			return func.Contains(value);
		}

		public static bool IsNewLine(string value)
		{
			return value == Environment.NewLine;
		}

		public static bool IsTab(string value)
		{
			return value == "\t";
		}

		public static bool IsOperator(string value)
		{
			var list = new[] { "+", "-", "*", "/", "^"};
			return list.Contains(value);
		}

		public static bool IsVariable(string value)
		{
			string[] func = { "sin", "cos", "abs" };
			if (func.Contains(value))
				return false;
			return Regex.IsMatch(value, @"^[а-яА-ЯёЁa-zA-Z]([а-яА-ЯёЁa-zA-Z0-9]+)?$");
		}

		public static bool IsInt(string value)
		{
			return Regex.IsMatch(value, @"^[0-9]+$");
		}
	}
}