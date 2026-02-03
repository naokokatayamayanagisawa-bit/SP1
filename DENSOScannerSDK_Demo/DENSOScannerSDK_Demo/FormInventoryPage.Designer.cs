namespace DENSOScannerSDK_Demo
{
    partial class FormInventoryPage
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
            text_title_inventory = new Label();
            button_switch = new Button();
            list_view = new ListView();
            text_total_tags_head = new Label();
            Text_Total_Tags_Value = new Label();
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
            button_navigate_up.TabIndex = 10;
            button_navigate_up.Text = "<TOP";
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Clicked;
            // 
            // text_title_inventory
            // 
            text_title_inventory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            text_title_inventory.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_title_inventory.Location = new Point(14, 27);
            text_title_inventory.Name = "text_title_inventory";
            text_title_inventory.Size = new Size(711, 57);
            text_title_inventory.TabIndex = 9;
            text_title_inventory.Text = "Inventory";
            // 
            // button_switch
            // 
            button_switch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_switch.BackColor = Color.FromArgb(159, 191, 190);
            button_switch.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button_switch.Location = new Point(14, 498);
            button_switch.Name = "button_switch";
            button_switch.Size = new Size(698, 51);
            button_switch.TabIndex = 8;
            button_switch.Text = "Start";
            button_switch.UseVisualStyleBackColor = false;
            button_switch.Click += button_switch_Clicked;
            // 
            // list_view
            // 
            list_view.Activation = ItemActivation.OneClick;
            list_view.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            list_view.BorderStyle = BorderStyle.None;
            list_view.Font = new Font("Yu Gothic UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            list_view.Location = new Point(16, 105);
            list_view.Name = "list_view";
            list_view.Size = new Size(703, 387);
            list_view.TabIndex = 7;
            list_view.UseCompatibleStateImageBehavior = false;
            // 
            // text_total_tags_head
            // 
            text_total_tags_head.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            text_total_tags_head.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold);
            text_total_tags_head.Location = new Point(16, 102);
            text_total_tags_head.Name = "text_total_tags_head";
            text_total_tags_head.Size = new Size(702, 57);
            text_total_tags_head.TabIndex = 11;
            text_total_tags_head.Text = "Total Tags";
            text_total_tags_head.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Text_Total_Tags_Value
            // 
            Text_Total_Tags_Value.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Text_Total_Tags_Value.Font = new Font("Yu Gothic UI Semibold", 18F, FontStyle.Bold);
            Text_Total_Tags_Value.ImageAlign = ContentAlignment.MiddleRight;
            Text_Total_Tags_Value.Location = new Point(514, 102);
            Text_Total_Tags_Value.Name = "Text_Total_Tags_Value";
            Text_Total_Tags_Value.Size = new Size(198, 57);
            Text_Total_Tags_Value.TabIndex = 13;
            Text_Total_Tags_Value.Text = "0";
            Text_Total_Tags_Value.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormInventoryPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 561);
            Controls.Add(Text_Total_Tags_Value);
            Controls.Add(text_total_tags_head);
            Controls.Add(button_navigate_up);
            Controls.Add(text_title_inventory);
            Controls.Add(button_switch);
            Controls.Add(list_view);
            Name = "FormInventoryPage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormInventoryPage_FormClosed;
            Load += FormInventoryPage_Load;
            Resize += FormInventoryPage_SizeChanged;
            ResumeLayout(false);
        }

        #endregion

        private Button button_navigate_up;
        private Label text_title_inventory;
        private Button button_switch;
        private ListView list_view;
        private Label text_total_tags_head;
        private Label Text_Total_Tags_Value;
    }
}