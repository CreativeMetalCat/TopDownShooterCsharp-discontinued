using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Engine;
using Engine.Animation;
using System.Drawing;
using Game;
using System.Xml;
using FMOD;
using Engine.SoundWork;

namespace game_base
{
    
    //class Ambient_Sound : SoundData
    //{
    //    public bool Looped = false;
    //    public VECTOR Location = new VECTOR();
    //    public Ambient_Sound(Vector v, bool Looped)
    //    {
    //        this.Looped = Looped;
    //        Location = v.ToVECTOR();
    //    }
    //    public Ambient_Sound(SoundData data, Vector v, bool Looped) : base(data.sound, data.Name)
    //    {
    //        this.Looped = Looped;
    //        Location = v.ToVECTOR();
    //    }
    //    public Ambient_Sound(Vector v, bool Looped, Sound sound, string name) : base(sound, name)
    //    {
    //        this.Looped = Looped;
    //        Location = v.ToVECTOR();
    //    }
    //    public Ambient_Sound(Vector v, bool Looped, string name) : base(name)
    //    {
    //        this.Looped = Looped;
    //        Location = v.ToVECTOR();
    //    }
    //    public Ambient_Sound() { }
    //}
      



    class World
    {
        public List<Actor> World_Actors = new List<Actor>();
        public World() { }
    }

    class Game
    {
        public FMOD.Studio.System Sound_System = null;
        public List<SoundData> sounds = new List<SoundData>();
        public List<Reverb3D> reverbs = new List<Reverb3D>();
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Ammo_Projectile> Projectiles = new List<Ammo_Projectile>();
        public Player player;
        public World world;
        public bool WorldScrollEnabled = true;
        public List<Func_View_Layer> Layer_View_Areas = new List<Func_View_Layer>();

        public static bool CheckPlayerCollision(ref Player player,List<Actor> World_Actors)
        {
            if (World_Actors.Count > 0)
            {
                for (int i = 0; i < World_Actors.Count; i++)
                {
                    if (player.Layer == World_Actors.ElementAt(i).Layer)
                    {
                        if (player.Collision.IntersectsWith(World_Actors.ElementAt(i).Collision))
                        {
                            while (player.Collision.IntersectsWith(World_Actors.ElementAt(i).Collision))
                            {
                                if (player.LastMoveDirection == MoveDirection.DOWN)
                                {
                                    player.SetY(player.GetY() - 1);
                                }
                                else if (player.LastMoveDirection == MoveDirection.UP)
                                {
                                    player.SetY(player.GetY() + 1);
                                }
                                else if (player.LastMoveDirection == MoveDirection.RIGHT)
                                {
                                    player.SetX(player.GetX() - 1);
                                }
                                else if (player.LastMoveDirection == MoveDirection.LEFT)
                                {
                                    player.SetX(player.GetX() + 1);
                                }
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckPlayerCollision(ref Game game)
        {
            if (game.world.World_Actors.Count > 0)
            {
                for (int i = 0; i < game.world.World_Actors.Count; i++)
                {
                    if (game.player.Layer == game.world.World_Actors.ElementAt(i).Layer)
                    {
                        if (game.player.Collision.IntersectsWith(game.world.World_Actors.ElementAt(i).Collision))
                        {
                            while (game.player.Collision.IntersectsWith(game.world.World_Actors.ElementAt(i).Collision))
                            {
                                if (game.player.LastMoveDirection == MoveDirection.DOWN)
                                {
                                    game.player.SetY(game.player.GetY() - 1);
                                }
                                else if (game.player.LastMoveDirection == MoveDirection.UP)
                                {
                                    game.player.SetY(game.player.GetY() + 1);
                                }
                                else if (game.player.LastMoveDirection == MoveDirection.RIGHT)
                                {
                                    game.player.SetX(game.player.GetX() - 1);
                                }
                                else if (game.player.LastMoveDirection == MoveDirection.LEFT)
                                {
                                    game.player.SetX(game.player.GetX() + 1);
                                }
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public bool CheckPlayerCollision()
        {
            if (world.World_Actors.Count > 0)
            {
                //Layer_View_Areas.Clear();
                for (int i = 0; i < world.World_Actors.Count; i++)
                {
                    if (world.World_Actors.ElementAt(i).HasCollision == true)
                    {
                        if (player.Layer == world.World_Actors.ElementAt(i).Layer)
                        {

                            if (player.Collision.IntersectsWith(world.World_Actors.ElementAt(i).Collision))
                            {
                                if (world.World_Actors.ElementAt(i) is Trigger)
                                {
                                    if (world.World_Actors.ElementAt(i) is Trigger_Change_Layer)
                                    {
                                        Trigger_Change_Layer t = world.World_Actors.ElementAt(i) as Trigger_Change_Layer;
                                        if (t.Used == false)
                                        {
                                            if (t.Type == TriggerType.Once)
                                            {
                                                player.Layer = t.LayerToChangeTo;
                                                t.Used = true;
                                            }
                                            if (t.Type == TriggerType.Multi)
                                            {
                                                player.Layer = t.LayerToChangeTo;
                                            }
                                            if (t.Type == TriggerType.One_Side_Multi)
                                            {
                                                if (player.LastMoveDirection == t.EnterDirection)
                                                {
                                                    player.Layer = t.LayerToChangeTo;
                                                }
                                            }
                                            if (t.Type == TriggerType.One_Side_Once)
                                            {
                                                player.Layer = t.LayerToChangeTo;
                                                t.Used = true;
                                            }
                                        }
                                    }
                                    if (world.World_Actors.ElementAt(i) is Trigger_Damage)
                                    {
                                        Trigger_Damage t = world.World_Actors.ElementAt(i) as Trigger_Damage;
                                        if (t.Used == false)
                                        {
                                            if (t.Type == TriggerType.Once)
                                            {
                                                player.ApplyDamage(t.Damage, world.World_Actors.ElementAt(i));
                                                t.Used = true;
                                            }
                                            if (t.Type == TriggerType.Multi)
                                            {
                                                player.ApplyDamage(t.Damage, world.World_Actors.ElementAt(i));
                                            }
                                            if (t.Type == TriggerType.One_Side_Multi)
                                            {
                                                if (player.LastMoveDirection == t.EnterDirection)
                                                {
                                                    player.ApplyDamage(t.Damage, world.World_Actors.ElementAt(i));
                                                }
                                            }
                                            if (t.Type == TriggerType.One_Side_Once)
                                            {
                                                player.ApplyDamage(t.Damage, world.World_Actors.ElementAt(i));
                                                t.Used = true;
                                            }
                                        }
                                    }
                                }
                                if (world.World_Actors.ElementAt(i) is Func_Base)
                                {
                                    //if(world.World_Actors.ElementAt(i) is Func_View_Layer)
                                    //{
                                    //    Layer_View_Areas.Add((world.World_Actors.ElementAt(i) as Func_View_Layer));
                                    //}
                                }
                                else
                                {
                                    while (player.Collision.IntersectsWith(world.World_Actors.ElementAt(i).Collision))
                                    {
                                        if (player.LastMoveDirection == MoveDirection.DOWN)
                                        {
                                            player.SetY(player.Collision.Y - 1);
                                        }
                                        else if (player.LastMoveDirection == MoveDirection.UP)
                                        {
                                            player.SetY(player.Collision.Y + 1);
                                        }
                                        else if (player.LastMoveDirection == MoveDirection.RIGHT)
                                        {
                                            player.SetX(player.Collision.X - 1);
                                        }
                                        else if (player.LastMoveDirection == MoveDirection.LEFT)
                                        {
                                            player.SetX(player.Collision.X + 1);
                                        }
                                    }
                                }
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void SetPlayerWeapon(int WeaponId)
        {
            if (WeaponId > -1 && WeaponId < Weapons.Count)
            {
                player.weapon = Weapons.ElementAt(WeaponId);
            }
        }
        public void SetPlayerWeapon(string Name)
        {
            if (Weapons.Count > 0)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    if(Weapons.ElementAt(i).Name==Name)
                    {
                        player.weapon = Weapons.ElementAt(i);
                    }
                }
            }
        }

        public Game(Player player,World world,int PlayerViewRadius)
        {
            
            RESULT res = FMOD.Studio.System.create(out Sound_System);
            if (res == RESULT.OK)
            {
                res = Sound_System.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, INITFLAGS.NORMAL,new IntPtr(0));
                if(res!=RESULT.OK)
                {
                    throw (new ApplicationException("Unable to init sound_system"));
                }
            }
            else
            {
                throw (new ApplicationException("Unable to create sound_system"));
            }
            this.player = player;
            this.PlayerViewRadius = PlayerViewRadius;
            PlayerViewArea.Height = PlayerViewRadius * 2;
            PlayerViewArea.Width = PlayerViewRadius * 2;
            this.world = world;
        }
        public float PlayerViewRadius = 0;
        public RectangleF PlayerViewArea = new RectangleF(0, 0, 0, 0);


        public void LoadWorldFromFile(string filename)
        {
            XmlDocument map = new XmlDocument();
            map.Load(filename);
            XmlElement root = map.DocumentElement;
            foreach (XmlNode node in root)
            {
                if (node.Attributes.Count > 0)
                {
                    if(node.Name=="world")
                    {
                        if(node.ChildNodes.Count>0)
                        {
                            foreach (XmlNode actor in node)
                            {
                                if(actor.Name=="actor")
                                {
                                    if (actor.Attributes.Count > 0)
                                    {
                                        if (actor.Attributes.GetNamedItem("x") != null && actor.Attributes.GetNamedItem("y") != null && actor.Attributes.GetNamedItem("height") != null && actor.Attributes.GetNamedItem("width") != null && actor.Attributes.GetNamedItem("layer") != null && actor.Attributes.GetNamedItem("drawassprite") != null && actor.Attributes.GetNamedItem("hascollision") != null && actor.Attributes.GetNamedItem("imagefile") != null)
                                        {
                                            Image g = Image.FromFile(actor.Attributes.GetNamedItem("imagefile").Value);
                                            RectangleF collision = new RectangleF();
                                            collision.X = Convert.ToSingle(actor.Attributes.GetNamedItem("x").Value);
                                            collision.Y = Convert.ToSingle(actor.Attributes.GetNamedItem("y").Value);
                                            collision.Width = Convert.ToSingle(actor.Attributes.GetNamedItem("width").Value);
                                            collision.Height = Convert.ToSingle(actor.Attributes.GetNamedItem("height").Value);
                                            if (actor.Attributes.GetNamedItem("height").Value == "-1")
                                            {
                                                collision.Width = g.Width;
                                            }
                                            if (actor.Attributes.GetNamedItem("width").Value == "-1")
                                            {
                                                collision.Height = g.Height;
                                            }
                                            int layer = Convert.ToInt32(actor.Attributes.GetNamedItem("layer").Value);
                                            bool drawassprite = Convert.ToBoolean(actor.Attributes.GetNamedItem("drawassprite").Value);
                                            bool hascollision = Convert.ToBoolean(actor.Attributes.GetNamedItem("hascollision").Value);

                                            if (actor.Attributes.GetNamedItem("type") != null)
                                            {
                                                if (actor.Attributes.GetNamedItem("type").Value == "solid")
                                                {
                                                    world.World_Actors.Add(new Actor(g, layer, hascollision, collision, drawassprite));
                                                }
                                                if (actor.Attributes.GetNamedItem("type").Value == "trigger")
                                                {
                                                    if (actor.Attributes.GetNamedItem("trigger_type") != null && actor.Attributes.GetNamedItem("enter_dir") != null && actor.Attributes.GetNamedItem("trigger_use_type") != null)
                                                    {
                                                        //MoveDirection md = (MoveDirection)Convert.ToInt32(actor.Attributes.GetNamedItem("enter_dir").Value);                                                
                                                        //TriggerType tt = (TriggerType)Convert.ToInt32(actor.Attributes.GetNamedItem("trigger_type").Value);

                                                        MoveDirection md=MoveDirection.NONE;
                                                        Enum.TryParse<MoveDirection>(actor.Attributes.GetNamedItem("enter_dir").Value,out md);
                                                        TriggerType tt = TriggerType.Once;
                                                        Enum.TryParse<TriggerType>(actor.Attributes.GetNamedItem("trigger_type").Value, out tt);

                                                        if (actor.Attributes.GetNamedItem("trigger_type").Value == "change_layer")
                                                        {
                                                            int tlayer = Convert.ToInt32(actor.Attributes.GetNamedItem("target_layer").Value);
                                                            world.World_Actors.Add(new Trigger_Change_Layer(tt, md,tlayer, g, layer, hascollision, collision, drawassprite));
                                                        }
                                                    }
                                                }
                                                if (actor.Attributes.GetNamedItem("type").Value == "func")
                                                {
                                                    if (actor.Attributes.GetNamedItem("func_type") != null)
                                                    {
                                                        if (actor.Attributes.GetNamedItem("func_type").Value == "view_layer")
                                                        {
                                                            if (actor.Attributes.GetNamedItem("t_x") != null && actor.Attributes.GetNamedItem("t_y") != null && actor.Attributes.GetNamedItem("t_height") != null && actor.Attributes.GetNamedItem("t_width") != null && actor.Attributes.GetNamedItem("target_layer") != null)
                                                            {

                                                                float tx = Convert.ToSingle(actor.Attributes.GetNamedItem("t_x").Value);
                                                                float ty = Convert.ToSingle(actor.Attributes.GetNamedItem("t_y").Value);
                                                                float twidth = Convert.ToSingle(actor.Attributes.GetNamedItem("t_width").Value);
                                                                float theight = Convert.ToSingle(actor.Attributes.GetNamedItem("t_height").Value);
                                                                int tlayer= Convert.ToInt32(actor.Attributes.GetNamedItem("target_layer").Value);
                                                                world.World_Actors.Add(new Func_View_Layer(tlayer, new RectangleF(tx, ty, twidth, theight), g, layer, hascollision, collision, drawassprite));
                                                            }
                                                        }
                                                    }
                                                }
                                                if (actor.Attributes.GetNamedItem("type").Value == "reverb")
                                                {
                                                    if(actor.Attributes.GetNamedItem("z") != null && actor.Attributes.GetNamedItem("max") != null && actor.Attributes.GetNamedItem("min") != null && actor.Attributes.GetNamedItem("reverb_preset_name") != null)
                                                    {
                                                        Reverb_Props_Presets rp = Reverb_Props_Presets.OFF;
                                                        float z=Convert.ToSingle(actor.Attributes.GetNamedItem("z").Value);
                                                        float min = Convert.ToSingle(actor.Attributes.GetNamedItem("min").Value);
                                                        float max = Convert.ToSingle(actor.Attributes.GetNamedItem("max").Value);

                                                        Enum.TryParse<Reverb_Props_Presets>(actor.Attributes.GetNamedItem("reverb_preset_name").Value, out rp);
                                                        REVERB_PROPERTIES rev = Reverb_Area.LoadPreset(rp);
                                                        FMOD.System system;
                                                        Sound_System.getLowLevelSystem(out system);

                                                        world.World_Actors.Add(new Reverb_Area(system, new Reverb_Attributes(Vector.ToVECTOR(0, 0, 0), 0, 1000), ref rev, g, layer, hascollision, collision, drawassprite));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if(node.Name=="entity")
                    {
                        if(node.ChildNodes.Count>0)
                        {
                            foreach (XmlNode ent in node)
                            {
                                if(ent.Name=="ent")
                                {
                                    if(ent.Attributes.Count>0)
                                    {
                                        if (ent.Attributes.GetNamedItem("name") != null)
                                        {
                                            if (ent.Attributes.GetNamedItem("name").Value == "player_start")
                                            {
                                                if(ent.Attributes.GetNamedItem("x")!=null&& ent.Attributes.GetNamedItem("y") != null)
                                                {
                                                    player.SetX(Convert.ToSingle(ent.Attributes.GetNamedItem("x").Value));
                                                    player.SetX(Convert.ToSingle(ent.Attributes.GetNamedItem("y").Value));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void LoadGameFromFile(string filename) { }

        public void AddProjectile(Ammo_Projectile e,float RotationDegree,PointF point)
        {
            if(!e.IsThreadAlive())
            {
                e.RotationDegree = RotationDegree;
                e.StartThread(point);
                Projectiles.Add(e);
            }
        }
        public void CleanProjectiles()
        {
            if (Projectiles.Count > 0)
            {
                for (int i = 0; i < Projectiles.Count; i++)
                {
                    if(!Projectiles.ElementAt(i).IsThreadAlive())
                    {
                        Projectiles.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actor">Projectile that need to be checked</param>
        /// <returns>null if no actor been impacted or actor itself</returns>
        public Actor CheckProjectileCollision(Ammo_Projectile actor)
        {
            if (world.World_Actors.Count > 0)
            {
                for (int i = 0; i < world.World_Actors.Count; i++)
                {
                    if (world.World_Actors.ElementAt(i).HasCollision == true)
                    {
                        if (world.World_Actors.ElementAt(i).Layer == actor.Layer)
                        {
                            if (actor.Collision.IntersectsWith(world.World_Actors.ElementAt(i).Collision))
                            {
                                actor.OnImpact(world.World_Actors.ElementAt(i));
                                if (actor.DestroyOnImpact == true)
                                {
                                    
                                }
                                return world.World_Actors.ElementAt(i);
                            }
                        }
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public void AddActorToWorld(Actor actor)
        {
            world.World_Actors.Add(actor);
        }
        public SoundData GetSoundObjectByName(string name)
        {
            if (sounds.Count > 0)
            {
                for (int i = 0; i < sounds.Count; i++)
                {
                    if (sounds.ElementAt(i).Name == name)
                    {
                        return sounds.ElementAt(i);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public void Dispose()
        {
            Layer_View_Areas.Clear();
        }
    }


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
