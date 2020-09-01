using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldsGroupFactory
    {
        //public ShieldGroupFactory()
        //{
        //}

        public static Composite Create(float startPosX, float startPosY, GameObjectMan pGameObjectMan)
        {
            //if (shieldsCreated == false)
            //{
                pShieldRoot = (Composite)new ShieldRoot(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
                GameObjectMan.Attach(pShieldRoot);

                ShieldFactory SF = new ShieldFactory(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

                float start_x = startPosX;
                float start_y = startPosY;
                // load by column
                for (int i = 0; i < 4; i++)
                {
                    int j = 0;

                    GameObject pColumn;

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    start_x = 100.0f + i * 170;

                    float off_x = 0;
                    float brickWidth = 15.0f;
                    float brickHeight = 6.0f;

                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.LeftTop1, GameObject.Name.ShieldBrick, start_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.LeftTop0, GameObject.Name.ShieldBrick, start_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.LeftBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.RightBottom, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);

                    SF.SetParent(pShieldRoot);
                    pColumn = SF.Create(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + j++);

                    SF.SetParent(pColumn);

                    off_x += brickWidth;
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 0 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 1 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 2 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 3 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 4 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 5 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 6 * brickHeight);
                    SF.Create(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 7 * brickHeight);
                    SF.Create(ShieldCategory.Type.RightTop1, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 8 * brickHeight);
                    SF.Create(ShieldCategory.Type.RightTop0, GameObject.Name.ShieldBrick, start_x + off_x, start_y + 9 * brickHeight);
                }

            //    shieldsCreated = true;
            //}

            return pShieldRoot;
        }

        //private static bool shieldsCreated;
        private static Composite pShieldRoot;
    }
}
