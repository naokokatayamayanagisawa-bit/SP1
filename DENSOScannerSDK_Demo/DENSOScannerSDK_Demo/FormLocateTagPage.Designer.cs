namespace DENSOScannerSDK_Demo
{
    partial class FormLocateTagPage
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
            components = new System.ComponentModel.Container();
            button_navigate_up = new Button();
            text_title_locate_tag = new Label();
            imageList1 = new ImageList(components);
            imageList2 = new ImageList(components);
            image_search_radar = new PictureBox();
            image_search_circle = new PictureBox();
            spinner_power_level_value_on_read_search_tag = new ComboBox();
            picker_match_direction = new ComboBox();
            button_setting = new Button();
            button_search_tag_toggle = new Button();
            text_search_tag_uii_value = new TextBox();
            text_search_tag_uii_head = new Label();
            button_read_search_tag = new Button();
            text_read_power_value_on_search = new Label();
            text_read_power_unit_on_search = new Label();
            ((System.ComponentModel.ISupportInitialize)image_search_radar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)image_search_circle).BeginInit();
            SuspendLayout();
            // 
            // button_navigate_up
            // 
            button_navigate_up.BackgroundImageLayout = ImageLayout.None;
            button_navigate_up.FlatAppearance.BorderSize = 0;
            button_navigate_up.FlatStyle = FlatStyle.Flat;
            button_navigate_up.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button_navigate_up.ForeColor = Color.Blue;
            button_navigate_up.Location = new Point(1, 2);
            button_navigate_up.Name = "button_navigate_up";
            button_navigate_up.Size = new Size(60, 30);
            button_navigate_up.TabIndex = 52;
            button_navigate_up.Text = "<TOP";
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Clicked;
            // 
            // text_title_locate_tag
            // 
            text_title_locate_tag.AutoSize = true;
            text_title_locate_tag.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_title_locate_tag.Location = new Point(11, 35);
            text_title_locate_tag.Name = "text_title_locate_tag";
            text_title_locate_tag.Size = new Size(167, 45);
            text_title_locate_tag.TabIndex = 51;
            text_title_locate_tag.Text = "LocateTag";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth32Bit;
            imageList2.ImageSize = new Size(16, 16);
            imageList2.TransparentColor = Color.Transparent;
            // 
            // image_search_radar
            // 
            image_search_radar.Location = new Point(210, 224);
            image_search_radar.Name = "image_search_radar";
            image_search_radar.Size = new Size(250, 250);
            image_search_radar.SizeMode = PictureBoxSizeMode.Zoom;
            image_search_radar.TabIndex = 58;
            image_search_radar.TabStop = false;
            // 
            // image_search_circle
            // 
            image_search_circle.BackColor = Color.Transparent;
            image_search_circle.Location = new Point(210, 224);
            image_search_circle.Name = "image_search_circle";
            image_search_circle.Size = new Size(250, 250);
            image_search_circle.SizeMode = PictureBoxSizeMode.Zoom;
            image_search_circle.TabIndex = 59;
            image_search_circle.TabStop = false;
            // 
            // spinner_power_level_value_on_read_search_tag
            // 
            spinner_power_level_value_on_read_search_tag.Font = new Font("Yu Gothic UI", 12F);
            spinner_power_level_value_on_read_search_tag.FormattingEnabled = true;
            spinner_power_level_value_on_read_search_tag.Location = new Point(73, 105);
            spinner_power_level_value_on_read_search_tag.Name = "spinner_power_level_value_on_read_search_tag";
            spinner_power_level_value_on_read_search_tag.Size = new Size(184, 29);
            spinner_power_level_value_on_read_search_tag.TabIndex = 60;
            spinner_power_level_value_on_read_search_tag.SelectedIndexChanged += spinner_power_level_value_on_read_search_tag_SelectedIndexChanged;
            // 
            // picker_match_direction
            // 
            picker_match_direction.Font = new Font("Yu Gothic UI", 12F);
            picker_match_direction.FormattingEnabled = true;
            picker_match_direction.Location = new Point(73, 179);
            picker_match_direction.Name = "picker_match_direction";
            picker_match_direction.Size = new Size(184, 29);
            picker_match_direction.TabIndex = 61;
            picker_match_direction.SelectedIndexChanged += Picker_match_direction_SelectedIndexChanged;
            // 
            // button_setting
            // 
            button_setting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_setting.BackColor = Color.FromArgb(159, 191, 190);
            button_setting.FlatAppearance.BorderSize = 0;
            button_setting.FlatStyle = FlatStyle.Flat;
            button_setting.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_setting.Location = new Point(548, 48);
            button_setting.Name = "button_setting";
            button_setting.Size = new Size(150, 32);
            button_setting.TabIndex = 63;
            button_setting.Text = "Range";
            button_setting.UseVisualStyleBackColor = false;
            button_setting.Click += button_setting_Clicked;
            // 
            // button_search_tag_toggle
            // 
            button_search_tag_toggle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_search_tag_toggle.BackColor = Color.FromArgb(159, 191, 190);
            button_search_tag_toggle.FlatAppearance.BorderSize = 0;
            button_search_tag_toggle.FlatStyle = FlatStyle.Flat;
            button_search_tag_toggle.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_search_tag_toggle.Location = new Point(49, 488);
            button_search_tag_toggle.Name = "button_search_tag_toggle";
            button_search_tag_toggle.Size = new Size(649, 51);
            button_search_tag_toggle.TabIndex = 62;
            button_search_tag_toggle.Text = "Search";
            button_search_tag_toggle.UseVisualStyleBackColor = false;
            button_search_tag_toggle.Click += button_search_tag_toggle_Clicked;
            // 
            // text_search_tag_uii_value
            // 
            text_search_tag_uii_value.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            text_search_tag_uii_value.Font = new Font("Yu Gothic UI", 14.25F);
            text_search_tag_uii_value.Location = new Point(73, 140);
            text_search_tag_uii_value.Name = "text_search_tag_uii_value";
            text_search_tag_uii_value.Size = new Size(625, 33);
            text_search_tag_uii_value.TabIndex = 65;
            text_search_tag_uii_value.KeyPress += text_search_tag_uii_value_KeyPress;
            text_search_tag_uii_value.Validated += text_search_tag_uii_value_Completed;
            // 
            // text_search_tag_uii_head
            // 
            text_search_tag_uii_head.AutoSize = true;
            text_search_tag_uii_head.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_search_tag_uii_head.ForeColor = Color.FromArgb(98, 120, 255);
            text_search_tag_uii_head.Location = new Point(22, 140);
            text_search_tag_uii_head.Name = "text_search_tag_uii_head";
            text_search_tag_uii_head.Size = new Size(45, 32);
            text_search_tag_uii_head.TabIndex = 64;
            text_search_tag_uii_head.Text = "UII";
            // 
            // button_read_search_tag
            // 
            button_read_search_tag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_read_search_tag.BackColor = Color.FromArgb(159, 191, 190);
            button_read_search_tag.FlatAppearance.BorderSize = 0;
            button_read_search_tag.FlatStyle = FlatStyle.Flat;
            button_read_search_tag.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_read_search_tag.Location = new Point(548, 86);
            button_read_search_tag.Name = "button_read_search_tag";
            button_read_search_tag.Size = new Size(150, 32);
            button_read_search_tag.TabIndex = 66;
            button_read_search_tag.Text = "Read";
            button_read_search_tag.UseVisualStyleBackColor = false;
            button_read_search_tag.Click += button_read_search_tag_Clicked;
            // 
            // text_read_power_value_on_search
            // 
            text_read_power_value_on_search.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            text_read_power_value_on_search.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            text_read_power_value_on_search.ForeColor = Color.FromArgb(98, 120, 255);
            text_read_power_value_on_search.Location = new Point(476, 443);
            text_read_power_value_on_search.Name = "text_read_power_value_on_search";
            text_read_power_value_on_search.Size = new Size(176, 37);
            text_read_power_value_on_search.TabIndex = 68;
            text_read_power_value_on_search.Text = "0.0";
            text_read_power_value_on_search.TextAlign = ContentAlignment.MiddleRight;
            // 
            // text_read_power_unit_on_search
            // 
            text_read_power_unit_on_search.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            text_read_power_unit_on_search.AutoSize = true;
            text_read_power_unit_on_search.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            text_read_power_unit_on_search.ForeColor = Color.FromArgb(98, 120, 255);
            text_read_power_unit_on_search.Location = new Point(658, 449);
            text_read_power_unit_on_search.Name = "text_read_power_unit_on_search";
            text_read_power_unit_on_search.Size = new Size(40, 25);
            text_read_power_unit_on_search.TabIndex = 67;
            text_read_power_unit_on_search.Text = "pcs";
            // 
            // FormLocateTagPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 551);
            Controls.Add(text_read_power_value_on_search);
            Controls.Add(text_read_power_unit_on_search);
            Controls.Add(button_read_search_tag);
            Controls.Add(text_search_tag_uii_value);
            Controls.Add(text_search_tag_uii_head);
            Controls.Add(button_setting);
            Controls.Add(button_search_tag_toggle);
            Controls.Add(picker_match_direction);
            Controls.Add(spinner_power_level_value_on_read_search_tag);
            Controls.Add(image_search_circle);
            Controls.Add(image_search_radar);
            Controls.Add(button_navigate_up);
            Controls.Add(text_title_locate_tag);
            Name = "FormLocateTagPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormLocateTagPage_FormClosed;
            Load += FormLocateTagPage_Load;
            ((System.ComponentModel.ISupportInitialize)image_search_radar).EndInit();
            ((System.ComponentModel.ISupportInitialize)image_search_circle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_navigate_up;
        private Label text_title_locate_tag;
        private ImageList imageList1;
        private ImageList imageList2;
        private PictureBox image_search_radar;
        private PictureBox image_search_circle;
        private ComboBox spinner_power_level_value_on_read_search_tag;
        private ComboBox picker_match_direction;
        private Button button_setting;
        private Button button_search_tag_toggle;
        private TextBox text_search_tag_uii_value;
        private Label text_search_tag_uii_head;
        private Button button_read_search_tag;
        private Label text_read_power_value_on_search;
        private Label text_read_power_unit_on_search;
    }
}