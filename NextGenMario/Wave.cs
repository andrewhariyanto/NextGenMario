using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class Wave{

    int BLOCKHEIGHT = 700;

    public Vector2 position;
    public int speed;
    public Wall[] walls;
    public int orientation;
    public Texture2D wallTexture;
    public Vector2[] wallPositions = new Vector2[] {
        new Vector2(0, 0),
        new Vector2(0, 120),
        new Vector2(50, -200),
        new Vector2(100, -300),
        new Vector2(150, -400),
    }

    public Wave(Vector2 position, int speed, int orientation, Texture2D wallTexture){
        this.position = position;
        this.speed = speed;
        this.orientation = orientation;
        this.wallTexture = wallTexture;
    }

    private void initializeWalls(){
        walls = new Wall[]{
            new Wall(wallTexture){
                position = new Vector2(this.position.X, this.position.Y - 100),
                speed = 0,
            },
            new Wall(wallTexture){
                position = new Vector2(this.position.X + 50, this.position.Y - 200),
                speed = 0,
            }
        };
    }

    public void Update(GameTime gameTime)
    {
    }
}