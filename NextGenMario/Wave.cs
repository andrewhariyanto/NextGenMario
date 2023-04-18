using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class Wave : Level
{
    int BLOCKHEIGHT = 700;

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

    public Wave(Vector2 position, float speed, int orientation, Texture2D wallTexture){
        this.position = position;
        this.speed = speed;
        this.orientation = orientation;
        this.wallTexture = wallTexture;
        levelType = "Wave";
        initializeWalls();
    }

    private void initializeWalls(){
        walls = new Wall[40];
        for(int i = 0; i < walls.Length; i++){
            walls[i] = (new Wall(wallTexture){
                position = new Vector2(this.position.X + (i/2)*100, this.position.Y + wallPositions[i%12].Y),
                speed = 0,
            });
        }
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.position.X -= speed * deltaTime;
        redrawWalls();
    }

    public void Draw(SpriteBatch spriteBatch){
        foreach(Wall wall in walls){
            spriteBatch.Draw(wallTexture, wall.position, Color.White);
        }
    }

    private void redrawWalls(){
        for(int i = 0; i < walls.Length; i++){
            walls[i].position.X = this.position.X + (i/2)*100;
        }
    }
}