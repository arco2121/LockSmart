using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace LockSmart
{
    static class Criptografia
    {
        static private Random caso = new Random();
        static public string Cripta(string testoDaCifrare, string chiave, string vettore)
        {
            byte[] testoBytes = Encoding.UTF8.GetBytes(testoDaCifrare);
            byte[] chiaveBytes = Convert.FromBase64String(chiave);
            byte[] vettoreBytes = Convert.FromBase64String(vettore);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = chiaveBytes;
                aesAlg.IV = vettoreBytes;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(testoBytes, 0, testoBytes.Length);
                        csEncrypt.FlushFinalBlock();
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        static public string DeCripta(string testoCifrato, string chiave, string vettore)
        {
            try
            {
                byte[] testoCifratoBy = Convert.FromBase64String(testoCifrato);
                byte[] chiaveBy = Convert.FromBase64String(chiave);
                byte[] vettoreBy = Convert.FromBase64String(vettore);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = chiaveBy;
                    aesAlg.IV = vettoreBy;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(testoCifratoBy))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Impossibile leggere i dati. PadLock resettato","Kiwi Lock",MessageBoxButtons.OK,MessageBoxIcon.Error);
                File.Delete("Memory.PadLock");
                Environment.Exit(1);
                return "";
            }
        }

        static public string[] GeneraParametri()
        {
            int lunghezzaChiave = 32; 
            int lunghezzaIV = 16;

            byte[] chiaveBytes = new byte[lunghezzaChiave];
            byte[] ivBytes = new byte[lunghezzaIV];
            caso.NextBytes(chiaveBytes);
            caso.NextBytes(ivBytes);
            string chiave = Convert.ToBase64String(chiaveBytes);
            string iv = Convert.ToBase64String(ivBytes);

            return new string[] { chiave, iv };
        }

        static public string GeneraCodice()
        {
            //Alfabetato dei comandi Arduino = "BbYVHCs01245";
            string codice;
            string alfabeto = "!\"#$%&'()*+,-./36789:;<=>?@ADEFGIJKLMNOPQRTUVWXZ[\\]^_`adefghijklmnopqrtuwxyz{|}~";
            string[] parametri = GeneraParametri();
            string chiave = parametri[0];
            string iv = parametri[1];
            int numeroCasuale = caso.Next();
            string testoDaCifrare = numeroCasuale.ToString();
            string crittogramma = Cripta(testoDaCifrare, chiave, iv);
            byte[] crittogrammaBytes = Encoding.UTF8.GetBytes(crittogramma);

            int indice = 0;
            foreach (byte b in crittogrammaBytes)
            {
                indice = (indice + b) % alfabeto.Length;
            }
            indice = (indice ^ numeroCasuale) % alfabeto.Length;
            indice = ((indice << 3) | (indice >> (32 - 3))) % alfabeto.Length;
            codice = alfabeto[indice].ToString();

            return codice;
        }
    }
}
