using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Key_B_Observer : InputObserver
    {
        public override void Notify()
        {
            // ToDo: Toggle boxes => Set Flag show to true or false
            //Debug.WriteLine("Key B pressed");

            System.Threading.Thread.Sleep(85);
            SpriteBatchMan.ToggleView(SpriteBatch.Name.Boxes);
        }
    }
}
