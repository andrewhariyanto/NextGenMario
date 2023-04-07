using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

enum Direction
{
    Down,
    DownLeft,
    DownRight,
    Up,
    UpLeft,
    UpRight,
    Left,
    Right
}

public class Player : Sprite
{
    private Direction direction = Direction.Right;
    private bool isMoving = false;

    public Player(Texture2D texture) : base(texture)
    {
        type = "player";
    }

    public void setX(float newX)
    {
        position.X = newX;
    }

    public void setY(float newY)
    {
        position.Y = newY;
    }

    public override void Update(GameTime gameTime, List<Sprite> _sprites)
    {
        KeyboardState kState = Keyboard.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Every frame reset the moving condition
        isMoving = false;

        // Left
        if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
        {
            direction = Direction.Left;
            isMoving = true;
            velocity.X = -speed * deltaTime;
        }

        // Right 
        if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
        {
            direction = Direction.Right;
            isMoving = true;
            velocity.X = speed * deltaTime;
        }

        // Up
        if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
        {
            direction = Direction.Up;
            velocity.Y = -speed * deltaTime;
            isMoving = true;
        }

        // Up Left
        if ((kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Left)) || (kState.IsKeyDown(Keys.W) && kState.IsKeyDown(Keys.A)))
        {
            direction = Direction.UpLeft;
            velocity.Y = -speed * deltaTime;
            velocity.X = -speed * deltaTime;
            isMoving = true;
        }

        // Up Right
        if ((kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Right)) || (kState.IsKeyDown(Keys.W) && kState.IsKeyDown(Keys.D)))
        {
            direction = Direction.UpRight;
            velocity.Y = -speed * deltaTime;
            velocity.X = speed * deltaTime;
            isMoving = true;
        }

        // Down
        if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
        {
            direction = Direction.Down;
            isMoving = true;
            velocity.Y = speed * deltaTime;
        }

        // Down Left
        if ((kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Left)) || (kState.IsKeyDown(Keys.S) && kState.IsKeyDown(Keys.A)))
        {
            direction = Direction.DownLeft;
            velocity.Y = speed * deltaTime;
            velocity.X = -speed * deltaTime;
            isMoving = true;
        }

        // Down Right
        if ((kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Right)) || (kState.IsKeyDown(Keys.S) && kState.IsKeyDown(Keys.D)))
        {
            direction = Direction.DownRight;
            velocity.Y = speed * deltaTime;
            velocity.X = speed * deltaTime;
            isMoving = true;
        }

        foreach(var sprite in _sprites){
            if(sprite == this)
                continue;
            
            if((this.velocity.X > 0 && this.isTouchingLeft(sprite)) || (this.velocity.X < 0 && this.isTouchingRight(sprite)))
                this.velocity.X = 0;
            if((this.velocity.Y > 0 && this.isTouchingTop(sprite)) || (this.velocity.Y < 0 && this.isTouchingBottom(sprite)))
                this.velocity.Y = 0;
        }

        if (isMoving)
        {
            switch (direction)
            {
                case Direction.Left:
                    position.X += velocity.X;
                    break;

                case Direction.Right:
                    position.X += velocity.X;
                    break;

                case Direction.Up:
                    position.Y += velocity.Y;
                    break;

                case Direction.UpLeft:
                    position.Y += velocity.Y * 0.7f;
                    position.X += velocity.X * 0.7f;
                    break;

                case Direction.UpRight:
                    position.Y += velocity.Y * 0.7f;
                    position.X += velocity.X * 0.7f;
                    break;

                case Direction.Down:
                    position.Y += velocity.Y;
                    break;

                case Direction.DownLeft:
                    position.Y += velocity.Y * 0.7f;
                    position.X += velocity.X * 0.7f;
                    break;

                case Direction.DownRight:
                    position.Y += velocity.Y * 0.7f;
                    position.X += velocity.X * 0.7f;
                    break;
            }
        }
        
        velocity = Vector2.Zero;
    }
}