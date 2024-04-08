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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void ConectarAuthWindows()
        {
            string servidor = txtServidor.Text;
            string dbname = txtBD.Text;

            if (string.IsNullOrWhiteSpace(servidor) || string.IsNullOrWhiteSpace(dbname))
            {
                throw new ArgumentException("Por favor ingrese el servidor y la base de datos.");
            }

            string cadenaConexion = $"Data Source={servidor};Initial Catalog={dbname};Integrated Security=True;Encrypt=False";

            SqlConnection cnx = new SqlConnection(cadenaConexion);

            try
            {
                cnx.Open();
                Form2 fr2 = new Form2(cnx, "producto");
                fr2.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error durante la autenticación Windows: " + ex.Message);
            }
            finally
            {
                cnx.Close(); // Cerrar la conexión
            }
        }

        void ConectarAuthSQLServer()
        {
            string user = txtUsuario.Text;
            string dbPass = txtPass.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(dbPass))
            {
                MessageBox.Show("Por favor ingrese el usuario y la contraseña.");
                return;
            }

            string cadenaConexion = $"Data Source =.; Initial Catalog = tempdb; User ID = {user}; Password = {dbPass}; Encrypt = False";

            SqlConnection cnx = new SqlConnection(cadenaConexion);

            try
            {
                cnx.Open();
                Form2 fr2 = new Form2(cnx, "producto");
                fr2.Show();
                Hide();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error durante la autenticación SQL Server: " + ex.Message);
            }
            finally
            {
                cnx.Close(); // Cerrar la conexión
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chckAuth.Checked)
                {
                    ConectarAuthWindows();
                }
                else
                {
                    ConectarAuthSQLServer();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}


