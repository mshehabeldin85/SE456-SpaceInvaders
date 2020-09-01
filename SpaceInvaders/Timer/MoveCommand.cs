using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveCommand : Command
    {
        public MoveCommand(GameObject grid)
        {
            //this.pString = txt;
            this.pGrid = (AlienGrid)grid;
        }

        public override void Execute(float deltaTime)
        {
            this.pGrid.MoveGrid();

            // Add itself back to timer
            TimerMan.Add(TimeEvent.Name.MoveGrid, this, deltaTime);
        }

        private AlienGrid pGrid;
    }
}
