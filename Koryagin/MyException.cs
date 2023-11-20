using System;

namespace Koryagin
{
	public class MyException : Exception
	{
		public int Index { get; set; }
		public int Length { get; set; }
		public MyException(int index, int length, string message)
		: base(message) 
		{
			Index = index;
			Length = length;
		}
	}
}