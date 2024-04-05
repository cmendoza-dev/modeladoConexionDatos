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

namespace modeladoConexionDatos
{
    public partial class Form1 : Form
    {


        void conectarAuthWindows()
        { 
            string servidor = txtServidor.Text;
            string dbname = txtBD.Text;

            // Concatenar en la cadena de conexión
            string cadenaConexion = $"Data Source={servidor};Initial Catalog={dbname};Integrated Security=True;Encrypt=False";

            // Crear la conexión
            SqlConnection cnx = new SqlConnection(cadenaConexion);

            try
            {
                cnx.Open();
                MessageBox.Show("Autenticación Windows");
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void conectarAuthSQLServer()
        {
            string user = txtUsuario.Text;
            string dbPass = txtPass.Text;

            // Concatenar en la cadena de conexión
            string cadenaConexion = $"Data Source =.; Initial Catalog = tempdb; User ID = {user}; Password = {dbPass}; Encrypt = False";
            
            // Crear la conexión
            SqlConnection cnx = new SqlConnection(cadenaConexion);

            try
            {
                cnx.Open();
                MessageBox.Show("Autenticación SQLServer");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (chckAuth.Checked)
            {
                conectarAuthWindows();
            } else
            {
                conectarAuthSQLServer();
            }

            
        }
    }
}
