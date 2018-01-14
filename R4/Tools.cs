using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R4
{
    internal class Tools
    {
        public static string uyari1 { get { return " tarihi boş geçilemez"; } }
        public static string uyari2 { get { return " tarihi uyumsuz,Kontrol ediniz!"; } }
        internal static bool isDateTime(string metin)
        {
            return Regex.IsMatch(metin, @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
        }
        internal static int DateToInt(DateTime date)
        {
            return Convert.ToInt32(date.ToOADate());
        }
        internal static void IsNumeric(KeyPressEventArgs e)
        {
            if ((int)e.KeyChar > 47 && (int)e.KeyChar <= 57 || Kontrol(e))
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private static bool Kontrol(KeyPressEventArgs e)
        {
            bool durum = false;
            List<string> keys = new List<string>();
            keys.Add(".");
            keys.Add("/");
            keys.Add("-");
            foreach (string item in keys)
            {
                if (e.KeyChar == Convert.ToChar(item))
                {
                    durum = true;
                    break;
                }
            }
            return durum;
        }
    }
}
