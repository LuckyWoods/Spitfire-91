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
        public int spawnDelay = 0; // Tracks how long until an enemy can spawn again

        // Global bools
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
        }

        
        
        
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

            if(spawnDelay <= 0) // If there is no spawn delay, spwan an enemy
            {
                ge.spawnEnemy(GameCanvas);
                spawnDelay = 1000;
            }
            else
            {
                int spawnDelayReducer = 10; // (1 * (score + 1 / 3)); // Reduction to spawn delay increases with score
                if (spawnDelayReducer > 950) // Check to make sure enemies can't spawn instantly with the current spawn delay
                    spawnDelay = spawnDelayReducer = 950;
                spawnDelay -= spawnDelayReducer;
            }

            int points = ge.ScoreUpdate();
            scoreText.Content = "Score: " + points;

            int health = ge.HealthUpdate();
            healthText.Content = "Score: " + health;
        }


    }
}
