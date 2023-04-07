using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

enum MovementState
{
    Crouch,
    Jump,
    MoveLeft,
    MoveRight,
    Idle
}

public class Player
{
    private Vector2 position = new Vector2(500, 300);
    private int speed = 300;
    private MovementState movement = MovementState.Idle;
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

        if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
        {
            movement = MovementState.MoveRight;
            isMoving = true;
        }

        if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
        {
            movement = MovementState.MoveLeft;
            isMoving = true;
        }

        if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Space))
        {
            movement = MovementState.Jump;
            isMoving = true;
        }

        if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
        {
            movement = MovementState.Crouch;
            isMoving = true;
        }

        if (isMoving)
        {
            switch (movement)
            {
                case MovementState.MoveRight:
                    position.X += speed * deltaTime;
                    break;

                case MovementState.MoveLeft:
                    position.X -= speed * deltaTime;
                    break;

                case MovementState.Crouch:
                    // This is not what we want, but for testing the player will just go down so that we can move around first
                    position.Y += speed * deltaTime;
                    break;

                case MovementState.Jump:
                    position.Y -= speed * deltaTime;
                    break;
            }
        }
    }
}