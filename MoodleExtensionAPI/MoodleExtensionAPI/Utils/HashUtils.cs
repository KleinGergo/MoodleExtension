﻿using System.Security.Cryptography;
using System.Text;

public static class HashUtils
{
    public static string HashString(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2")); // Convert byte to hexadecimal
            }
            return builder.ToString();
        }
    }

}

