using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReCreateShipEvent : Command
    {
        public ReCreateShipEvent()
        {
            
        }

        override public void Execute(float deltaTime)
        {
            ShipMan.Create();
        }
    }
}
