namespace FNC
{
    partial class VentanaCYK
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
            this.dGVMatriz = new System.Windows.Forms.DataGridView();
            this.txtCadena = new System.Windows.Forms.TextBox();
            this.butAplicar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGVMatriz)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVMatriz
            // 
            this.dGVMatriz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVMatriz.Location = new System.Drawing.Point(49, 103);
            this.dGVMatriz.Name = "dGVMatriz";
            this.dGVMatriz.Size = new System.Drawing.Size(544, 170);
            this.dGVMatriz.TabIndex = 0;
            // 
            // txtCadena
            // 
            this.txtCadena.Location = new System.Drawing.Point(96, 54);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.Size = new System.Drawing.Size(100, 20);
            this.txtCadena.TabIndex = 1;
            // 
            // butAplicar
            // 
            this.butAplicar.Location = new System.Drawing.Point(221, 52);
            this.butAplicar.Name = "butAplicar";
            this.butAplicar.Size = new System.Drawing.Size(75, 23);
            this.butAplicar.TabIndex = 2;
            this.butAplicar.Text = "Aplicar";
            this.butAplicar.UseVisualStyleBackColor = true;
            this.butAplicar.Click += new System.EventHandler(this.butAplicar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cadena";
            // 
            // VentanaCYK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(626, 315);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butAplicar);
            this.Controls.Add(this.txtCadena);
            this.Controls.Add(this.dGVMatriz);
            this.MaximizeBox = false;
            this.Name = "VentanaCYK";
            this.Text = "Ventana CYK";
            ((System.ComponentModel.ISupportInitialize)(this.dGVMatriz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVMatriz;
        private System.Windows.Forms.TextBox txtCadena;
        private System.Windows.Forms.Button butAplicar;
        private System.Windows.Forms.Label label1;
    }
}