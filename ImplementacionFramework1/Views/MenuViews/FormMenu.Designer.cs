namespace ImplementacionFramework1.Views.MenuViews
{
    partial class FormMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.perfilStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelUsrLogueado2 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelNmEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.labelRolEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripPrincipal = new System.Windows.Forms.MenuStrip();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.11929F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.88072F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuStripPrincipal, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(503, 88);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.perfilStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(403, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(100, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // perfilStripMenuItem
            // 
            this.perfilStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelUsrLogueado2,
            this.labelNmEmpresa,
            this.labelRolEmpresa,
            this.agregarUsuarioToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem});
            this.perfilStripMenuItem.Name = "perfilStripMenuItem";
            this.perfilStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.perfilStripMenuItem.Text = "Perfil";
            this.perfilStripMenuItem.Click += new System.EventHandler(this.perfilStripMenuItem_Click);
            // 
            // labelUsrLogueado2
            // 
            this.labelUsrLogueado2.Name = "labelUsrLogueado2";
            this.labelUsrLogueado2.Size = new System.Drawing.Size(180, 22);
            this.labelUsrLogueado2.Click += new System.EventHandler(this.labelUsrLogueado2_Click);
            // 
            // labelNmEmpresa
            // 
            this.labelNmEmpresa.Name = "labelNmEmpresa";
            this.labelNmEmpresa.Size = new System.Drawing.Size(180, 22);
            this.labelNmEmpresa.Click += new System.EventHandler(this.labelNmEmpresa_Click);
            // 
            // labelRolEmpresa
            // 
            this.labelRolEmpresa.Name = "labelRolEmpresa";
            this.labelRolEmpresa.Size = new System.Drawing.Size(180, 22);
            this.labelRolEmpresa.Click += new System.EventHandler(this.rolStripMenuItem_Click);
            // 
            // agregarUsuarioToolStripMenuItem
            // 
            this.agregarUsuarioToolStripMenuItem.Name = "agregarUsuarioToolStripMenuItem";
            this.agregarUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.agregarUsuarioToolStripMenuItem.Text = "Agregar Usuario";
            this.agregarUsuarioToolStripMenuItem.Click += new System.EventHandler(this.agregarUsuarioToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // menuStripPrincipal
            // 
            this.menuStripPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuStripPrincipal.Name = "menuStripPrincipal";
            this.menuStripPrincipal.Size = new System.Drawing.Size(403, 24);
            this.menuStripPrincipal.TabIndex = 0;
            this.menuStripPrincipal.Text = "menuStrip1";
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 359);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormMenu";
            this.Text = "FormMenu";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStripPrincipal;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem perfilStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelUsrLogueado2;
        private System.Windows.Forms.ToolStripMenuItem labelNmEmpresa;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelRolEmpresa;
        private System.Windows.Forms.ToolStripMenuItem agregarUsuarioToolStripMenuItem;
    }
}