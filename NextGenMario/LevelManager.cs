using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

public class LevelManager
{
    int levelNumber = 0;
    int levelToPlay = 0;
    List<Level> levelList = new List<Level>();

    public LevelManager(List<Level> newlevelList) 
    {
        // Update the level number and the level list
        levelNumber = levelList.Count;
        levelList = newlevelList;

    }

    public void Update(GameTime gameTime, Vector2 playerPos)
    {
        if (levelList[levelToPlay].levelType == "Wave")
        {
            Wave wave = (Wave)levelList[levelToPlay];
            wave.Update(gameTime);
        }
        if (levelList[levelToPlay].levelType == "BulletManager")
        {
            BulletManager bulletManager = (BulletManager)levelList[levelToPlay];
            bulletManager.Update(gameTime, playerPos);
        }

    }
}