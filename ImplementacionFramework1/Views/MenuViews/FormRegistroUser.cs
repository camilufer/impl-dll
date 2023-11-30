using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using ImplementacionFramework1.Views.MenuViews;
using System.Configuration;

namespace ImplementacionFramework1.Views.MenuViews
{
    public partial class FormRegistroUser : Form
    {

        public Form MainForm { get; set; }
        public FormRegistroUser()
        {
            InitializeComponent();
            // Asigna los manejadores de eventos a los cuadros de texto
            crearUsuarioText.GotFocus += TextBox_GotFocus;
            crearUsuarioText.LostFocus += TextBox_LostFocus;
            crearClaveText.GotFocus += TextBox_GotFocus;
            crearClaveText.LostFocus += TextBox_LostFocus;
            crearNombreEmpresaText.GotFocus += TextBox_GotFocus;
            crearNombreEmpresaText.LostFocus += TextBox_LostFocus;

            SetPlaceholder(crearUsuarioText, "Nombre de usuario");
            SetPlaceholder(crearClaveText, "Contraseña");
            SetPlaceholder(crearNombreEmpresaText, "Nombre de la empresa");

            // Rellenar el ComboBox con opciones de tipo de usuario desde la base de datos
            FillTipoUsuarioComboBox();
        }


        
        // completa el combobox
        private void FillTipoUsuarioComboBox()
        {
            // Conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener tipos de usuario desde la base de datos
                string getRolesQuery = "SELECT NombreRol FROM Roles";

                using (MySqlCommand getRolesCommand = new MySqlCommand(getRolesQuery, connection))
                {
                    using (MySqlDataReader reader = getRolesCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipoUsuario = reader.GetString("NombreRol");
                            tipoUsuarioCombo.Items.Add(tipoUsuario);
                        }
                    }
                }
            }
        }


        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (IsPlaceholder(textBox))
            {
                ClearPlaceholder(textBox);
            }
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholder(textBox, textBox == crearUsuarioText ? "Nombre de usuario" : "Contraseña");
            }
        }

        private bool IsPlaceholder(TextBox textBox)
        {
            return textBox.Text == textBox.Tag as string;
        }

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



      

 
        private void crearNombreEmpresa_TextChanged(object sender, EventArgs e)
        {

        }


        private void cerrarSesionBtn_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual (FormPrincipal)
            this.Close();

            // Abre el formulario de inicio de sesión (Form1)
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                string nombreUsuario = crearUsuarioText.Text;
                string clave = crearClaveText.Text;
                string nombreEmpresa = crearNombreEmpresaText.Text; 
                string tipoDeUsuario = tipoUsuarioCombo.Text;


                if (nombreUsuario != "Nombre de usuario" && clave != "Contraseña" && nombreEmpresa != "Nombre de la empresa")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            string checkUserQuery = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

                            using (MySqlCommand checkUserCommand = new MySqlCommand(checkUserQuery, connection))
                            {
                                checkUserCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                long existingUserCount = (long)checkUserCommand.ExecuteScalar();

                                if (existingUserCount > 0)
                                {
                                    MessageBox.Show("El nombre de usuario ya está en uso. Elija otro nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Insertar nuevo usuario
                            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(clave);
                            string insertUserQuery = "INSERT INTO Usuarios (NombreUsuario, ContraseñaHash, Activo) VALUES (@nombreUsuario, @clave, 1)";
                            using (MySqlCommand insertCommand = new MySqlCommand(insertUserQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                insertCommand.Parameters.AddWithValue("@clave", hashedPassword);
                                insertCommand.Parameters.AddWithValue("@nombreEmpresa", nombreEmpresa); // Agregar "nombre_empresa"

                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Obtener el ID del rol basado en el nombre del rol
                                    string getRoleIdQuery = "SELECT RolID FROM Roles WHERE NombreRol = @tipoDeUsuario";
                                    int rolId;
                                    using (MySqlCommand getRoleIdCommand = new MySqlCommand(getRoleIdQuery, connection))
                                    {
                                        getRoleIdCommand.Parameters.AddWithValue("@tipoDeUsuario", tipoDeUsuario);
                                        rolId = Convert.ToInt32(getRoleIdCommand.ExecuteScalar());
                                    }

                                    // Insertar la relación usuario-rol
                                    string insertUserRoleQuery = "INSERT INTO UsuarioRoles (UsuarioID, RolID) VALUES ((SELECT UsuarioID FROM Usuarios WHERE NombreUsuario = @nombreUsuario), @rolId)";
                                    using (MySqlCommand insertUserRoleCommand = new MySqlCommand(insertUserRoleQuery, connection))
                                    {
                                        insertUserRoleCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                                        insertUserRoleCommand.Parameters.AddWithValue("@rolId", rolId);
                                        insertUserRoleCommand.ExecuteNonQuery();
                                    }

                                    MessageBox.Show("Registro exitoso en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo insertar el registro en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Por favor, complete todos los campos formuser.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void crearUsuarioText_TextChanged(object sender, EventArgs e)
        {

        }

        private void crearClaveText_TextChanged(object sender, EventArgs e)
        {

        }

        private void crearNombreEmpresaText_TextChanged(object sender, EventArgs e)
        {

        }

        private void tipoUsuarioCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            {
                // Asegúrate de que MainForm no sea null antes de intentar utilizarlo
                if (MainForm != null)
                {
                    // Simplemente muestra la instancia de FormPrincipal que ya existe.
                    MainForm.Show();
                }
                else
                {
                    // Si por alguna razón MainForm es null, maneja ese caso aquí
                    // (Por ejemplo, mostrando un mensaje de error o creando una nueva instancia).
                    MessageBox.Show("nullo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                // Cierra el formulario actual (FormRegistroUser)
                this.Close();
            }
        }
    }
}
