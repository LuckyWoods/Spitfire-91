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
        Random rand = new Random();
        List<Rectangle> itemstoremove = new List<Rectangle>(); // Remove List

        // Player Stats
        int score = 0;
        int health = 5;
        int playerSpeed = 10;
        int bulletSpeed = 20;

        // Collision
        Rect playerHitBox;

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

            // Visuals

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

            // Shooting
            if (e.Key == Key.Space)
            {
                Rectangle enemyBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 5,
                    Width = 20,
                    Fill = Brushes.Yellow,
                    Stroke = Brushes.Gold
                };

                Canvas.SetTop(enemyBullet, Canvas.GetTop(player) - enemyBullet.Height); // place the bullet on top of the player location
                Canvas.SetLeft(enemyBullet, Canvas.GetLeft(player) + player.Height / 2); // place the bullet middle of the player image
                GameCanvas.Children.Add(enemyBullet); // add the bullet to the screen
            }
        }

        private void makeEnemies()
        {
            ImageBrush enemyPlaneSprite = new ImageBrush();

            Rectangle enemyPlane = new Rectangle()
            {
                Tag = "enemy",
                Height = 100,
                Width = 50,
                Fill = Brushes.Red
            };

            Canvas.SetTop(enemyPlane, - 100);
            Canvas.SetLeft(enemyPlane, rand.Next(30, 430));
            GameCanvas.Children.Add(enemyPlane);

            // garbage collection
            GC.Collect();
        }

        private void Game_Tick(object sender, EventArgs e)
        {
            // Player Movement
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


            // 
            scoreText.Content = "Score: " + score;

            // Hit Detection
            foreach (var x in GameCanvas.Children.OfType<Rectangle>())
            {
                // if any rectangle has the tag bullet in it
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletSpeed); // move bullet right
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // make a rect class with bullet properties

                    
                    if (Canvas.GetRight(x) > GameCanvas.Width)
                    {
                        itemstoremove.Add(x); // if it has then add it to the item to remove list
                    }

                    
                    foreach (var y in GameCanvas.Children.OfType<Rectangle>()) // run another for each loop inside of the main loop
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemy = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);


                            // now check if bullet and enemy is colliding or not
                            // if the bullet is colliding with the enemy rectangle
                            if (bullet.IntersectsWith(enemy))
                            {
                                itemstoremove.Add(x); // remove bullet
                                itemstoremove.Add(y); // remove enemy
                                score++; // add one to the score
                            }
                        }

                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetTop(x, Canvas.GetRight(x) + 10); // move the enemy downwards

                    // make a new enemy rect for enemy hit box
                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // Enemies going off screen
                    if (Canvas.GetLeft(x) < 0)
                    {
                        itemstoremove.Add(x);
                    }

                    // if the player hit box and the enemy is colliding 
                    if (playerHitBox.IntersectsWith(enemy))
                    {
                        health += 5; // add 5 to the damage
                        itemstoremove.Add(x); // remove the enemy object
                    }
                }


            }

            // if the damage integer is greater than 99
            if (health < 0)
            {
                healthText.Content = "Health: 0"; // show this on the damaged text
                healthText.Foreground = Brushes.Red; // change the text colour to 100
                MessageBox.Show(" " + score + " ");
            }

            // removing the rectangles

            // check how many rectangles are inside of the item to remove list
            foreach (Rectangle y in itemstoremove)
            {
                // remove them permanently from the canvas
                GameCanvas.Children.Remove(y);
            }


        }
    }
}
