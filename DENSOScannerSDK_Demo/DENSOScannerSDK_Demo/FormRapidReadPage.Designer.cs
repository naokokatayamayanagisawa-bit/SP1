namespace DENSOScannerSDK_Demo
{
    partial class FormRapidReadPage
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
            text_title_rapid_read = new Label();
            button_switch = new Button();
            button_clear = new Button();
            text_read_tags_head = new Label();
            text_read_tags_unit = new Label();
            text_read_tags_per_second_unit = new Label();
            text_read_time = new Label();
            text_read_tags_per_second_value = new Label();
            text_read_tags_value = new Label();
            button_navigate_up = new Button();
            SuspendLayout();
            // 
            // text_title_rapid_read
            // 
            text_title_rapid_read.AutoSize = true;
            text_title_rapid_read.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_title_rapid_read.Location = new Point(0, 36);
            text_title_rapid_read.Name = "text_title_rapid_read";
            text_title_rapid_read.Size = new Size(176, 45);
            text_title_rapid_read.TabIndex = 4;
            text_title_rapid_read.Text = "RapidRead";
            // 
            // button_switch
            // 
            button_switch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_switch.BackColor = Color.FromArgb(159, 191, 190);
            button_switch.FlatAppearance.BorderSize = 0;
            button_switch.FlatStyle = FlatStyle.Flat;
            button_switch.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_switch.Location = new Point(14, 361);
            button_switch.Name = "button_switch";
            button_switch.Size = new Size(698, 51);
            button_switch.TabIndex = 6;
            button_switch.Text = "Start";
            button_switch.UseVisualStyleBackColor = false;
            button_switch.Click += button_switch_Clicked;
            // 
            // button_clear
            // 
            button_clear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_clear.BackColor = Color.FromArgb(159, 191, 190);
            button_clear.FlatAppearance.BorderSize = 0;
            button_clear.FlatStyle = FlatStyle.Flat;
            button_clear.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_clear.Location = new Point(14, 418);
            button_clear.Name = "button_clear";
            button_clear.Size = new Size(698, 51);
            button_clear.TabIndex = 7;
            button_clear.Text = "Clear";
            button_clear.UseVisualStyleBackColor = false;
            button_clear.Click += button_clear_Clicked;
            // 
            // text_read_tags_head
            // 
            text_read_tags_head.AutoSize = true;
            text_read_tags_head.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_read_tags_head.Location = new Point(1, 94);
            text_read_tags_head.Name = "text_read_tags_head";
            text_read_tags_head.Size = new Size(134, 37);
            text_read_tags_head.TabIndex = 8;
            text_read_tags_head.Text = "Read Tag:";
            // 
            // text_read_tags_unit
            // 
            text_read_tags_unit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            text_read_tags_unit.AutoSize = true;
            text_read_tags_unit.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_read_tags_unit.Location = new Point(644, 94);
            text_read_tags_unit.Name = "text_read_tags_unit";
            text_read_tags_unit.Size = new Size(58, 37);
            text_read_tags_unit.TabIndex = 9;
            text_read_tags_unit.Text = "pcs";
            // 
            // text_read_tags_per_second_unit
            // 
            text_read_tags_per_second_unit.AutoSize = true;
            text_read_tags_per_second_unit.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_read_tags_per_second_unit.ForeColor = Color.Blue;
            text_read_tags_per_second_unit.Location = new Point(368, 283);
            text_read_tags_per_second_unit.Name = "text_read_tags_per_second_unit";
            text_read_tags_per_second_unit.Size = new Size(81, 37);
            text_read_tags_per_second_unit.TabIndex = 10;
            text_read_tags_per_second_unit.Text = "pcs/s";
            // 
            // text_read_time
            // 
            text_read_time.AutoSize = true;
            text_read_time.Font = new Font("Yu Gothic UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_read_time.Location = new Point(283, 208);
            text_read_time.Name = "text_read_time";
            text_read_time.Size = new Size(166, 50);
            text_read_time.TabIndex = 11;
            text_read_time.Text = "00:00:00";
            // 
            // text_read_tags_per_second_value
            // 
            text_read_tags_per_second_value.AutoSize = true;
            text_read_tags_per_second_value.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_read_tags_per_second_value.ForeColor = Color.Blue;
            text_read_tags_per_second_value.Location = new Point(283, 283);
            text_read_tags_per_second_value.Name = "text_read_tags_per_second_value";
            text_read_tags_per_second_value.Size = new Size(32, 37);
            text_read_tags_per_second_value.TabIndex = 12;
            text_read_tags_per_second_value.Text = "0";
            // 
            // text_read_tags_value
            // 
            text_read_tags_value.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            text_read_tags_value.Font = new Font("Yu Gothic UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_read_tags_value.ForeColor = SystemColors.ControlText;
            text_read_tags_value.Location = new Point(462, 96);
            text_read_tags_value.Name = "text_read_tags_value";
            text_read_tags_value.Size = new Size(188, 37);
            text_read_tags_value.TabIndex = 13;
            text_read_tags_value.Text = "0";
            text_read_tags_value.TextAlign = ContentAlignment.MiddleRight;
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
            button_navigate_up.TabIndex = 14;
            button_navigate_up.Text = "<TOP";
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Clicked;
            // 
            // FormRapidReadPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 481);
            Controls.Add(button_navigate_up);
            Controls.Add(text_read_tags_value);
            Controls.Add(text_read_tags_per_second_value);
            Controls.Add(text_read_time);
            Controls.Add(text_read_tags_per_second_unit);
            Controls.Add(text_read_tags_unit);
            Controls.Add(text_read_tags_head);
            Controls.Add(button_clear);
            Controls.Add(button_switch);
            Controls.Add(text_title_rapid_read);
            Name = "FormRapidReadPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormRapidReadPage_FormClosed;
            Load += FormRapidReadPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label text_title_rapid_read;
        private Button button_switch;
        private Button button_clear;
        private Label text_read_tags_head;
        private Label text_read_tags_unit;
        private Label text_read_tags_per_second_unit;
        private Label text_read_time;
        private Label text_read_tags_per_second_value;
        private Label text_read_tags_value;
        private Button button_navigate_up;
    }
}