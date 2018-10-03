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
using Engine.SoundWork;
using System.IO;
using FMOD;


namespace world_editor
{
 
    public partial class Form1 : Form
    {
        public FMOD.Studio.System Sound_System = null;

        
        RectangleF rect;
        Graphics graphics;
        PointF rectStartPoint = new PointF(0, 0);
        bool LeftMouseDown = false;
        bool RectIsEmrty = false;
        List<RectangleF> rects = new List<RectangleF>();
        List<Actor> actors = new List<Actor>();
        List<Texture> textures = new List<Texture>();
        Tool CurrectTool = Tool.Rect;
        public string CurrentEntTool = "Player Startpoint";
        int CurrentLayer = 0;
        Actor SelectedActor;
        Texture GetTextureByName(string name)
        {
            if (textures.Count > 0)
            {
                for (int i = 0; i < textures.Count; i++)
                {
                    if (textures.ElementAt(i).Name == name)
                    {
                        return textures.ElementAt(i);
                    }
                }
                return null;
            }
            return null;
        }
        Texture CurrentTexture;
        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            rect = new RectangleF(0, 0, 0, 0);
            graphics = CreateGraphics();
            List<string> vs= Directory.GetFiles("assets\\models\\", "*png").ToList();
            if (vs.Count > 0)
            {
                float per = 100 / vs.Count;
                LoadingForm lf = new LoadingForm();
                lf.Show(this);
                lf.progressBar1.Value = 0;
                lf.label1.Text = "";
                for(int i = 0; i < vs.Count; i++)
                {
                    //vs.ElementAt(i).Substring(
                    textures.Add(new Texture(Image.FromFile(vs.ElementAt(i)), vs.ElementAt(i)));
                    TexturesBox.Items.Add(vs.ElementAt(i));
                    lf.label1.Text = vs.ElementAt(i);
                    if (lf.progressBar1.Value + Convert.ToInt32(per) < 0)
                    {
                        lf.progressBar1.Value += Convert.ToInt32(per);
                    }
                    lf.Close();
                }
            }
            for(int i = 0; i < 1; i++)
            {
                comboBoxEnt.Items.Add("Player Startpoint");
                comboBoxEnt.Items.Add("Sound Reverb");
                comboBoxEnt.Items.Add("Trigger Change Layer");
                comboBoxEnt.Items.Add("View Layer");
                comboBoxEnt.Items.Add("Trigger Damage");

            }
            RESULT res = FMOD.Studio.System.create(out Sound_System);
            if (res == RESULT.OK)
            {
                res = Sound_System.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, INITFLAGS.NORMAL, new IntPtr(0));
                if (res != RESULT.OK)
                {
                    throw (new ApplicationException("Unable to init sound_system"));
                }
            }
            else
            {
                throw (new ApplicationException("Unable to create sound_system"));
            }
            timer1.Start();
        }




        private void Form1_Load(object sender, EventArgs e)
        {

            textures.Add(new Texture(Image.FromFile("assets\\models\\floor_checkboard_texture.png"), "Floor_Checkboard"));
            pictureBox1.Image = GetTextureByName("Floor_Checkboard").image;
            CurrentTexture = GetTextureByName("Floor_Checkboard");
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CurrectTool == Tool.Rect)
                {
                    LeftMouseDown = true;
                    rect.X = e.X;
                    rect.Y = e.Y;
                    rectStartPoint.X = e.X;
                    rectStartPoint.Y = e.Y;
                    RectIsEmrty = false;
                }
                if (CurrectTool == Tool.Ent)
                {
                    if(CurrentEntTool!="Player Startpoint")
                    {
                        LeftMouseDown = true;
                        rect.X = e.X;
                        rect.Y = e.Y;
                        rectStartPoint.X = e.X;
                        rectStartPoint.Y = e.Y;
                        RectIsEmrty = false;
                    }
                    else
                    {

                    }
                }
                
            }
            if (e.Button == MouseButtons.Right)
            {
                if (actors.Count > 0)
                {
                    for (int i = actors.Count - 1; i >= 0; i--)
                    {
                        if (actors.ElementAt(i).Collision.Contains(e.X, e.Y))
                        {
                            SelectedActor = actors.ElementAt(i);
                            rect = SelectedActor.Collision;
                            ObjectContextMenu.Show(new Point(this.Location.X + e.X, this.Location.Y + e.Y));
                            break;
                        }
                    }
                }
                
                
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rect != new RectangleF(0, 0, 0, 0))
                {
                    if (!RectIsEmrty)
                    {

                        rects.Add(rect);
                        if(CurrectTool==Tool.Ent)
                        {
                            EntityEditor ee = new EntityEditor(CurrentEntTool);
                            ee.Show(this);
                            if(CurrentEntTool== "Trigger Change Layer")
                            {
                                ee.comboBoxType.SelectedItem="Trigger Change Layer";
                            }
                        }
                        actors.Add(new Actor(CurrentTexture.image, 0, checkCollision.Checked, rect, checkSprite.Checked, false));
                        rect = new RectangleF(0, 0, 0, 0);
                        RectIsEmrty = true;
                    }
                }
            }
            Refresh();
        }
        private void CheckBox_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EntityEditor ee = new EntityEditor(CurrentEntTool);
                if (rect != new RectangleF(0, 0, 0, 0))
                {
                    if (!RectIsEmrty)
                    {

                        rects.Add(rect);
                        if (CurrectTool == Tool.Ent)
                        {

                            ee.ShowDialog(this);
                            if (CurrentEntTool == "Trigger Change Layer")
                            {
                                ee.comboBoxType.SelectedItem = "Trigger Change Layer";
                                MoveDirection md = MoveDirection.NONE;
                                TriggerType tt = TriggerType.Once;
                                int layer = Convert.ToInt32(ee.numericUpDown1.Value);
                                int tlayer = Convert.ToInt32(ee.numericUpDown2.Value);
                                Enum.TryParse(ee.comboBoxDir.SelectedItem.ToString(), out md);
                                if (ee.checkOneUse.Checked == true)
                                {
                                    if (ee.checkBoxSide.Checked == true)
                                    {
                                        tt = TriggerType.One_Side_Once;
                                    }
                                    else
                                    {
                                        tt = TriggerType.One_Side_Multi;
                                    }
                                }
                                else
                                {
                                    if (ee.checkBoxSide.Checked == true)
                                    {
                                        tt = TriggerType.Once;
                                    }
                                    else
                                    {
                                        tt = TriggerType.Multi;
                                    }
                                }

                                actors.Add(new Trigger_Change_Layer(tt, md, tlayer, CurrentTexture.image, layer, checkCollision.Checked, rect, checkSprite.Checked, false));
                            }
                            if (CurrentEntTool == "Sound Reverb")
                            {
                                ee.comboBoxType.SelectedItem = "Sound Reverb";
                                FMOD.System system = null;
                                this.Sound_System.getLowLevelSystem(out system);
                                Reverb_Props_Presets preset = Reverb_Props_Presets.OFF;
                                Enum.TryParse<Reverb_Props_Presets>(ee.comboBoxPreset.SelectedItem.ToString(), out preset);
                                FMOD.REVERB_PROPERTIES prop = Reverb_Area.LoadPreset(preset);
                                Reverb_Attributes ra = new Reverb_Attributes(Vector.ToVECTOR(rect.X, rect.Y, Convert.ToSingle(ee.textZ.Value)), Convert.ToSingle(ee.textMax.Value), Convert.ToSingle(ee.textMin.Value));

                                actors.Add(new Reverb_Area(ref system, ra, ref prop, CurrentTexture.image, CurrentLayer, checkCollision.Checked, rect, checkSprite.Checked, false));
                            }
                        }
                        else
                        {
                            actors.Add(new Actor(CurrentTexture.image, 0, checkCollision.Checked, rect, checkSprite.Checked, false));
                        }
                        rect = new RectangleF(0, 0, 0, 0);
                        RectIsEmrty = true;
                    }
                }
            }
            Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (LeftMouseDown == true && RectIsEmrty == false)
                {
                    if (e.Y >= rectStartPoint.Y)
                    {
                        rect.Height = e.Y - rectStartPoint.Y;
                    }
                    else
                    {
                        float h = rectStartPoint.Y - e.Y;
                        rect.Y = rectStartPoint.Y - h;
                        rect.Height = h;
                    }
                    if (e.X >= rectStartPoint.X)
                    {
                        rect.Width = e.X - rectStartPoint.X;
                    }
                    else
                    {
                        float w = rectStartPoint.X - e.X;
                        rect.X = rectStartPoint.X - w;
                        rect.Width = w;
                    }

                }

                //if (rect != new RectangleF(0, 0, 0, 0))
                //{
                //    graphics.Clear(Color.White);
                //    graphics.DrawRectangle(new Pen(Color.Black, 2), rect.X, rect.Y, rect.Width, rect.Height);
                //    if (!checkSprite.Checked)
                //    {
                //        graphics.FillRectangle(new TextureBrush(CurrentTexture.image), rect);
                //    }
                //    else
                //    {
                //        graphics.DrawImage(CurrentTexture.image, rect);
                //    }
                //}
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LeftMouseDown = false;
            }
            if (e.Button == MouseButtons.Middle)
            {
                Console.Write("000");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (rect != new RectangleF(0, 0, 0, 0))
            {
                graphics.DrawRectangle(new Pen(Color.Black,2), rect.X, rect.Y, rect.Width, rect.Height);
                if (!checkSprite.Checked)
                {
                    graphics.FillRectangle(new TextureBrush(CurrentTexture.image), rect);
                }
                else
                {
                    graphics.DrawImage(CurrentTexture.image, rect);
                }

            }
            if (actors.Count > 0)
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    if (actors.ElementAt(i).Layer == CurrentLayer)
                    {
                        if (actors.ElementAt(i).DrawAsSprite == true)
                        {
                            graphics.DrawImage(actors.ElementAt(i).image, actors.ElementAt(i).Collision);
                        }
                        else
                        {
                            graphics.FillRectangle(new TextureBrush(actors.ElementAt(i).image), actors.ElementAt(i).Collision);
                        }
                    }

                }
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
           
            
        }

        private void TexturesBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentTexture = GetTextureByName(TexturesBox.SelectedItem.ToString());
            pictureBox1.Image = CurrentTexture.image;
        }

        private void checkCollision_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void checkPointer_CheckedChanged(object sender, EventArgs e)
        {
            CurrectTool = Tool.Pointer;
        }

        private void checkDrawRect_CheckedChanged(object sender, EventArgs e)
        {
            CurrectTool = Tool.Rect;
        }

        private void checkEnt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            CurrectTool = Tool.Pointer;
            groupBox1.Enabled = false;
            groupBox1.Visible = false;

            EntGroup.Visible = false;
            EntGroup.Enabled = false;
        }

        private void buttonRect_Click(object sender, EventArgs e)
        {
            CurrectTool = Tool.Rect;
            groupBox1.Enabled = true;
            groupBox1.Visible = true;

            EntGroup.Visible = false;
            EntGroup.Enabled = false;
        }

        private void buttonEnt_Click(object sender, EventArgs e)
        {
            CurrectTool = Tool.Ent;
            groupBox1.Enabled = true;
            groupBox1.Visible = true;
            EntGroup.Visible = true;
            EntGroup.Enabled = true;
        }

        private void comboBoxEnt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEnt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CurrentEntTool = comboBoxEnt.SelectedItem.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CurrentLayer = Convert.ToInt32(numericUpDown1.Value);
        }

        private void ObjectContextMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void itemDelete_Click(object sender, EventArgs e)
        {
            actors.Remove(SelectedActor);
            SelectedActor = null;
            rect = new RectangleF(0, 0, 0, 0);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void entTextureBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void checkCollision_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    
}
