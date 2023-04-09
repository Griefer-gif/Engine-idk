using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace EngineIdk.EngineIdk
{

    class Canvas : Form
    {
        public Canvas()
        {

            this.DoubleBuffered = true;
        }

    }

    public abstract class EngineIdk
    {

        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;
        public bool Loading = true;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();

        public Color BackGroundColor = Color.Aquamarine;

        public Vector2 CameraZoom = new Vector2(1,1);
        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;

        public EngineIdk(Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");

            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.FormClosing += Window_FormClosing;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            //GameLoopThread.Abort();
        }

        private void Window_KeyUp(object? sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object? sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShape(Shape2D shape)
        {

            AllShapes.Add(shape);

        }

        public static void UnRegisterShape(Shape2D shape)
        {

            AllShapes.Remove(shape);

        }

        public static void RegisterSprite(Sprite2D sprite)
        {

            AllSprites.Add(sprite);

        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {

            AllSprites.Remove(sprite);

        }
        
        void GameLoop()
        { 

            Log.Warning($"[TIMER {timer}]");
            OnLoad();

            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game has not been found...");
                }
            }
        }

        int timer = 0;
        private void Renderer(object sender, PaintEventArgs e)
        {
            if (!Loading)
            {


                Graphics g = e.Graphics;

                g.Clear(BackGroundColor);

                g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
                g.RotateTransform(CameraAngle);
                g.ScaleTransform(CameraZoom.X, CameraZoom.Y);

                foreach (Shape2D shape in AllShapes)
                {

                    g.FillRectangle(new SolidBrush(Color.Red), shape.position.X, shape.position.Y, shape.scale.X, shape.scale.Y);

                }
                //Log.Warning("Before draw sprites " + AllSprites.Count());
                for (int i = 1; AllSprites.Count() > i; i++)
                {
                    
                    Sprite2D sprite = AllSprites[i];
                    if (!sprite.isReference)
                    {
                        g.DrawImage(sprite.Sprite, sprite.position.X, sprite.position.Y, sprite.scale.X, sprite.scale.Y);
                        //timer++;
                        //Log.Warning($"[DRAWN] ({sprite.tag} - {timer} - {AllSprites.IndexOf(sprite)})");
                    }
                    //Log.Info(AllSprites[0].directory);
                    
                }
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);

    }
}
