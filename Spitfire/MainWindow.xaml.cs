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
using System.Windows.Threading; // For game timer

namespace Spitfire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int playerSpeed = 10;

        // === Default Keybinds ===
        // Movement
        char kbMoveUp = 'w';
        char kbMoveDown = 's';
        char kbMoveLeft = 'a';
        char kbMoveRight = 'd';

        // Movement Bools
        bool goUp;
        bool goDown;
        bool goLeft;
        bool goRight;
        public MainWindow()
        {
            InitializeComponent();

            // Game Timer
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Tick += Game_Tick;
            tmr.Interval = TimeSpan.FromMilliseconds(20); // running the timer every 20 milliseconds
            tmr.Start();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            // Player Movement
            if (e.Key == Key.Down)
            {
                goDown = true;
            }
            else if (e.Key == Key.Up)
            {
                goUp = true;
            }
            else if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            else if (e.Key == Key.Right)
            {
                goRight = true;
            }
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            // Player Movement
            if (e.Key == Key.Down)
            {
                goDown = false;
            }
            else if (e.Key == Key.Up)
            {
                goUp = false;
            }
            else if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            else if (e.Key == Key.Right)
            {
                goRight = false;
            }
        }

        private void Game_Tick(object sender, EventArgs e)
        {
            if (goUp && Canvas.GetTop(player) > 0)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
            }
            if (goDown && Canvas.GetTop(player) + (player.Height * 2) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            }
            if (goLeft && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (goRight && Canvas.GetLeft(player) + (player.Width * 2) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
        }
    }
}
