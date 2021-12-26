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
    public partial class Login : Form
    {
      
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ANFSKNC;Initial Catalog=GestionDesBibliotheuqes;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
           
           

        }

        private void button1_Click(object sender, EventArgs e)
        {



            string status = "";
            if (radioButton1.Checked == true)
            {
                status = radioButton1.Text;
            }

            if (radioButton2.Checked == true)
            {
                status = radioButton2.Text;
            }

            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || status.Equals("") )
            {
                MessageBox.Show("Remplis les champs");
            }
            else
            {
                SqlCommand com = new SqlCommand("select Count(*) from dbo.Users  where Name = '" + textBox1.Text+ "' AND  Password = '" + textBox2.Text + "' AND  status = '" + status + "' " , con);

                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();

                da.Fill(dt);
                
               
                                if (dt.Rows[0][0].ToString() == "1")
                                {
                                
                               this.Hide();
                               new Menu(status).Show();
                                 

                            
                                }
                else {  MessageBox.Show("Invalid username or password");}
                                   

                            }
              
              
          



        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
        
                Environment.Exit(0);
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
