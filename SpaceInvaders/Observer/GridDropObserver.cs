using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GridDropObserver : ColObserver
    {
        public GridDropObserver()
        {
            this.pGrid = null;
        }

        public override void Notify()
        {
            this.pGrid = (AlienGrid)GameObjectMan.Find(GameObject.Name.AlienGrid);
            this.pGrid.DropGrid();
        }

        private AlienGrid pGrid;
    }
}
