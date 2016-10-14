using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;

namespace NASP_1_WPF
{

    public partial class MainWindow : Window
    {
        private string filePath;
        private string fileContent;
        private List<int> numbersFromFile = new List<int>();
        
        public MainWindow()
        {
            InitializeComponent();
            AVLradioButton.IsChecked = true;
        }

        private void FilePickerButton_Click(object sender, RoutedEventArgs e)
        {
            int tryParseOut;
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
            { 
                using (StreamReader sr = new StreamReader(filePath))
                {

                    fileContent = sr.ReadToEnd();

                    numbersFromFile = fileContent.Split(',', ' ', '\n', '\r')
                        .Where(x=> int.TryParse(x, out tryParseOut) == true)
                        .Select(x=> int.Parse(x))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AVLradioButton.IsChecked = false;
        }

        private void AVLradioButton_Checked(object sender, RoutedEventArgs e)
        {
            RBRadioButton.IsChecked = false;
        }
    }
}
