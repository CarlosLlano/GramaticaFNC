namespace FNC
{
    partial class VentanaPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.txtSalida = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butPaso1 = new System.Windows.Forms.Button();
            this.butPaso2 = new System.Windows.Forms.Button();
            this.butPaso3 = new System.Windows.Forms.Button();
            this.butPaso5 = new System.Windows.Forms.Button();
            this.butPaso6 = new System.Windows.Forms.Button();
            this.txtNoAlcanzables = new System.Windows.Forms.TextBox();
            this.txtNoTerminables = new System.Windows.Forms.TextBox();
            this.txtAnulables = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.butPaso4 = new System.Windows.Forms.Button();
            this.comboBoxConjunto = new System.Windows.Forms.ComboBox();
            this.txtConjUnitario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butLimpiar = new System.Windows.Forms.Button();
            this.butCYK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtEntrada
            // 
            this.txtEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrada.Location = new System.Drawing.Point(24, 58);
            this.txtEntrada.Multiline = true;
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(260, 223);
            this.txtEntrada.TabIndex = 0;
            // 
            // txtSalida
            // 
            this.txtSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalida.Location = new System.Drawing.Point(303, 58);
            this.txtSalida.Multiline = true;
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.Size = new System.Drawing.Size(430, 223);
            this.txtSalida.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Gramatica";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(300, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Forma Normal de Chomsky";
            // 
            // butPaso1
            // 
            this.butPaso1.Location = new System.Drawing.Point(24, 324);
            this.butPaso1.Name = "butPaso1";
            this.butPaso1.Size = new System.Drawing.Size(129, 43);
            this.butPaso1.TabIndex = 15;
            this.butPaso1.Text = "Eliminar no terminables";
            this.butPaso1.UseVisualStyleBackColor = true;
            this.butPaso1.Click += new System.EventHandler(this.butPaso1_Click);
            // 
            // butPaso2
            // 
            this.butPaso2.Location = new System.Drawing.Point(24, 378);
            this.butPaso2.Name = "butPaso2";
            this.butPaso2.Size = new System.Drawing.Size(129, 47);
            this.butPaso2.TabIndex = 16;
            this.butPaso2.Text = "Eliminar no alcanzables";
            this.butPaso2.UseVisualStyleBackColor = true;
            this.butPaso2.Click += new System.EventHandler(this.butPaso2_Click);
            // 
            // butPaso3
            // 
            this.butPaso3.Location = new System.Drawing.Point(24, 431);
            this.butPaso3.Name = "butPaso3";
            this.butPaso3.Size = new System.Drawing.Size(129, 41);
            this.butPaso3.TabIndex = 17;
            this.butPaso3.Text = "simular producciones lambda";
            this.butPaso3.UseVisualStyleBackColor = true;
            this.butPaso3.Click += new System.EventHandler(this.butPaso3_Click);
            // 
            // butPaso5
            // 
            this.butPaso5.Location = new System.Drawing.Point(413, 384);
            this.butPaso5.Name = "butPaso5";
            this.butPaso5.Size = new System.Drawing.Size(129, 41);
            this.butPaso5.TabIndex = 18;
            this.butPaso5.Text = "variable por cada terminal";
            this.butPaso5.UseVisualStyleBackColor = true;
            this.butPaso5.Click += new System.EventHandler(this.butPaso5_Click);
            // 
            // butPaso6
            // 
            this.butPaso6.Location = new System.Drawing.Point(413, 436);
            this.butPaso6.Name = "butPaso6";
            this.butPaso6.Size = new System.Drawing.Size(129, 41);
            this.butPaso6.TabIndex = 19;
            this.butPaso6.Text = "produccion binarias";
            this.butPaso6.UseVisualStyleBackColor = true;
            this.butPaso6.Click += new System.EventHandler(this.butPaso6_Click);
            // 
            // txtNoAlcanzables
            // 
            this.txtNoAlcanzables.Location = new System.Drawing.Point(170, 405);
            this.txtNoAlcanzables.Name = "txtNoAlcanzables";
            this.txtNoAlcanzables.Size = new System.Drawing.Size(103, 20);
            this.txtNoAlcanzables.TabIndex = 20;
            // 
            // txtNoTerminables
            // 
            this.txtNoTerminables.Location = new System.Drawing.Point(170, 347);
            this.txtNoTerminables.Name = "txtNoTerminables";
            this.txtNoTerminables.Size = new System.Drawing.Size(103, 20);
            this.txtNoTerminables.TabIndex = 21;
            // 
            // txtAnulables
            // 
            this.txtAnulables.Location = new System.Drawing.Point(170, 452);
            this.txtAnulables.Name = "txtAnulables";
            this.txtAnulables.Size = new System.Drawing.Size(103, 20);
            this.txtAnulables.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Proceso de conversion";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(167, 331);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "No terminables";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(167, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "No alcanzables";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(167, 436);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Anulables";
            // 
            // butPaso4
            // 
            this.butPaso4.Location = new System.Drawing.Point(552, 324);
            this.butPaso4.Name = "butPaso4";
            this.butPaso4.Size = new System.Drawing.Size(129, 41);
            this.butPaso4.TabIndex = 27;
            this.butPaso4.Text = "Simular producciones Unitarias";
            this.butPaso4.UseVisualStyleBackColor = true;
            this.butPaso4.Click += new System.EventHandler(this.butPaso4_Click);
            // 
            // comboBoxConjunto
            // 
            this.comboBoxConjunto.FormattingEnabled = true;
            this.comboBoxConjunto.Location = new System.Drawing.Point(309, 337);
            this.comboBoxConjunto.Name = "comboBoxConjunto";
            this.comboBoxConjunto.Size = new System.Drawing.Size(98, 21);
            this.comboBoxConjunto.TabIndex = 28;
            this.comboBoxConjunto.SelectedIndexChanged += new System.EventHandler(this.comboBoxConjunto_SelectedIndexChanged);
            // 
            // txtConjUnitario
            // 
            this.txtConjUnitario.Location = new System.Drawing.Point(422, 338);
            this.txtConjUnitario.Name = "txtConjUnitario";
            this.txtConjUnitario.Size = new System.Drawing.Size(103, 20);
            this.txtConjUnitario.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Conjunto Unitario";
            // 
            // butLimpiar
            // 
            this.butLimpiar.Location = new System.Drawing.Point(186, 28);
            this.butLimpiar.Name = "butLimpiar";
            this.butLimpiar.Size = new System.Drawing.Size(75, 23);
            this.butLimpiar.TabIndex = 31;
            this.butLimpiar.Text = "LIMPIAR";
            this.butLimpiar.UseVisualStyleBackColor = true;
            this.butLimpiar.Click += new System.EventHandler(this.butLimpiar_Click);
            // 
            // butCYK
            // 
            this.butCYK.Location = new System.Drawing.Point(622, 29);
            this.butCYK.Name = "butCYK";
            this.butCYK.Size = new System.Drawing.Size(111, 23);
            this.butCYK.TabIndex = 32;
            this.butCYK.Text = "Algoritmo CYK";
            this.butCYK.UseVisualStyleBackColor = true;
            this.butCYK.Click += new System.EventHandler(this.butCYK_Click);
            // 
            // VentanaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(751, 498);
            this.Controls.Add(this.butCYK);
            this.Controls.Add(this.butLimpiar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtConjUnitario);
            this.Controls.Add(this.comboBoxConjunto);
            this.Controls.Add(this.butPaso4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAnulables);
            this.Controls.Add(this.txtNoTerminables);
            this.Controls.Add(this.txtNoAlcanzables);
            this.Controls.Add(this.butPaso6);
            this.Controls.Add(this.butPaso5);
            this.Controls.Add(this.butPaso3);
            this.Controls.Add(this.butPaso2);
            this.Controls.Add(this.butPaso1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSalida);
            this.Controls.Add(this.txtEntrada);
            this.MaximizeBox = false;
            this.Name = "VentanaPrincipal";
            this.Text = "Gramatica en FNC";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEntrada;
        private System.Windows.Forms.TextBox txtSalida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butPaso1;
        private System.Windows.Forms.Button butPaso2;
        private System.Windows.Forms.Button butPaso3;
        private System.Windows.Forms.Button butPaso5;
        private System.Windows.Forms.Button butPaso6;
        private System.Windows.Forms.TextBox txtNoAlcanzables;
        private System.Windows.Forms.TextBox txtNoTerminables;
        private System.Windows.Forms.TextBox txtAnulables;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button butPaso4;
        private System.Windows.Forms.ComboBox comboBoxConjunto;
        private System.Windows.Forms.TextBox txtConjUnitario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butLimpiar;
        private System.Windows.Forms.Button butCYK;
    }
}

