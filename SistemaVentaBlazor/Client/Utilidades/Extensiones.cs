using Microsoft.JSInterop;
using System.Text;
using System.Security.Cryptography;
using System.Text;

namespace SistemaVentaBlazor.Client.Utilidades
{
    public static class Extensiones
    {


        private static string Key ="C74V3S3G2R4CRCV1"; // 32 bytes (256-bit)

        public static async Task GenerarArchivo(this IJSRuntime js, string nombre, byte[] arrayBytes ) {
            await js.InvokeAsync<object>("DescargarArchivo", nombre, Convert.ToBase64String(arrayBytes));
        }


        public static async Task<string> Encrypt(this IJSRuntime js,string plainText)
        {
            var cifrado = await js.InvokeAsync<string>("encryptData", plainText, Key);
            return cifrado;
        }

        public static async Task<string> Decrypt(this IJSRuntime js,string encryptedText)
        {
            var json = await js.InvokeAsync<string>("decryptData", encryptedText, Key);

            return json;
        }
    }
}
