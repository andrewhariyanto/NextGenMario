using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class LevelManager
{
    int levelNumber;
    int levelToPlay = 0;
    List<Level> levelList = new List<Level>();

    public LevelManager(List<Level> newlevelList)
    {
        // Update the level number and the level list
        levelNumber = newlevelList.Count;
        levelList = newlevelList;
    }

    public void Update(GameTime gameTime, Vector2 playerPos)
    {
        if (levelList[levelToPlay].isDone && levelToPlay < levelNumber - 1)
        {
            levelToPlay++;
            System.Console.WriteLine("LevelToPlay: " + levelToPlay);
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
    }

    public void ResetLevels()
    {
        levelToPlay = 0;
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
        }
    }
}