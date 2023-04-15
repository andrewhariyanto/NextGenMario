using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;

namespace NextGenMario;

public class BulletManager
{
    public Queue<Bullet> bulletQ;
    public List<Vector2> spawnPoints;
    private int fireCooldown = 30;
    private int frameCounter = 0;

    public BulletManager(List<Texture2D> textures, int bulletCount)
    {
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
        for (int i = 0; i <= 1280; i += 128)
        {
            for (int j = 0; j <= 720; j += 72)
            {
                if ((i > 0 && i < 1280) && (j > 0 && j < 720))
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
        bulletToFire.speed = 1000;

        return bulletToFire;
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        frameCounter ++;

        if (frameCounter % fireCooldown == 0)
        {
            Bullet bulletToFire = FireBulletFromQueue();
            bulletQ.Enqueue(bulletToFire);
            bulletToFire.isFired = true;
        }
    }
}