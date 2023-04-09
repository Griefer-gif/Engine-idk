using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineIdk.EngineIdk
{
    public class Shape2D
    {

        public Vector2 position = null;
        public Vector2 scale = null;
        public string tag = "DefaultTag";

        public Shape2D(Vector2 Position, Vector2 Scale, string Tag)
        {
            this.position = Position;
            this.scale = Scale;
            this.tag = Tag;

            Log.Info($"[SHAPE2D]({tag}) - has been registered!");
            EngineIdk.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({tag}) - has been destroyed!");
            EngineIdk.UnRegisterShape(this);
        }

    }
}
