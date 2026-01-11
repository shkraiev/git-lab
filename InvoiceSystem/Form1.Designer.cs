namespace InvoiceSystem
{
    partial class Form1
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
            this.btnOpenRegister = new System.Windows.Forms.Button();
            this.btnOpenLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenRegister
            // 
            this.btnOpenRegister.Location = new System.Drawing.Point(197, 102);
            this.btnOpenRegister.Name = "btnOpenRegister";
            this.btnOpenRegister.Size = new System.Drawing.Size(75, 23);
            this.btnOpenRegister.TabIndex = 0;
            this.btnOpenRegister.Text = "Rejestracja";
            this.btnOpenRegister.UseVisualStyleBackColor = true;
            this.btnOpenRegister.Click += new System.EventHandler(this.btnOpenRegister_Click);
            // 
            // btnOpenLogin
            // 
            this.btnOpenLogin.Location = new System.Drawing.Point(197, 142);
            this.btnOpenLogin.Name = "btnOpenLogin";
            this.btnOpenLogin.Size = new System.Drawing.Size(75, 23);
            this.btnOpenLogin.TabIndex = 1;
            this.btnOpenLogin.Text = "Logowanie";
            this.btnOpenLogin.UseVisualStyleBackColor = true;
            this.btnOpenLogin.Click += new System.EventHandler(this.btnOpenLogin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.btnOpenLogin);
            this.Controls.Add(this.btnOpenRegister);
            this.MaximumSize = new System.Drawing.Size(480, 360);
            this.MinimumSize = new System.Drawing.Size(480, 360);
            this.Name = "Form1";
            this.Text = "Strona Logowania";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenRegister;
        private System.Windows.Forms.Button btnOpenLogin;
    }
}

