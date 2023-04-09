using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineIdk.EngineIdk
{
    public class Sprite2D
    {
        public Vector2 position = null;
        public Vector2 scale = null;
        public bool isReference = false;
        public float scaleMultiplier = 0f;
        public string directory = "";
        public string tag = "";
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string tag)
        {
            this.position = Position;
            this.scale = Scale;
            this.directory = Directory;
            this.tag = tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[SPRITE2D]({tag}) - has been registered!");
            EngineIdk.RegisterSprite(this);
            
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string tag)
        {
            this.position = Position;
            this.scale = Scale;
            this.Sprite = reference.Sprite;
            this.tag = tag;

            Log.Info($"[SPRITE2D]({tag}) - has been registered!");
            EngineIdk.RegisterSprite(this);

        }

        public Sprite2D(string Directory)
        {
            this.isReference = true;
            this.directory = Directory;

            Image tmp = Image.FromFile($"Assets/Sprites/{directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[SPRITE2D]({tag}) - has been registered!");
            EngineIdk.RegisterSprite(this);

        }

        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
            
          if (a.position.X < b.position.X + b.scale.X &&
             a.position.X + a.scale.X > b.position.X &&
             a.position.Y < b.position.Y + b.scale.Y &&
             a.position.Y + a.scale.Y > b.position.Y)
          {
             return true;
          }
           
         return false;

        }
        public Sprite2D IsColliding(string tag)
        {

            foreach (Sprite2D b in EngineIdk.AllSprites)
            {
                if(b.tag == tag)
                {
                    if (position.X < b.position.X + b.scale.X &&
                            position.X + scale.X > b.position.X &&
                            position.Y < b.position.Y + b.scale.Y &&
                            position.Y + scale.Y > b.position.Y)
                    {
                        return b;
                    }

                }
            }

            return null;
        }

        public void DestroySelf()
        {
            Log.Info($"[SPRITE2D]({tag}) - has been destroyed!");
            EngineIdk.UnRegisterSprite(this);
        }

    }
}
