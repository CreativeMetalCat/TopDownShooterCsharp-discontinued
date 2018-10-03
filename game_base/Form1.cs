#define DEBUG



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using Engine.Animation;
using Game;
using FMOD;
using Engine.SoundWork;

namespace game_base
{
    public partial class Form1 : Form
    {
        Image bullet;
        Graphics graphics;
        Game game;
        Ammo_Projectile ap;
        FMOD.System system;
        Reverb3D hall;
        public Form1()
        {
            InitializeComponent();
            //DoubleBuffered = true;
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            bullet = Image.FromFile("assets\\models\\gunshot.png");
            game = new Game(new Player(new AnimatedSprite(true, true, 20, Image.FromFile("assets\\models\\player_idle.gif"), 0, true, new RectangleF(0, 0, 50, 50), true),0, new RectangleF(0, 0, 50, 50)), new World(),500);
            game.LoadWorldFromFile("assets\\maps\\test.mf");
            ap = new Ammo_Projectile(true,2, 1000, Image.FromFile("assets\\models\\gunshot.png"), 0, true,new PointF(0, 0), true);
            game.player.AddPointSocket("Weapon", new PointF(34.5f,30.6f));
            game.player.AddPointSocket("projectile", new PointF(34.5f, 30.6f));
            if ((game.player.GetSocketByName("Weapon")!=null)&&(game.player.GetSocketByName("projectile")!= null))
            {
                game.Weapons.Add(new Weapon_Moveable(10, 100, Image.FromFile("assets\\models\\weapon_pistol.gif"), "Pistol", false, game.player.GetSocketByName("Weapon"), game.player.GetSocketByName("Projectile"),0.6f,0.6f));
            }
            //game.world.World_Actors.Add(new Actor(Image.FromFile("assets\\models\\floor_checkboard_texture.png"), 0, true, new PointF(50, 50), true));
            //game.world.World_Actors.Add(new Actor(Image.FromFile("assets\\models\\floor_checkboard_texture.png"), 1, true, new PointF(100, 50), true));
            ////game.world.World_Actors.Add(new Func_View_Layer(1, new RectangleF(90, 40, 100, 100), Image.FromFile("assets\\models\\ground_texture.png"), 0, false, new PointF(100,50), true));
            //game.world.World_Actors.Add(new Trigger_Change_Layer(TriggerType.Once, MoveDirection.NONE, 1, Image.FromFile("assets\\models\\ground_texture.png"), 0, true, new PointF(200, 200), true));
            game.SetPlayerWeapon(0);

            //Sound Work
            game.Sound_System.getLowLevelSystem(out system);
           
            //system.createReverb3D(out hall);
            REVERB_PROPERTIES Hall_Props = PRESET.HALLWAY();
            //hall.setProperties(ref Hall_Props);
            //VECTOR v = new VECTOR();
            //v = Vector.ToVECTOR(50, 0, 0);
            //hall.set3DAttributes(ref v, 0, 100);

            //game.AddActorToWorld(new Reverb_Area(system, new Reverb_Attributes(Vector.ToVECTOR(100, 100, 0), 0, 100), ref Hall_Props, Image.FromFile("assets\\models\\gunshot.bmp"), 0, false,new PointF(100,100), true));
            
            SoundData data = new SoundData(); 
            system.createSound("assets\\sounds\\pistol_shot.wav", MODE._3D, out data.sound);
            game.sounds.Add(data);
            game.AddActorToWorld(new Ambient_Sound(data,Image.FromFile("assets\\models\\gunshot.bmp"),0,false,new RectangleF(0,0,50,50),true));
            data = new SoundData();
            system.createSound("assets\\sounds\\sound_test_0.wav", MODE._3D, out data.sound);
            game.sounds.Add(data);
            game.Sound_System.update();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            if(e.KeyCode==Keys.NumPad1)
            {
                game.player.RotationDegree -= 1;
                if (game.player.RotationDegree == -360) { game.player.RotationDegree = 0; }
            }
            if (e.KeyCode == Keys.NumPad2)
            {
                game.player.RotationDegree += 1;
                if (game.player.RotationDegree == 360) { game.player.RotationDegree = 0; }
            }
            if (e.KeyCode == Keys.NumPad0)
            {
                game.player.RotationDegree = 0;
            }
            if(e.KeyCode==Keys.NumPad7)
            {
                game.player.WeaponID -= 1;
                if (game.player.WeaponID < 0) { game.player.WeaponID = 0; }
                game.SetPlayerWeapon(game.player.WeaponID);
            }
            if (e.KeyCode == Keys.NumPad8)
            {
                game.player.WeaponID += 1;
                if (game.player.WeaponID > game.Weapons.Count) { game.player.WeaponID = 0; }
                game.SetPlayerWeapon(game.player.WeaponID);
            }
            if(e.KeyCode==Keys.End)
            {
                game.player.ApplyDamage(20, game.player);
            }
#endif
            if (game.WorldScrollEnabled == true)
            {
                if (e.KeyCode == Keys.Up)
                {
                    float sy = game.player.GetY();
                    game.player.Collision.Y -= game.player.Speed / 2;
                    game.player.LastMoveDirection = MoveDirection.UP;
                    if (game.player.Collision.Y < 0) { game.player.Collision.Y = 0; }
                    game.CheckPlayerCollision();
                    if (sy != game.player.GetY())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.Y += game.player.Speed / 2;
                        }
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    float sy = game.player.GetY();
                    game.player.Collision.Y += game.player.Speed / 2;
                    game.player.LastMoveDirection = MoveDirection.DOWN;
                    game.CheckPlayerCollision();
                    if (sy != game.player.GetY())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.Y -= game.player.Speed / 2;
                        }
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    float sx = game.player.GetX();
                    game.player.Collision.X -= game.player.Speed / 2;
                    game.player.LastMoveDirection = MoveDirection.LEFT;
                    if (game.player.Collision.X < 0) { game.player.Collision.X = 0; }
                    game.CheckPlayerCollision();
                    if (sx != game.player.GetX())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.X += game.player.Speed / 2;
                        }
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    float sx = game.player.GetX();
                    game.player.Collision.X += game.player.Speed / 2;
                    game.player.LastMoveDirection = MoveDirection.RIGHT;
                    game.CheckPlayerCollision();
                    if (sx != game.player.GetX())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.X -= game.player.Speed / 2;
                        }
                    }

                }
            }
            else
            {
                if (e.KeyCode == Keys.Up)
                {
                    float sy = game.player.GetY();
                    game.player.Collision.Y -= game.player.Speed;
                    game.player.LastMoveDirection = MoveDirection.UP;
                    if (game.player.Collision.Y < 0) { game.player.Collision.Y = 0; }
                    game.CheckPlayerCollision();
                    if (sy != game.player.GetY())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.Y += game.player.Speed;
                        }
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    float sy = game.player.GetY();
                    game.player.Collision.Y += game.player.Speed;
                    game.player.LastMoveDirection = MoveDirection.DOWN;
                    game.CheckPlayerCollision();
                    if (sy != game.player.GetY())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.Y -= game.player.Speed;
                        }
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    float sx = game.player.GetX();
                    game.player.Collision.X -= game.player.Speed;
                    game.player.LastMoveDirection = MoveDirection.LEFT;
                    if (game.player.Collision.X < 0) { game.player.Collision.X = 0; }
                    game.CheckPlayerCollision();
                    if (sx != game.player.GetX())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.X += game.player.Speed;
                        }
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    float sx = game.player.GetX();
                    game.player.Collision.X += game.player.Speed;
                    game.player.LastMoveDirection = MoveDirection.RIGHT;
                    game.CheckPlayerCollision();
                    if (sx != game.player.GetX())
                    {
                        for (int i = 0; i < game.world.World_Actors.Count; i++)
                        {
                            game.world.World_Actors.ElementAt(i).Collision.X -= game.player.Speed;
                        }
                    }

                }
            }
            
            game.PlayerViewArea.X = game.player.GetX() - game.PlayerViewRadius;
            game.PlayerViewArea.Y = game.player.GetY() - game.PlayerViewRadius;
            FMOD.Studio._3D_ATTRIBUTES attrib = new FMOD.Studio._3D_ATTRIBUTES();
            attrib.position = Vector.ToVECTOR(game.player.GetX(), game.player.GetY(), 0);
            attrib.up = Vector.ToVECTOR(0, 1, 0);
            attrib.velocity = Vector.ToVECTOR(0, 0, 0);
            attrib.forward = Vector.ToVECTOR(1, 0, 0);
            RESULT r = game.Sound_System.setListenerAttributes(0, attrib);
            if (r!=RESULT.OK)
            {
                MessageBox.Show(FMOD.Error.String(r));
            }
            
            game.Sound_System.update();
            FMOD.Studio._3D_ATTRIBUTES attrib1 = new FMOD.Studio._3D_ATTRIBUTES();
            game.Sound_System.getListenerAttributes(0, out attrib1);
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            graphics = CreateGraphics();
          
            //game.Layer_View_Areas.Add(new Func_View_Layer(1, new RectangleF(90, 40, 100, 100), Image.FromFile("assets\\models\\ground_texture.png"), 0, false, new PointF(100, 50), true));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.CleanProjectiles();
            //game.Layer_View_Areas.Clear();
            if (game.player.image.AnimationIsActive == false)
            {
                game.player.image.StartAnimationThread();
            }
            for (int i = 0; i < game.world.World_Actors.Count; i++)
            {
                if(game.PlayerViewArea.Contains(game.world.World_Actors.ElementAt(i).Collision)|| game.PlayerViewArea.IntersectsWith(game.world.World_Actors.ElementAt(i).Collision))
                {
                    if (game.world.World_Actors.ElementAt(i).Visible == true)
                    {
                        if (game.world.World_Actors.ElementAt(i).Layer == game.player.Layer)
                        {
                            if (game.world.World_Actors.ElementAt(i) is Func_View_Layer)
                            {
                                game.Layer_View_Areas.Add((game.world.World_Actors.ElementAt(i) as Func_View_Layer));
                            }
                            else if (game.world.World_Actors.ElementAt(i).DrawAsSprite == true)
                            {
                                graphics.DrawImage(game.world.World_Actors.ElementAt(i).image, game.world.World_Actors.ElementAt(i).Collision);
                            }
                        }
                        else if (game.Layer_View_Areas.Count > 0)
                        {
                            for (int u = 0; u < game.Layer_View_Areas.Count; u++)
                            {
                                if ((game.Layer_View_Areas.ElementAt(u).TargetArea.Contains(game.world.World_Actors.ElementAt(i).Collision) || game.Layer_View_Areas.ElementAt(u).TargetArea.IntersectsWith(game.world.World_Actors.ElementAt(i).Collision)) && !(game.world.World_Actors.ElementAt(i) is Func_View_Layer))
                                {
                                    if (game.Layer_View_Areas.ElementAt(u).TargetLayer == game.world.World_Actors.ElementAt(i).Layer)
                                    {
                                        if (game.world.World_Actors.ElementAt(i).DrawAsSprite == true)
                                        {
                                            graphics.DrawImage(game.world.World_Actors.ElementAt(i).image, game.world.World_Actors.ElementAt(i).Collision);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                
            }

            graphics.TranslateTransform(((game.player.Collision.Width / 2) + game.player.Collision.X), ((game.player.Collision.Height  / 2) + game.player.Collision.Y));
            graphics.RotateTransform(game.player.RotationDegree);
            graphics.TranslateTransform(-((game.player.Collision.Width) / 2 + game.player.Collision.X), -((game.player.Collision.Height / 2) + game.player.Collision.Y));
            try
            {
                graphics.DrawImage(game.player.image.image, game.player.Collision);
            }
            catch (Exception exc)
            {

            }

            if (game.player.weapon != null)
            {
                graphics.DrawImage(game.player.weapon.image, new RectangleF(game.player.Collision.X + game.player.weapon.WeaponLocation.RelativeLocation.X, game.player.Collision.Y + game.player.weapon.WeaponLocation.RelativeLocation.Y,game.player.weapon.image.Width*game.player.weapon.ScaleX, game.player.weapon.image.Height * game.player.weapon.ScaleY));
            }
            graphics.ResetTransform();

            //graphics.TranslateTransform(((ap.Collision.Width / 2) + ap.Collision.X), ((ap.Collision.Height / 2) + ap.Collision.Y));
            //graphics.RotateTransform(ap.RotationDegree);
            //graphics.TranslateTransform(-((ap.Collision.Width) / 2 + ap.Collision.X), -((ap.Collision.Height / 2) + ap.Collision.Y));
            //graphics.DrawImage(ap.image, ap.Collision);
            for (int i = 0; i < game.Projectiles.Count; i++)
            {
                if (game.PlayerViewArea.Contains(game.Projectiles.ElementAt(i).Collision))
                {
                    graphics.TranslateTransform(((game.Projectiles.ElementAt(i).Collision.Width / 2) + game.Projectiles.ElementAt(i).Collision.X), ((game.Projectiles.ElementAt(i).Collision.Height / 2) + game.Projectiles.ElementAt(i).Collision.Y));
                    graphics.RotateTransform(game.Projectiles.ElementAt(i).RotationDegree);
                    graphics.TranslateTransform(-((game.Projectiles.ElementAt(i).Collision.Width) / 2 + game.Projectiles.ElementAt(i).Collision.X), -((game.Projectiles.ElementAt(i).Collision.Height / 2) + game.Projectiles.ElementAt(i).Collision.Y));
                    graphics.DrawImage(game.Projectiles.ElementAt(i).image, game.Projectiles.ElementAt(i).Collision);
                    graphics.ResetTransform();
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            float xDiff = e.X - game.player.GetX();
            float yDiff = e.Y - game.player.GetY();

            game.player.RotationDegree =Convert.ToSingle(Math.Atan2(yDiff, xDiff) * (180 / Math.PI));
           
            //if(!ap.IsThreadAlive())
            //{
            //    ap.Collision.X = game.player.GetX();
            //    ap.Collision.Y = game.player.GetY();
            //}
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //if(!ap.IsThreadAlive())
            //{
            //    ap.RotationDegree = game.player.RotationDegree;
            //    ap.StartThread(new PointF(e.X, e.Y));
            //}
            if(e.Button==MouseButtons.Middle)
            {
                //game.player.ApplyDamage(10, game.world.World_Actors.ElementAt(0));
                system.playSound(game.sounds.ElementAt(1).sound, null, game.sounds.ElementAt(1).Paused, out game.sounds.ElementAt(1).channel);
            }
            if(game.player.GetSocketByName("projectile")!=null)
            {
                game.AddProjectile(new Ammo_Projectile(true,2, 1000, bullet, game.player.Layer, true, new PointF(game.player.GetSocketByName("projectile").RelativeLocation.X+game.player.GetX(), game.player.GetSocketByName("projectile").RelativeLocation.Y + game.player.GetY()), true), game.player.RotationDegree, new PointF(e.X, e.Y));
                system.playSound(game.sounds.ElementAt(0).sound, null, game.sounds.ElementAt(0).Paused, out game.sounds.ElementAt(0).channel);
                Console.Write(":");
            }
            
        }
    }
}
