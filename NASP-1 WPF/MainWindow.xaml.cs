using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NASP_1_WPF
{

    public partial class MainWindow : Window
    {
        private string filePath;
        private string fileContent;
        private List<int> numbersFromFile = new List<int>();
        AVLLib.AVLTree<int> AVLtree;

        public MainWindow()
        {
            InitializeComponent();
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
                        .Where(x => int.TryParse(x, out tryParseOut) == true)
                        .Select(x => int.Parse(x))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            AVLtree = AVLtree ?? new AVLLib.AVLTree<int>(numbersFromFile[0]);

            for (int i = 0; i < numbersFromFile.Count; i++)
            {
                AVLtree.Insert(numbersFromFile[i]);
            }
            DisplayTree();
        }

        private void DisplayTree()
        {
            treeTB.Text = AVLtree.IndorderTraversal() + "\n" + AVLtree.Draw();
        }


        private void ConfirmInsertButton_Click(object sender, RoutedEventArgs e)
        {
            int tryParseOut;
            List<int> numbersFromTextBox = new List<int>();

            try
            {
                numbersFromTextBox = ToInsertTextBox.Text.Split(',', ' ')
                          .Where(x => int.TryParse(x, out tryParseOut) == true)
                          .Select(x => int.Parse(x))
                          .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (numbersFromTextBox.Count == 0) return;

            AVLtree = AVLtree ?? new AVLLib.AVLTree<int>(numbersFromTextBox[0]);

            for (int i = 0; i < numbersFromTextBox.Count; i++)
            {
                AVLtree.Insert(numbersFromTextBox[i]);
            }

            ToInsertTextBox.Text = "";
            DisplayTree();

        }
    }
}
