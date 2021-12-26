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
    public partial class EmpruntForm : Form
    {
       
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ANFSKNC;Initial Catalog=GestionDesBibliotheuqes;Integrated Security=True");
        int Id;
         string Titre;
        string Category;
        public EmpruntForm(int id ,string Titre ,string Category)
        {
            this.Id = id;
            this.Titre = Titre;
            this.Category = Category;
             InitializeComponent();
          
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("")
                || textBox3.Text.Equals("") || textBox4.Text.Equals("")
                || textBox5.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
            }
            else
            {
                SqlCommand coun = new SqlCommand("select Count(*) from dbo.Emprunt where cin = '" + textBox5.Text + "' group by cin  ", con);
                SqlDataAdapter d = new SqlDataAdapter(coun);

                DataTable d1 = new DataTable();
                d.Fill(d1);
                if (d1.Rows.Count > 0)
                {

                    if (int.Parse(d1.Rows[0][0].ToString()) >= 3)
                    {
                        MessageBox.Show("impossible le client ila 3 ouvrage");
                    }

                    else
                    {

                   
                        SqlCommand comm = new SqlCommand("select Count(*) from dbo.Emprunt  where ouvrage_id = '" + textBox1.Text + "' AND  cin = '" + textBox5.Text + "' and category = '" + textBox3.Text + "' ", con);

                        SqlDataAdapter da = new SqlDataAdapter(comm);

                        DataTable dt = new DataTable();

                        da.Fill(dt);
                        if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                        {
                            MessageBox.Show("impossible le client ila deja cet ouvrage");
                        }
                        else
                        {

                            con.Open();
                            SqlCommand com = new SqlCommand("insert into dbo.Emprunt(ouvrage_id,ouvrage_Titre,category,nom_complet,cin,date_emprunt,date_return ) " +
                                "values ( '" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + DateTime.Parse(dateTimePicker1.Text) + "','" + DateTime.Parse(dateTimePicker2.Text) + "')", con);
                            com.ExecuteNonQuery();
                            con.Close();
                            button1.Enabled = false;
                            MessageBox.Show("secuussefully saved");
                            //Gerer le stock 
                            if (Category.Equals("Livres"))
                            {

                                SqlCommand Cds = new SqlCommand("select * from Livres  where Id = '" + Id + "' ", con);

                                SqlDataAdapter Cdsda = new SqlDataAdapter(Cds);

                                DataTable Cdsdt = new DataTable();

                                Cdsda.Fill(Cdsdt);
                                Console.WriteLine("HAAAAAAA" + int.Parse(Cdsdt.Rows[0]["Stock"].ToString()));

                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) > 1)
                                {
                                    con.Open();
                                    SqlCommand com1 = new SqlCommand("update Livres  set   Stock = '" + (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) - 1) + "' where Id = ' " + Id + "'", con);
                                    com1.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) == 1)
                                {
                                    con.Open();
                                    SqlCommand com11 = new SqlCommand("delete from Livres  where Id = '" + Id + "'", con);
                                    com11.ExecuteNonQuery();
                                    con.Close();

                                }

                            }

                            if (Category.Equals("Cds"))
                            {

                                SqlCommand Cds = new SqlCommand("select * from Cds  where Id = '" + Id + "' ", con);


                                SqlDataAdapter Cdsda = new SqlDataAdapter(Cds);

                                DataTable Cdsdt = new DataTable();

                                Cdsda.Fill(Cdsdt);
                                Console.WriteLine("HAAAAAAA" + int.Parse(Cdsdt.Rows[0]["Stock"].ToString()));

                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) > 1)
                                {
                                    con.Open();
                                    SqlCommand com2 = new SqlCommand("update Cds set   Stock = '" + (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) - 1) + "' where Id = ' " + Id + "'", con);
                                    com2.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) == 1)
                                {
                                    con.Open();
                                    SqlCommand com22 = new SqlCommand("delete from Cds  where Id = '" + Id + "'", con);
                                    com22.ExecuteNonQuery();
                                    con.Close();

                                }

                            }
                            if (Category.Equals("Periodique"))
                            {
                                SqlCommand Cds = new SqlCommand("select * from Periodique  where Id = '" + Id + "' ", con);
                                // SqlCommand Cds = new SqlCommand("exec d_table1'" + Category+ "' , '"+Id.ToString()+"' ", con);

                                SqlDataAdapter Cdsda = new SqlDataAdapter(Cds);

                                DataTable Cdsdt = new DataTable();

                                Cdsda.Fill(Cdsdt);

                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) > 1)
                                {
                                    con.Open();
                                    SqlCommand com3 = new SqlCommand("update Periodique  set   Stock = '" + (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) - 1) + "' where Id = ' " + Id + "'", con);
                                    com3.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) == 1)
                                {
                                    con.Open();
                                    SqlCommand com33 = new SqlCommand("delete from Periodique  where Id = '" + Id + "'", con);

                                    com33.ExecuteNonQuery();
                                    con.Close();
                                }

                            }
                            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                            LoadAllRecords();
                        }
                    }
                }


                // New  User +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                else
                {
                 con.Open();
                            SqlCommand com = new SqlCommand("insert into dbo.Emprunt(ouvrage_id,ouvrage_Titre,category,nom_complet,cin,date_emprunt,date_return ) " +
                                "values ( '" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + DateTime.Parse(dateTimePicker1.Text) + "','" + DateTime.Parse(dateTimePicker2.Text) + "')", con);
                            com.ExecuteNonQuery();
                            con.Close();
                            button1.Enabled = false;
                            MessageBox.Show("secuussefully saved");
                            //Gerer le stock 
                            if (Category.Equals("Livres"))
                            {

                                SqlCommand Cds = new SqlCommand("select * from Livres  where Id = '" + Id + "' ", con);

                                SqlDataAdapter Cdsda = new SqlDataAdapter(Cds);

                                DataTable Cdsdt = new DataTable();

                                Cdsda.Fill(Cdsdt);
                                Console.WriteLine("HAAAAAAA" + int.Parse(Cdsdt.Rows[0]["Stock"].ToString()));

                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) > 1)
                                {
                                    con.Open();
                                    SqlCommand com1 = new SqlCommand("update Livres  set   Stock = '" + (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) - 1) + "' where Id = ' " + Id + "'", con);
                                    com1.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) == 1)
                                {
                            con.Open();
                            SqlCommand com11 = new SqlCommand("delete from Livres  where Id = '" + Id + "'", con);
                            com11.ExecuteNonQuery();
                            con.Close();

                        }

                            }

                            if (Category.Equals("Cds"))
                            {

                                SqlCommand Cds = new SqlCommand("select * from Cds  where Id = '" + Id + "' ", con);


                                SqlDataAdapter Cdsda = new SqlDataAdapter(Cds);

                                DataTable Cdsdt = new DataTable();

                                Cdsda.Fill(Cdsdt);
                                Console.WriteLine("HAAAAAAA" + int.Parse(Cdsdt.Rows[0]["Stock"].ToString()));

                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) > 1)
                                {
                                    con.Open();
                                    SqlCommand com2 = new SqlCommand("update Cds set   Stock = '" + (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) - 1) + "' where Id = ' " + Id + "'", con);
                                    com2.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) == 1)
                                {
                            con.Open();
                                    SqlCommand com22 = new SqlCommand("delete from Cds  where Id = '" + Id + "'", con);
                            com22.ExecuteNonQuery();
                            con.Close();

                        }

                            }
                            if (Category.Equals("Periodique"))
                            {
                                SqlCommand Cds = new SqlCommand("select * from Periodique  where Id = '" + Id + "' ", con);
                                // SqlCommand Cds = new SqlCommand("exec d_table1'" + Category+ "' , '"+Id.ToString()+"' ", con);

                                SqlDataAdapter Cdsda = new SqlDataAdapter(Cds);

                                DataTable Cdsdt = new DataTable();

                                Cdsda.Fill(Cdsdt);

                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) > 1)
                                {
                                    con.Open();
                                    SqlCommand com3 = new SqlCommand("update Periodique  set   Stock = '" + (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) - 1) + "' where Id = ' " + Id + "'", con);
                                    com3.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (int.Parse(Cdsdt.Rows[0]["Stock"].ToString()) == 1)
                                {
                            con.Open();
                                    SqlCommand com33 = new SqlCommand("delete from Periodique  where Id = '" + Id + "'", con);

                            com33.ExecuteNonQuery();
                            con.Close();
                        }

                            }
                            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                            LoadAllRecords();
                }
           

                 
               
            }
        }

        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("select * from dbo.Emprunt ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void EmpruntForm_Load(object sender, EventArgs e)
        {
           // textBox2.Text = CdsControl1.Titre;
            //textBox3.Text = CdsControl1.Category;
            //textBox1.Text = CdsControl1.Id.ToString();

            textBox2.Text =Titre;
            textBox3.Text = Category;
            textBox1.Text = Id.ToString();
            LoadAllRecords();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
                LoadAllRecords();
            }
            else
            {
                SqlCommand com = new SqlCommand("select Count(*) as nombre_emprunt  from dbo.Emprunt where cin = '" + textBox6.Text + "' group by cin  ", con);
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            
            }


        
        }
    }
}
