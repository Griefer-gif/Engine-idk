using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EngineIdk.EngineIdk;

namespace EngineIdk
{
    public class DemoGame : EngineIdk.EngineIdk
    {
        Sprite2D player;
        Shape2D pHitBox;
        Vector2 pSpawnPos;
        Vector2 lasPos = Vector2.Zero();


        //Sprite2D player2;
        Random r = new Random();
        bool left;
        bool right;
        bool up;
        bool down;

        string[,] Map =
        {
            {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"},
            {"w",".",".",".",".","w",".",".",".","w",".",".",".",".","w"},
            {"w",".",".",".",".","w",".","p",".","w",".",".",".",".","w"},
            {"w",".",".",".",".","w",".",".",".","w",".",".",".",".","w"},
            {"w",".",".",".",".","w","w",".","w","w",".",".",".",".","w"},
            {"w",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
            {"w",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
            {"w",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
            {"w",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
            {"w",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
            {"w",".",".",".",".",".",".",".",".",".",".",".",".",".","w"},
            {"w","w","w","w","w","w","w","w","w","w","w","w","w","w","w"},
        };

        public DemoGame() : base(new Vector2(490, 425), "Demo  The Game") { }

        public override void OnLoad()
        {

            CameraZoom = new Vector2(2f, 2f);

            BackGroundColor = Color.Black;
            Sprite2D WallRef = new Sprite2D($"Tiles/Wall/wall_banner_blue");
            Sprite2D GroundRef = new Sprite2D($"Tiles/Floor/floor_{r.Next(1, 8)}");
            //player = new Shape2D(new Vector2(10, 10), new Vector2(10,10), "test");

            //player.scaleMultiplier = 5f;
            new Sprite2D(new Vector2(0, 0), new Vector2(50, 50), GroundRef, "Ground");
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j,i] == ".")
                    {
                        //new Sprite2D(new Vector2(i * 32, j * 32), new Vector2(32, 32), GroundRef, "Ground");
                    }
                    if (Map[j, i] == "w")
                    {
                        new Sprite2D(new Vector2(i * 16, j * 16), new Vector2(16, 16), WallRef, "Wall");
                    }
                    if (Map[j, i] == "p")
                    {
                        new Sprite2D(new Vector2(i * 16, j * 16), new Vector2(16, 16), GroundRef, "Ground");
                        pSpawnPos = new Vector2(i * 16, j * 16);
                    }
                }
            }
            player = new Sprite2D(pSpawnPos, new Vector2(12, 12), "Player/wizzard_m_idle_anim_f1", "player");
            pHitBox = new Shape2D(new Vector2(200,200), new Vector2(12,12), "pHitBox");
            Loading = false;
        }

        public override void OnDraw()
        {
            
        }

        
        public override void OnUpdate()
        {
            pHitBox.position.X = player.position.X;
            pHitBox.position.Y = player.position.Y;

            if (player.IsColliding("Wall") != null)
            {
                player.position.X = lasPos.X;
                player.position.Y = lasPos.Y;
                Log.Info($"Colliding, {lasPos.X}, {lasPos.Y}");
            }
            else
            {
                lasPos.X = player.position.X;
                lasPos.Y = player.position.Y;
                //Log.Info($"Not colliding, {lasPos.X}, {lasPos.Y}");
            }
            if (up)
            {
                player.position.Y -= 1;
                Log.Info("PRESSED W POGGG");
            }
            if (down)
            {
                player.position.Y += 1;
            }
            if (left)
            {
                player.position.X -= 1;
            }
            if (right)
            {
                player.position.X += 1;
            }
            
            //CameraPosition = player.position;
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = true; };
            if (e.KeyCode == Keys.S) { down = true; };
            if (e.KeyCode == Keys.A) { left = true; };
            if (e.KeyCode == Keys.D) { right = true; };
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; };
            if (e.KeyCode == Keys.S) { down = false; };
            if (e.KeyCode == Keys.A) { left = false; };
            if (e.KeyCode == Keys.D) { right = false; };
        }
    }
}
