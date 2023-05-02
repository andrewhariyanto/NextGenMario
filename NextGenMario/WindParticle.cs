using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace NextGenMario;

public class WindParticle : Sprite
{
    public bool isFired;
    private Rectangle screenArea = new Rectangle(0, 0, 1280, 720);
    public int orientation = 1;

    public WindParticle(Texture2D texture) : base(texture)
    {
        type = "windParticle";
        isFired = false;
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!this.BoundingBox.Intersects(screenArea))
        {
            isFired = false;
        }

        if (isFired)
        {
            if (orientation == 1)
            {
                position.X += -1 * speed * deltaTime;
            }
            if (orientation == 2)
            {
                position.X += 1 * speed * deltaTime;
            }
            if (orientation == 3)
            {
                position.Y += 1 * speed * deltaTime;
            }
            if (orientation == 4)
            {
                position.Y += -1 * speed * deltaTime;
            }
        }
    }
}