﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;

namespace NextGenMario;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Sprite> _environmentSprites;
    Texture2D enemyTexture;
    Texture2D enemyTexture1;

    // Declare window size
    const int WINDOW_WIDTH = 1280;
    const int WINDOW_HEIGHT = 720;

    // Create reference for the and the background 
    Texture2D background;

    // Create reference for the custom text font
    SpriteFont gameFont;

    // Declare camera and hold a reference for the player
    Camera camera;
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

        // Initialize the camera
        camera = new Camera(_graphics.GraphicsDevice);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // load the sprites
        var playerTexture = Content.Load<Texture2D>("ball");

        enemyTexture = NewTexture(GraphicsDevice, 100, WINDOW_HEIGHT, Color.White);
        enemyTexture1 = NewTexture(GraphicsDevice, WINDOW_WIDTH-200, 100, Color.White);

        // Initialize the player
        player = new Player(playerTexture)
            {
                position = new Vector2(300, 500),
                color = Color.Wheat,
                speed = 300f,
            };

        _environmentSprites = new List<Sprite>()
        {
            new Enemy(enemyTexture)
            {
                position = new Vector2(0, 0),
                color = Color.CornflowerBlue,
                speed = 0f,
            },
            new Enemy(enemyTexture)
            {
                position = new Vector2(WINDOW_WIDTH - 100, 0),
                color = Color.CornflowerBlue,
                speed = 0f,
            },
            new Enemy(enemyTexture1)
            {
                position = new Vector2(100, 0),
                color = Color.CornflowerBlue,
                speed = 0f,
            },
            new Enemy(enemyTexture1)
            {
                position = new Vector2(100, WINDOW_HEIGHT-100),
                color = Color.CornflowerBlue,
                speed = 0f,
            },
        };

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
        enemyTexture.Dispose();
        enemyTexture1.Dispose();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Handle player update
        player.Update(gameTime);

        foreach( Sprite sprite in _environmentSprites)
            sprite.Update(gameTime);

        // Handle camera update (camera follows the player)
        camera.Position = player.position;
        camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Render cycle (draw order matters)
        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

        // Draw the player
        player.Draw(_spriteBatch);

        // draw the sprites
        foreach(Sprite sprite in _environmentSprites){
            sprite.Draw(_spriteBatch);
        }
        
        _spriteBatch.DrawString(gameFont, "Test Message - Sprite Font Test", new Vector2(0,0), Color.Chocolate);

        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private Texture2D NewTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
    {
        Texture2D newTexture;
        newTexture = new Texture2D(GraphicsDevice, width, height);
        Color[] data = new Color[width * height];
        for(int i = 0; i < data.Length; i++) data[i] = color;
        newTexture.SetData(data);

        return newTexture;
    }
}
