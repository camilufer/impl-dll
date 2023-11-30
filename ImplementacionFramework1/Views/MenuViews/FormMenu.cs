using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ImplementacionFramework1.Views.MenuViews
{


    public partial class FormMenu : Form
    {
        private string connectionString;
        public event EventHandler AbrirForm1Solicitado;
        private string nombreUsuarioLogueado;
        private string nombreEmpresaLogueado;
        private string nombreRolLogueado;
        public FormMenu(string nombreUsuario, string nombreEmpresa, string nombreRol)
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.FormMenuLateral_Load);

            MenuStrip menuStrip1 = new MenuStrip();
            this.MainMenuStrip = menuStrip1;
            this.Controls.Add(menuStrip1);

            CargarMenuDinamico();

            nombreUsuarioLogueado = nombreUsuario;
            labelUsrLogueado2.Text = "Bienvenido(a): " + nombreUsuarioLogueado;

            nombreEmpresaLogueado = nombreEmpresa;
            labelNmEmpresa.Text = nombreEmpresaLogueado;

            nombreRolLogueado = nombreRol;
            labelRolEmpresa.Text = nombreRolLogueado;

            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        }

        private void CargarMenuDinamico()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            List<Tuple<string, ToolStripMenuItem>> menuItems = new List<Tuple<string, ToolStripMenuItem>>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Obtener elementos de menú principales
                string queryElementosMenu = "SELECT ElementoMenuID, Nombre FROM ElementosMenu ORDER BY Orden";
                using (MySqlCommand cmdElementosMenu = new MySqlCommand(queryElementosMenu, connection))
                {
                    using (MySqlDataReader readerElementosMenu = cmdElementosMenu.ExecuteReader())
                    {
                        while (readerElementosMenu.Read())
                        {
                            ToolStripMenuItem menuItem = new ToolStripMenuItem(readerElementosMenu["Nombre"].ToString());
                            menuStripPrincipal.Items.Add(menuItem);
                            menuItems.Add(new Tuple<string, ToolStripMenuItem>(readerElementosMenu["ElementoMenuID"].ToString(), menuItem));
                        }
                    }
                }
            }

            // cargar subelementos después de cerrar el readerElementosMenu
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                foreach (var item in menuItems)
                {
                    CargarSubelementosMenu(connection, item.Item1, item.Item2);
                }
            }
        }


        private void CargarSubelementosMenu(MySqlConnection connection, string elementoMenuID, ToolStripMenuItem menuItem)
        {
            string querySubelementosMenu = "SELECT Componente FROM SubElementosMenu WHERE ElementoMenuID = @ElementoMenuID ORDER BY Orden";
            MySqlCommand cmdSubelementosMenu = new MySqlCommand(querySubelementosMenu, connection);
            cmdSubelementosMenu.Parameters.AddWithValue("@ElementoMenuID", elementoMenuID);

            using (MySqlDataReader readerSubelementosMenu = cmdSubelementosMenu.ExecuteReader())
            {
                while (readerSubelementosMenu.Read())
                {
                    string subMenuName = readerSubelementosMenu["Componente"].ToString();
                    ToolStripMenuItem subMenuItem = new ToolStripMenuItem(subMenuName);

                    subMenuItem.Click += SubMenu_Click;
                    subMenuItem.Tag = subMenuName;

                    menuItem.DropDownItems.Add(subMenuItem);
                }
            }
        }

        private void SubMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem subMenu = sender as ToolStripMenuItem;
            string subMenuName = subMenu.Tag.ToString();

            // Consultar la base de datos para obtener Libreria y Componente basados en subMenuName
            string query = "SELECT Libreria, Componente FROM SubElementosMenu WHERE Nombre = @SubMenuName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SubMenuName", subMenuName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string libreria = reader["Libreria"].ToString();
                            string componente = reader["Componente"].ToString();

                            // Cargar dinámicamente el formulario basado en libreria y componente
                            Form ctrGUI = SmartControl.LoadSmartControl(libreria, componente) as Form;

                            if (ctrGUI != null)
                            {
                                // Mostrar el formulario
                                ctrGUI.Show();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo cargar el formulario: " + subMenuName);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Submenú no encontrado en la base de datos: " + subMenuName);
                        }
                    }
                }
            }
        }

        // muestra o no la opcion agregar usuario del menu
        private void FormMenuLateral_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Rol del usuario: " + nombreRolLogueado);
            if (nombreRolLogueado != "Admin")
            {
                agregarUsuarioToolStripMenuItem.Visible = false;
            }
            else
            {
                agregarUsuarioToolStripMenuItem.Visible = true;
            }

        }



        private void FormAgregar_AbrirForm1Solicitado(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

      

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual (FormPrincipal)
            this.Close();
            // Abre el formulario de inicio de sesión (Form1)
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void menuStripPrincipal_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void labelNmEmpresa2_Click(object sender, EventArgs e)
        {

        }

        private void labelNmEmpresa_Click(object sender, EventArgs e)
        {

        }

        private void perfilStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void labelUsrLogueado2_Click(object sender, EventArgs e)
        {

        }

        private void agregarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Creas una instancia de FormRegistroUser
            FormRegistroUser formRegistroUser = new FormRegistroUser();

            // Antes de mostrarlo, estableces esta instancia de FormPrincipal como la MainForm de FormRegistroUser
             formRegistroUser.MainForm = this;

            // Opcionalmente, ocultas FormPrincipal si deseas que solo se muestre FormRegistroUser
            this.Hide();

            // Finalmente, muestras FormRegistroUser
            formRegistroUser.Show();
        }
    }
}
