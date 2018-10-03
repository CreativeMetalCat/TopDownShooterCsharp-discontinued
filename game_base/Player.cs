using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Animation;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Game
{
    class Point_Socket
    {
        public string Name="";
        public PointF RelativeLocation = new PointF(0, 0);
        public object Object;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Object">Object that has this socket</param>
        /// <param name="Name"></param>
        /// <param name="RelativeLocation">Realative to obkject</param>
        public Point_Socket(object Object,string Name,PointF RelativeLocation)
        {
            this.Name = Name;
            this.RelativeLocation = RelativeLocation;
            this.Object = Object;
        }
    }


    class Ammo_Projectile:Actor
    {
        public void OnImpact(Actor impacted)
        {
            impacted.ApplyDamage(50, this);
        }
        public static void Count_Physics(Ammo_Projectile e,PointF point)
        {
            if (point.X < 0 || point.X < 0) { return; }
            else
            {
                float sx = e.Collision.X;
                float sy = e.Collision.Y;
                int distance = 0;
                decimal d = (1m / e.Speed);
                int delay = (int)(d * 10m);
                float m = point.Y / point.X;
                while (distance <= e.MaxDistance)
                {
                    if (point.X != sx)
                    {
                        if (sx > point.X)
                        {
                            e.Collision.X -= 1;
                        }
                        else
                        {
                            e.Collision.X += 1;
                        }
                    }
                    if (point.Y != sy)
                    {
                        if (sy > point.Y)
                        {
                            e.Collision.Y = -e.Collision.X * m;
                        }
                        else
                        {
                            e.Collision.Y = e.Collision.X * m;
                        }
                    }

                    distance++;
                    System.Threading.Thread.Sleep(delay);
                }
            }

        }
        protected Thread PhysicsThread;
        public int MaxDistance = 0;
        public int Speed = 0;
        public float RotationDegree = 0;
        public bool DestroyOnImpact = true;
        public Ammo_Projectile(bool DestroyOnImpact,int Speed,int MaxDistance,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage):base(image,Layer,HasCollision,collision,true,AnimateImage)
        {
            this.MaxDistance = MaxDistance;
            this.DestroyOnImpact = DestroyOnImpact;
            this.Speed = Speed;
        }
        public Ammo_Projectile(bool DestroyOnImpact,int Speed,int MaxDistance,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, true, AnimateImage)
        {
            this.MaxDistance = MaxDistance;
            this.Speed = Speed;
            this.DestroyOnImpact = DestroyOnImpact;
        }
        public Ammo_Projectile(bool DestroyOnImpact,int Speed,int MaxDistance,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, true, false)
        {
            this.MaxDistance = MaxDistance;
            this.Speed = Speed;
            this.DestroyOnImpact = DestroyOnImpact;
        }
        public Ammo_Projectile(bool DestroyOnImpact, int Speed,int MaxDistance,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, true, false)
        {
            this.MaxDistance = MaxDistance;
            this.Speed = Speed;
            this.DestroyOnImpact = DestroyOnImpact;
        }
        public void StartThread(PointF point)
        {
            PhysicsThread = new Thread(() => Ammo_Projectile.Count_Physics(this, point));
            PhysicsThread.IsBackground = true;
            PhysicsThread.Start();
        }
        public bool IsThreadAlive()
        {
            if (PhysicsThread == null) { return false; }
            return PhysicsThread.IsAlive;
        }
    }

    /// <summary>
    /// Base class for weapon
    /// </summary>
    class Weapon
    {
        public Point_Socket WeaponLocation;
        public Image image;
        public string Name = "";
        public bool OverridePlayerSprite = false;
        public Point_Socket ProjectileLocation;
        public float ScaleX = 1;
        public float ScaleY = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="Name"></param>
        /// <param name="OverridePlayerSprite">Replace sprite of player with Weaopn.image</param>
        /// <param name="WeaponLocation">Location of weapon sprite(Relative to Player)</param>
        /// <param name="ProjectioleLocation">Location that projectiles will be lauched from(Relative to Player)</param>
        public Weapon(Image image, string Name, bool OverridePlayerSprite, Point_Socket WeaponLocation,Point_Socket ProjectileLocation,float ScaleX, float ScaleY)
        {
            this.ScaleX = ScaleX;
            this.ScaleY = ScaleY;
            this.Name = Name;
            this.image = image;
            this.OverridePlayerSprite = OverridePlayerSprite;
            this.WeaponLocation = WeaponLocation;
            this.ProjectileLocation = ProjectileLocation;
        }

       
    }

    class Weapon_Moveable:Weapon
    {
        public int ClipSize = 0;
        public int MaxAmmo = 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClipSize">Amount of ammo per clip(-1 for infinite)</param>
        /// <param name="MaxAmmo">Amount of ammo per clip(-1 for infinite)</param>
        public Weapon_Moveable(int ClipSize, int MaxAmmo,Image image, string Name, bool OverridePlayerSprite, Point_Socket WeaponLocation, Point_Socket ProjectioleLocation, float ScaleX, float ScaleY) :base(image,Name,OverridePlayerSprite,WeaponLocation,ProjectioleLocation,ScaleX,ScaleY)
        {
            this.ClipSize = ClipSize;
            this.MaxAmmo = MaxAmmo;
        }
    }

    //class Player
    //{
    //    public delegate void DamageApplied(Actor caller, float damage);
    //    public event DamageApplied OnDamageApplied;
    //    public void ApplyDamage(float Damage, Actor actor)
    //    {
    //        actor.OnDamageApplied(this, Damage);
    //    }
    //    public List<Point_Socket> Point_Sockets = new List<Point_Socket>();
    //    public Weapon weapon;
    //    public float Speed = 2;
    //    public MoveDirection LastMoveDirection = MoveDirection.NONE;
    //    public RectangleF Collision;
    //    public AnimatedSprite image;
    //    public int Layer { get => image.Layer; set => image.Layer = value; }
    //    public void SetX(float x) { image.Collision.X = x; Collision.X = x; }
    //    public void SetY(float y) { image.Collision.Y = y; Collision.Y = y; }
    //    public float GetX() { return Collision.X; }
    //    public float GetY() { return Collision.Y; }
    //    public int WeaponID = 0;
    //    public float RotationDegree = 0;
    //    public Point_Socket GetSocketByName(string Name)
    //    {
    //        if (Point_Sockets.Count > 0)
    //        {
    //            for (int i = 0; i < Point_Sockets.Count; i++)
    //            {
    //                if (Point_Sockets.ElementAt(i).Name == Name)
    //                {
    //                    return Point_Sockets.ElementAt(i);
    //                }
    //            }
    //            return new Point_Socket(this, "null", new PointF(0, 0));
    //        }
    //        else
    //        {
    //            return new Point_Socket(this, "null", new PointF(0, 0));
    //        }
    //    }
    //    public void AddPointSocket(string name, PointF RelativeLocation)
    //    {
    //        Point_Sockets.Add(new Point_Socket(this, name, RelativeLocation));
    //    }
    //    public Player(AnimatedSprite image, RectangleF Collision)
    //    {
    //        this.image = image;
    //        this.Collision = Collision;
    //    }

    //}

    class Player : Actor
    {
        public void OnDamageApllied(Actor caller, float damage)
        {
            System.Console.Write("d");
            Health -= Convert.ToInt32(damage);
        }
        public List<Point_Socket> Point_Sockets = new List<Point_Socket>();
        public Weapon weapon;
        public float Speed = 2;
        public int Health = 100;
        public MoveDirection LastMoveDirection = MoveDirection.NONE;
        public new AnimatedSprite image;
        public new int Layer { get { return image.Layer; } set { image.Layer = value; } }
        public void SetX(float x)
        {
            image.Collision.X = x; Collision.X = x;
        }
        public void SetY(float y)
        {
            image.Collision.Y = y; Collision.Y = y;
        }
        public float GetX() { return Collision.X; }
        public float GetY() { return Collision.Y; }
        public int WeaponID = 0;
        public float RotationDegree = 0;
        public Point_Socket GetSocketByName(string Name)
        {
            if (Point_Sockets.Count > 0)
            {
                for (int i = 0; i < Point_Sockets.Count; i++)
                {
                    if (Point_Sockets.ElementAt(i).Name == Name)
                    {
                        return Point_Sockets.ElementAt(i);
                    }
                }
                //return new Point_Socket(this, "null", new PointF(0, 0));
                return null;
            }
            else
            {
                //return new Point_Socket(this, "null", new PointF(0, 0));
                return null;
            }
        }
        public void AddPointSocket(string name, PointF RelativeLocation)
        {
            Point_Sockets.Add(new Point_Socket(this, name, RelativeLocation));
        }
        public Player(AnimatedSprite image,int layer, RectangleF Collision):base(image.image,layer,true,Collision,true,true)
        {
            this.OnDamageApplied += OnDamageApllied;
            this.image = image;
            this.Collision = Collision;
        }
    }
}
