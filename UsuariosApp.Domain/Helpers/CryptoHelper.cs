using System.Security.Cryptography;
using System.Text;

namespace UsuariosApp.Domain.Helpers
{
    /// <summary>
    /// Classe auxiliar para criptografia
    /// </summary>
    public class CryptoHelper
    {
        /// <summary>
        /// Método para retornar um valor criptografado
        /// com algoritmo SHA256
        /// </summary>
        public static string GetSHA256(string value)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Converte a string para array de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(value);

                // Computa o hash
                byte[] hashBytes = sha256Hash.ComputeHash(bytes);

                // Converte os bytes do hash para string hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
