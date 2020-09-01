using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class InputObserver : DLink
    {
        // define this in concrete
        abstract public void Notify();

        public InputSubject pSubject;
    }
}
