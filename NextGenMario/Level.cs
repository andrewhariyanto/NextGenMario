using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NextGenMario;

// Just a skeleton class for now
public class Level
{
    public string levelType;
    public bool isDone = false;

    public virtual void Reset(){
        isDone = false;
    }
}