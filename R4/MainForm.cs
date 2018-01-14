using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R4
{
    public partial class MainForm : Form
    {
        SqlHelper sql;
        public MainForm()
        {
            InitializeComponent();
            //Otomatik datagridde sıralaması için
            dgvList.AutoGenerateColumns = true;
        }
        private void Listele()
        {
            //Parametrelerin veritabanına gönderilmeden önceki uygunluk kontrolleri
            //Girilen tarih değerler uygun mu kontolü.31.28.2076,30.02.2016 gibi... 
            string msg = "";
            if (Kontrol(ref msg))
            {
                MessageBox.Show(msg, "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //dispose etmesi için using kullanımı
                using (sql = new SqlHelper())
                {
                    //İlgili parametrelerin param listine eklenip,Prosedür adı ve tipini belirleyip ExecuteDataSet methouna gönderilmesi
                    List<SqlParameter> param = new List<SqlParameter>()
                    {
                    new SqlParameter() { ParameterName= "@BASTAR" , DbType= DbType.Int32, Value= Tools.DateToInt(Convert.ToDateTime(txBasTar.Text.Trim()))},
                    new SqlParameter() { ParameterName= "@BITTAR" , DbType= DbType.Int32, Value= Tools.DateToInt(Convert.ToDateTime(txBitTar.Text.Trim()))},
                    new SqlParameter() { ParameterName= "@VALUE" , DbType= DbType.String, Value= txMalKoduAdi.Text.Trim()},
                    };
                    //ExecuteDataReader ile de aynı işlem yapılabilirdi.
                    var r4 = sql.ExecuteDataSet("dbo.LISTELE", CommandType.StoredProcedure, param);
                    //Dönen r4 verileri başarılıysa datagride listeleme,değilse ekrana hata mesajı gönderme
                    if (r4.BasariliMi)
                    {
                        if (r4.Veri != null)
                        {
                            dgvList.DataSource = r4.Veri.Tables[0];
                            dgvList.ClearSelection();
                        }
                    }
                    else
                    {
                        MessageBox.Show(r4.Mesaj, "HATA", MessageBoxButtons.OK, r4.MesajIcon);
                    }
                }
            }
        }

        private bool Kontrol(ref string msg)
        {
            bool durum = false;
            IEnumerable<Control> sortedlist = from muc in spcMain.Panel1.Controls.Cast<Control>()
                                              orderby muc.Name
                                              select muc;
            foreach (Control item in sortedlist)
            {
                if (item is TextBox t)
                {
                    if (t.Name != "txMalKoduAdi")
                    {
                        if (String.IsNullOrEmpty(t.Text.Trim()))
                        {
                            durum = true;
                            msg = t.Tag.ToString() + Tools.uyari1;
                            t.Focus();
                            break;
                        }
                        else
                        {
                            if (Tools.isDateTime(txBitTar.Text.Trim()) == false)
                            {
                                durum = true;
                                msg = t.Tag.ToString() + Tools.uyari2;
                                t.Focus();
                                t.SelectAll();
                                break;
                            }
                        }
                    }
                }
            }
            return durum;
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void txBasTar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Harf girilmesi engelleme
            Tools.IsNumeric(e);
        }

        private void txBitTar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Harf girilmesi engelleme
            Tools.IsNumeric(e);
        }
    }
}
