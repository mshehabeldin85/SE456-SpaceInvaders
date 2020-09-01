using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameFactory_Base
    {

    }

    public class AlienFactory : GameFactory_Base
    {
        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);
        }

        public GameObject Create(GameObject.Name name, AlienCategory.Type type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case AlienCategory.Type.Squid:
                    pGameObj = new SquidAlien(name, GameSprite.Name.SquidOpen, posX, posY);
                    break;

                case AlienCategory.Type.Crab:
                    pGameObj = new CrabAlien(name, GameSprite.Name.CrabOpen, posX, posY);
                    break;

                case AlienCategory.Type.Octopus:
                    pGameObj = new OctopusAlien(name, GameSprite.Name.OctopusOpen, posX, posY);
                    break;

                case AlienCategory.Type.Grid:
                    pGameObj = new AlienGrid(name, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                case AlienCategory.Type.Column:
                    pGameObj = new AlienColumn(name, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the gameObjectManager
            Debug.Assert(pGameObj != null);
            //GameObjectMan.Attach(pGameObj);

            // Attached to Group
            pGameObj.ActivateGameSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pGameObj;
        }

        // Data: ---------------------
        SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pCollisionSpriteBatch;
    }
}
