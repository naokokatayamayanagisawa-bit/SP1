namespace DENSOScannerSDK_Demo
{
    partial class FormSplash
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
            image_SplashScreen = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)image_SplashScreen).BeginInit();
            SuspendLayout();
            // 
            // image_SplashScreen
            // 
            image_SplashScreen.Image = Properties.Resource.SplashScreen_scale_400;
            image_SplashScreen.Location = new Point(24, 96);
            image_SplashScreen.Name = "image_SplashScreen";
            image_SplashScreen.Size = new Size(440, 255);
            image_SplashScreen.SizeMode = PictureBoxSizeMode.Zoom;
            image_SplashScreen.TabIndex = 50;
            image_SplashScreen.TabStop = false;
            // 
            // FormSplash
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DodgerBlue;
            ClientSize = new Size(496, 447);
            Controls.Add(image_SplashScreen);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormSplash";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DENSOScannerSDK_Demo";
            ((System.ComponentModel.ISupportInitialize)image_SplashScreen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox image_SplashScreen;
    }
}