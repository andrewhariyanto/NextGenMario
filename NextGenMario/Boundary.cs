using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class Boundary : Sprite
{

    public bool isHit;

    public Boundary(Texture2D texture) : base(texture)
    {
        type = "boundary";
        isHit = false;
    }

    public override void Update(GameTime gameTime)
    {
    }
}