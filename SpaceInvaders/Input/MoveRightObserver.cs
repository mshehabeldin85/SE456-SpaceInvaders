using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveRightObserver : InputObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();
            pShip.MoveRight();
        }
    }
}
