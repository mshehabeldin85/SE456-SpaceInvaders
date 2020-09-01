using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Component : ColVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Print();
        public abstract void Move();
        public abstract Component GetFirstChild();
        public abstract void DumpNode();

        // Data: ---------------------
        public Component pParent = null;
        public Component pReverse = null;
        public bool lastChild = false;
        public Container holder = Container.Unknown;
    }
}
