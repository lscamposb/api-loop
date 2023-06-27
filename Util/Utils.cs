namespace LoopApi.Util
{
    public class Utils
    {
        public static string EncriptarContrasena(string contrasena)
        {
            string lContrasena = "";

            byte[] data = System.Text.Encoding.ASCII.GetBytes(contrasena);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            lContrasena = System.Text.Encoding.ASCII.GetString(data);

            return lContrasena;
        }

        public static string GenerarCodigoActivacion()
        {
            string codigo = "";

            Random rnd = new Random();
            codigo = rnd.Next(100000, 999999).ToString();

            return codigo;
        }

        public static void EnviarCodigoActivacion(string codigo, string userName)
        {

        }
    }
}
