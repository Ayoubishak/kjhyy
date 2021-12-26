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
    public partial class UsersControl1 : UserControl
    {
        static string nom = "";
        public UsersControl1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ANFSKNC;Initial Catalog=GestionDesBibliotheuqes;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("")||comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
            }
            else
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    
                    SqlCommand comm = new SqlCommand("select Count(*) from dbo.Users  where Name = '" + textBox1.Text + "' ", con);

                    SqlDataAdapter da1 = new SqlDataAdapter(comm);
 
                    DataTable dt1 = new DataTable();

                    da1.Fill(dt1);
                  

                    if (dt1.Rows[0][0].ToString() == "1")
                    {


MessageBox.Show("Name already exist");

                    }
                    else {   
                        con.Open();
                    SqlCommand com = new SqlCommand("insert into dbo.Users(  Name , Password , status ) values ( '" + textBox1.Text + "', '" + textBox2.Text + "', '" + comboBox1.Text + "')", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("secuussefully saved");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox1.Text = "";
                    LoadAllRecords(); }


                }
             else
                {
                    MessageBox.Show("Password and Confirm Password not match ");
                }
            }

        }



        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("select * from Users ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                DataGridViewRow row = dataGridView1.SelectedRows[0];
              
             nom =   textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();

                button3.Enabled = true;

            }
            else
            {  //optional    
                MessageBox.Show("Please select a row");
            }
        }

        private void UsersControl1_Load(object sender, EventArgs e)
        {
            LoadAllRecords();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you confirm to delete ? ", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // DataGridViewSelectedRowCollection row = dataGridView1.SelectedRows;
                    // taking the index of the selected rows and removing/
                    con.Open();
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    // textBox1.Text = row.Cells[1].Value.ToString();
                    //textBox2.Text = row.Cells[2].Value.ToString();
                    SqlCommand com = new SqlCommand("delete from dbo.Users where Name= '" + row.Cells[0].Value.ToString() + "'", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully deleted");
                    LoadAllRecords();
                    //dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {  //optional    
                MessageBox.Show("Please select a row");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
            }
            else
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    SqlCommand comm = new SqlCommand("select Count(*) from dbo.Users  where Name = '" + textBox1.Text + "' ", con);

                    SqlDataAdapter da1 = new SqlDataAdapter(comm);

                    DataTable dt1 = new DataTable();

                    da1.Fill(dt1);


                    if (dt1.Rows[0][0].ToString() == "1")
                    {


                        MessageBox.Show("Name already exist");

                    }
                    else
                    {
                    con.Open();
                    SqlCommand com = new SqlCommand("update dbo.Users set  Name =  '" + textBox1.Text + "' ,Password = '" + textBox2.Text + "', status = '" + comboBox1.Text + "' where Name= ' " + nom + "'", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("secuussefully Modified");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox1.Text = "";
                    LoadAllRecords();

                    button3.Enabled = false;
                    }

                   
                }
                else
                {
                    MessageBox.Show("Password and Confirm Password not match ");
                }
            }
        }
    }
}
