using System;

namespace Algorithms.ImplementationHR
{
	class CeaserCipher
	{
		public CeaserCipher()
		{
			System.Console.WriteLine("Please enter the string you want to encrypt, and the number number positions each character to should move up by");
			//string s = "www.abc.xy";
			//int k = 87;
			string s = System.Console.ReadLine();
			int k = Int32.Parse(System.Console.ReadLine());
			System.Console.WriteLine("\nthe string you entered: " + s + " and the number position you want it to move by " + k);
			Execute(s, k); // here we execute the encryption
		}

		private void Execute(string _UncryptedData, int k)
		{
			char[] letters = _UncryptedData.ToCharArray(); // we slipt up the string into seperate characters, so we can encrpty each element
			char[] encryptdData = Encrypt(letters, k); // here, we are going to ecnrpt the data, and return an encrpted version of the array.
			string st = new string(encryptdData); // convert the encrpted data into a string
			System.Console.WriteLine("\n" + "Your encrpted data\n" + st);
		}

		private char[] Encrypt(char[] array, int k)
		{
			char[] ar = array;

			for (int i = 0; i < array.Length; i++)
			{
				// here we are now encrypting each element by using the GetEncrptedLetter() method, which will take in a character ar[], and the number of positions (k)
				// we want our character move it by in the alphabet. 
				ar[i] = GetEncryptedLetter(ar[i], k);
			}

			return ar;
		}

		private char GetEncryptedLetter(char ch, int ky)
		{
			char character = ch;
			int ascii = (int)ch;
			int key = ky;

			// This branching statement decides if the letter we are dealing with is lowercase or uppercase.
			if (ascii > 64 && ascii < 91)
			{
				for (int iteration = 0; iteration < ky; iteration++)
				{
					if (ascii >= 90)
					{
						ascii = 64; // if we come to a point where the ascii limit for uppercase letters is reached, then we reset it to the beginning by, 
										//so we could continue back to the beginning of the alphabet. 
					}

					ascii = ascii + 1; // we iterate throw the alphabet
					character = (char)ascii; // we hold the results. this will change change value until the intercation stop, which then will hold the final value
				}
			}
			else if (ascii > 96 && ascii < 123)
			{
				for (int iteration = 0; iteration < ky; iteration++)
				{
					if (ascii >= 122)
					{
						ascii = 96; // if we come to a point where the ascii limit for lowercase letters is reached, then we reset it to the beginning by, 
										//so we could continue back to the beginning of the alphabet. 
					}

					ascii = ascii + 1;
					character = (char)ascii;
				}
			}

			return character; // we return enctrpted character
		}
	}
}
