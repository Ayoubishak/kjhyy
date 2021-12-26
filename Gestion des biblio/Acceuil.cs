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

namespace Gestion_des_biblio
{
    public partial class Acceuil : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ANFSKNC;Initial Catalog=GestionDesBibliotheuqes;Integrated Security=True");

        public Acceuil()
        {
            InitializeComponent();
            fillChart();
        }

        private void Acceuil_Load(object sender, EventArgs e)
        {

            Timer timer = new Timer();
            timer.Interval = (1 * 1000); // 10 secs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void fillChart()
        {
            // Livres
            SqlCommand com = new SqlCommand("select Count(*) from dbo.Livres ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // Periodique
            SqlCommand comm = new SqlCommand("select Count(*) from dbo.Periodique ", con);
            SqlDataAdapter daa = new SqlDataAdapter(comm);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            // Cds
            SqlCommand com1 = new SqlCommand("select Count(*) from dbo.Cds ", con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            // Emprunts
            SqlCommand com2 = new SqlCommand("select Count(*) from dbo.Emprunt ", con);
            SqlDataAdapter da2 = new SqlDataAdapter(com2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
           if(dt.Rows[0][0].ToString()=="0" && dt1.Rows[0][0].ToString() == "0" && dt2.Rows[0][0].ToString() == "0" && dtt.Rows[0][0].ToString() == "0")
            {
               
                chart1.Titles.Add("Nombre d'ouvrage dans le stock est 0 ");
            }
           else
            {
                chart1.Titles.Add("Nombre d'ouvrage dans le stock");
                chart1.Series["Nombre d'ouvrage"].Points.AddXY("Livres", dt.Rows[0][0].ToString());
               chart1.Series["Nombre d'ouvrage"].Points.AddXY("Periodiques", dtt.Rows[0][0].ToString());
               chart1.Series["Nombre d'ouvrage"].Points.AddXY("Cds", dt1.Rows[0][0].ToString());
               chart1.Series["Nombre d'ouvrage"].Points.AddXY("Emprunts", dt2.Rows[0][0].ToString());
             
            }
           



        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }
        private void timer_Tick(object sender, EventArgs e)
        {



            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
                chart1.Titles.Clear();
            }
            fillChart();
        }

    }
}
