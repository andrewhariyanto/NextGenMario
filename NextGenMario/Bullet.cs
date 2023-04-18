using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace NextGenMario;

public class Bullet : Sprite
{
    public bool isHit;
    public bool isFired;
    public Vector2 playerDirectionVector;
    private Rectangle screenArea = new Rectangle(0, 0, 1280, 720);

    public Bullet(Texture2D texture) : base(texture)
    {
        type = "bullet";
        isHit = false;
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
            // TO-DO: handle the consistency for speed for different shooting direction
            position.X += playerDirectionVector.X * speed * deltaTime;
            position.Y += playerDirectionVector.Y * speed * deltaTime;
        }
    }
}