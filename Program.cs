using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Rpn
{
	public class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Calc();
			}
		}

		static void Calc()
		{
			Console.Write("Калькулятор. Задайте выражение вида 2*(2.5+1.5):\r\n");
			var input = Console.ReadLine();

			if (Regex.IsMatch(input, "[a-zA-Z]") || String.IsNullOrEmpty(input))
			{
				Console.WriteLine("Введены буквы или строка пуста. Расчет невозможен.");
				return;
			}

			Console.WriteLine("Результат: " + RPN.Calculate(input));
			Console.WriteLine(RPN.MyTemp(input));
		}

		public class RPN
		{
			static public double Calculate(string input)
			{
				string output = MyTemp(input); //Преобразовывание
				double result = Calculating(output); //Вычисление
				return result;
			}

			//Преобразовывание
			static public string MyTemp(string input)
			{
				string output = string.Empty; //Результат
				Stack<char> operators = new Stack<char>(); //Операторы

				for (int i = 0; i < input.Length; i++)
				{
					if (IsDelimeter(input[i]))
						continue;

					//Получаем число
					if (Char.IsDigit(input[i]))
					{
						while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
						{
							output += input[i];
							i++;

							if (i == input.Length) break;
						}

						output += " ";
						i--;
					}

					//Получаем операторы
					if (IsOperator(input[i]))
					{
						if (input[i] == '(')
							operators.Push(input[i]);
						else if (input[i] == ')')
						{
							char s = operators.Pop();
							while (s != '(')
							{
								output += s.ToString() + ' ';
								s = operators.Pop();
							}
						}
						else
						{
							if (operators.Count > 0)
								if (GetPriority(input[i]) <= GetPriority(operators.Peek())) //приоритет меньше или равен
									output += operators.Pop().ToString() + " ";

							operators.Push(char.Parse(input[i].ToString())); //стек пуст или приоритет выше
						}
					}
				}

				while (operators.Count > 0)
					output += operators.Pop() + " ";

				return output;
			}

			//Вычисление
			static private double Calculating(string input)
			{
				double result = 0; //Результат
				Stack<double> temp = new Stack<double>();

				for (int i = 0; i < input.Length; i++)
				{
					if (Char.IsDigit(input[i]))
					{
						string a = string.Empty;

						while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
						{
							a += input[i];
							i++;
							if (i == input.Length) break;
						}
						temp.Push(double.Parse(a));
						i--;
					}
					else if (IsOperator(input[i]))
					{
						//Два последних числа
						double a = temp.Pop();
						double b = temp.Pop();

						switch (input[i])
						{
							case '+': result = b + a; break;
							case '-': result = b - a; break;
							case '*': result = b * a; break;
							case '/': result = b / a; break;
							case '^': result = Math.Pow(b, a); break;
						}
						temp.Push(result); //Результат в стек
					}
				}
				return temp.Peek();
			}
		}

		//Проверка на пробел и равно
		static private bool IsDelimeter(char c)
		{
			if ((" =".IndexOf(c) != -1))
				return true;
			return false;
		}

		//Проверка на оператор
		static private bool IsOperator(char с)
		{
			if (("+-/*^()".IndexOf(с) != -1))
				return true;
			return false;
		}

		//Приоритет
		static private byte GetPriority(char s)
		{
			switch (s)
			{
				case '(': return 0;
				case ')': return 1;
				case '+': return 2;
				case '-': return 3;
				case '*': return 4;
				case '/': return 4;
				case '^': return 5;
				default: return 6;
			}
		}
	}
}