using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

public class StartScreen : IScreen
{

    Texture2D image;
    SpriteFont gameFont;
    
    public StartScreen(Texture2D image, SpriteFont gameFont){
        this.image = image;
        this.gameFont = gameFont;
    }

    public void Draw(SpriteBatch spriteBatch, float bestTime)
    {
        spriteBatch.Draw(image, Vector2.Zero, Color.White);
        spriteBatch.DrawString(gameFont, "Blip's Guide to Survive the Elements", new Vector2(400, 200), Color.White);
        spriteBatch.DrawString(gameFont, "Press Space to Play", new Vector2(500, 600), Color.White);
        spriteBatch.DrawString(gameFont, "Best Time: " + bestTime.ToString("n2") + "s", new Vector2(10, 10), Color.White);
    }

    public void Update(GameTime gameTime)
    {
        
    }
}