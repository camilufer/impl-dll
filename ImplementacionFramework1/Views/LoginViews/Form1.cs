using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using ImplementacionFramework1.Views.MenuViews;
using System.Configuration;

//PROYECTO FINAL FINAL DE FINAL
namespace ImplementacionFramework1
{
    public partial class Form1 : Form
    {
        private int intentosFallidos = 0;
        private const int intentosMaximos = 3;

        // constructor
        public Form1()
        {
            InitializeComponent();

            // cadena de conexión
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // inicia conexión a la base de datos
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Conexión exitosa a la base de datos.");

                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de conexión a la base de datos: " + ex.Message);
            }

            // asigna los manejadores de eventos a los cuadros de texto
            textBox2.GotFocus += TextBox2_GotFocus;
            textBox2.LostFocus += TextBox2_LostFocus;

            textBox3.GotFocus += TextBox3_GotFocus;
            textBox3.LostFocus += TextBox3_LostFocus;

            SetPlaceholder(textBox2, "Usuario");
            SetPlaceholder(textBox3, "Contraseña");

        }

        public void MostrarForm1()
        {
            this.Show();
        }

        private void TextBox2_GotFocus(object sender, EventArgs e)
        {
            if (IsPlaceholder(textBox2))
            {
                ClearPlaceholder(textBox2);
            }
        }

        private void TextBox2_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SetPlaceholder(textBox2, "Usuario");
            }
        }

        private void TextBox3_GotFocus(object sender, EventArgs e)
        {
            if (IsPlaceholder(textBox3))
            {
                ClearPlaceholder(textBox3);
                textBox3.PasswordChar = '*'; // Para ocultar la contraseña
            }
        }

        private void TextBox3_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SetPlaceholder(textBox3, "Contraseña");
                textBox3.PasswordChar = '\0'; // Mostrar el texto de ejemplo en lugar de caracteres ocultos
            }
        }

        // verifica si el texto actual es un placeholder
        private bool IsPlaceholder(TextBox textBox)
        {
            return textBox.Text == textBox.Tag as string;
        }

        // limpia el texto del cuadro
        private void ClearPlaceholder(TextBox textBox)
        {
            textBox.Text = "";
            textBox.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = System.Drawing.SystemColors.GrayText;
            textBox.Tag = placeholder;
        }

        private string ObtenerNombreDeUsuario(string nombreUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string nombreUsuarioLogueado = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT NombreUsuario FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombreUsuarioLogueado = reader["NombreUsuario"].ToString();
                            }
                        }
                    }
                }
            }
            //error de conexión 
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de conexión a la base de datos: " + ex.Message);
            }

            return nombreUsuarioLogueado;
        }

        // obtiene el nombre del rol asociado al usuario
        private string ObtenerNombreRolLogueado(string nombreUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            string nombreRol = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT r.NombreRol
                FROM Usuarios u
                JOIN UsuarioRoles ur ON u.UsuarioID = ur.UsuarioID
                JOIN Roles r ON ur.RolID = r.RolID
                WHERE u.NombreUsuario = @nombreUsuario";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombreRol = reader["NombreRol"].ToString();
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de conexión a la base de datos: " + ex.Message);
            }

            return nombreRol;
        }


        // obtiene el nombre de la empresa desde la configuración de la base de datos
        private string ObtenerNombreDeEmpresa(string nombreUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;


            string nombreEmpresa = string.Empty;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Valor FROM ConfiguracionEmpresa WHERE NombreEmpresa = 'NombreEmpresa';";


                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombreEmpresa = reader["Valor"].ToString();
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de conexión a la base de datos: " + ex.Message);
                // error de conexión 
            }

            return nombreEmpresa;
        }

        private void iniciarSesion_Click(object sender, EventArgs e)
        {
            string nombreUsuario = textBox2.Text;
            string clave = textBox3.Text;

            if (!string.IsNullOrWhiteSpace(nombreUsuario) && !string.IsNullOrWhiteSpace(clave) && (nombreUsuario != "Usuario" && clave != "Contraseña"))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Consulta para verificar si el usuario existe
                        string checkUserQuery = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

                        using (MySqlCommand checkUserCommand = new MySqlCommand(checkUserQuery, connection))
                        {
                            checkUserCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                            long existingUserCount = (long)checkUserCommand.ExecuteScalar();

                            if (existingUserCount == 0)
                            {
                                MessageBox.Show("El usuario no existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                // actualizamos intentos_login ya que el usuario no existe
                                string updateLoginAttemptsQuery = "UPDATE Usuarios SET IntentosLogin = IntentosLogin + 1 WHERE NombreUsuario = @nombreUsuario";

                                using (MySqlCommand updateLoginAttemptsCommand = new MySqlCommand(updateLoginAttemptsQuery, connection))
                                {
                                    updateLoginAttemptsCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                    updateLoginAttemptsCommand.ExecuteNonQuery();
                                }
                                return;
                            }
                        }

                        // consulta para obtener el campo "Activo" de la base de datos
                        string getHabilitadoQuery = "SELECT Activo FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

                        using (MySqlCommand getHabilitadoCommand = new MySqlCommand(getHabilitadoQuery, connection))
                        {
                            getHabilitadoCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                            bool habilitado = Convert.ToBoolean(getHabilitadoCommand.ExecuteScalar());

                            if (!habilitado)
                            {
                                MessageBox.Show("Cuenta deshabilitada. Contacta al administrador.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // salir de la función y bloquear el inicio de sesión
                            }
                        }

                        // consulta para obtener la contraseña cifrada almacenada en la base de datos
                        string getPasswordQuery = "SELECT ContraseñaHash FROM Usuarios WHERE NombreUsuario = @nombreUsuario";
                        using (MySqlCommand getPasswordCommand = new MySqlCommand(getPasswordQuery, connection))
                        {
                            getPasswordCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                            string storedHashedPassword = getPasswordCommand.ExecuteScalar() as string;

                            if (BCrypt.Net.BCrypt.Verify(clave, storedHashedPassword))
                            {
                                // consultar el rol del usuario
                                string getUserRoleQuery = @"
                                    SELECT r.NombreRol 
                                    FROM Usuarios u
                                    JOIN UsuarioRoles ur ON u.UsuarioID = ur.UsuarioID
                                    JOIN Roles r ON ur.RolID = r.RolID
                                    WHERE u.NombreUsuario = @nombreUsuario";

                                using (MySqlCommand getUserRoleCommand = new MySqlCommand(getUserRoleQuery, connection))
                                {
                                    getUserRoleCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                    string userRole = (string)getUserRoleCommand.ExecuteScalar();


                                    // obtener el nombre de la empresa desde la base de datos
                                    string nombreEmpresaLogueado = ObtenerNombreDeEmpresa(nombreUsuario);
                                    // recupera el nombre de usuario desde la base de datos
                                    string nombreUsuarioLogueado = ObtenerNombreDeUsuario(nombreUsuario);

                                    string nombreRolLogueado = ObtenerNombreRolLogueado(nombreUsuario);


                                    FormMenu formMenu = new FormMenu(nombreUsuarioLogueado, nombreEmpresaLogueado, nombreRolLogueado);
                                    formMenu.Show();
                                    this.Hide();
                                }
                            }

                            else
                            {
                                // incrementar los intentos fallidos en la base de datos
                                string updateLoginAttemptsQuery = "UPDATE Usuarios SET IntentosLogin = IntentosLogin + 1 WHERE NombreUsuario = @nombreUsuario";

                                using (MySqlCommand updateLoginAttemptsCommand = new MySqlCommand(updateLoginAttemptsQuery, connection))
                                {
                                    updateLoginAttemptsCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                    updateLoginAttemptsCommand.ExecuteNonQuery();
                                }

                                // obtener el número actualizado de intentos fallidos
                                string getLoginAttemptsQuery = "SELECT IntentosLogin FROM Usuarios WHERE NombreUsuario = @nombreUsuario";
                                using (MySqlCommand getLoginAttemptsCommand = new MySqlCommand(getLoginAttemptsQuery, connection))
                                {
                                    getLoginAttemptsCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                    intentosFallidos = Convert.ToInt32(getLoginAttemptsCommand.ExecuteScalar());
                                }

                                if (intentosFallidos < intentosMaximos)
                                {
                                    MessageBox.Show($"Contraseña incorrecta. Te quedan {intentosMaximos - intentosFallidos} intentos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    MessageBox.Show("Cuenta bloqueada. Contacta al administrador.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    // bloquear la cuenta del usuario
                                    string updateHabilitadoQuery = "UPDATE Usuarios SET Activo = 0 WHERE NombreUsuario = @nombreUsuario";
                                    using (MySqlCommand updateHabilitadoCommand = new MySqlCommand(updateHabilitadoQuery, connection))
                                    {
                                        updateHabilitadoCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                        updateHabilitadoCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error de conexión a la base de datos: " + ex.Message);
                    MessageBox.Show("Error de conexión a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
      

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}