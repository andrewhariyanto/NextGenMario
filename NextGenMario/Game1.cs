using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Declare window size
    const int WINDOW_WIDTH = 1280;
    const int WINDOW_HEIGHT = 720;

    // Create reference for the ball and the background 
    Texture2D ball;
    Texture2D background;

    // Create reference for the custom text font
    SpriteFont gameFont;

    // Declare player
    Player player = new Player();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
    }

    protected override void Initialize()
    {
        // Initialize the game window size
        _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load the ball and the background
        ball = Content.Load<Texture2D>("ball");
        background = Content.Load<Texture2D>("Background");

        // Load sprite font
        gameFont = Content.Load<SpriteFont>("DefaultFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Render cycle (draw order matters)
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
        _spriteBatch.Draw(ball, player.Position, Color.White);
        _spriteBatch.DrawString(gameFont, "Test Message - Sprite Font Test", new Vector2(0,0), Color.Chocolate);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
