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
    private WindManager windManager;
    private LevelManager levelManager;
    private float timer = 0;
    Texture2D wallTexture;
    Texture2D wallTexture1;
    Texture2D bulletTexture;
    Texture2D rockTexture;
    Texture2D obstacleTexture;

    // Declare window size
    const int WINDOW_WIDTH = 1280;
    const int WINDOW_HEIGHT = 720;

    // Create reference for the and the background 
    Texture2D background;

    // Create reference for the custom text font
    SpriteFont gameFont;

    // Create a reference for the player
    Player player;

    // Create reference for Wave
    WaveHorizontal waveHorizontal;
    WaveVertical waveVertical;

    // Reference for Rock Manager
    private RockManager rockManager;

    // titlescreen
    private IScreen startScreen;
    private float bestTime = 0f;

    public bool isPlaying = false;

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
        var playerTexture = Content.Load<Texture2D>("frame1");
        var wallTextureWave = Content.Load<Texture2D>("wall");
        var wallTextureWave_Vertical = Content.Load<Texture2D>("wallVertical");

        wallTexture = NewTexture(GraphicsDevice, 100, WINDOW_HEIGHT, Color.White);
        wallTexture1 = NewTexture(GraphicsDevice, WINDOW_WIDTH - 200, 100, Color.White);

        rockTexture = Content.Load<Texture2D>("rock");

        // All bullets texture
        List<Texture2D> bulletTextureList = new List<Texture2D>();
        bulletTexture = NewTexture(GraphicsDevice, 25, 25, Color.White);
        bulletTextureList.Add(bulletTexture);

        // Initialize bulletManager
        bulletManager = new BulletManager(bulletTextureList, 100);

        // All wind obstacles texture
        List<Texture2D> obstacleTextureList = new List<Texture2D>();
        obstacleTexture = NewTexture(GraphicsDevice, 25, 25, Color.White);
        obstacleTextureList.Add(obstacleTexture);

        

        // Initialize the player
        player = new Player(playerTexture)
        {
            position = new Vector2(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2),
            color = Color.Wheat,
            speed = 300f
        };

        // Initialize windObstacle
        windManager = new WindManager(obstacleTextureList, 100, player, 200);

        // Initialize the wave
        waveHorizontal = new WaveHorizontal(new Vector2(WINDOW_WIDTH, 100), 300f, 0, wallTextureWave);
        waveVertical = new WaveVertical(new Vector2(0, -1000), 200f, 0, wallTextureWave_Vertical);

        // Initialize rock
        rockManager = new RockManager(new Vector2(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2), 500, rockTexture, 6);

        // Initialize Level Manager
        List<Level> levels = new List<Level>();
        levels.Add(waveHorizontal);
        levels.Add(waveVertical);
        levels.Add(bulletManager);
        levels.Add(windManager);
        levels.Add(rockManager);
        levelManager = new LevelManager(levels);

        _environmentSprites = new List<Sprite>()
        {
            new Boundary(wallTexture)
            {
                position = new Vector2(0, 0),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Boundary(wallTexture)
            {
                position = new Vector2(WINDOW_WIDTH - 100, 0),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Boundary(wallTexture1)
            {
                position = new Vector2(100, 0),
                color = Color.CornflowerBlue,
                speed = 0f
            },
            new Boundary(wallTexture1)
            {
                position = new Vector2(100, WINDOW_HEIGHT-100),
                color = Color.CornflowerBlue,
                speed = 0f
            }
        };

        // Add all bullets to the environment list
        foreach (Sprite bullet in bulletManager.bulletQ)
        {
            _environmentSprites.Add(bullet);
        }

        // Add all obstalces to the environment list
        foreach (Sprite obstacle in windManager.windObstaclesQ)
        {
            _environmentSprites.Add(obstacle);
        }

        // Add all obstalces to the environment list
        foreach (Sprite particle in windManager.windParticlesQ)
        {
            _environmentSprites.Add(particle);
        }

        // add all horizontal waves to environment list
        foreach (Sprite wave in waveHorizontal.getWalls())
        {
            _environmentSprites.Add(wave);
        }

        foreach (Sprite wave in waveVertical.getWalls())
        {
            _environmentSprites.Add(wave);
        }

        foreach (Sprite wall in rockManager.getWalls())
        {
            _environmentSprites.Add(wall);
        }

        player._environmentSprites = _environmentSprites;

        // Load the background
        background = Content.Load<Texture2D>("Background");

        // Load sprite font
        gameFont = Content.Load<SpriteFont>("DefaultFont");


        var startScreenBackground = Content.Load<Texture2D>("titlescreen");
        // load start screen
        startScreen = new StartScreen(startScreenBackground, gameFont);
    }

    protected override void UnloadContent()
    {
        base.UnloadContent();
        _spriteBatch.Dispose();
        wallTexture.Dispose();
        wallTexture1.Dispose();
        bulletTexture.Dispose();
    }

    protected override void Update(GameTime gameTime)
    {

        if (isPlaying)
        {
            // Add the elapsed time since the last frame to the timer
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Handle levels
            levelManager.Update(gameTime, new Vector2(player.position.X + player.BoundingBox.Width / 2, player.position.Y + player.BoundingBox.Height / 2));

            // Handle environment updates
            foreach (Sprite sprite in _environmentSprites)
                sprite.Update(gameTime);

            // Handle player update
            player.Update(gameTime);

            if (player.health <= 0)
            {
                if(timer > bestTime){
                    bestTime = timer;
                }
                isPlaying = false;
            }
        }
        else
        {
            startScreen.Update(gameTime);
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                isPlaying = true;
                ResetGame();
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Render cycle (draw order matters)
        _spriteBatch.Begin();

        if (isPlaying)
        {
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            // draw the sprites
            foreach (Sprite sprite in _environmentSprites)
            {
                sprite.Draw(_spriteBatch);
            }

            // Draw the player
            player.Draw(_spriteBatch);


            _spriteBatch.DrawString(gameFont, "Player Health: " + player.health.ToString(), new Vector2(0, 0), Color.Chocolate);
            _spriteBatch.DrawString(gameFont, "Timer: " + timer.ToString("0.#"), new Vector2(WINDOW_WIDTH / 2 - 50, 0), Color.Chocolate);
            _spriteBatch.DrawString(gameFont, "Stage: " + levelManager.stageNumber, new Vector2(WINDOW_WIDTH - 150, 0), Color.Chocolate);
        }
        else
        {
            startScreen.Draw(_spriteBatch, bestTime);
        }



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

    private void ResetGame()
    {
        player.position = new Vector2(WINDOW_WIDTH/2, WINDOW_HEIGHT/2);
        player.health = 100;
        timer = 0;
        levelManager.ResetLevels();
    }
}
