using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_des_biblio
{
    public partial class Menu : Form
    {
        string role;
        public Menu( string role)
        {
            this.role = role;


            InitializeComponent();
            
            
            livresControl1.Hide();
            periodiqueControl11.Hide();
            cdsControl11.Hide();
            empruntControl11.Hide();
            acceuil1.Show();
            usersControl11.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            livresControl1.Show();
            periodiqueControl11.Hide();
            cdsControl11.Hide();
            empruntControl11.Hide();
            acceuil1.Hide();
            usersControl11.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            acceuil1.Hide();
            livresControl1.Hide();
            periodiqueControl11.Hide();
            cdsControl11.Show();
            empruntControl11.Hide();
            usersControl11.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            livresControl1.Hide();
            periodiqueControl11.Show();
            cdsControl11.Hide();
            empruntControl11.Hide();
            acceuil1.Hide();
            usersControl11.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            livresControl1.Hide();
            periodiqueControl11.Hide();
            cdsControl11.Hide();
            empruntControl11.Show();
            acceuil1.Hide();
            usersControl11.Hide();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
         
                Environment.Exit(0);
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            if(role.Equals("Admin"))
            {
                button6.Visible = true;

            }
            else
            {
                button6.Visible = false;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            livresControl1.Hide();
            periodiqueControl11.Hide();
            cdsControl11.Hide();
            empruntControl11.Hide();
            acceuil1.Show();
            usersControl11.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            livresControl1.Hide();
            periodiqueControl11.Hide();
            cdsControl11.Hide();
            empruntControl11.Hide();
            acceuil1.Hide();
            usersControl11.Show();
        
        }
    }
}
