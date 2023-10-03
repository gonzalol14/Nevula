﻿using Microsoft.EntityFrameworkCore.Query.Internal;
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
    }
}
