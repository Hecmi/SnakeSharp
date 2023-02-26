
namespace Snake.images
{
    partial class frmColorPicker
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cabeza = new System.Windows.Forms.PictureBox();
            this.torso2 = new System.Windows.Forms.PictureBox();
            this.torso1 = new System.Windows.Forms.PictureBox();
            this.cola = new System.Windows.Forms.PictureBox();
            this.uI_ShadowPanel1 = new UIDC.UI_ShadowPanel();
            this.txt_NombreJugador = new System.Windows.Forms.TextBox();
            this.ia = new UIDC.UI_MaterialToggle();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cabeza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.torso2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.torso1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cola)).BeginInit();
            this.uI_ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Snake.Properties.Resources.barra_Color;
            this.pictureBox1.Location = new System.Drawing.Point(115, 283);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(572, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Snake.Properties.Resources.botones_snake_visto;
            this.pictureBox2.Location = new System.Drawing.Point(699, 361);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(102, 87);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // cabeza
            // 
            this.cabeza.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cabeza.Image = global::Snake.Properties.Resources.snake_parte_33;
            this.cabeza.Location = new System.Drawing.Point(523, 142);
            this.cabeza.Name = "cabeza";
            this.cabeza.Size = new System.Drawing.Size(141, 150);
            this.cabeza.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cabeza.TabIndex = 14;
            this.cabeza.TabStop = false;
            // 
            // torso2
            // 
            this.torso2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.torso2.Image = global::Snake.Properties.Resources.snake_parte_22;
            this.torso2.Location = new System.Drawing.Point(391, 159);
            this.torso2.Name = "torso2";
            this.torso2.Size = new System.Drawing.Size(135, 118);
            this.torso2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.torso2.TabIndex = 13;
            this.torso2.TabStop = false;
            // 
            // torso1
            // 
            this.torso1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.torso1.Image = global::Snake.Properties.Resources.snake_parte_22;
            this.torso1.Location = new System.Drawing.Point(257, 159);
            this.torso1.Name = "torso1";
            this.torso1.Size = new System.Drawing.Size(136, 118);
            this.torso1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.torso1.TabIndex = 12;
            this.torso1.TabStop = false;
            // 
            // cola
            // 
            this.cola.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cola.Image = global::Snake.Properties.Resources.snake_parte_11;
            this.cola.Location = new System.Drawing.Point(115, 159);
            this.cola.Name = "cola";
            this.cola.Size = new System.Drawing.Size(156, 118);
            this.cola.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cola.TabIndex = 11;
            this.cola.TabStop = false;
            // 
            // uI_ShadowPanel1
            // 
            this.uI_ShadowPanel1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(255)))), ((int)(((byte)(155)))));
            this.uI_ShadowPanel1.Controls.Add(this.txt_NombreJugador);
            this.uI_ShadowPanel1.Location = new System.Drawing.Point(168, 22);
            this.uI_ShadowPanel1.Name = "uI_ShadowPanel1";
            this.uI_ShadowPanel1.ParentControl = this;
            this.uI_ShadowPanel1.Radius = 15;
            this.uI_ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.uI_ShadowPanel1.ShadowStyle = UIDC.UI_ShadowPanel.ShadowMode.Centrada;
            this.uI_ShadowPanel1.Size = new System.Drawing.Size(436, 71);
            this.uI_ShadowPanel1.TabIndex = 8;
            // 
            // txt_NombreJugador
            // 
            this.txt_NombreJugador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(255)))), ((int)(((byte)(155)))));
            this.txt_NombreJugador.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_NombreJugador.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NombreJugador.ForeColor = System.Drawing.SystemColors.MenuText;
            this.txt_NombreJugador.Location = new System.Drawing.Point(25, 18);
            this.txt_NombreJugador.MaxLength = 10;
            this.txt_NombreJugador.Name = "txt_NombreJugador";
            this.txt_NombreJugador.Size = new System.Drawing.Size(366, 37);
            this.txt_NombreJugador.TabIndex = 2;
            this.txt_NombreJugador.Text = "nothing";
            this.txt_NombreJugador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ia
            // 
            this.ia.AutoSize = true;
            this.ia.EllipseBorderColor = "#3b73d1";
            this.ia.EllipseColor = "#508ef5";
            this.ia.Location = new System.Drawing.Point(163, 415);
            this.ia.Name = "ia";
            this.ia.Size = new System.Drawing.Size(47, 19);
            this.ia.TabIndex = 10;
            this.ia.Text = "uI_MaterialToggle1";
            this.ia.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(16, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 31);
            this.label1.TabIndex = 9;
            this.label1.Text = "Modo CPU:";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 600;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(194)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(812, 459);
            this.Controls.Add(this.cabeza);
            this.Controls.Add(this.torso2);
            this.Controls.Add(this.torso1);
            this.Controls.Add(this.cola);
            this.Controls.Add(this.uI_ShadowPanel1);
            this.Controls.Add(this.ia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmColorPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmColorPicker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmColorPicker_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cabeza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.torso2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.torso1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cola)).EndInit();
            this.uI_ShadowPanel1.ResumeLayout(false);
            this.uI_ShadowPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox cabeza;
        private System.Windows.Forms.PictureBox torso2;
        private System.Windows.Forms.PictureBox torso1;
        private System.Windows.Forms.PictureBox cola;
        private UIDC.UI_ShadowPanel uI_ShadowPanel1;
        private System.Windows.Forms.TextBox txt_NombreJugador;
        private UIDC.UI_MaterialToggle ia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
    }
}