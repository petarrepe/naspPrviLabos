using System;
using System.IO;
using System.Windows;

namespace NASP_1_WPF
{

    public partial class MainWindow : Window
    {
        private string filePath;
        private string fileContent;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FilePickerButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";


            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                filePath = dlg.FileName;
                PathToFiletextBlock.Text = filename;
            }

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    fileContent = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
