using System.Text;
using System;

namespace Neopocket.Utils
{
    public static class Crypt
    {
        public static Int32 key = 129;
  
        public static String Transform(String textToEncrypt)
        {
            if (String.IsNullOrEmpty(textToEncrypt))
                return String.Empty;

            StringBuilder inSb = new StringBuilder(textToEncrypt);
            StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
            char c;
            for (int i = 0; i < textToEncrypt.Length; i++)
            {
                c = inSb[i];
                c = (char)(c ^ key);
                outSb.Append(c);
            }
            return outSb.ToString();

        }
    }
}
