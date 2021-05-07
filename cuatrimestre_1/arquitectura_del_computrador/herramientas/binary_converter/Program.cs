using System;
using System.Collections.Generic;

namespace binary_converter
{
    class Program
    {
		static string GetHexadecimalEquivalent(string term) {
			List<string> constants = new List<string>() { "A", "B", "C", "D", "E", "F" };

			int parsedTerm = Int16.Parse(term);
			if (parsedTerm < 10) {
				return term;
			}
			return constants[parsedTerm - 10];
		}

		static string DecimalToBinary(int decimalNumber, int binaryBase, string converted) {
            int newDecimalNumber = decimalNumber / binaryBase;
			string rest = Convert.ToString(decimalNumber % binaryBase);
			string sufix = "";
			switch (binaryBase) {
				case 2:
					sufix = "base 2";
					if (rest == "0") {
						converted = "0" + converted;
					} else {
						converted = "1" + converted;
					}
					break;
				case 8:
					sufix = "base 8";
					converted = rest + converted;
					break;
				case 16:
					sufix = "base 16";
					converted = GetHexadecimalEquivalent(rest) + converted;
					break;
			}

			if (decimalNumber != 1 && decimalNumber > binaryBase) {
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
