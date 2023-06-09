using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NextGenMario;

public class LevelManager
{
    int levelNumber;
    int levelToPlay = 0;
    List<Level> levelList = new List<Level>();
    public int stageNumber = 1;
    public int orientation; //Blow toward direction of 1: Left 2: Right 3: Top 4: Down
    Random random;

    public LevelManager(List<Level> newlevelList)
    {
        // Update the level number and the level list
        levelNumber = newlevelList.Count;
        levelList = newlevelList;

        // Create a new instance of random class
        random = new Random();
        orientation = random.Next(1,101) % 4;
    }

    public void Update(GameTime gameTime, Vector2 playerPos)
    {
        if (levelList[levelToPlay].isDone)
        {
            System.Console.WriteLine("Level " + (levelToPlay + 1) + " Done!");
            levelList[levelToPlay].Reset();
            levelToPlay++;
        }
        // If finish the last stage, reset from stage one but harder
        if (levelToPlay == levelNumber)
        {
            levelToPlay = 0;
            stageNumber ++;
            orientation = random.Next(1,5);
            System.Console.WriteLine("--- Reset Stage ---");
        }
        if (levelList[levelToPlay].levelType == "WaveHorizontal")
        {
            WaveHorizontal waveHorizontal = (WaveHorizontal)levelList[levelToPlay];
            waveHorizontal.Update(gameTime);
        }
        if (levelList[levelToPlay].levelType == "WaveVertical")
        {
            WaveVertical waveVertical = (WaveVertical)levelList[levelToPlay];
            waveVertical.Update(gameTime);
        }
        if (levelList[levelToPlay].levelType == "RockManager")
        {
            RockManager rockManager = (RockManager)levelList[levelToPlay];
            rockManager.Update(gameTime);
        }
        if (levelList[levelToPlay].levelType == "BulletManager")
        {
            BulletManager bulletManager = (BulletManager)levelList[levelToPlay];
            bulletManager.Update(gameTime, playerPos);
        }
        if (levelList[levelToPlay].levelType == "WindManager")
        {
            WindManager windManager = (WindManager)levelList[levelToPlay];
            windManager.Update(gameTime, playerPos, orientation);
        }
    }

    public void ResetLevels()
    {
        levelToPlay = 0;
        stageNumber = 1;
        for (int i = 0; i < levelList.Count; i++)
        {
            levelList[i].Reset();

            if (levelList[i].levelType == "WaveHorizontal")
            {
                WaveHorizontal waveHorizontal = (WaveHorizontal)levelList[i];
                waveHorizontal.speed = 300f;
            }
            if (levelList[i].levelType == "WaveVertical")
            {
                WaveVertical waveVertical = (WaveVertical)levelList[i];
                waveVertical.speed = 200f;
            }
            if (levelList[i].levelType == "RockManager")
            {
                RockManager rockManager = (RockManager)levelList[i];
                rockManager.speed = 500f;
            }
            if (levelList[i].levelType == "BulletManager")
            {
                BulletManager bulletManager = (BulletManager)levelList[i];
                bulletManager.bulletSpeed = 500f;
            }
            if (levelList[i].levelType == "WindManager")
            {
                WindManager windManager = (WindManager)levelList[i];
                windManager.obstacleSpeed = 500f;
            }
        }
    }
}