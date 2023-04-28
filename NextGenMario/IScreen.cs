using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public interface IScreen{
    void Update(GameTime gameTime);

    void Draw(SpriteBatch spriteBatch, float bestTime);
}