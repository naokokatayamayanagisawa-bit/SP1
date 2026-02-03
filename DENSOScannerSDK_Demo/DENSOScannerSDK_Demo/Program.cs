namespace DENSOScannerSDK_Demo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            FormSplash fs = new FormSplash();
            fs.Show();
            fs.Refresh();
            Thread.Sleep(1000);
            fs.Close();

            Application.Run(new FormMainPage());
        }
    }
}