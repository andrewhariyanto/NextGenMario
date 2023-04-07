using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;
public class Sprite
{
    protected Texture2D _texture;
    public Vector2 position;
    public Vector2 velocity;
    public Color color = Color.White;
    public float speed;
    public Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height);
        }
    }

    public Sprite(Texture2D texture)
    {
        _texture = texture;
    }
    public virtual void Update(GameTime gameTime)
    {

    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, position, color);
    }

    #region Collision
    protected bool isTouchingLeft(Sprite sprite)
    {
        return this.BoundingBox.Right + this.velocity.X > sprite.BoundingBox.Left && this.BoundingBox.Left < sprite.BoundingBox.Left &&
        this.BoundingBox.Bottom > sprite.BoundingBox.Top && this.BoundingBox.Top < sprite.BoundingBox.Bottom;
    }

    protected bool isTouchingRight(Sprite sprite)
    {
        return this.BoundingBox.Left + this.velocity.X < sprite.BoundingBox.Right && this.BoundingBox.Right > sprite.BoundingBox.Right &&
        this.BoundingBox.Bottom > sprite.BoundingBox.Top && this.BoundingBox.Top < sprite.BoundingBox.Bottom;
    }

    protected bool isTouchingTop(Sprite sprite)
    {
        return this.BoundingBox.Bottom + this.velocity.Y > sprite.BoundingBox.Top && this.BoundingBox.Top < sprite.BoundingBox.Top &&
        this.BoundingBox.Right > sprite.BoundingBox.Left && this.BoundingBox.Left < sprite.BoundingBox.Right;
    }

    protected bool isTouchingBottom(Sprite sprite)
    {
        return this.BoundingBox.Top + this.velocity.Y < sprite.BoundingBox.Bottom && this.BoundingBox.Bottom > sprite.BoundingBox.Bottom &&
        this.BoundingBox.Right > sprite.BoundingBox.Left && this.BoundingBox.Left < sprite.BoundingBox.Right;
    }
    #endregion
}