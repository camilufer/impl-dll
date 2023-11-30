namespace ImplementacionFramework1.Views.MenuViews
{
    partial class FormRegistroUser
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tipoUsuarioCombo = new System.Windows.Forms.ComboBox();
            this.crearNombreEmpresaText = new System.Windows.Forms.TextBox();
            this.iniciarSesion = new System.Windows.Forms.Button();
            this.crearClaveText = new System.Windows.Forms.TextBox();
            this.crearUsuarioText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tipoUsuarioCombo);
            this.panel1.Controls.Add(this.crearNombreEmpresaText);
            this.panel1.Controls.Add(this.iniciarSesion);
            this.panel1.Controls.Add(this.crearClaveText);
            this.panel1.Controls.Add(this.crearUsuarioText);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 394);
            this.panel1.TabIndex = 3;
            // 
            // tipoUsuarioCombo
            // 
            this.tipoUsuarioCombo.FormattingEnabled = true;
            this.tipoUsuarioCombo.Location = new System.Drawing.Point(128, 227);
            this.tipoUsuarioCombo.Name = "tipoUsuarioCombo";
            this.tipoUsuarioCombo.Size = new System.Drawing.Size(245, 21);
            this.tipoUsuarioCombo.TabIndex = 12;
            this.tipoUsuarioCombo.Text = "Tipo de usuario";
            this.tipoUsuarioCombo.SelectedIndexChanged += new System.EventHandler(this.tipoUsuarioCombo_SelectedIndexChanged);
            // 
            // crearNombreEmpresaText
            // 
            this.crearNombreEmpresaText.Location = new System.Drawing.Point(128, 201);
            this.crearNombreEmpresaText.Name = "crearNombreEmpresaText";
            this.crearNombreEmpresaText.Size = new System.Drawing.Size(245, 20);
            this.crearNombreEmpresaText.TabIndex = 11;
            this.crearNombreEmpresaText.Text = "Nombre de la empresa";
            this.crearNombreEmpresaText.TextChanged += new System.EventHandler(this.crearNombreEmpresaText_TextChanged);
            // 
            // iniciarSesion
            // 
            this.iniciarSesion.Location = new System.Drawing.Point(128, 308);
            this.iniciarSesion.Name = "iniciarSesion";
            this.iniciarSesion.Size = new System.Drawing.Size(103, 33);
            this.iniciarSesion.TabIndex = 10;
            this.iniciarSesion.Text = "Aceptar";
            this.iniciarSesion.UseVisualStyleBackColor = true;
            this.iniciarSesion.Click += new System.EventHandler(this.button1_Click);
            // 
            // crearClaveText
            // 
            this.crearClaveText.Location = new System.Drawing.Point(128, 175);
            this.crearClaveText.Name = "crearClaveText";
            this.crearClaveText.Size = new System.Drawing.Size(245, 20);
            this.crearClaveText.TabIndex = 9;
            this.crearClaveText.Text = "Crear contraseña";
            this.crearClaveText.TextChanged += new System.EventHandler(this.crearClaveText_TextChanged);
            // 
            // crearUsuarioText
            // 
            this.crearUsuarioText.Location = new System.Drawing.Point(128, 149);
            this.crearUsuarioText.Name = "crearUsuarioText";
            this.crearUsuarioText.Size = new System.Drawing.Size(245, 20);
            this.crearUsuarioText.TabIndex = 8;
            this.crearUsuarioText.Text = "Crear nombre usuario";
            this.crearUsuarioText.TextChanged += new System.EventHandler(this.crearUsuarioText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(138, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Registrar usuario";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(336, 318);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Volver";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // FormRegistroUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 394);
            this.Controls.Add(this.panel1);
            this.Name = "FormRegistroUser";
            this.Text = "FormRegistroUser";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox tipoUsuarioCombo;
        private System.Windows.Forms.TextBox crearNombreEmpresaText;
        private System.Windows.Forms.Button iniciarSesion;
        private System.Windows.Forms.TextBox crearClaveText;
        private System.Windows.Forms.TextBox crearUsuarioText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}