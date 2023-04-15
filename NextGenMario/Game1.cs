using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> _environmentSprites;
    private BulletManager bulletManager;
    Texture2D wallTexture;
    Texture2D wallTexture1;
    Texture2D wallTexture2;
    Texture2D bulletTexture;

    // Declare window size
    const int WINDOW_WIDTH = 1280;
    const int WINDOW_HEIGHT = 720;

    // Create reference for the and the background 
    Texture2D background;

    // Create reference for the custom text font
    SpriteFont gameFont;

    // Create a reference for the player
    Player player;

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

        // load the sprites
        var playerTexture = Content.Load<Texture2D>("ball");

        wallTexture = NewTexture(GraphicsDevice, 100, WINDOW_HEIGHT, Color.White);
        wallTexture1 = NewTexture(GraphicsDevice, WINDOW_WIDTH - 200, 100, Color.White);
        wallTexture2 = NewTexture(GraphicsDevice, 50, WINDOW_HEIGHT-300, Color.White);

        // All bullets texture
        List<Texture2D> bulletTextureList = new List<Texture2D>(); 
        bulletTexture = NewTexture(GraphicsDevice, 25, 25, Color.White);
        bulletTextureList.Add(bulletTexture);

        // Initialize bulletManager
        bulletManager = new BulletManager(bulletTextureList, 100);

        // Initialize the player
        player = new Player(playerTexture)
        {
            position = new Vector2(300, 500),
            color = Color.Wheat,
            speed = 500f
        };

        _environmentSprites = new List<Sprite>()
        {
            new Wall(wallTexture)
            {
                position = new Vector2(0, 0),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Wall(wallTexture)
            {
                position = new Vector2(WINDOW_WIDTH - 100, 0),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Wall(wallTexture1)
            {
                position = new Vector2(100, 0),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Wall(wallTexture1)
            {
                position = new Vector2(100, WINDOW_HEIGHT-100),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Wall(wallTexture2)
            {
                position = new Vector2(WINDOW_WIDTH - 500, 100),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Wall(wallTexture2)
            {
                position = new Vector2(WINDOW_WIDTH - 900, 200),
                color = Color.CornflowerBlue,
                speed = 0f
            }
        };

        // Add all bullets to the environment list
        

        player._environmentSprites = _environmentSprites;

        // Load the background
        background = Content.Load<Texture2D>("Background");

        // Load sprite font
        gameFont = Content.Load<SpriteFont>("DefaultFont");
    }

    protected override void UnloadContent()
    {
        base.UnloadContent();
        _spriteBatch.Dispose();
        wallTexture.Dispose();
        wallTexture1.Dispose();
        wallTexture2.Dispose();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Handle player update
        player.Update(gameTime);

        // Handle environment updates
        foreach (Sprite sprite in _environmentSprites)
            sprite.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Render cycle (draw order matters)
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

        // draw the sprites
        foreach (Sprite sprite in _environmentSprites)
        {
            sprite.Draw(_spriteBatch);
        }

        // Draw the player
        player.Draw(_spriteBatch);

        _spriteBatch.DrawString(gameFont, "Player Health: "  + player.health.ToString(), new Vector2(0, 0), Color.Chocolate);


        _spriteBatch.End();

        base.Draw(gameTime);
    }

    /// <summary>
    /// Used to create a new texture for sprite 
    /// </summary>
    private Texture2D NewTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
    {
        Texture2D newTexture;
        newTexture = new Texture2D(GraphicsDevice, width, height);
        Color[] data = new Color[width * height];
        for (int i = 0; i < data.Length; i++) data[i] = color;
        newTexture.SetData(data);

        return newTexture;
    }
}
