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

public class Player
{
    private Vector2 position = new Vector2(500, 300);
    private int speed = 300;
    private Direction direction = Direction.Right;
    private bool isMoving = false;

    public Vector2 Position
    {
        get { return position; }
    }

    public void setX(float newX)
    {
        position.X = newX;
    }

    public void setY(float newY)
    {
        position.Y = newY;
    }

    public void Update(GameTime gameTime)
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
        }

        // Right 
        if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
        {
            direction = Direction.Right;
            isMoving = true;
        }

        // Up
        if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
        {
            direction = Direction.Up;
            isMoving = true;
        }

        // Up Left
        if ((kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Left)) || (kState.IsKeyDown(Keys.W) && kState.IsKeyDown(Keys.A)))
        {
            direction = Direction.UpLeft;
            isMoving = true;
        }

        // Up Right
        if ((kState.IsKeyDown(Keys.Up) && kState.IsKeyDown(Keys.Right)) || (kState.IsKeyDown(Keys.W) && kState.IsKeyDown(Keys.D)))
        {
            direction = Direction.UpRight;
            isMoving = true;
        }

        // Down
        if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
        {
            direction = Direction.Down;
            isMoving = true;
        }

        // Down Left
        if ((kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Left)) || (kState.IsKeyDown(Keys.S) && kState.IsKeyDown(Keys.A)))
        {
            direction = Direction.DownLeft;
            isMoving = true;
        }

        // Down Right
        if ((kState.IsKeyDown(Keys.Down) && kState.IsKeyDown(Keys.Right)) || (kState.IsKeyDown(Keys.S) && kState.IsKeyDown(Keys.D)))
        {
            direction = Direction.DownRight;
            isMoving = true;
        }

        if (isMoving)
        {
            switch (direction)
            {
                case Direction.Left:
                    position.X -= speed * deltaTime;
                    break;

                case Direction.Right:
                    position.X += speed * deltaTime;
                    break;

                case Direction.Up:
                    position.Y -= speed * deltaTime;
                    break;

                case Direction.UpLeft:
                    position.Y -= speed * 0.7f * deltaTime;
                    position.X -= speed * 0.7f * deltaTime;
                    break;

                case Direction.UpRight:
                    position.Y -= speed * 0.7f * deltaTime;
                    position.X += speed * 0.7f * deltaTime;
                    break;

                case Direction.Down:
                    position.Y += speed * deltaTime;
                    break;

                case Direction.DownLeft:
                    position.Y += speed * 0.7f * deltaTime;
                    position.X -= speed * 0.7f * deltaTime;
                    break;

                case Direction.DownRight:
                    position.Y += speed * 0.7f * deltaTime;
                    position.X += speed * 0.7f * deltaTime;
                    break;
            }
        }
    }
}