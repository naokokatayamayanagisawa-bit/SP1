namespace DENSOScannerSDK_Demo
{
    partial class FormSettingsPage
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
            title_settings = new Label();
            text_scanner_version_head = new Label();
            text_scanner_version_value = new Label();
            text_read_power_level_head = new Label();
            text_read_power_level_unit = new Label();
            text_read_power_level_value = new ComboBox();
            text_session_value = new ComboBox();
            text_session_head = new Label();
            text_report_unique_tags_head = new Label();
            text_channel_head = new Label();
            text_channel5 = new Label();
            text_channel11 = new Label();
            text_channel23 = new Label();
            text_channel17 = new Label();
            text_channel25 = new Label();
            text_channel24 = new Label();
            checkbox_report_unique_tags = new CheckBox();
            checkbox_channel5 = new CheckBox();
            checkbox_channel17 = new CheckBox();
            checkbox_channel24 = new CheckBox();
            checkbox_channel25 = new CheckBox();
            checkbox_channel23 = new CheckBox();
            checkbox_channel11 = new CheckBox();
            text_q_factor_value = new ComboBox();
            text_q_factor_head = new Label();
            switch_auto_link_profile = new CheckBox();
            text_auto_link_profile_head = new Label();
            text_link_profile_value = new ComboBox();
            text_link_profile_head = new Label();
            text_polarization_head = new Label();
            text_polarization_value = new ComboBox();
            checkbox_power_save = new CheckBox();
            text_power_save_head = new Label();
            checkbox_buzzer = new CheckBox();
            text_buzzer_head = new Label();
            text_buzzer_volume_head = new Label();
            text_buzzer_volume_value = new ComboBox();
            text_barcode_head = new Label();
            text_trigger_mode_head = new Label();
            text_trigger_mode_value = new ComboBox();
            checkbox_enable_all_2d_codes = new CheckBox();
            text_enable_all_2d_codes_head = new Label();
            checkbox_enable_all_1d_codes = new CheckBox();
            text_enable_all_1d_codes_head = new Label();
            image_scanner_battery = new PictureBox();
            button_navigate_up = new Button();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)image_scanner_battery).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // title_settings
            // 
            title_settings.AutoSize = true;
            title_settings.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            title_settings.Location = new Point(11, 36);
            title_settings.Name = "title_settings";
            title_settings.Size = new Size(138, 45);
            title_settings.TabIndex = 0;
            title_settings.Text = "Settings";
            // 
            // text_scanner_version_head
            // 
            text_scanner_version_head.AutoSize = true;
            text_scanner_version_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_scanner_version_head.Location = new Point(28, 90);
            text_scanner_version_head.Name = "text_scanner_version_head";
            text_scanner_version_head.Size = new Size(125, 21);
            text_scanner_version_head.TabIndex = 4;
            text_scanner_version_head.Text = "Scanner version";
            // 
            // text_scanner_version_value
            // 
            text_scanner_version_value.AutoSize = true;
            text_scanner_version_value.Font = new Font("Arial Narrow", 12F);
            text_scanner_version_value.Location = new Point(298, 91);
            text_scanner_version_value.Name = "text_scanner_version_value";
            text_scanner_version_value.Size = new Size(34, 20);
            text_scanner_version_value.TabIndex = 5;
            text_scanner_version_value.Text = "0.00";
            // 
            // text_read_power_level_head
            // 
            text_read_power_level_head.AutoSize = true;
            text_read_power_level_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_read_power_level_head.Location = new Point(15, 19);
            text_read_power_level_head.Name = "text_read_power_level_head";
            text_read_power_level_head.Size = new Size(139, 21);
            text_read_power_level_head.TabIndex = 6;
            text_read_power_level_head.Text = "Read Power Level";
            // 
            // text_read_power_level_unit
            // 
            text_read_power_level_unit.AutoSize = true;
            text_read_power_level_unit.Font = new Font("Arial Narrow", 12F);
            text_read_power_level_unit.Location = new Point(428, 20);
            text_read_power_level_unit.Name = "text_read_power_level_unit";
            text_read_power_level_unit.Size = new Size(37, 20);
            text_read_power_level_unit.TabIndex = 7;
            text_read_power_level_unit.Text = "dBm";
            // 
            // text_read_power_level_value
            // 
            text_read_power_level_value.Font = new Font("Yu Gothic UI", 12F);
            text_read_power_level_value.FormattingEnabled = true;
            text_read_power_level_value.Location = new Point(317, 21);
            text_read_power_level_value.Name = "text_read_power_level_value";
            text_read_power_level_value.Size = new Size(105, 29);
            text_read_power_level_value.TabIndex = 8;
            // 
            // text_session_value
            // 
            text_session_value.Font = new Font("Yu Gothic UI", 12F);
            text_session_value.FormattingEnabled = true;
            text_session_value.Location = new Point(319, 58);
            text_session_value.Name = "text_session_value";
            text_session_value.Size = new Size(105, 29);
            text_session_value.TabIndex = 10;
            // 
            // text_session_head
            // 
            text_session_head.AutoSize = true;
            text_session_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_session_head.Location = new Point(14, 58);
            text_session_head.Name = "text_session_head";
            text_session_head.Size = new Size(65, 21);
            text_session_head.TabIndex = 9;
            text_session_head.Text = "Session";
            // 
            // text_report_unique_tags_head
            // 
            text_report_unique_tags_head.AutoSize = true;
            text_report_unique_tags_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_report_unique_tags_head.Location = new Point(14, 96);
            text_report_unique_tags_head.Name = "text_report_unique_tags_head";
            text_report_unique_tags_head.Size = new Size(102, 21);
            text_report_unique_tags_head.TabIndex = 11;
            text_report_unique_tags_head.Text = "Anti Re-read";
            // 
            // text_channel_head
            // 
            text_channel_head.AutoSize = true;
            text_channel_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_channel_head.Location = new Point(16, 128);
            text_channel_head.Name = "text_channel_head";
            text_channel_head.Size = new Size(68, 21);
            text_channel_head.TabIndex = 12;
            text_channel_head.Text = "Channel";
            // 
            // text_channel5
            // 
            text_channel5.AutoSize = true;
            text_channel5.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold);
            text_channel5.Location = new Point(274, 125);
            text_channel5.Name = "text_channel5";
            text_channel5.Size = new Size(33, 20);
            text_channel5.TabIndex = 15;
            text_channel5.Text = "ch5";
            // 
            // text_channel11
            // 
            text_channel11.AutoSize = true;
            text_channel11.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold);
            text_channel11.Location = new Point(384, 125);
            text_channel11.Name = "text_channel11";
            text_channel11.Size = new Size(37, 20);
            text_channel11.TabIndex = 16;
            text_channel11.Text = "ch11";
            // 
            // text_channel23
            // 
            text_channel23.AutoSize = true;
            text_channel23.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold);
            text_channel23.Location = new Point(382, 158);
            text_channel23.Name = "text_channel23";
            text_channel23.Size = new Size(41, 20);
            text_channel23.TabIndex = 18;
            text_channel23.Text = "ch23";
            // 
            // text_channel17
            // 
            text_channel17.AutoSize = true;
            text_channel17.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold);
            text_channel17.Location = new Point(274, 158);
            text_channel17.Name = "text_channel17";
            text_channel17.Size = new Size(39, 20);
            text_channel17.TabIndex = 17;
            text_channel17.Text = "ch17";
            // 
            // text_channel25
            // 
            text_channel25.AutoSize = true;
            text_channel25.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold);
            text_channel25.Location = new Point(380, 190);
            text_channel25.Name = "text_channel25";
            text_channel25.Size = new Size(41, 20);
            text_channel25.TabIndex = 20;
            text_channel25.Text = "ch25";
            // 
            // text_channel24
            // 
            text_channel24.AutoSize = true;
            text_channel24.Font = new Font("Yu Gothic UI Semibold", 11.25F, FontStyle.Bold);
            text_channel24.Location = new Point(277, 190);
            text_channel24.Name = "text_channel24";
            text_channel24.Size = new Size(42, 20);
            text_channel24.TabIndex = 19;
            text_channel24.Text = "ch24";
            // 
            // checkbox_report_unique_tags
            // 
            checkbox_report_unique_tags.AutoSize = true;
            checkbox_report_unique_tags.Location = new Point(319, 102);
            checkbox_report_unique_tags.Name = "checkbox_report_unique_tags";
            checkbox_report_unique_tags.Size = new Size(15, 14);
            checkbox_report_unique_tags.TabIndex = 21;
            checkbox_report_unique_tags.UseVisualStyleBackColor = true;
            // 
            // checkbox_channel5
            // 
            checkbox_channel5.AutoSize = true;
            checkbox_channel5.Location = new Point(320, 131);
            checkbox_channel5.Name = "checkbox_channel5";
            checkbox_channel5.Size = new Size(15, 14);
            checkbox_channel5.TabIndex = 22;
            checkbox_channel5.UseVisualStyleBackColor = true;
            // 
            // checkbox_channel17
            // 
            checkbox_channel17.AutoSize = true;
            checkbox_channel17.Location = new Point(319, 163);
            checkbox_channel17.Name = "checkbox_channel17";
            checkbox_channel17.Size = new Size(15, 14);
            checkbox_channel17.TabIndex = 23;
            checkbox_channel17.UseVisualStyleBackColor = true;
            // 
            // checkbox_channel24
            // 
            checkbox_channel24.AutoSize = true;
            checkbox_channel24.Location = new Point(320, 196);
            checkbox_channel24.Name = "checkbox_channel24";
            checkbox_channel24.Size = new Size(15, 14);
            checkbox_channel24.TabIndex = 24;
            checkbox_channel24.UseVisualStyleBackColor = true;
            // 
            // checkbox_channel25
            // 
            checkbox_channel25.AutoSize = true;
            checkbox_channel25.Location = new Point(427, 196);
            checkbox_channel25.Name = "checkbox_channel25";
            checkbox_channel25.Size = new Size(15, 14);
            checkbox_channel25.TabIndex = 27;
            checkbox_channel25.UseVisualStyleBackColor = true;
            // 
            // checkbox_channel23
            // 
            checkbox_channel23.AutoSize = true;
            checkbox_channel23.Location = new Point(427, 163);
            checkbox_channel23.Name = "checkbox_channel23";
            checkbox_channel23.Size = new Size(15, 14);
            checkbox_channel23.TabIndex = 26;
            checkbox_channel23.UseVisualStyleBackColor = true;
            // 
            // checkbox_channel11
            // 
            checkbox_channel11.AutoSize = true;
            checkbox_channel11.Location = new Point(427, 131);
            checkbox_channel11.Name = "checkbox_channel11";
            checkbox_channel11.Size = new Size(15, 14);
            checkbox_channel11.TabIndex = 25;
            checkbox_channel11.UseVisualStyleBackColor = true;
            // 
            // text_q_factor_value
            // 
            text_q_factor_value.Font = new Font("Yu Gothic UI", 12F);
            text_q_factor_value.FormattingEnabled = true;
            text_q_factor_value.Location = new Point(317, 232);
            text_q_factor_value.Name = "text_q_factor_value";
            text_q_factor_value.Size = new Size(105, 29);
            text_q_factor_value.TabIndex = 29;
            // 
            // text_q_factor_head
            // 
            text_q_factor_head.AutoSize = true;
            text_q_factor_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_q_factor_head.Location = new Point(16, 240);
            text_q_factor_head.Name = "text_q_factor_head";
            text_q_factor_head.Size = new Size(70, 21);
            text_q_factor_head.TabIndex = 28;
            text_q_factor_head.Text = "Q factor";
            // 
            // switch_auto_link_profile
            // 
            switch_auto_link_profile.AutoSize = true;
            switch_auto_link_profile.Location = new Point(316, 283);
            switch_auto_link_profile.Name = "switch_auto_link_profile";
            switch_auto_link_profile.Size = new Size(15, 14);
            switch_auto_link_profile.TabIndex = 31;
            switch_auto_link_profile.UseVisualStyleBackColor = true;
            // 
            // text_auto_link_profile_head
            // 
            text_auto_link_profile_head.AutoSize = true;
            text_auto_link_profile_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_auto_link_profile_head.Location = new Point(12, 283);
            text_auto_link_profile_head.Name = "text_auto_link_profile_head";
            text_auto_link_profile_head.Size = new Size(132, 21);
            text_auto_link_profile_head.TabIndex = 30;
            text_auto_link_profile_head.Text = "Auto Link Profile";
            // 
            // text_link_profile_value
            // 
            text_link_profile_value.Font = new Font("Yu Gothic UI", 12F);
            text_link_profile_value.FormattingEnabled = true;
            text_link_profile_value.Location = new Point(317, 309);
            text_link_profile_value.Name = "text_link_profile_value";
            text_link_profile_value.Size = new Size(105, 29);
            text_link_profile_value.TabIndex = 32;
            // 
            // text_link_profile_head
            // 
            text_link_profile_head.AutoSize = true;
            text_link_profile_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_link_profile_head.Location = new Point(12, 317);
            text_link_profile_head.Name = "text_link_profile_head";
            text_link_profile_head.Size = new Size(92, 21);
            text_link_profile_head.TabIndex = 33;
            text_link_profile_head.Text = "Link Profile";
            // 
            // text_polarization_head
            // 
            text_polarization_head.AutoSize = true;
            text_polarization_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_polarization_head.Location = new Point(13, 351);
            text_polarization_head.Name = "text_polarization_head";
            text_polarization_head.Size = new Size(95, 21);
            text_polarization_head.TabIndex = 35;
            text_polarization_head.Text = "Polarization";
            // 
            // text_polarization_value
            // 
            text_polarization_value.Font = new Font("Yu Gothic UI", 12F);
            text_polarization_value.FormattingEnabled = true;
            text_polarization_value.Location = new Point(317, 348);
            text_polarization_value.Name = "text_polarization_value";
            text_polarization_value.Size = new Size(105, 29);
            text_polarization_value.TabIndex = 34;
            // 
            // checkbox_power_save
            // 
            checkbox_power_save.AutoSize = true;
            checkbox_power_save.Location = new Point(319, 392);
            checkbox_power_save.Name = "checkbox_power_save";
            checkbox_power_save.Size = new Size(15, 14);
            checkbox_power_save.TabIndex = 37;
            checkbox_power_save.UseVisualStyleBackColor = true;
            // 
            // text_power_save_head
            // 
            text_power_save_head.AutoSize = true;
            text_power_save_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_power_save_head.Location = new Point(12, 392);
            text_power_save_head.Name = "text_power_save_head";
            text_power_save_head.Size = new Size(92, 21);
            text_power_save_head.TabIndex = 36;
            text_power_save_head.Text = "Power save";
            // 
            // checkbox_buzzer
            // 
            checkbox_buzzer.AutoSize = true;
            checkbox_buzzer.Location = new Point(319, 422);
            checkbox_buzzer.Name = "checkbox_buzzer";
            checkbox_buzzer.Size = new Size(15, 14);
            checkbox_buzzer.TabIndex = 39;
            checkbox_buzzer.UseVisualStyleBackColor = true;
            // 
            // text_buzzer_head
            // 
            text_buzzer_head.AutoSize = true;
            text_buzzer_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_buzzer_head.Location = new Point(12, 422);
            text_buzzer_head.Name = "text_buzzer_head";
            text_buzzer_head.Size = new Size(58, 21);
            text_buzzer_head.TabIndex = 38;
            text_buzzer_head.Text = "Buzzer";
            // 
            // text_buzzer_volume_head
            // 
            text_buzzer_volume_head.AutoSize = true;
            text_buzzer_volume_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_buzzer_volume_head.Location = new Point(12, 457);
            text_buzzer_volume_head.Name = "text_buzzer_volume_head";
            text_buzzer_volume_head.Size = new Size(116, 21);
            text_buzzer_volume_head.TabIndex = 41;
            text_buzzer_volume_head.Text = "Buzzer volume";
            // 
            // text_buzzer_volume_value
            // 
            text_buzzer_volume_value.Font = new Font("Yu Gothic UI", 12F);
            text_buzzer_volume_value.FormattingEnabled = true;
            text_buzzer_volume_value.Location = new Point(318, 449);
            text_buzzer_volume_value.Name = "text_buzzer_volume_value";
            text_buzzer_volume_value.Size = new Size(106, 29);
            text_buzzer_volume_value.TabIndex = 40;
            // 
            // text_barcode_head
            // 
            text_barcode_head.AutoSize = true;
            text_barcode_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_barcode_head.Location = new Point(14, 492);
            text_barcode_head.Name = "text_barcode_head";
            text_barcode_head.Size = new Size(82, 21);
            text_barcode_head.TabIndex = 42;
            text_barcode_head.Text = "BARCODE";
            // 
            // text_trigger_mode_head
            // 
            text_trigger_mode_head.AutoSize = true;
            text_trigger_mode_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_trigger_mode_head.Location = new Point(14, 516);
            text_trigger_mode_head.Name = "text_trigger_mode_head";
            text_trigger_mode_head.Size = new Size(109, 21);
            text_trigger_mode_head.TabIndex = 44;
            text_trigger_mode_head.Text = "Trigger mode";
            // 
            // text_trigger_mode_value
            // 
            text_trigger_mode_value.Font = new Font("Yu Gothic UI", 12F);
            text_trigger_mode_value.FormattingEnabled = true;
            text_trigger_mode_value.Location = new Point(316, 508);
            text_trigger_mode_value.Name = "text_trigger_mode_value";
            text_trigger_mode_value.Size = new Size(227, 29);
            text_trigger_mode_value.TabIndex = 43;
            // 
            // checkbox_enable_all_2d_codes
            // 
            checkbox_enable_all_2d_codes.AutoSize = true;
            checkbox_enable_all_2d_codes.Location = new Point(317, 573);
            checkbox_enable_all_2d_codes.Name = "checkbox_enable_all_2d_codes";
            checkbox_enable_all_2d_codes.Size = new Size(15, 14);
            checkbox_enable_all_2d_codes.TabIndex = 48;
            checkbox_enable_all_2d_codes.UseVisualStyleBackColor = true;
            // 
            // text_enable_all_2d_codes_head
            // 
            text_enable_all_2d_codes_head.AutoSize = true;
            text_enable_all_2d_codes_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_enable_all_2d_codes_head.Location = new Point(13, 573);
            text_enable_all_2d_codes_head.Name = "text_enable_all_2d_codes_head";
            text_enable_all_2d_codes_head.Size = new Size(150, 21);
            text_enable_all_2d_codes_head.TabIndex = 47;
            text_enable_all_2d_codes_head.Text = "Enable all 2D codes";
            // 
            // checkbox_enable_all_1d_codes
            // 
            checkbox_enable_all_1d_codes.AutoSize = true;
            checkbox_enable_all_1d_codes.Location = new Point(317, 549);
            checkbox_enable_all_1d_codes.Name = "checkbox_enable_all_1d_codes";
            checkbox_enable_all_1d_codes.Size = new Size(15, 14);
            checkbox_enable_all_1d_codes.TabIndex = 46;
            checkbox_enable_all_1d_codes.UseVisualStyleBackColor = true;
            // 
            // text_enable_all_1d_codes_head
            // 
            text_enable_all_1d_codes_head.AutoSize = true;
            text_enable_all_1d_codes_head.Font = new Font("Yu Gothic UI Semibold", 12F, FontStyle.Bold);
            text_enable_all_1d_codes_head.Location = new Point(13, 549);
            text_enable_all_1d_codes_head.Name = "text_enable_all_1d_codes_head";
            text_enable_all_1d_codes_head.Size = new Size(147, 21);
            text_enable_all_1d_codes_head.TabIndex = 45;
            text_enable_all_1d_codes_head.Text = "Enable all 1D codes";
            // 
            // image_scanner_battery
            // 
            image_scanner_battery.Image = Properties.Resource.battery_0;
            image_scanner_battery.Location = new Point(395, 64);
            image_scanner_battery.Name = "image_scanner_battery";
            image_scanner_battery.Size = new Size(112, 47);
            image_scanner_battery.SizeMode = PictureBoxSizeMode.Zoom;
            image_scanner_battery.TabIndex = 49;
            image_scanner_battery.TabStop = false;
            // 
            // button_navigate_up
            // 
            button_navigate_up.BackgroundImageLayout = ImageLayout.None;
            button_navigate_up.FlatAppearance.BorderSize = 0;
            button_navigate_up.FlatStyle = FlatStyle.Flat;
            button_navigate_up.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button_navigate_up.ForeColor = Color.Blue;
            button_navigate_up.Location = new Point(1, 3);
            button_navigate_up.Name = "button_navigate_up";
            button_navigate_up.Size = new Size(60, 30);
            button_navigate_up.TabIndex = 50;
            button_navigate_up.Text = "<TOP";
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel2.AutoScroll = true;
            panel2.Controls.Add(text_read_power_level_value);
            panel2.Controls.Add(text_read_power_level_head);
            panel2.Controls.Add(text_read_power_level_unit);
            panel2.Controls.Add(checkbox_enable_all_2d_codes);
            panel2.Controls.Add(text_session_head);
            panel2.Controls.Add(text_enable_all_2d_codes_head);
            panel2.Controls.Add(text_session_value);
            panel2.Controls.Add(checkbox_enable_all_1d_codes);
            panel2.Controls.Add(text_report_unique_tags_head);
            panel2.Controls.Add(text_enable_all_1d_codes_head);
            panel2.Controls.Add(text_channel_head);
            panel2.Controls.Add(text_trigger_mode_head);
            panel2.Controls.Add(text_channel5);
            panel2.Controls.Add(text_trigger_mode_value);
            panel2.Controls.Add(text_channel11);
            panel2.Controls.Add(text_barcode_head);
            panel2.Controls.Add(text_channel17);
            panel2.Controls.Add(text_buzzer_volume_head);
            panel2.Controls.Add(text_buzzer_volume_value);
            panel2.Controls.Add(text_channel23);
            panel2.Controls.Add(checkbox_buzzer);
            panel2.Controls.Add(text_channel24);
            panel2.Controls.Add(text_buzzer_head);
            panel2.Controls.Add(text_channel25);
            panel2.Controls.Add(checkbox_report_unique_tags);
            panel2.Controls.Add(checkbox_power_save);
            panel2.Controls.Add(checkbox_channel5);
            panel2.Controls.Add(text_power_save_head);
            panel2.Controls.Add(checkbox_channel17);
            panel2.Controls.Add(text_polarization_head);
            panel2.Controls.Add(checkbox_channel24);
            panel2.Controls.Add(text_polarization_value);
            panel2.Controls.Add(checkbox_channel11);
            panel2.Controls.Add(text_link_profile_head);
            panel2.Controls.Add(checkbox_channel23);
            panel2.Controls.Add(text_link_profile_value);
            panel2.Controls.Add(switch_auto_link_profile);
            panel2.Controls.Add(checkbox_channel25);
            panel2.Controls.Add(text_auto_link_profile_head);
            panel2.Controls.Add(text_q_factor_value);
            panel2.Controls.Add(text_q_factor_head);
            panel2.Location = new Point(11, 117);
            panel2.Name = "panel2";
            panel2.Size = new Size(701, 422);
            panel2.TabIndex = 83;
            // 
            // FormSettingsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 551);
            Controls.Add(panel2);
            Controls.Add(button_navigate_up);
            Controls.Add(image_scanner_battery);
            Controls.Add(text_scanner_version_value);
            Controls.Add(text_scanner_version_head);
            Controls.Add(title_settings);
            Name = "FormSettingsPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormSettingsPage_FormClosed;
            Load += FormSettingsPage_Load;
            ((System.ComponentModel.ISupportInitialize)image_scanner_battery).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title_settings;
        private Label text_scanner_version_head;
        private Label text_scanner_version_value;
        private Label text_read_power_level_head;
        private Label text_read_power_level_unit;
        private ComboBox text_read_power_level_value;
        private ComboBox text_session_value;
        private Label text_session_head;
        private Label text_report_unique_tags_head;
        private Label text_channel_head;
        private Label text_channel5;
        private Label text_channel11;
        private Label text_channel23;
        private Label text_channel17;
        private Label text_channel25;
        private Label text_channel24;
        private CheckBox checkbox_report_unique_tags;
        private CheckBox checkbox_channel5;
        private CheckBox checkbox_channel17;
        private CheckBox checkbox_channel24;
        private CheckBox checkbox_channel25;
        private CheckBox checkbox_channel23;
        private CheckBox checkbox_channel11;
        private ComboBox text_q_factor_value;
        private Label text_q_factor_head;
        private CheckBox switch_auto_link_profile;
        private Label text_auto_link_profile_head;
        private ComboBox text_link_profile_value;
        private Label text_link_profile_head;
        private Label text_polarization_head;
        private ComboBox text_polarization_value;
        private CheckBox checkbox_power_save;
        private Label text_power_save_head;
        private CheckBox checkbox_buzzer;
        private Label text_buzzer_head;
        private Label text_buzzer_volume_head;
        private ComboBox text_buzzer_volume_value;
        private Label text_barcode_head;
        private Label text_trigger_mode_head;
        private ComboBox text_trigger_mode_value;
        private CheckBox checkbox_enable_all_2d_codes;
        private Label text_enable_all_2d_codes_head;
        private CheckBox checkbox_enable_all_1d_codes;
        private Label text_enable_all_1d_codes_head;
        private PictureBox image_scanner_battery;
        private Button button_navigate_up;
        private Panel panel2;
    }
}