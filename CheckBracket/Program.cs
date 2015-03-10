using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckBracket
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine (IsWellFormedBrackets("[(x+y)]"));
			Console.WriteLine (AreParenthesesBalanced("{[x+y] ^ (2x-z)}"));
			Console.WriteLine (CheckBalancedParentheses("{[x+y]}"));
		}

		/// <summary>
		/// Checks to see if brackets are well formed.
		/// Passes "Valid parentheses" challenge on www.codeeval.com,
		/// which is a programming challenge site much like www.projecteuler.net.
		/// http://stackoverflow.com/questions/2509358/how-to-find-validity-of-a-string-of-parentheses-curly-brackets-and-square-brack
		/// </summary>
		/// <param name="input">Input string, consisting of nothing but various types of brackets.</param>
		/// <returns>True if brackets are well formed, false if not.</returns>
		static bool IsWellFormedBrackets(string input)
		{
			string previous = "";
			while (input.Length != previous.Length)
			{
				previous = input;
				input = input
					.Replace("()", String.Empty)
					.Replace("[]", String.Empty)
					.Replace("{}", String.Empty);                
			}
			return (input.Length == 0);
		}



		/// <summary>
		/// Using two stacks, one for allowed left char and other for allowed right chars.
		/// http://stackoverflow.com/questions/1380610/checking-string-has-balanced-parentheses
		/// </summary> 
		/// <param name="input">Input string, consisting of nothing but various types of brackets.</param>
		/// <returns>True if brackets are well formed, false if not.</returns>
		static char[] allowedLeftChars = { '(', '[', '{' };
		static char[] allowedRightChars = { ')', ']', '}' };

		static bool AreParenthesesBalanced(string input)
		{
			var items = new Stack<int>(input.Length);

			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (allowedLeftChars.Any(x => x == c))
					items.Push(i);
				else if (allowedRightChars.Any(x => x == c))
				{
					if (items.Count == 0)
					{
						return false;
					}
					items.Pop();
				}
			}
			if (items.Count > 0)
			{
				return false;
			}
			return true;
		}


		/// <summary>
		/// Using Hashset of matching starting and ending pairs and Linq instead of for-loops
		/// uses a hash-based implementation, these operation are O(1).
		/// As opposed to List for example, which is O(n) for Contains and Remove
		/// http://stackoverflow.com/questions/4558754/define-what-is-a-hashset
		/// http://codereview.stackexchange.com/questions/67602/check-for-balanced-parentheses
		/// </summary> 
		/// <param name="input">Input string, consisting of nothing but various types of brackets.</param>
		/// <returns>True if brackets are well formed, false if not.</returns>
		static HashSet<char> allowedChars = new HashSet<char>(new []{'(', '[', '{', ')', ']', '}' });

		static bool CheckBalancedParentheses(string input)
		{
			var stack = new Stack<char>(input.Where(c => allowedChars.Contains(c)));

			return (stack.Count % 2 == 0);
		}
	}
}
