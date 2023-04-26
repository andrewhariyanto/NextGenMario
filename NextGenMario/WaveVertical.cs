using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class WaveVertical : Level {

    int BLOCKHEIGHT = 1400;

    public Vector2 position;
    public float speed;
    public Wall[] walls;
    public int orientation;
    public Texture2D wallTexture;
    public Vector2[] wallPositions = new Vector2[] {
        new Vector2(-820, 0),
        new Vector2(700, 0),
        new Vector2(-870, 0),
        new Vector2(650, 0),
        new Vector2(-920, 0),
        new Vector2(600, 0),
        new Vector2(-920, 0),
        new Vector2(600, 0),
        new Vector2(-870, 0),
        new Vector2(650, 0),
        new Vector2(-820, 0),
        new Vector2(700, 0),
    };

    public WaveVertical(Vector2 position, float speed, int orientation, Texture2D wallTexture){
        levelType = "WaveVertical";
        this.position = position;
        this.speed = speed;
        this.orientation = orientation;
        this.wallTexture = wallTexture;
        initializeWalls();
    }

    private void initializeWalls(){
        walls = new Wall[24];
        for(int i = 0; i < walls.Length; i++){
            walls[i] = (new Wall(wallTexture){
                position = new Vector2(this.position.X + wallPositions[i%12].X, this.position.Y - (i/2)*100),
                speed = 0,
            });
        }
    }

    public Wall[] getWalls(){
        return walls;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.position.Y += speed * deltaTime;
        redrawWalls();

        if (walls[0].position.Y > 900)
        {
            isDone = true;
        }
    }

    private void redrawWalls(){
        for(int i = 0; i < walls.Length; i++){
            walls[i].position.Y = this.position.Y + (i/2)*100;
        }
    }
}