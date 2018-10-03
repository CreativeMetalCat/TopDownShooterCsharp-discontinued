

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using FMOD;

namespace Engine
{
    ///<summary>Base class for every drawable object</summary>
    class Drawable
    {
        public Image image;
        public Drawable(Image image)
        {
            this.image = image;
        }
    }



    enum MoveDirection
    {
        ///<summary>If object is not suposed to be moved, or haven't been yet</summary>
        NONE,
        UP = 1,
        DOWN,
        LEFT,
        RIGHT,
    }
    /// <summary>
    /// Base class for all objects in world, if they need to be checked for collision,moved with world etc.
    /// </summary>
    class Actor : Drawable
    {
        public delegate void DamageApplied(Actor caller, float damage);
        /// <summary>
        /// Gets scale of Actor.Image by Dividing original image size by Actor.Image size
        /// </summary>
        /// <returns></returns>
        public float GetActorScaleY()
        {
            return Collision.Height / image.Height;

        }
        public float GetActorScaleX()
        {
            return Collision.Width / image.Width;
            
        }
        public event DamageApplied OnDamageApplied;
        public void ApplyDamage(float Damage,Actor actor)
        {
            actor.OnDamageApplied(this,Damage);
        }
        public int Layer;
        public bool HasCollision = true;
        public bool AnimateImage = false;
        public bool Visible = true;
        public bool DrawAsSprite = true;
        public RectangleF Collision;
        public Actor(Image image,int Layer,bool HasCollision,RectangleF collision,bool DrawAsSprite,bool AnimateImage):base (image)
        {
            this.Layer = Layer;
            this.AnimateImage = AnimateImage;
            this.DrawAsSprite = DrawAsSprite;
            Collision = collision;
            this.HasCollision = HasCollision;
        }
        public Actor(Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image)
        {
            this.Layer = Layer;
            this.AnimateImage = AnimateImage;
            this.DrawAsSprite = DrawAsSprite;
            Collision =new  RectangleF(pt.X, pt.Y, image.Width, image.Height);
            this.HasCollision = HasCollision;
        }
        public Actor(Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image)
        {
            this.Layer = Layer;
            this.DrawAsSprite = DrawAsSprite;
            Collision = collision;
            this.HasCollision = HasCollision;
        }
        public Actor(Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image)
        {
            this.Layer = Layer;
            this.DrawAsSprite = DrawAsSprite;
            Collision = new RectangleF(pt.X, pt.Y, image.Width, image.Height);
            this.HasCollision = HasCollision;
        }

    }
}

namespace Engine.Animation
{

    class Animation
    {
        //public static void StartSpriteAnimation(AnimatedSprite e)
        //{
        //    e.AnimationIsActive = true;
        //    do
        //    {
        //        FrameDimension frameDimension = new FrameDimension(e.image.FrameDimensionsList[0]);
        //        int FrameCount = e.image.GetFrameCount(frameDimension);
        //        for(int i=0;i<=FrameCount;i++)
        //        {
        //            e.image.SelectActiveFrame(frameDimension, i);
        //        }
        //        e.image.SelectActiveFrame(frameDimension, 0);

        //    } while (e.Looped == true);
        //}
        public static void StartSpriteAnimation(object Sprite)
        {

            if (Sprite is AnimatedSprite)
            {
                try
                {
                    AnimatedSprite e = Sprite as AnimatedSprite;

                    e.AnimationIsActive = true;
                    do
                    {

                        FrameDimension frameDimension = new FrameDimension(e.image.FrameDimensionsList[0]);
                        int FrameCount = e.image.GetFrameCount(frameDimension);
                        for (int i = 0; i <= FrameCount; i++)
                        {
                            e.image.SelectActiveFrame(frameDimension, i);
                            Thread.Sleep(e.FrameDelay);
                        }
                        e.image.SelectActiveFrame(frameDimension, 0);


                    } while (e.Looped == true);
                    e.AnimationIsActive = false;
                }
                catch (System.Runtime.InteropServices.ExternalException e)
                {

                }
            }
            else
            {
                throw (new ArgumentException("Object wasn't animation sprite", "Sprite"));
            }
        }
        public static void StartSpriteAnimation(AnimatedSprite e)
        {
            try
            {

                e.AnimationIsActive = true;
                do
                {
                    lock (e)
                    {
                        FrameDimension frameDimension = new FrameDimension(e.image.FrameDimensionsList[0]);
                        int FrameCount = e.image.GetFrameCount(frameDimension);
                        for (int i = 0; i < FrameCount; i++)
                        {

                            e.image.SelectActiveFrame(frameDimension, i);
                            Thread.Sleep(e.FrameDelay);

                        }
                        e.image.SelectActiveFrame(frameDimension, 0);

                    }
                } while (e.Looped == true);
                e.AnimationIsActive = false;
            }
            catch (System.Runtime.InteropServices.ExternalException exc)
            {
                Console.Beep();
            }
            catch(Exception exc)
            {
                e.AnimationIsActive = false;
            }
        }
        
        public static void StartDrawingSpriteAnimation(AnimatedSprite e,ref Graphics graphics)
        {
            e.AnimationIsActive = true;
            do
            {
                FrameDimension frameDimension = new FrameDimension(e.image.FrameDimensionsList[0]);
                int FrameCount = e.image.GetFrameCount(frameDimension);
                for (int i = 0; i <= FrameCount; i++)
                {
                    try
                    {
                        e.image.SelectActiveFrame(frameDimension, i);
                        graphics.DrawImage(e.image, e.Collision);
                    }
                    catch(ArgumentNullException a)
                    {
                        
                    }
                }
                e.image.SelectActiveFrame(frameDimension, 0);

            } while (e.Looped == true);
        }
    }
    
    class AnimatedSprite : Actor
    {

        protected Thread Animation_Thread;
        public int FrameDelay = 20;
        public bool UsePresetFrameDelay = false;
        public bool AnimationIsActive = false;
        public bool Looped = false;
        public AnimatedSprite(bool Looped, bool UsePresetFrameDelay, int FrameDelay, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite)
        {
            this.Looped = Looped;
            this.FrameDelay = FrameDelay;
            this.UsePresetFrameDelay = UsePresetFrameDelay;
        }
        public AnimatedSprite(bool Looped, bool UsePresetFrameDelay, int FrameDelay, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite)
        {
            this.Looped = Looped;
            this.FrameDelay = FrameDelay;
            this.UsePresetFrameDelay = UsePresetFrameDelay;
        }
        public void StartAnimationThread()
        {

            Animation_Thread = new Thread(() => Animation.StartSpriteAnimation(this));
            Animation_Thread.IsBackground = true;
            Animation_Thread.Start();
        }
    }
}

namespace Engine.SoundWork
{
    class SoundData
    {
        public Sound sound = null;
        public bool Paused = false;
        public string Name = "";
        public Channel channel = null;
        public SoundData(Sound sound, string name)
        {
            this.sound = sound;
            this.Name = name;
        }
        public SoundData()
        {

        }
        public SoundData(string name)
        {
            this.Name = name;
        }
    }
    class Vector
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static VECTOR ToVECTOR(int x, int y, int z)
        {
            VECTOR v = new VECTOR();
            v.x = x;
            v.y = y;
            v.z = z;
            return v;
        }
        public static VECTOR ToVECTOR(float x, float y, float z)
        {
            VECTOR v = new VECTOR();
            v.x = x;
            v.y = y;
            v.z = z;
            return v;
        }
        public static VECTOR ToVECTOR(Vector vec)
        {
            VECTOR v = new VECTOR();
            v.x = vec.x;
            v.y = vec.y;
            v.z = vec.z;
            return v;
        }
        public VECTOR ToVECTOR()
        {
            VECTOR v = new VECTOR();
            v.x = x;
            v.y = y;
            v.z = z;
            return v;
        }

    }

    struct Reverb_Attributes
    {
        public VECTOR Position;
        public float MinDistance;
        public float MaxDistance;
        public Reverb_Attributes(VECTOR Position, float MinDistance, float MaxDistance)
        {
            this.Position = Position;
            this.MinDistance = MinDistance;
            this.MaxDistance = MaxDistance;
        }

    }

    enum Reverb_Props_Presets
    {
            OFF,             
            GENERIC,        
            PADDEDCELL,     
            ROOM,           
            BATHROOM,       
            LIVINGROOM,     
            STONEROOM,       
            AUDITORIUM,      
            CONCERTHALL,     
            CAVE,           
            ARENA,           
            HANGAR,         
            CARPETTEDHALLWAY,
            HALLWAY,        
            STONECORRIDOR, 
            ALLEY,           
            FOREST,          
            CITY,           
            MOUNTAINS,       
            QUARRY,          
            PLAIN,           
            PARKINGLOT,     
            SEWERPIPE,     
            UNDERWATER,
    }
    
    class Reverb_Area : Actor
    {
        public static REVERB_PROPERTIES LoadPreset(Reverb_Props_Presets p)
        {
            if (p == Reverb_Props_Presets.ALLEY)
            {
                return PRESET.ALLEY();
            }
            if (p == Reverb_Props_Presets.GENERIC)
            {
                return PRESET.GENERIC();
            }
            if (p == Reverb_Props_Presets.PADDEDCELL)
            {
                return PRESET.PADDEDCELL();
            }
            if (p == Reverb_Props_Presets.LIVINGROOM)
            {
                return PRESET.LIVINGROOM();
            }
            if (p == Reverb_Props_Presets.BATHROOM)
            {
                return PRESET.BATHROOM();
            }
            if (p == Reverb_Props_Presets.STONEROOM)
            {
                return PRESET.STONEROOM();
            }
            if (p == Reverb_Props_Presets.AUDITORIUM)
            {
                return PRESET.AUDITORIUM();
            }
            if (p == Reverb_Props_Presets.CONCERTHALL)
            {
                return PRESET.CONCERTHALL();
            }
            if (p == Reverb_Props_Presets.CAVE)
            {
                return PRESET.CAVE();
            }
            if (p == Reverb_Props_Presets.ARENA)
            {
                return PRESET.ARENA();
            }
            if (p == Reverb_Props_Presets.HANGAR)
            {
                return PRESET.HANGAR();
            }
            if (p == Reverb_Props_Presets.CARPETTEDHALLWAY)
            {
                return PRESET.CARPETTEDHALLWAY();
            }
            if (p == Reverb_Props_Presets.HALLWAY)
            {
                return PRESET.HALLWAY();
            }
            if (p == Reverb_Props_Presets.STONECORRIDOR)
            {
                return PRESET.STONECORRIDOR();
            }
            if (p == Reverb_Props_Presets.FOREST)
            {
                return PRESET.FOREST();
            }
            if (p == Reverb_Props_Presets.CITY)
            {
                return PRESET.CITY();
            }
            if (p == Reverb_Props_Presets.MOUNTAINS)
            {
                return PRESET.MOUNTAINS();
            }
            if (p == Reverb_Props_Presets.QUARRY)
            {
                return PRESET.QUARRY();
            }
            if (p == Reverb_Props_Presets.PARKINGLOT)
            {
                return PRESET.PARKINGLOT();
            }
            if (p == Reverb_Props_Presets.SEWERPIPE)
            {
                return PRESET.SEWERPIPE();
            }
            if (p == Reverb_Props_Presets.UNDERWATER)
            {
                return PRESET.UNDERWATER();
            }
            else
            {
                return PRESET.OFF();
            }
        }

        protected Reverb3D reverb;
        protected float Z;
        public REVERB_PROPERTIES Properties;
        public float MaxDistance = 0;
        public float MinDistance = 0;

        public Reverb3D Reverb3D { get { return reverb; } set { reverb = value; } }

        public RESULT SetReverbProperties(ref REVERB_PROPERTIES prop)
        {
            this.Properties = prop;
            RESULT r =reverb.setProperties(ref prop);
            return r;
        }
        public RESULT Set3DAttributes(ref VECTOR pos, float MinDist, float MaxDist)
        {
            this.Collision.X = pos.x;
            Collision.Y = pos.y;
            Z = pos.z;
            MaxDistance = MaxDist;
            MinDistance = MinDist;
            RESULT r = reverb.set3DAttributes(ref pos, MinDist, MaxDist);
            return r;
        }

        public Reverb_Area(Reverb3D Reverb,Reverb_Attributes attribs, ref REVERB_PROPERTIES prop,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
            this.Reverb3D = Reverb;
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(Reverb3D Reverb, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            this.Reverb3D = Reverb;
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(Reverb3D Reverb, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            this.Reverb3D = Reverb;
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(Reverb3D Reverb, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            this.Reverb3D = Reverb;
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }

        public Reverb_Area(FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }

        public Reverb_Area(ref FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(ref FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(ref FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }
        public Reverb_Area(ref FMOD.System system, Reverb_Attributes attribs, ref REVERB_PROPERTIES prop, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            Reverb3D = null;
            system.createReverb3D(out reverb);
            SetReverbProperties(ref prop);
            Set3DAttributes(ref attribs.Position, attribs.MinDistance, attribs.MaxDistance);
        }

    }

    class Ambient_Sound : Actor
    {
        protected SoundData sound;
        public bool Looped;
        public Sound Sound { get { return sound.sound; } set { sound.sound = value; } }
        public string Name { get { return sound.Name; } set { sound.Name = value; } }
        public bool Paused { get { return sound.Paused; } set { sound.Paused = value; } }
        public Channel Channel { get { return sound.channel; } set { sound.channel = value; } }
        public float GetMaxDistance()
        {
            float res;
            float tmp;
            sound.sound.get3DMinMaxDistance(out tmp, out res);
            return res;
        }
        public float GetMinDistance()
        {
            float res;
            float tmp;
            sound.sound.get3DMinMaxDistance(out res, out tmp);
            return res;
        }
        public RESULT SetMinMaxDistance(float min,float max)
        {
            RESULT r = sound.sound.set3DMinMaxDistance(min, max);
            return r;
        }
        public Ambient_Sound(SoundData sound,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
            this.sound = sound;
        }
        public Ambient_Sound(SoundData sound,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            this.sound = sound;
        }
        public Ambient_Sound(SoundData sound,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            this.sound = sound;
        }
        public Ambient_Sound(SoundData sound,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            this.sound = sound;
        }
    }

}

namespace Engine
{
    enum TriggerType
    {
        Once,One_Side_Once,Multi,One_Side_Multi
    }

    class Trigger : Actor
    {
        public TriggerType Type = TriggerType.Once;
        /// <summary>
        /// If Type Set to One_Side_Once or One_Side_Multi
        /// </summary>
        public MoveDirection EnterDirection = MoveDirection.NONE;
        /// <summary>
        /// If type Set to Once or One_Side_Once
        /// </summary>
        public bool Used = false;

        
        public Trigger(TriggerType Type, MoveDirection EnterDirection,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image,Layer,HasCollision,collision,DrawAsSprite,AnimateImage)
        {
            this.Type = Type;
            this.EnterDirection = EnterDirection;
        }
        public Trigger(TriggerType Type, MoveDirection EnterDirection,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision,pt,DrawAsSprite, AnimateImage)
        {
            this.Type = Type;
            this.EnterDirection = EnterDirection;
        }
        public Trigger(TriggerType Type, MoveDirection EnterDirection, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            this.Type = Type;
            this.EnterDirection = EnterDirection;
        }
        public Trigger(TriggerType Type, MoveDirection EnterDirection, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            this.Type = Type;
            this.EnterDirection = EnterDirection;
        }
    }

    class Trigger_Change_Layer : Trigger
    {
        public int LayerToChangeTo = 0;
        public Trigger_Change_Layer(TriggerType Type, MoveDirection EnterDirection, int LayerToChangeTo,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(Type,EnterDirection, image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
            this.LayerToChangeTo = LayerToChangeTo;
        }
        public Trigger_Change_Layer(TriggerType Type, MoveDirection EnterDirection, int LayerToChangeTo,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(Type, EnterDirection, image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            this.LayerToChangeTo = LayerToChangeTo;
        }
        public Trigger_Change_Layer(TriggerType Type, MoveDirection EnterDirection, int LayerToChangeTo,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(Type, EnterDirection, image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            this.LayerToChangeTo = LayerToChangeTo;
        }
        public Trigger_Change_Layer(TriggerType Type, MoveDirection EnterDirection, int LayerToChangeTo,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(Type, EnterDirection, image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            this.LayerToChangeTo = LayerToChangeTo;
        }
        public void Dispose()
        {
            this.image.Dispose();
        }
    }

    class Trigger_Damage:Trigger
    {
        public int Damage=0;
        public Trigger_Damage(int Damage,TriggerType Type, MoveDirection EnterDirection, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(Type, EnterDirection, image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
           
        }
        public Trigger_Damage(int Damage,TriggerType Type, MoveDirection EnterDirection, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(Type, EnterDirection, image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            
        }
        public Trigger_Damage(int Damage,TriggerType Type, MoveDirection EnterDirection, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(Type, EnterDirection, image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            
        }
        public Trigger_Damage(int Damage,TriggerType Type, MoveDirection EnterDirection, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(Type, EnterDirection, image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            
        }

    }
    /// <summary>
    /// Func class(doesn't do anything). Class for different in-game logic(Can not be used as triggers or actors)
    /// </summary>
    class Func_Base : Actor
    {
        public Func_Base(Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {

        }
        public Func_Base(Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {

        }
        public Func_Base(Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {

        }
        public Func_Base(Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {

        }
    }

    /// <summary>
    /// Allows to view object of another layer. Object can not be interacted with.
    /// </summary>
    class Func_View_Layer : Func_Base
    {
        public int TargetLayer=0;
        public RectangleF TargetArea=new RectangleF(0,0,0,0);
        public Func_View_Layer(int TargetLayer, RectangleF TargetArea,Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, collision, DrawAsSprite, AnimateImage)
        {
            this.TargetLayer = TargetLayer;
            this.TargetArea = TargetArea;
        }
        public Func_View_Layer(int TargetLayer, RectangleF TargetArea, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite, AnimateImage)
        {
            this.TargetLayer = TargetLayer;
            this.TargetArea = TargetArea;
        }
        public Func_View_Layer(int TargetLayer, RectangleF TargetArea, Image image, int Layer, bool HasCollision, RectangleF collision, bool DrawAsSprite) : base(image, Layer, HasCollision, collision, DrawAsSprite, false)
        {
            this.TargetLayer = TargetLayer;
            this.TargetArea = TargetArea;
        }
        public Func_View_Layer(int TargetLayer, RectangleF TargetArea, Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite, false)
        {
            this.TargetLayer = TargetLayer;
            this.TargetArea = TargetArea;
        }
    }

}

namespace Engine
{
    class Point_Entity : Actor
    {
        public float Scale = 1;
        public Point_Entity(float Scale,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite, bool AnimateImage) : base(image, Layer, HasCollision, pt, DrawAsSprite,AnimateImage)
        {
            this.Scale = Scale;
            Collision.Width = image.Width * Scale;
            Collision.Height = image.Height * Scale;
        }
        public Point_Entity(float Scale,Image image, int Layer, bool HasCollision, PointF pt, bool DrawAsSprite) : base(image, Layer, HasCollision, pt, DrawAsSprite)
        {
            this.Scale = Scale;
            Collision.Width = image.Width * Scale;
            Collision.Height = image.Height * Scale;
        }
    }
}



