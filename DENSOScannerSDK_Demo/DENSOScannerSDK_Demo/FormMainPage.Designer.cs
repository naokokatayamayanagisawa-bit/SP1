namespace DENSOScannerSDK_Demo
{
    partial class FormMainPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            connection_status = new Label();
            AppVersion = new Label();
            flowLayoutPanelMainPage = new FlowLayoutPanel();
            button_rapid_read = new Button();
            button_inventory = new Button();
            button_barcode = new Button();
            button_settings = new Button();
            button_locate_tag = new Button();
            button_pre_filters = new Button();
            flowLayoutPanelMainPage.SuspendLayout();
            SuspendLayout();
            // 
            // connection_status
            // 
            connection_status.AutoSize = true;
            connection_status.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            connection_status.Location = new Point(11, 9);
            connection_status.Name = "connection_status";
            connection_status.Size = new Size(198, 21);
            connection_status.TabIndex = 0;
            connection_status.Text = "Waiting for connection ....";
            // 
            // AppVersion
            // 
            AppVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AppVersion.AutoSize = true;
            AppVersion.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            AppVersion.Location = new Point(643, 451);
            AppVersion.Name = "AppVersion";
            AppVersion.Size = new Size(69, 21);
            AppVersion.TabIndex = 1;
            AppVersion.Text = "Ver0.0.0";
            // 
            // flowLayoutPanelMainPage
            // 
            flowLayoutPanelMainPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelMainPage.Controls.Add(button_rapid_read);
            flowLayoutPanelMainPage.Controls.Add(button_inventory);
            flowLayoutPanelMainPage.Controls.Add(button_barcode);
            flowLayoutPanelMainPage.Controls.Add(button_settings);
            flowLayoutPanelMainPage.Controls.Add(button_locate_tag);
            flowLayoutPanelMainPage.Controls.Add(button_pre_filters);
            flowLayoutPanelMainPage.Location = new Point(44, 50);
            flowLayoutPanelMainPage.Name = "flowLayoutPanelMainPage";
            flowLayoutPanelMainPage.Size = new Size(630, 398);
            flowLayoutPanelMainPage.TabIndex = 8;
            // 
            // button_rapid_read
            // 
            button_rapid_read.BackColor = Color.FromArgb(159, 191, 190);
            button_rapid_read.FlatAppearance.BorderSize = 0;
            button_rapid_read.FlatStyle = FlatStyle.Flat;
            button_rapid_read.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_rapid_read.Location = new Point(3, 3);
            button_rapid_read.Name = "button_rapid_read";
            button_rapid_read.Size = new Size(307, 125);
            button_rapid_read.TabIndex = 8;
            button_rapid_read.Text = "RapidRead";
            button_rapid_read.UseVisualStyleBackColor = false;
            button_rapid_read.Click += button_rapid_read_Clicked;
            // 
            // button_inventory
            // 
            button_inventory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_inventory.BackColor = Color.FromArgb(159, 191, 190);
            button_inventory.FlatAppearance.BorderSize = 0;
            button_inventory.FlatStyle = FlatStyle.Flat;
            button_inventory.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_inventory.Location = new Point(316, 3);
            button_inventory.Name = "button_inventory";
            button_inventory.Size = new Size(307, 125);
            button_inventory.TabIndex = 13;
            button_inventory.Text = "Inventory";
            button_inventory.UseVisualStyleBackColor = false;
            button_inventory.Click += button_inventory_Clicked;
            // 
            // button_barcode
            // 
            button_barcode.BackColor = Color.FromArgb(159, 191, 190);
            button_barcode.FlatAppearance.BorderSize = 0;
            button_barcode.FlatStyle = FlatStyle.Flat;
            button_barcode.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_barcode.Location = new Point(3, 134);
            button_barcode.Name = "button_barcode";
            button_barcode.Size = new Size(307, 125);
            button_barcode.TabIndex = 9;
            button_barcode.Text = "Barcode";
            button_barcode.UseVisualStyleBackColor = false;
            button_barcode.Click += button_barcode_Clicked;
            // 
            // button_settings
            // 
            button_settings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_settings.BackColor = Color.FromArgb(159, 191, 190);
            button_settings.FlatAppearance.BorderSize = 0;
            button_settings.FlatStyle = FlatStyle.Flat;
            button_settings.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_settings.Location = new Point(316, 134);
            button_settings.Name = "button_settings";
            button_settings.Size = new Size(307, 125);
            button_settings.TabIndex = 12;
            button_settings.Text = "Settings";
            button_settings.UseVisualStyleBackColor = false;
            button_settings.Click += button_settings_Clicked;
            // 
            // button_locate_tag
            // 
            button_locate_tag.BackColor = Color.FromArgb(159, 191, 190);
            button_locate_tag.FlatAppearance.BorderSize = 0;
            button_locate_tag.FlatStyle = FlatStyle.Flat;
            button_locate_tag.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_locate_tag.Location = new Point(3, 265);
            button_locate_tag.Name = "button_locate_tag";
            button_locate_tag.Size = new Size(307, 125);
            button_locate_tag.TabIndex = 10;
            button_locate_tag.Text = "LocateTag";
            button_locate_tag.UseVisualStyleBackColor = false;
            button_locate_tag.Click += button_locate_tag_Clicked;
            // 
            // button_pre_filters
            // 
            button_pre_filters.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_pre_filters.BackColor = Color.FromArgb(159, 191, 190);
            button_pre_filters.FlatAppearance.BorderSize = 0;
            button_pre_filters.FlatStyle = FlatStyle.Flat;
            button_pre_filters.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_pre_filters.Location = new Point(316, 265);
            button_pre_filters.Name = "button_pre_filters";
            button_pre_filters.Size = new Size(307, 125);
            button_pre_filters.TabIndex = 11;
            button_pre_filters.Text = "PreFilters";
            button_pre_filters.UseVisualStyleBackColor = false;
            button_pre_filters.Click += button_pre_filters_Clicked;
            // 
            // FormMainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 481);
            Controls.Add(flowLayoutPanelMainPage);
            Controls.Add(AppVersion);
            Controls.Add(connection_status);
            Name = "FormMainPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosing += FormMainPage_FormClosing;
            FormClosed += FormMainPage_FormClosed;
            Load += FormMainPage_Load;
            Resize += FormMainPage_Resize;
            flowLayoutPanelMainPage.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label connection_status;
        private Label AppVersion;
        private FlowLayoutPanel flowLayoutPanelMainPage;
        private Button button_inventory;
        private Button button_settings;
        private Button button_pre_filters;
        private Button button_locate_tag;
        private Button button_barcode;
        private Button button_rapid_read;
    }
}
