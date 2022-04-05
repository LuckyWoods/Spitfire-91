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

namespace Spitfire
{
    public struct MoveBools
    {
        bool goUp, goDown, goLeft, goRight;

        public MoveBools(bool up, bool down, bool left, bool right)
        {
            this.goUp = up;
            this.goRight = right;
            this.goLeft = left;
            this.goDown = down;
        }

        public bool up
        { get { return this.goUp; } }

        public bool down
        { get { return this.goDown; } }
        public bool left
        { get { return this.goLeft; } }

        public bool right
        { get { return this.goRight; } }
    }
    internal class MovementControl : IObserveable<MoveBools>
    {

            private IDisposable unsubscriber;
            private string instName;

            public MovementControl(string name)
            {
                this.instName = name;
            }

            public string Name
            { get { return this.instName; } }

            public virtual void Subscribe(IObservable<MoveBools> provider)
            {
                if (provider != null)
                    unsubscriber = provider.Subscribe(this);
            }

            public virtual void OnCompleted()
            {
                Console.WriteLine("The moveObserver class has completed transmitting data to {0}.", this.Name);
                this.Unsubscribe();
            }

            public virtual void OnError(Exception e)
            {
                Console.WriteLine("{0}: Move bool observer error!");
            }

            public virtual void OnNext(MoveBools value)
            {
            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }
        }
}
