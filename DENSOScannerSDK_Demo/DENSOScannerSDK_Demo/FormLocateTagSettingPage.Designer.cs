namespace DENSOScannerSDK_Demo
{
    partial class FormLocateTagSettingPage
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
            image_setting_radar = new PictureBox();
            button_navigate_up = new Button();
            text_title_locate_tag = new Label();
            stage2_max_read_power_level_on_search = new TextBox();
            stage2_Label1 = new Label();
            stage2_Label2 = new Label();
            stage3_Label2 = new Label();
            stage3_max_read_power_level_on_search = new TextBox();
            stage3_Label1 = new Label();
            stage4_Label2 = new Label();
            stage4_max_read_power_level_on_search = new TextBox();
            stage4_Label1 = new Label();
            stage5_Label2 = new Label();
            stage5_max_read_power_level_on_search = new TextBox();
            stage5_Label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)image_setting_radar).BeginInit();
            SuspendLayout();
            // 
            // image_setting_radar
            // 
            image_setting_radar.BackColor = Color.Transparent;
            image_setting_radar.Location = new Point(97, 76);
            image_setting_radar.Name = "image_setting_radar";
            image_setting_radar.Size = new Size(250, 466);
            image_setting_radar.SizeMode = PictureBoxSizeMode.Zoom;
            image_setting_radar.TabIndex = 60;
            image_setting_radar.TabStop = false;
            // 
            // button_navigate_up
            // 
            button_navigate_up.BackgroundImageLayout = ImageLayout.None;
            button_navigate_up.FlatAppearance.BorderSize = 0;
            button_navigate_up.FlatStyle = FlatStyle.Flat;
            button_navigate_up.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button_navigate_up.ForeColor = Color.Blue;
            button_navigate_up.Location = new Point(-1, 2);
            button_navigate_up.Name = "button_navigate_up";
            button_navigate_up.Size = new Size(60, 30);
            button_navigate_up.TabIndex = 62;
            button_navigate_up.Text = "RETURN";
            button_navigate_up.TextAlign = ContentAlignment.MiddleLeft;
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Click;
            // 
            // text_title_locate_tag
            // 
            text_title_locate_tag.AutoSize = true;
            text_title_locate_tag.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_title_locate_tag.Location = new Point(320, 28);
            text_title_locate_tag.Name = "text_title_locate_tag";
            text_title_locate_tag.Size = new Size(167, 45);
            text_title_locate_tag.TabIndex = 61;
            text_title_locate_tag.Text = "LocateTag";
            // 
            // stage2_max_read_power_level_on_search
            // 
            stage2_max_read_power_level_on_search.Font = new Font("Yu Gothic UI", 14.25F);
            stage2_max_read_power_level_on_search.Location = new Point(420, 378);
            stage2_max_read_power_level_on_search.Name = "stage2_max_read_power_level_on_search";
            stage2_max_read_power_level_on_search.Size = new Size(191, 33);
            stage2_max_read_power_level_on_search.TabIndex = 67;
            // 
            // stage2_Label1
            // 
            stage2_Label1.AutoSize = true;
            stage2_Label1.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            stage2_Label1.ForeColor = Color.Black;
            stage2_Label1.Location = new Point(390, 379);
            stage2_Label1.Name = "stage2_Label1";
            stage2_Label1.Size = new Size(24, 32);
            stage2_Label1.TabIndex = 66;
            stage2_Label1.Text = "-";
            // 
            // stage2_Label2
            // 
            stage2_Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            stage2_Label2.AutoSize = true;
            stage2_Label2.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            stage2_Label2.ForeColor = Color.Black;
            stage2_Label2.Location = new Point(617, 385);
            stage2_Label2.Name = "stage2_Label2";
            stage2_Label2.Size = new Size(75, 25);
            stage2_Label2.TabIndex = 68;
            stage2_Label2.Text = "0.1dBm";
            // 
            // stage3_Label2
            // 
            stage3_Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            stage3_Label2.AutoSize = true;
            stage3_Label2.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            stage3_Label2.ForeColor = Color.Black;
            stage3_Label2.Location = new Point(617, 334);
            stage3_Label2.Name = "stage3_Label2";
            stage3_Label2.Size = new Size(75, 25);
            stage3_Label2.TabIndex = 71;
            stage3_Label2.Text = "0.1dBm";
            // 
            // stage3_max_read_power_level_on_search
            // 
            stage3_max_read_power_level_on_search.Font = new Font("Yu Gothic UI", 14.25F);
            stage3_max_read_power_level_on_search.Location = new Point(420, 327);
            stage3_max_read_power_level_on_search.Name = "stage3_max_read_power_level_on_search";
            stage3_max_read_power_level_on_search.Size = new Size(191, 33);
            stage3_max_read_power_level_on_search.TabIndex = 70;
            // 
            // stage3_Label1
            // 
            stage3_Label1.AutoSize = true;
            stage3_Label1.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            stage3_Label1.ForeColor = Color.Black;
            stage3_Label1.Location = new Point(390, 328);
            stage3_Label1.Name = "stage3_Label1";
            stage3_Label1.Size = new Size(24, 32);
            stage3_Label1.TabIndex = 69;
            stage3_Label1.Text = "-";
            // 
            // stage4_Label2
            // 
            stage4_Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            stage4_Label2.AutoSize = true;
            stage4_Label2.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            stage4_Label2.ForeColor = Color.Black;
            stage4_Label2.Location = new Point(617, 284);
            stage4_Label2.Name = "stage4_Label2";
            stage4_Label2.Size = new Size(75, 25);
            stage4_Label2.TabIndex = 74;
            stage4_Label2.Text = "0.1dBm";
            // 
            // stage4_max_read_power_level_on_search
            // 
            stage4_max_read_power_level_on_search.Font = new Font("Yu Gothic UI", 14.25F);
            stage4_max_read_power_level_on_search.Location = new Point(420, 277);
            stage4_max_read_power_level_on_search.Name = "stage4_max_read_power_level_on_search";
            stage4_max_read_power_level_on_search.Size = new Size(191, 33);
            stage4_max_read_power_level_on_search.TabIndex = 73;
            // 
            // stage4_Label1
            // 
            stage4_Label1.AutoSize = true;
            stage4_Label1.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            stage4_Label1.ForeColor = Color.Black;
            stage4_Label1.Location = new Point(390, 278);
            stage4_Label1.Name = "stage4_Label1";
            stage4_Label1.Size = new Size(24, 32);
            stage4_Label1.TabIndex = 72;
            stage4_Label1.Text = "-";
            // 
            // stage5_Label2
            // 
            stage5_Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            stage5_Label2.AutoSize = true;
            stage5_Label2.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            stage5_Label2.ForeColor = Color.Black;
            stage5_Label2.Location = new Point(617, 230);
            stage5_Label2.Name = "stage5_Label2";
            stage5_Label2.Size = new Size(75, 25);
            stage5_Label2.TabIndex = 77;
            stage5_Label2.Text = "0.1dBm";
            // 
            // stage5_max_read_power_level_on_search
            // 
            stage5_max_read_power_level_on_search.Font = new Font("Yu Gothic UI", 14.25F);
            stage5_max_read_power_level_on_search.Location = new Point(420, 223);
            stage5_max_read_power_level_on_search.Name = "stage5_max_read_power_level_on_search";
            stage5_max_read_power_level_on_search.Size = new Size(191, 33);
            stage5_max_read_power_level_on_search.TabIndex = 76;
            // 
            // stage5_Label1
            // 
            stage5_Label1.AutoSize = true;
            stage5_Label1.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            stage5_Label1.ForeColor = Color.Black;
            stage5_Label1.Location = new Point(390, 224);
            stage5_Label1.Name = "stage5_Label1";
            stage5_Label1.Size = new Size(24, 32);
            stage5_Label1.TabIndex = 75;
            stage5_Label1.Text = "-";
            // 
            // FormLocateTagSettingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 551);
            Controls.Add(stage5_Label2);
            Controls.Add(stage5_max_read_power_level_on_search);
            Controls.Add(stage5_Label1);
            Controls.Add(stage4_Label2);
            Controls.Add(stage4_max_read_power_level_on_search);
            Controls.Add(stage4_Label1);
            Controls.Add(stage3_Label2);
            Controls.Add(stage3_max_read_power_level_on_search);
            Controls.Add(stage3_Label1);
            Controls.Add(stage2_Label2);
            Controls.Add(stage2_max_read_power_level_on_search);
            Controls.Add(stage2_Label1);
            Controls.Add(button_navigate_up);
            Controls.Add(text_title_locate_tag);
            Controls.Add(image_setting_radar);
            Name = "FormLocateTagSettingPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormLocateTagSettingPage_FormClosed;
            Load += FormLocateTagSettingPage_Load;
            ((System.ComponentModel.ISupportInitialize)image_setting_radar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox image_setting_radar;
        private Button button_navigate_up;
        private Label text_title_locate_tag;
        private TextBox stage2_max_read_power_level_on_search;
        private Label stage2_Label1;
        private Label stage2_Label2;
        private Label stage3_Label2;
        private TextBox stage3_max_read_power_level_on_search;
        private Label stage3_Label1;
        private Label stage4_Label2;
        private TextBox stage4_max_read_power_level_on_search;
        private Label stage4_Label1;
        private Label stage5_Label2;
        private TextBox stage5_max_read_power_level_on_search;
        private Label stage5_Label1;
    }
}