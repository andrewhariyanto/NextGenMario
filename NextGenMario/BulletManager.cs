using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NextGenMario;

public class BulletManager : Level
{
    public Queue<Bullet> bulletQ = new Queue<Bullet>();
    public List<Vector2> spawnPoints = new List<Vector2>();
    private float timer = 0.0f;
    private float waitTime = 0.5f;
    private float survival_Timer = 0.0f;


    public BulletManager(List<Texture2D> textures, int bulletCount)
    {
        levelType = "BulletManager";

        // Create a new instance of random class
        Random random = new Random();

        // Create bullets to fill the bullet queue
        for (int i = 0; i < bulletCount; i++)
        {
            // Get a random texture index
            int randomIndex = random.Next(1, 101) % textures.Count;

            // Create the bullet
            Bullet newBullet = new Bullet(textures[randomIndex])
            {
                position = new Vector2(-100, -100),
                color = Color.Orange,
                isHit = false,
                speed = 0
            };

            bulletQ.Enqueue(newBullet);
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

    public Bullet FireBulletFromQueue()
    {
        Bullet bulletToFire = bulletQ.Dequeue();

        // Create a new instance of random class
        Random random = new Random();

        // Get a random position index
        int randomIndex = random.Next(1, 101) % spawnPoints.Count;

        // Prepare each bullet
        bulletToFire.position = spawnPoints[randomIndex];
        bulletToFire.speed = 500;
        bulletToFire.isHit = false;

        // Put back to queue
        bulletQ.Enqueue(bulletToFire);

        return bulletToFire;
    }

    public void Update(GameTime gameTime, Vector2 playerPos)
    {
        // Add the elapsed time since the last frame to the timer
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Add the survival timer for gameplay
        survival_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (survival_Timer > 20f)
        {
            isDone = true;
        }

        if (timer >= waitTime && survival_Timer <= 17f)
        {
            // Reset timer
            timer = 0.0f;

            Bullet bulletToFire = FireBulletFromQueue();
            bulletToFire.playerDirectionVector = Vector2.Normalize(playerPos - bulletToFire.position);
            bulletToFire.isFired = true;
        }
    }

    public override void Reset()
    {
        
    }
}