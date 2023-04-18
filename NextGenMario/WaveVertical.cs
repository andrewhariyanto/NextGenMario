using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class WaveVertical{

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
    }

    public void Draw(SpriteBatch spriteBatch){
        foreach(Wall wall in walls){
            spriteBatch.Draw(wallTexture, wall.position, null, Color.White, (float) MathHelper.PiOver2, new Vector2((float)wall.BoundingBox.Width, (float)wall.BoundingBox.Height), new Vector2(1, 1), SpriteEffects.None, 1);
        }
    }

    private void redrawWalls(){
        for(int i = 0; i < walls.Length; i++){
            walls[i].position.Y = this.position.Y + (i/2)*100;
        }
    }
}