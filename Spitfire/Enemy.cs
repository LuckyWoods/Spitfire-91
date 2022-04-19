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
            public Rectangle enemyBasic = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = Brushes.Red
            };

            public int enemySpeed = 15;
        }
    }
}
