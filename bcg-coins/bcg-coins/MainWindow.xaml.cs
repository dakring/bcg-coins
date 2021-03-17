using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace bcg_coins {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void mnuOpen_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true) {

                string metaData = "BaseImage:\n";

                string filePath = openFileDialog.SafeFileName;

                metaData = metaData + filePath + "\n";

                Uri fileUri = new Uri(openFileDialog.FileName);
                baseImage.Source = new BitmapImage(fileUri);

                using (var imageStream = openFileDialog.OpenFile()) {
                    var decoder = BitmapDecoder.Create(imageStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                    var height = decoder.Frames[0].PixelHeight;
                    var width = decoder.Frames[0].PixelWidth;
                    metaData = metaData + "h: " + height + "px, w: " + width + "px\n";
                }
                baseImageMetaData.Text = metaData;
            }
        }

        private void mnuSettings_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Settings-Callback", "Debug Window");
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Das Projekt \"Coins\" ist im Rahmen des Moduls Bildbasierte Computergrafik an der Technischen Hochschule Köln enstanden.\n2021 - David Alexander Kring\nBetreuender Professor: Andreas Karge", "About");
        }
        private void mnuExit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
