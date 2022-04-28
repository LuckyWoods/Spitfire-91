using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading; // For game timer


namespace Spitfire
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        { 
            InitializeComponent();
            // Music
            music.Load();
            music.PlayLooping();
        }

        private SoundPlayer music = new SoundPlayer(Properties.Resources.TheAceofSevens);

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }

        private void CheckScore_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
    }
}
