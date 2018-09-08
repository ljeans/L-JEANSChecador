namespace Checador.empleados
{
    partial class huella
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
            this.btn_siguiente2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_huella = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_siguiente2
            // 
            this.btn_siguiente2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(32)))), ((int)(((byte)(105)))));
            this.btn_siguiente2.FlatAppearance.BorderSize = 0;
            this.btn_siguiente2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.btn_siguiente2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_siguiente2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_siguiente2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_siguiente2.ForeColor = System.Drawing.Color.White;
            this.btn_siguiente2.Location = new System.Drawing.Point(325, 290);
            this.btn_siguiente2.Name = "btn_siguiente2";
            this.btn_siguiente2.Size = new System.Drawing.Size(123, 41);
            this.btn_siguiente2.TabIndex = 27;
            this.btn_siguiente2.Text = "Cancelar";
            this.btn_siguiente2.UseVisualStyleBackColor = false;
            this.btn_siguiente2.Click += new System.EventHandler(this.btn_siguiente2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 24);
            this.label1.TabIndex = 30;
            this.label1.Text = "Seleccione huella a capturar:";
            // 
            // cbx_huella
            // 
            this.cbx_huella.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_huella.FormattingEnabled = true;
            this.cbx_huella.Items.AddRange(new object[] {
            "0 (meñique izquierdo)",
            "1 (anular izquierdo)",
            "2 (medio izquierdo)",
            "3 (indice izquierdo)",
            "4 (pulgar izquierdo)",
            "5 (pulgar derecho)",
            "6 (indice  derecho)",
            "7 (medio derecho)",
            "8 (anular derecho)",
            "9 (meñique derecho)"});
            this.cbx_huella.Location = new System.Drawing.Point(109, 317);
            this.cbx_huella.Name = "cbx_huella";
            this.cbx_huella.Size = new System.Drawing.Size(121, 21);
            this.cbx_huella.TabIndex = 29;
            this.cbx_huella.SelectedIndexChanged += new System.EventHandler(this.cbx_huella_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Checador.Properties.Resources.huella;
            this.pictureBox1.Location = new System.Drawing.Point(47, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(401, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // huella
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(510, 369);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_huella);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_siguiente2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "huella";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "huella";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.huella_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_siguiente2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbx_huella;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}