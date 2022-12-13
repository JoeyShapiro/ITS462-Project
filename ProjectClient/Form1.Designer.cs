namespace ProjectClient
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
            this.deviceDetailsBox = new System.Windows.Forms.TextBox();
            this.idBox = new System.Windows.Forms.TextBox();
            this.scrapeDataBtn = new System.Windows.Forms.Button();
            this.getDeviceByIdBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.getAllDevicesBtn = new System.Windows.Forms.Button();
            this.filterBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filterResultsBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.getDeviceByFilterBtn = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deviceDetailsBox
            // 
            this.deviceDetailsBox.Location = new System.Drawing.Point(494, 40);
            this.deviceDetailsBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deviceDetailsBox.Multiline = true;
            this.deviceDetailsBox.Name = "deviceDetailsBox";
            this.deviceDetailsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.deviceDetailsBox.Size = new System.Drawing.Size(337, 247);
            this.deviceDetailsBox.TabIndex = 1;
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(49, 151);
            this.idBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(106, 20);
            this.idBox.TabIndex = 2;
            // 
            // scrapeDataBtn
            // 
            this.scrapeDataBtn.ForeColor = System.Drawing.Color.Red;
            this.scrapeDataBtn.Location = new System.Drawing.Point(8, 321);
            this.scrapeDataBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scrapeDataBtn.Name = "scrapeDataBtn";
            this.scrapeDataBtn.Size = new System.Drawing.Size(105, 31);
            this.scrapeDataBtn.TabIndex = 3;
            this.scrapeDataBtn.Text = "Scrape Data";
            this.scrapeDataBtn.UseVisualStyleBackColor = true;
            this.scrapeDataBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // getDeviceByIdBtn
            // 
            this.getDeviceByIdBtn.Location = new System.Drawing.Point(49, 172);
            this.getDeviceByIdBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.getDeviceByIdBtn.Name = "getDeviceByIdBtn";
            this.getDeviceByIdBtn.Size = new System.Drawing.Size(105, 31);
            this.getDeviceByIdBtn.TabIndex = 4;
            this.getDeviceByIdBtn.Text = "Get Device by ID";
            this.getDeviceByIdBtn.UseVisualStyleBackColor = true;
            this.getDeviceByIdBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ID";
            // 
            // getAllDevicesBtn
            // 
            this.getAllDevicesBtn.Location = new System.Drawing.Point(8, 238);
            this.getAllDevicesBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.getAllDevicesBtn.Name = "getAllDevicesBtn";
            this.getAllDevicesBtn.Size = new System.Drawing.Size(105, 31);
            this.getAllDevicesBtn.TabIndex = 6;
            this.getAllDevicesBtn.Text = "Get All Devices";
            this.getAllDevicesBtn.UseVisualStyleBackColor = true;
            this.getAllDevicesBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // filterBox
            // 
            this.filterBox.FormattingEnabled = true;
            this.filterBox.Items.AddRange(new object[] {
            "vendor",
            "computer_type",
            "link"});
            this.filterBox.Location = new System.Drawing.Point(49, 25);
            this.filterBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(106, 21);
            this.filterBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Filter";
            // 
            // filterResultsBox
            // 
            this.filterResultsBox.FormattingEnabled = true;
            this.filterResultsBox.Location = new System.Drawing.Point(189, 44);
            this.filterResultsBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.filterResultsBox.Name = "filterResultsBox";
            this.filterResultsBox.Size = new System.Drawing.Size(286, 238);
            this.filterResultsBox.TabIndex = 9;
            this.filterResultsBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(627, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Device Details";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(49, 55);
            this.searchBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(106, 20);
            this.searchBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Search";
            // 
            // getDeviceByFilterBtn
            // 
            this.getDeviceByFilterBtn.Location = new System.Drawing.Point(49, 75);
            this.getDeviceByFilterBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.getDeviceByFilterBtn.Name = "getDeviceByFilterBtn";
            this.getDeviceByFilterBtn.Size = new System.Drawing.Size(105, 39);
            this.getDeviceByFilterBtn.TabIndex = 13;
            this.getDeviceByFilterBtn.Text = "Get Device by Filter";
            this.getDeviceByFilterBtn.UseVisualStyleBackColor = true;
            this.getDeviceByFilterBtn.Click += new System.EventHandler(this.getDeviceByFilterBtn_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Location = new System.Drawing.Point(8, 274);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(102, 35);
            this.btnPDF.TabIndex = 14;
            this.btnPDF.Text = "Print to PDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 359);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.getDeviceByFilterBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.filterResultsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.getAllDevicesBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getDeviceByIdBtn);
            this.Controls.Add(this.scrapeDataBtn);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.deviceDetailsBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox deviceDetailsBox;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Button scrapeDataBtn;
        private System.Windows.Forms.Button getDeviceByIdBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button getAllDevicesBtn;
        private System.Windows.Forms.ComboBox filterBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox filterResultsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button getDeviceByFilterBtn;
        private System.Windows.Forms.Button btnPDF;
    }
}

