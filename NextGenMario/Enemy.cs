using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

public class Enemy : Sprite
{

    public Enemy(Texture2D texture) : base(texture)
    {
        type = "Enemy";
    }

    public override void Update(GameTime gameTime, List<Sprite> _sprites)
    {
    }
}