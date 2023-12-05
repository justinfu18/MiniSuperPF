using System.Security.Cryptography;
using System.Text;

namespace MiniSuperPF.Tools
{
    public class Crypto
    {
        string LLavePersonalizada = "APPIhsjdjkahds2738TEMP";
        public string DesEncriptarPassword(string pass)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(pass);
            String R = string.Empty;
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    Byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(LLavePersonalizada));


                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;

                    String s = Convert.ToBase64String(plainTextBytes);

                    Byte[] data = Convert.FromBase64String(s);
                    R = Encoding.UTF8.GetString(tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));

                }
            }
            return R;
        }
        public string EncriptarPassword(String pass)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(pass);
            String R = string.Empty;

            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    Byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(LLavePersonalizada));

                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;


                    String s = Convert.ToBase64String(plainTextBytes);
                    //      byte[] val1 = { 5, 10, 15, 20, 25, 30 };
                    Byte[] data = Convert.FromBase64String(s);

                    R = Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
            return R;
        }

        public string EncriptarEnUnSentido(string Entrada)
        {
            string PorEncriptar = EncriptarPassword(Entrada);
           // PorEncriptar += "PalabraClave";

            SHA256CryptoServiceProvider ProveedorCrypto = new SHA256CryptoServiceProvider();

            byte[] BytesDeEntrada = Encoding.UTF8.GetBytes(PorEncriptar);
            byte[] BytesConHash = ProveedorCrypto.ComputeHash(BytesDeEntrada);

            StringBuilder Resultado = new StringBuilder();
            for (int i = 0; i < BytesConHash.Length; i++)
                Resultado.Append(BytesConHash[i].ToString("x2").ToLower());


            return PorEncriptar;
            
            
           
        }
    }
}
