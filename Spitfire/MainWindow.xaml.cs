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
        public bool pause = false;
        public bool fireRelease = true; // Bool to track whether the fire key has been released or not (used to keep firing from being automatic)

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
                GameEngine ge = new GameEngine(player, playerSpeed);
                if (fireRelease && !pause) // If the fire key is release and the game is not paused
                {
                    ge.playerFire(GameCanvas);
                    fireRelease = false;
                }
                    
            }
            else if (e.Key == Key.P) // Pause functionality
            {
                if(pause)
                    pause = false;
                else
                    pause = true;
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
                fireRelease = true;
            }
            else if (e.Key == Key.M)
            {
                MenuPause();
            }
        }

        
        Enemy.EnemyBasic eb = new Enemy.EnemyBasic();
        
        private void Game_Tick(object sender, EventArgs e)
        {
            GameEngine ge = new GameEngine(player, playerSpeed);

            if (!pause) // Checks to make sure game isn't paused
            {
                pauseTxt.Content = ""; // Empties pause screen

                ge.playerMove(goUp, goDown, goRight, goLeft); // Player movement controller
                ge.HitDetection(GameCanvas);
            } else // Display Pause screen
            {
                pauseTxt.Content = "Paused";
            }
            
        }

        private void MenuPause()
        {
            MainMenu menu = new MainMenu();
            Close();
            menu.Show();

        }


    }
}
