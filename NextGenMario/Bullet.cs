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
    private Vector2 playerDirectionVector;
    private TimeSpan totalGameTime = TimeSpan.Zero;

    public Bullet(Texture2D texture) : base(texture)
    {
        type = "bullet";
        isHit = false;
        isFired = false;
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Add the elapsed time to the total game time
        totalGameTime += gameTime.ElapsedGameTime;

        if (totalGameTime.TotalSeconds > 3) // TO-DO: Check if the position is out of the camera view instead
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