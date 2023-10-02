using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;
using System.Text;


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

                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string StylizeDate(DateTime fecha)
        {
            TimeSpan diferencia = DateTime.Now - fecha;

            if (diferencia.TotalDays > 7)
            {
                if (fecha.Year != DateTime.Now.Year)
                {
                    return fecha.ToString("dd MMM yyyy");
                }
                else
                {
                    return fecha.ToString("dd MMM");
                }
            }
            else if (diferencia.TotalHours > 24)
            {
                return $"hace {(int)diferencia.TotalDays}d";
            }
            else if (diferencia.TotalMinutes > 60)
            {
                return $"hace {(int)diferencia.TotalHours}h";
            }
            else if (diferencia.TotalSeconds > 60)
            {
                return $"hace {(int)diferencia.TotalMinutes}m";
            }
            else
            {
                //return "Hace un instante";
                return $"hace {(int)diferencia.TotalSeconds}s";
            }
        }
    }
}
