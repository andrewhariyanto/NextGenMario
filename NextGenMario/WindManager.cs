using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NextGenMario;

public class WindManager : Level
{
    public Queue<WindObstacle> windObstaclesQ = new Queue<WindObstacle>();
    public Queue<WindParticle> windParticlesQ = new Queue<WindParticle>();

    public List<Vector2> spawnPoints = new List<Vector2>();
    private float timer = 0.0f;
    private float waitTime = 0.5f;
    private float survival_Timer = 0.0f;
    public float obstacleSpeed;
    Player player;
    bool isSet = false;



    public WindManager(List<Texture2D> textures, int obstacleCount, Player newplayer, int particleCount)
    {
        levelType = "WindManager";

        obstacleSpeed = 500f;

        player = newplayer;

        // Create a new instance of random class
        Random random = new Random();

        // Create obstacles to fill the obstacle queue
        for (int i = 0; i < obstacleCount; i++)
        {
            // Get a random texture index
            int randomIndex = random.Next(1, 101) % textures.Count;

            // Create the obstacle
            WindObstacle newBullet = new WindObstacle(textures[randomIndex])
            {
                position = new Vector2(-100, -100),
                color = Color.White,
                isHit = false,
                speed = 0
            };

            windObstaclesQ.Enqueue(newBullet);
        }

        // Particles
        for (int i = 0; i < particleCount; i++)
        {
            // Get a random texture index
            int randomIndex = random.Next(1, 101) % textures.Count;

            // Create the obstacle
            WindParticle particle = new WindParticle(textures[randomIndex])
            {
                position = new Vector2(-10, -10),
                color = Color.BlueViolet,
                speed = 0
            };

            windParticlesQ.Enqueue(particle);
        }
        // Initialize the spawn points
        for (int i = 0; i <= 1200; i += 120)
        {
            for (int j = 0; j <= 650; j += 65)
            {
                if ((i > 0 && i < 1200) && (j > 0 && j < 650))
                {
                    continue;
                }
                else
                {
                    Vector2 spawnPoint = new Vector2(i, j);
                    spawnPoints.Add(spawnPoint);
                }
            }
        }
    }

    //Orientation: Blow toward direction of 1: Left 2: Right 3: Top 4: Down
    public WindObstacle PullObstacleFromQueue(int orientation)
    {
        WindObstacle obstacleToPull = windObstaclesQ.Dequeue();

        // Create a new instance of random class
        Random random = new Random();

        // Get a random position index
        int randomIndex = 0;
        if (orientation == 1) // Left
        {
            randomIndex = random.Next(29, 40);
        }
        if (orientation == 2) // Right
        {
            randomIndex = random.Next(0, 11);
        }
        if (orientation == 3) // Top
        {
            randomIndex = random.Next((29 - 11) / 2 + 1) * 2 + 11;
        }
        if (orientation == 4) // Down
        {
            randomIndex = random.Next((29 - 11) / 2 + 1) * 2 + 11 + 1;
        }
        // Prepare each obstacle
        obstacleToPull.position = spawnPoints[randomIndex];
        obstacleToPull.speed = obstacleSpeed;
        obstacleToPull.isHit = false;

        // Put back to queue
        windObstaclesQ.Enqueue(obstacleToPull);

        return obstacleToPull;
    }

    public List<WindParticle> createParticleList(int orientation)
    {
        List<WindParticle> windParticles = new List<WindParticle>();

        if (orientation == 1) // Left
        {
            for (int i = 29; i < 40; i++)
            {
                WindParticle particle = windParticlesQ.Dequeue();
                particle.position = spawnPoints[i];
                particle.speed = obstacleSpeed + 300;

                windParticles.Add(particle);
                windParticlesQ.Enqueue(particle);
            }
            
        }
        if (orientation == 2) // Right
        {
            for (int i = 0; i < 11; i++)
            {
                WindParticle particle = windParticlesQ.Dequeue();
                particle.position = spawnPoints[i];
                particle.speed = obstacleSpeed + 300;

                windParticles.Add(particle);
                windParticlesQ.Enqueue(particle);

            }
        }
        if (orientation == 3) // Top
        {
            for (int i = 11; i < 29; i+=2)
            {
                WindParticle particle = windParticlesQ.Dequeue();
                particle.position = spawnPoints[i];
                particle.speed = obstacleSpeed + 300;

                windParticles.Add(particle);
                windParticlesQ.Enqueue(particle);

            }
        }
        if (orientation == 4) // Down
        {
            for (int i = 12; i < 29; i+=2)
            {
                WindParticle particle = windParticlesQ.Dequeue();
                particle.position = spawnPoints[i];
                particle.speed = obstacleSpeed + 300;

                windParticles.Add(particle);
                windParticlesQ.Enqueue(particle);

            }
        }


        return windParticles;
    }

    public void Update(GameTime gameTime, Vector2 playerPos, int orientation)
    {
        if (!isSet)
        {
            player.isWinded = true;
            player.orientation = orientation;
            isSet = true;
        }
        
        // Add the elapsed time since the last frame to the timer
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Add the survival timer for gameplay
        survival_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (survival_Timer > 20f)
        {
            isDone = true;
            player.isWinded = false;
            obstacleSpeed += 50f;
        }

        if (timer >= waitTime && survival_Timer <= 18f)
        {
            // Reset timer
            timer = 0.0f;

            WindObstacle obstacle = PullObstacleFromQueue(orientation);
            obstacle.playerDirectionVector = Vector2.Normalize(playerPos - obstacle.position);
            obstacle.isFired = true;

            // List<WindParticle> windParticles = createParticleList(orientation);
            // foreach (WindParticle particle in windParticles)
            // {
            //     particle.isFired = true;
            //     particle.orientation = orientation;
            // }
        }
    }

    public override void Reset()
    {
        survival_Timer = 0.0f;
        foreach (WindObstacle obstacle in windObstaclesQ)
        {
            obstacle.position = new Vector2(-100, -100);
        }
        isSet = false;
        player.isWinded = false;
        base.Reset();
    }
}