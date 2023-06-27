using System.Text.RegularExpressions;

namespace LoopApi.Util
{
    public class Validacion
    {
        public static bool EsEmail(string pEmail)
        {		
            string lFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(pEmail, lFormato))
            {
                if (Regex.Replace(pEmail, lFormato, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ValidarContrasena(string contrasena)
        {
            bool valido = false;

            if (contrasena.Length >= 4 && contrasena.Length <= 100)
            {
                valido = true;
            }

            return valido;
        }
    }
}
