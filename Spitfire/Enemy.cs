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
using System.Media;

namespace Spitfire
{
    internal class Enemy
    {
        public class EnemyBasic
        {
            public void spawnEnemy(Canvas canvas)
            {
                Rectangle enemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 50,
                    Width = 56,
                    Fill = Brushes.Red
                };

                Random rng = new Random();
                int randPos = rng.Next(0, 720); // Random Y position for enemy to spawn

                Canvas.SetTop(enemy, Canvas.GetTop(canvas) + randPos); // place the bullet on top of the player location
                Canvas.SetLeft(enemy, Canvas.GetRight(canvas)); // place enemy on right side of the screen
                canvas.Children.Add(enemy);
            }

        }
    }
}
