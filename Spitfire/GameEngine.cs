﻿using System;
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
using System.Media;

namespace Spitfire
{
    internal class GameEngine
    {
        Random rng = new Random(); // RNG Values

        Rectangle player;
        int playerSpeed;
        int bulletSpeed = 25;

        int health;
        int score;

        List<Rectangle> itemstoremove = new List<Rectangle>(); // Remove List

        // Audio
        private SoundPlayer bulletSound = new SoundPlayer(Properties.Resources.bulletSound);

        public GameEngine(Rectangle player, int playerSpeed, int health, int score)
        {
            this.player = player;
            this.playerSpeed = playerSpeed;
            this.health = health;
            this.score = score;
         }

        public void playerMove(bool goUp, bool goDown, bool goRight, bool goLeft)
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

        public void playerFire(Canvas canvas)
        {
            // Bullet object
            Rectangle bullet = new Rectangle
            {
                Tag = "bullet",
                Height = 5,
                Width = 20,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Gold
            };
           
                Canvas.SetTop(bullet, Canvas.GetTop(player) + (bullet.Height * 3)); // place the bullet on top of the player location
                Canvas.SetLeft(bullet, Canvas.GetLeft(player) + player.Height); // place the bullet middle of the player image
                canvas.Children.Add(bullet); // add the bullet to the screen
                
                // Play Bullet firing sound
                //bulletSound.Load();
                //bulletSound.Play();
        }

        Enemy.EnemyBasic eb = new Enemy.EnemyBasic();
        public void spawnEnemy(Canvas canvas)
        {
            Rectangle e = eb.enemyBasic; // Need to make a new Rectangle each run to avoid errors

            int randPos = rng.Next(0, 720); // Random Y position for enemy to spawn

            Canvas.SetTop(e, Canvas.GetTop(canvas) - randPos); // place the bullet on top of the player location
            Canvas.SetLeft(e, Canvas.GetRight(canvas)); // place enemy on right side of the screen
            canvas.Children.Add(e);
        }

        public void HitDetection(Canvas canvas)
        {
            foreach (var x in canvas.Children.OfType<Rectangle>())
            {
                // if any rectangle has the tag bullet in it
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + bulletSpeed); // move bullet right
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // make a rect class with bullet properties


                    if (Canvas.GetRight(x) > canvas.Width)
                    {
                        itemstoremove.Add(x); // if it has then add it to the item to remove list
                    }


                    foreach (var y in canvas.Children.OfType<Rectangle>()) // run another for each loop inside of the main loop
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
                    Canvas.SetRight(x, Canvas.GetRight(x) + 10); // Moves enemy right

                    // make a new enemy rect for enemy hit box
                    Rect enemy = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // Enemies going off screen 
                    if (Canvas.GetLeft(x) < 0)
                    {
                        itemstoremove.Add(x);
                    }

                    /*
                    // if the player hit box and the enemy is colliding 
                    if (playerHitBox.IntersectsWith(enemy))
                    {
                        health += 5; // add 5 to the damage
                        itemstoremove.Add(x); // remove the enemy object
                    } */
                }

            foreach (Rectangle y in itemstoremove)
            {
                // remove them permanently from the canvas
                canvas.Children.Remove(y);
            }
            }
        }
    }
}
