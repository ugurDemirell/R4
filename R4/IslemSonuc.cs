using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R4
{
    public class IslemSonuc
    {
        public bool BasariliMi { get; set; }
        public object OutputValue { get; set; }
        public DateTime IslemTarihi { get; set; }
        public MessageBoxIcon MesajIcon { get; set; }
        public string Mesaj { get; set; }
    }
    public class IslemSonuc<T> : IslemSonuc
    {
        public T Veri { get; set; }
    }
}
