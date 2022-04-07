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
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        // Global values
        public int playerSpeed = 10;
        public bool goLeft = false;
        public bool goRight = false;
        public bool goUp = false;
        public bool goDown = false;
        public bool fireBul = false;
        int fireDelay = 0;

        // === Default Keybinds ===
        // Movement
        char kbMoveUp = 'w';
        char kbMoveDown = 's';
        char kbMoveLeft = 'a';
        char kbMoveRight = 'd';

        // Audio
        private SoundPlayer music = new SoundPlayer(Properties.Resources.TheAceofSevens);

        public MainWindow()
        {
            InitializeComponent();

            // Game Timer
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Tick += Game_Tick;
            tmr.Interval = TimeSpan.FromMilliseconds(20); // running the timer every 20 milliseconds
            tmr.Start();

            // Music
            music.Load();
            music.PlayLooping();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
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
            else if (e.Key == Key.Space)
            {
                fireBul = true;
            }
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
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
            else if (e.Key == Key.Space)
            {
                fireBul = false;
                fireDelay = 0; // Resets fire delay to 0
            }
        }
        private void Game_Tick(object sender, EventArgs e)
        {
            GameEngine ge = new GameEngine(player, playerSpeed);

            ge.playerMove(goUp, goDown, goRight, goLeft); // Player movement controller

            // Fire Bullets
            if(fireDelay <= 0)
            {
                fireDelay = ge.playerFire(GameCanvas, fireBul);
            }
            fireDelay--;

            ge.HitDetection(GameCanvas);
        }


    }
}
