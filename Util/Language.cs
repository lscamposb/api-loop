using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace LoopApi.Util
{
    public class Language
    {
        /// <summary>
        /// Devuelve el mensaje en el idioma indicado
        /// </summary>
        /// <param name="pKey">Mensaje a buscar</param>
        /// <param name="pLanguage">Lenguage del mensaje</param>
        /// <returns>Devuelve el mensaje en el idioma que se indica</returns>
        public static string GetMensage(string pKey, string pLanguage)
        {
            string lMensaje = "";

            string lArchivo = "LoopApi.Idioma.{0}";

            switch (pLanguage)
            {
                case "en":
                    lArchivo = string.Format(lArchivo, "InglesUS");
                    break;
                default:
                    lArchivo = string.Format(lArchivo, "EspanolCR");
                    break;
            }

            ResourceManager lRecursosManager = new ResourceManager(lArchivo, Assembly.GetCallingAssembly());
            foreach (DictionaryEntry item in lRecursosManager.GetResourceSet(CultureInfo.CurrentCulture, true, true))
            {
                if (item.Key.ToString().Equals(pKey))
                {
                    lMensaje = item.Value.ToString();
                    break;
                }
            }

            return lMensaje;
        }
    }
}
