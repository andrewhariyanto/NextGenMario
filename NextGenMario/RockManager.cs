using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NextGenMario;

public class RockManager : Level
{

    Vector2 startingPosition = new Vector2(1280 / 2, 720 / 2);

    public Vector2 position;
    public float speed;
    public Wall[] walls;
    private bool retract = false;
    private int numberOfRetracts;
    private Vector2 newPosition;

    public Texture2D rockTexture;
    public Vector2[] wallPositions = new Vector2[] {
        new Vector2(60, -90),
        new Vector2(90-1280, 60),
        new Vector2(-60-1280, 90 - 720),
        new Vector2(-90, -60-720),
    };

    public Vector2[] startPositions = new Vector2[] {
        new Vector2(60, -60 + 300 + 120),
        new Vector2(60-1280 - 120 - 580, 60),
        new Vector2(-60-1280, 60 - 720 - 120 - 300),
        new Vector2(-60 + 120 + 580, -60-720),
    };

    private Vector2[] offsets = new Vector2[] {
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
    };

    public RockManager(Vector2 position, float speed, Texture2D rockTexture, int numberOfRetracts)
    {
        levelType = "RockManager";
        this.position = position;
        this.newPosition = position;
        this.speed = speed;
        this.rockTexture = rockTexture;
        this.numberOfRetracts = numberOfRetracts;
        initializeWalls();
    }

    private void initializeWalls()
    {
        walls = new Wall[4];
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = (new Wall(rockTexture)
            {
                position = new Vector2(this.position.X + startPositions[i].X, this.position.Y + startPositions[i].Y),
                speed = 0,
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
        checkIfMax();
        if (numberOfRetracts >= 0)
        {
            if (retract)
            {
                this.position.X += (this.newPosition.X - this.position.X) * deltaTime;
                this.position.Y += (this.newPosition.Y - this.position.Y) * deltaTime;
                Retract(deltaTime);
            }
            else
            {
                CloseIn(deltaTime);
            }
        }
        else
        {
            this.position = new Vector2(640, 360);
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].position = position + startPositions[i];
            }
            speed += 100;
            isDone = true;
        }
    }

    private void CloseIn(float deltaTime)
    {
        for (int i = 0; i < walls.Length; i += 2)
        {
            walls[i].position.Y -= (float)Math.Pow(-1, i / 2) * speed * deltaTime;
        }

        for (int i = 1; i < walls.Length; i += 2)
        {
            walls[i].position.X += ((float)Math.Pow(-1, i / 2)) * speed * deltaTime;
        }
    }

    private void Retract(float deltaTime)
    {
        for (int i = 0; i < walls.Length; i += 2)
        {
            offsets[i].Y += (float)Math.Pow(-1, i / 2) * speed * deltaTime;
            walls[i].position.Y = offsets[i].Y + this.position.Y + wallPositions[i].Y;
            walls[i].position.X = this.position.X + wallPositions[i].X;
        }

        for (int i = 1; i < walls.Length; i += 2)
        {
            offsets[i].X -= ((float)Math.Pow(-1, i / 2)) * speed * deltaTime;
            walls[i].position.X = offsets[i].X + this.position.X + wallPositions[i].X;
            walls[i].position.Y = this.position.Y + wallPositions[i].Y;
        }
    }

    private void checkIfMax()
    {
        if (retract)
        {
            if (walls[0].position.Y > this.position.Y + startPositions[0].Y && walls[1].position.X < this.position.X + startPositions[1].X && walls[2].position.Y < this.position.Y + startPositions[2].Y && walls[3].position.X > this.position.X + startPositions[3].X)
            {
                retract = false;
                numberOfRetracts--;
                foreach (Wall wall in walls)
                {
                    wall.isHit = false;
                }
            }
        }
        else
        {
            if (walls[0].position.Y < this.position.Y + wallPositions[0].Y && walls[1].position.X > this.position.X + wallPositions[1].X && walls[2].position.Y > this.position.Y + wallPositions[2].Y && walls[3].position.X < this.position.X + wallPositions[3].X)
            {
                retract = true;
                Random newRand = new Random();
                int newNumX = newRand.Next() % 640 + 320;
                int newNumY = newRand.Next() % 360 + 180;
                newPosition = new Vector2(newNumX, newNumY);
                for (int i = 0; i < walls.Length; i++)
                {
                    offsets[i] = Vector2.Zero;
                }
            }
        }
    }

    private void redrawWalls(float deltaTime)
    {
        for (int i = 0; i < walls.Length; i++)
        {
            // walls[i].position.X = this.position.X + (i/2)*100;
        }
    }

    public override void Reset()
    {
        this.position = startingPosition;
        numberOfRetracts = 6;
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].position = new Vector2(this.position.X + startPositions[i].X, this.position.Y + startPositions[i].Y);
            walls[i].isHit = false;
        }
        for(int i = 0; i < offsets.Length; i++){
            offsets[i] = Vector2.Zero;
        }
        base.Reset();
    }
}