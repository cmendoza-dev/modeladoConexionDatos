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
        private string cadenaConexion;
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

            cadenaConexion = $"Data Source={servidor};Initial Catalog={dbname};Integrated Security=True;Encrypt=False";

            using (SqlConnection cnx = new SqlConnection(cadenaConexion))
            {
                try
                {
                    if (chckAuth.Checked)
                    {
                        cnx.Open();
                        Form2 fr2 = new Form2();
                        fr2.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Activar autenticación con Windows");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error durante la autenticación Windows: " + ex.Message);
                }
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

            using (SqlConnection cnx = new SqlConnection(cadenaConexion))
            {
                try
                {
                    cnx.Open();
                    Form2 fr2 = new Form2();
                    fr2.Show();
                    Hide();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error durante la autenticación SQL Server: " + ex.Message);
                }
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            SqlConnection cnx = null;

            try
            {
                ConectarAuthWindows();
                return; // Si la autenticación con Windows fue exitosa, no necesitamos continuar con la autenticación de SQL Server
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                cnx = new SqlConnection(cadenaConexion);
                cnx.Open();

                // Si llegamos aquí, la autenticación de SQL Server fue exitosa
                Form2 fr2 = new Form2(cnx, "Negocios"); // Ajusta el nombre de la tabla según tu caso
                fr2.Show();
                this.Hide();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error durante la autenticación SQL Server: " + ex.Message);
            }
            finally
            {
                cnx?.Close(); // Asegúrate de cerrar la conexión en caso de que se haya abierto
            }
        }

    }
}


