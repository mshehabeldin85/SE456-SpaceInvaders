using System;

namespace SpaceInvaders
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            // temporary location --> move this
            Squid,
            Crab,
            Octopus,

            Grid,
            Column,

            Missile,
            MissileGroup
        }

        public AlienCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {

        }

        public int scoreValue;
    }
}
