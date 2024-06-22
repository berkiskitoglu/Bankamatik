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
using System.Reflection;

namespace BankaTest
{
    public partial class Gerceklesmis_Islemler : Form
    {
        public Gerceklesmis_Islemler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=BERK;Initial Catalog=DbBankaTest;Integrated Security=True;");

        private void Gerceklesmis_Islemler_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select TBL1.AD + ' ' + TBL1.SOYAD as 'GONDEREN', TBL2.AD + ' ' + TBL2.SOYAD as 'ALICI' , TUTAR  From TBLHAREKET inner join TBLKISILER as TBL1 on TBLHAREKET.GONDEREN = TBL1.HESAPNO inner join TBLKISILER as TBL2 on TBLHAREKET.ALICI = TBL2.HESAPNO", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;         
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
