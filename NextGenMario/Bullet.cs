using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

public class Bullet : Sprite
{
    public bool isHit;

    public Bullet(Texture2D texture) : base(texture)
    {
        type = "bullet";
        isHit = false;
    }

    public override void Update(GameTime gameTime)
    {
    }
}