using Azure.Core;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NevulaForo.Resources
{
    public class Utilities
    {

        public static string EncryptPassword(string clave)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(clave));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string StylizeDate(DateTime date)
        {
            TimeSpan difference = DateTime.Now - date;

            if (difference.TotalDays > 7)
            {
                if (date.Year != DateTime.Now.Year)
                {
                    return date.ToString("dd MMM yyyy");
                }
                else
                {
                    return date.ToString("dd MMM");
                }
            }
            else if (difference.TotalHours > 24)
            {
                return $"hace {(int)difference.TotalDays}d";
            }
            else if (difference.TotalMinutes > 60)
            {
                return $"hace {(int)difference.TotalHours}h";
            }
            else if (difference.TotalSeconds > 60)
            {
                return $"hace {(int)difference.TotalMinutes}m";
            }
            else
            {
                //return "Hace un instante";
                return $"hace {(int)difference.TotalSeconds}s";
            }
        }

        public static string ReduceLineBreaks(string text)
        {
            if(text != null)
            {
                //Eliminar saltos de lineas al principio y al final
                text = Regex.Replace(text, @"^\s*[\r\n]+", "");
                text = Regex.Replace(text, @"[\r\n]+\s*$", "");

                // Utiliza una expresión regular para reemplazar más de 2 \r\n o \r o \n por solo dos
                return Regex.Replace(text, @"(\r\n|\r|\n)\1+", "$1$1");
            } else
            {
                return text;
            }
        }


        public static int valueParameterId(Uri uri, string nameParameter)
        {
            //Uri uri = new Uri(Request.Headers["Referer"].ToString());
            string? valueParameter = HttpUtility.ParseQueryString(uri.Query).Get(nameParameter);

            string[] errorGeneral = { "Error al intentar crear el comentario" };

            if (valueParameter == null || !int.TryParse(valueParameter, out int tempIdPublication))
            {
                //El parametro es nulo o no es entero
                return 0;
            }
            else
            {
                return tempIdPublication;
            }
        }


        public static void SendEmail(String receiver, String subject, String message)
        {

        }
    }
}
