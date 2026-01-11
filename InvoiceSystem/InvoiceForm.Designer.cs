namespace InvoiceSystem
{
    partial class InvoiceForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbContractor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.btnGetRate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtIssueDate = new System.Windows.Forms.DateTimePicker();
            this.gridItems = new System.Windows.Forms.DataGridView();
            this.btnSaveInvoice = new System.Windows.Forms.Button();
            this.lblTotalForeign = new System.Windows.Forms.Label();
            this.lblTotalPLN = new System.Windows.Forms.Label();
            this.btnExportPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kontrahent";
            // 
            // cmbContractor
            // 
            this.cmbContractor.FormattingEnabled = true;
            this.cmbContractor.Location = new System.Drawing.Point(12, 29);
            this.cmbContractor.Name = "cmbContractor";
            this.cmbContractor.Size = new System.Drawing.Size(200, 21);
            this.cmbContractor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Waluta";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(12, 69);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(200, 21);
            this.cmbCurrency.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kurs";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(12, 109);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(200, 20);
            this.txtRate.TabIndex = 5;
            this.txtRate.TextChanged += new System.EventHandler(this.txtRate_TextChanged);
            // 
            // btnGetRate
            // 
            this.btnGetRate.Location = new System.Drawing.Point(12, 135);
            this.btnGetRate.Name = "btnGetRate";
            this.btnGetRate.Size = new System.Drawing.Size(200, 23);
            this.btnGetRate.TabIndex = 6;
            this.btnGetRate.Text = "Pobierz z NBP";
            this.btnGetRate.UseVisualStyleBackColor = true;
            this.btnGetRate.Click += new System.EventHandler(this.btnGetRate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Numer";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(12, 177);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(200, 20);
            this.txtNumber.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Data";
            // 
            // dtIssueDate
            // 
            this.dtIssueDate.Location = new System.Drawing.Point(12, 216);
            this.dtIssueDate.Name = "dtIssueDate";
            this.dtIssueDate.Size = new System.Drawing.Size(200, 20);
            this.dtIssueDate.TabIndex = 10;
            // 
            // gridItems
            // 
            this.gridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItems.Location = new System.Drawing.Point(254, 13);
            this.gridItems.Name = "gridItems";
            this.gridItems.Size = new System.Drawing.Size(520, 150);
            this.gridItems.TabIndex = 11;
            // 
            // btnSaveInvoice
            // 
            this.btnSaveInvoice.Location = new System.Drawing.Point(254, 177);
            this.btnSaveInvoice.Name = "btnSaveInvoice";
            this.btnSaveInvoice.Size = new System.Drawing.Size(142, 23);
            this.btnSaveInvoice.TabIndex = 12;
            this.btnSaveInvoice.Text = "Zapisz fakturę";
            this.btnSaveInvoice.UseVisualStyleBackColor = true;
            this.btnSaveInvoice.Click += new System.EventHandler(this.btnSaveInvoice_Click);
            // 
            // lblTotalForeign
            // 
            this.lblTotalForeign.AutoSize = true;
            this.lblTotalForeign.Location = new System.Drawing.Point(251, 203);
            this.lblTotalForeign.Name = "lblTotalForeign";
            this.lblTotalForeign.Size = new System.Drawing.Size(59, 13);
            this.lblTotalForeign.TabIndex = 13;
            this.lblTotalForeign.Text = "Kontrahent";
            // 
            // lblTotalPLN
            // 
            this.lblTotalPLN.AutoSize = true;
            this.lblTotalPLN.Location = new System.Drawing.Point(251, 223);
            this.lblTotalPLN.Name = "lblTotalPLN";
            this.lblTotalPLN.Size = new System.Drawing.Size(59, 13);
            this.lblTotalPLN.TabIndex = 14;
            this.lblTotalPLN.Text = "Kontrahent";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Location = new System.Drawing.Point(637, 177);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(137, 23);
            this.btnExportPdf.TabIndex = 15;
            this.btnExportPdf.Text = "Export PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 265);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.lblTotalPLN);
            this.Controls.Add(this.lblTotalForeign);
            this.Controls.Add(this.btnSaveInvoice);
            this.Controls.Add(this.gridItems);
            this.Controls.Add(this.dtIssueDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGetRate);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCurrency);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbContractor);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(816, 304);
            this.MinimumSize = new System.Drawing.Size(816, 304);
            this.Name = "InvoiceForm";
            this.Text = "InvoiceForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbContractor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Button btnGetRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtIssueDate;
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.Button btnSaveInvoice;
        private System.Windows.Forms.Label lblTotalForeign;
        private System.Windows.Forms.Label lblTotalPLN;
        private System.Windows.Forms.Button btnExportPdf;
    }
}