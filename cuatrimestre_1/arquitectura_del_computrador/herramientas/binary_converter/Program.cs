using System;

namespace binary_converter
{
    class Program
    {
		static string DecimalToBinary(int decimalNumber, int binaryBase, string converted) {
            int newDecimalNumber = decimalNumber / binaryBase;
			if (decimalNumber % 2 == 0) {
				converted = "0" + converted;
			} else {
				converted = "1" + converted;
			}
			if (decimalNumber != 1) {
				converted = DecimalToBinary(newDecimalNumber, binaryBase, converted);
			}
			return converted;
		}

        static void Main(string[] args)
        {
            Console.WriteLine("Please introduce the conversion type");
			string conversion = Console.ReadLine();
			int binaryBase = 2;
			string converted = "";

			switch (conversion) {
				case "decimal to binary":
				case "d b":
					Console.WriteLine("Selected decimal to binary");
					Console.WriteLine("Please introduce the decimal number");
					int decimalNumber = int.Parse(Console.ReadLine());
					Console.Write("Introduce the binary base: ");
					binaryBase = int.Parse(Console.ReadLine());
					converted = DecimalToBinary(decimalNumber, binaryBase, converted);
					break;
				default:
					Console.WriteLine("Couldn't find that conversion type");
					break;
			}

			Console.WriteLine(converted);
        }
    }
}
