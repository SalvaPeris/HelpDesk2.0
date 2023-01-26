using System;

namespace HelpDesk.Server.Utils
{
	public class EncryptString
	{
		/// <summary>
        /// Encripta la cadena que le pases con el carácter que le pases
        /// </summary>
        /// <param name="Cadena"></param>
        /// <param name="Caracter"></param>
        /// <returns></returns>
		public static string Encrypt(string Cadena, char Caracter)
		{
			int emailIndex = Cadena.IndexOf("@");
			int numberOfAsterisks = emailIndex / 2;

			String asterisks = new String(new char[emailIndex - numberOfAsterisks]).Replace('\0', Caracter);

			return string.Concat(asterisks,Cadena.AsSpan(emailIndex - numberOfAsterisks));
		}
	}
}

