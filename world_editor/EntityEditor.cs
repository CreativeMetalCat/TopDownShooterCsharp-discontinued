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

namespace world_editor
{
    public partial class EntityEditor : Form
    {
        public bool Done = false;
        string tool = "";
        public EntityEditor(string EntTool)
        {
            InitializeComponent();
            comboBoxDir.Items.Add(MoveDirection.UP);
            comboBoxDir.Items.Add(MoveDirection.DOWN);
            comboBoxDir.Items.Add(MoveDirection.LEFT);
            comboBoxDir.Items.Add(MoveDirection.RIGHT);
            comboBoxDir.Items.Add(MoveDirection.NONE);

            comboBoxPreset.Items.Add(Reverb_Props_Presets.OFF);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.GENERIC);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.ALLEY);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.UNDERWATER);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.STONEROOM);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.STONECORRIDOR);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.SEWERPIPE);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.ROOM);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.QUARRY);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.PLAIN);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.PARKINGLOT);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.PADDEDCELL);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.MOUNTAINS);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.LIVINGROOM);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.HANGAR);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.HALLWAY);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.FOREST);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.CONCERTHALL);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.CITY);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.CAVE);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.CARPETTEDHALLWAY);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.BATHROOM);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.AUDITORIUM);
            comboBoxPreset.Items.Add(Reverb_Props_Presets.ARENA);
            tool = EntTool;

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxTriger_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxChangeLayer_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxDir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkOneUse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(comboBoxType.SelectedItem.ToString()=="Trigger Change Layer")
            {
                groupBoxChangeLayer.Enabled = true;
                groupBoxChangeLayer.Visible = true;

                groupBoxTriger.Visible = true;
                groupBoxTriger.Enabled = true;

                groupReverb.Enabled = false;
                groupReverb.Visible = false;

                groupPlayerStart.Enabled = false;
                groupPlayerStart.Visible = false;
            }
            else if(comboBoxType.SelectedItem.ToString() =="Sound Reverb")
            {
                groupBoxChangeLayer.Enabled = false;
                groupBoxChangeLayer.Visible = false;

                groupBoxTriger.Visible = false;
                groupBoxTriger.Enabled = false;

                groupReverb.Enabled = true;
                groupReverb.Visible = true;

                groupPlayerStart.Enabled = false;
                groupPlayerStart.Visible = false;
            }
            else if(comboBoxType.SelectedItem.ToString()== "Player Startpoint")
            {
                groupBoxChangeLayer.Enabled = false;
                groupBoxChangeLayer.Visible = false;

                groupBoxTriger.Visible = false;
                groupBoxTriger.Enabled = false;

                groupReverb.Enabled = false;
                groupReverb.Visible = false;

                groupPlayerStart.Enabled = true;
                groupPlayerStart.Visible = true;
            }
        }

        private void EntityEditor_Load(object sender, EventArgs e)
        {
            comboBoxType.SelectedItem = tool;
        }
    }
}
