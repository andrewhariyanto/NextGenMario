using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class WaveHorizontal : Level
{
    Vector2 startingPosition = new Vector2(1280, 100);
    public Vector2 position;
    public float speed;
    public Wall[] walls;
    public int orientation;
    public Texture2D wallTexture;
    public Vector2[] wallPositions = new Vector2[] {
        new Vector2(0, -1100),
        new Vector2(0, 420),
        new Vector2(100, -1150),
        new Vector2(100, 370),
        new Vector2(200, -1200),
        new Vector2(200, 320),
        new Vector2(300, -1200),
        new Vector2(300, 320),
        new Vector2(400, -1150),
        new Vector2(400, 370),
        new Vector2(500, -1100),
        new Vector2(500, 420),
    };

    public WaveHorizontal(Vector2 position, float speed, int orientation, Texture2D wallTexture)
    {
        levelType = "WaveHorizontal";
        this.position = position;
        this.speed = speed;
        this.orientation = orientation;
        this.wallTexture = wallTexture;
        initializeWalls();
    }

    private void initializeWalls()
    {
        walls = new Wall[24];
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = (new Wall(wallTexture)
            {
                position = new Vector2(this.position.X + (i / 2) * 100, this.position.Y + wallPositions[i % 12].Y),
            });
        }
    }

    public Wall[] getWalls()
    {
        return walls;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.position.X -= speed * deltaTime;
        redrawWalls();

        if (walls[walls.Length - 1].position.X < -180)
        {
            isDone = true;
            speed += 100;
        }
    }

    private void redrawWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].position.X = this.position.X + (i / 2) * 100;
        }
    }

    public override void Reset()
    {
        this.position = startingPosition;
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].position = new Vector2(this.position.X + (i / 2) * 100, this.position.Y + wallPositions[i % 12].Y);
            walls[i].isHit = false;
        }
        base.Reset();
    }
}
