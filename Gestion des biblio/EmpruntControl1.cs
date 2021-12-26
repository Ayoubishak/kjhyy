using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_des_biblio
{
    public partial class EmpruntControl1 : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ANFSKNC;Initial Catalog=GestionDesBibliotheuqes;Integrated Security=True");
    
        public EmpruntControl1()
        {
            InitializeComponent();
        }

        private void EmpruntControl1_Load(object sender, EventArgs e)

        {
            LoadAllRecords();
        }

        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("select * from dbo.Emprunt ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
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
                    SqlCommand com = new SqlCommand("delete from dbo.Emprunt where ouvrage_id = '" + row.Cells[0].Value.ToString() + "' and cin = '" + row.Cells[4].Value.ToString() + "'and category = '" + row.Cells[2].Value.ToString() + "'", con);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
                LoadAllRecords();
            }
            else
            {
                SqlCommand com = new SqlCommand("select * from dbo.Emprunt where cin = '" + textBox1.Text + "'   ", con);
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Emprunt.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
        Bitmap bitmap;
        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {


                DataGridViewRow row = dataGridView1.SelectedRows[0];
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[4].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();

                dateTimePicker1.Text = row.Cells[5].Value.ToString();
                dateTimePicker2.Text = row.Cells[6].Value.ToString();

                button4.Enabled = true;

            }
            else
            {  //optional    
                MessageBox.Show("Please select a row");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            if (DateTime.Parse(dateTimePicker1.Text) > DateTime.Parse(dateTimePicker2.Text))
            {
                MessageBox.Show("date d'emprunt bigger than date_return");
            }
            else
            {  
                con.Open();
                Console.WriteLine(dateTimePicker1.Text);
                Console.WriteLine(dateTimePicker2.Text);
                Console.WriteLine(textBox1.Text);
                Console.WriteLine(textBox2.Text);
                Console.WriteLine(textBox3.Text);
                Console.WriteLine(textBox4.Text);
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                SqlCommand com = new SqlCommand("update dbo.Emprunt " +
                    "set  date_emprunt =  '" + DateTime.Parse(dateTimePicker1.Text)+ "' " +
                    ", " +
                    "date_return =  '" + DateTime.Parse(dateTimePicker2.Text) +
                    "'  where ouvrage_id = '" + textBox2.Text.ToString() + "' and cin = '" + textBox3.Text.ToString() + "'and category = '" + textBox4.Text.ToString() + "' ", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("secuussefully Modified");
                LoadAllRecords();

                button4.Enabled = false;
            }
        }
    }
}

