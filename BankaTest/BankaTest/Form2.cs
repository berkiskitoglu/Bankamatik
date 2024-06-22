using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BankaTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=BERK;Initial Catalog=DbBankaTest;Integrated Security=True;");

        public string hesap;
        private void Form2_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesap;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLKISILER where hesapno="+hesap,baglanti);
            
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                LblTC.Text = dr[3].ToString();
                LblTel.Text = dr[4].ToString();

            }
            baglanti.Close();
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı bir kez aç ve işlemleri gerçekleştir
                baglanti.Open();

                // Gönderilen Hesabın Para Artışı
                SqlCommand komut = new SqlCommand("UPDATE TBLHESAP SET BAKIYE=BAKIYE + @p1 where HESAPNO = @hesapNo", baglanti);
                komut.Parameters.AddWithValue("@p1", decimal.Parse(TxtTutar.Text));
                komut.Parameters.AddWithValue("@hesapNo", MskHesapNo.Text);
                komut.ExecuteNonQuery();

                // Gönderen Hesabın Para Azalışı
                SqlCommand komut2 = new SqlCommand("UPDATE TBLHESAP SET BAKIYE=BAKIYE - @k1 where HESAPNO = @gonderenHesapNo", baglanti);
                komut2.Parameters.AddWithValue("@k1", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@gonderenHesapNo", LblHesapNo.Text);
                komut2.ExecuteNonQuery();

                // İşlem tamamlandıktan sonra mesaj göster
                MessageBox.Show("İşlem Gerçekleşti");
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj göster
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gerceklesmis_Islemler fr = new Gerceklesmis_Islemler();
            fr.Show();
        }
    }
}
