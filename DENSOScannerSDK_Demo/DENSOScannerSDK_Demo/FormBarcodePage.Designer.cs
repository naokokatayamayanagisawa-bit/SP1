namespace DENSOScannerSDK_Demo
{
    partial class FormBarcodePage
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
            list_view = new ListView();
            button_switch = new Button();
            text_title_barcode = new Label();
            button_navigate_up = new Button();
            SuspendLayout();
            // 
            // list_view
            // 
            list_view.Activation = ItemActivation.OneClick;
            list_view.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            list_view.BorderStyle = BorderStyle.None;
            list_view.Font = new Font("Yu Gothic UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            list_view.Location = new Point(10, 36);
            list_view.Name = "list_view";
            list_view.Size = new Size(700, 413);
            list_view.TabIndex = 0;
            list_view.UseCompatibleStateImageBehavior = false;
            // 
            // button_switch
            // 
            button_switch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_switch.BackColor = Color.FromArgb(159, 191, 190);
            button_switch.Font = new Font("Yu Gothic UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button_switch.Location = new Point(10, 468);
            button_switch.Name = "button_switch";
            button_switch.Size = new Size(698, 51);
            button_switch.TabIndex = 1;
            button_switch.Text = "Start";
            button_switch.UseVisualStyleBackColor = false;
            button_switch.Click += button_switch_Clicked;
            // 
            // text_title_barcode
            // 
            text_title_barcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            text_title_barcode.Font = new Font("Yu Gothic UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 128);
            text_title_barcode.Location = new Point(10, 36);
            text_title_barcode.Name = "text_title_barcode";
            text_title_barcode.Size = new Size(713, 57);
            text_title_barcode.TabIndex = 5;
            text_title_barcode.Text = "Barcode";
            // 
            // button_navigate_up
            // 
            button_navigate_up.BackgroundImageLayout = ImageLayout.None;
            button_navigate_up.FlatAppearance.BorderSize = 0;
            button_navigate_up.FlatStyle = FlatStyle.Flat;
            button_navigate_up.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            button_navigate_up.ForeColor = Color.Blue;
            button_navigate_up.Location = new Point(0, 0);
            button_navigate_up.Name = "button_navigate_up";
            button_navigate_up.Size = new Size(60, 30);
            button_navigate_up.TabIndex = 6;
            button_navigate_up.Text = "<TOP";
            button_navigate_up.UseVisualStyleBackColor = true;
            button_navigate_up.Click += button_navigate_up_Clicked;
            // 
            // FormBarcodePage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(724, 531);
            Controls.Add(button_navigate_up);
            Controls.Add(text_title_barcode);
            Controls.Add(button_switch);
            Controls.Add(list_view);
            Name = "FormBarcodePage";
            Text = "DENSOScannerSDK_Demo";
            FormClosed += FormBarcodePage_FormClosed;
            Load += FormBarcodePage_Load;
            Resize += FormBarcodePage_SizeChanged;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbxbarcodeData;
        private ListView list_view;
        private Button button_switch;
        private Label text_title_barcode;
        private Button button_navigate_up;
    }
}