namespace DENSOScannerSDK_Demo
{
    partial class FormPreFiltersPage
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
            button_navigate_up = new Button();
            text_title_pre_filters = new Label();
            text_bank_value = new ComboBox();
            text_bank_head = new Label();
            text_offset_value = new TextBox();
            text_offset_head = new Label();
            text_pattern_head = new Label();
            text_pattern_value = new TextBox();
            button_filter_clear = new Button();
            button_filter_load = new Button();
            button_filter_set = new Button();
            SuspendLayout();
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
            button_navigate_up.TabIndex = 52;
            button_navigate_up.Text = "<TOP";
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Clicked;
            // 
            // text_title_pre_filters
            // 
            text_title_pre_filters.AutoSize = true;
            text_title_pre_filters.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_title_pre_filters.Location = new Point(11, 36);
            text_title_pre_filters.Name = "text_title_pre_filters";
            text_title_pre_filters.Size = new Size(155, 45);
            text_title_pre_filters.TabIndex = 51;
            text_title_pre_filters.Text = "PreFilters";
            // 
            // text_bank_value
            // 
            text_bank_value.Font = new Font("Yu Gothic UI", 14.25F);
            text_bank_value.FormattingEnabled = true;
            text_bank_value.Location = new Point(23, 161);
            text_bank_value.Name = "text_bank_value";
            text_bank_value.Size = new Size(178, 33);
            text_bank_value.TabIndex = 54;
            text_bank_value.SelectedIndexChanged += text_bank_value_SelectedIndexChanged;
            // 
            // text_bank_head
            // 
            text_bank_head.AutoSize = true;
            text_bank_head.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_bank_head.ForeColor = Color.FromArgb(98, 120, 255);
            text_bank_head.Location = new Point(23, 113);
            text_bank_head.Name = "text_bank_head";
            text_bank_head.Size = new Size(77, 37);
            text_bank_head.TabIndex = 55;
            text_bank_head.Text = "Bank";
            // 
            // text_offset_value
            // 
            text_offset_value.Font = new Font("Yu Gothic UI", 14.25F);
            text_offset_value.Location = new Point(478, 161);
            text_offset_value.Name = "text_offset_value";
            text_offset_value.Size = new Size(223, 33);
            text_offset_value.TabIndex = 56;
            text_offset_value.TextChanged += Text_offset_value_TextChanged;
            text_offset_value.KeyPress += text_offset_value_KeyPress;
            text_offset_value.Validated += text_offset_value_Completed;
            // 
            // text_offset_head
            // 
            text_offset_head.AutoSize = true;
            text_offset_head.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_offset_head.ForeColor = Color.FromArgb(98, 120, 255);
            text_offset_head.Location = new Point(478, 113);
            text_offset_head.Name = "text_offset_head";
            text_offset_head.Size = new Size(143, 37);
            text_offset_head.TabIndex = 57;
            text_offset_head.Text = "Offset(bit)";
            // 
            // text_pattern_head
            // 
            text_pattern_head.AutoSize = true;
            text_pattern_head.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold);
            text_pattern_head.ForeColor = Color.FromArgb(98, 120, 255);
            text_pattern_head.Location = new Point(23, 225);
            text_pattern_head.Name = "text_pattern_head";
            text_pattern_head.Size = new Size(106, 37);
            text_pattern_head.TabIndex = 58;
            text_pattern_head.Text = "Pattern";
            // 
            // text_pattern_value
            // 
            text_pattern_value.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            text_pattern_value.Font = new Font("Yu Gothic UI", 14.25F);
            text_pattern_value.Location = new Point(23, 265);
            text_pattern_value.Name = "text_pattern_value";
            text_pattern_value.Size = new Size(678, 33);
            text_pattern_value.TabIndex = 59;
            text_pattern_value.TextChanged += Text_pattern_value_TextChanged;
            text_pattern_value.KeyPress += text_pattern_value_KeyPress;
            text_pattern_value.Validated += text_pattern_value_Completed;
            // 
            // button_filter_clear
            // 
            button_filter_clear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_filter_clear.BackColor = Color.FromArgb(159, 191, 190);
            button_filter_clear.FlatAppearance.BorderSize = 0;
            button_filter_clear.FlatStyle = FlatStyle.Flat;
            button_filter_clear.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_filter_clear.Location = new Point(23, 387);
            button_filter_clear.Name = "button_filter_clear";
            button_filter_clear.Size = new Size(678, 51);
            button_filter_clear.TabIndex = 61;
            button_filter_clear.Text = "Clear";
            button_filter_clear.UseVisualStyleBackColor = false;
            button_filter_clear.Click += button_filter_clear_Clicked;
            // 
            // button_filter_load
            // 
            button_filter_load.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_filter_load.BackColor = Color.FromArgb(159, 191, 190);
            button_filter_load.FlatAppearance.BorderSize = 0;
            button_filter_load.FlatStyle = FlatStyle.Flat;
            button_filter_load.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_filter_load.Location = new Point(23, 444);
            button_filter_load.Name = "button_filter_load";
            button_filter_load.Size = new Size(678, 51);
            button_filter_load.TabIndex = 60;
            button_filter_load.Text = "Load";
            button_filter_load.UseVisualStyleBackColor = false;
            button_filter_load.Click += button_filter_load_Clicked;
            // 
            // button_filter_set
            // 
            button_filter_set.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button_filter_set.BackColor = Color.FromArgb(159, 191, 190);
            button_filter_set.FlatAppearance.BorderSize = 0;
            button_filter_set.FlatStyle = FlatStyle.Flat;
            button_filter_set.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold);
            button_filter_set.Location = new Point(23, 330);
            button_filter_set.Name = "button_filter_set";
            button_filter_set.Size = new Size(678, 51);
            button_filter_set.TabIndex = 62;
            button_filter_set.Text = "Set";
            button_filter_set.UseVisualStyleBackColor = false;
            button_filter_set.Click += button_filter_set_Clicked;
            // 
            // FormPreFiltersPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 501);
            Controls.Add(button_filter_set);
            Controls.Add(button_filter_clear);
            Controls.Add(button_filter_load);
            Controls.Add(text_pattern_value);
            Controls.Add(text_pattern_head);
            Controls.Add(text_offset_head);
            Controls.Add(text_offset_value);
            Controls.Add(text_bank_head);
            Controls.Add(text_bank_value);
            Controls.Add(button_navigate_up);
            Controls.Add(text_title_pre_filters);
            Name = "FormPreFiltersPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormPreFiltersPage_FormClosed;
            Load += FormPreFiltersPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_navigate_up;
        private Label text_title_pre_filters;
        private ComboBox text_bank_value;
        private Label text_bank_head;
        private TextBox text_offset_value;
        private Label text_offset_head;
        private Label text_pattern_head;
        private TextBox text_pattern_value;
        private Button button_filter_clear;
        private Button button_filter_load;
        private Button button_filter_set;
    }
}