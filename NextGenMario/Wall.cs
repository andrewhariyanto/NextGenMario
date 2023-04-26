using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class Wall : Sprite
{

    public bool isHit;

    public Wall(Texture2D texture) : base(texture)
    {
        type = "wave";
        isHit = false;
    }

    public override void Update(GameTime gameTime)
    {
        
    }

}