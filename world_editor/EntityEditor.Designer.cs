namespace world_editor
{
    partial class EntityEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxTriger = new System.Windows.Forms.GroupBox();
            this.groupReverb = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textZ = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.textMax = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPreset = new System.Windows.Forms.ComboBox();
            this.checkBoxSide = new System.Windows.Forms.CheckBox();
            this.groupBoxChangeLayer = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDir = new System.Windows.Forms.ComboBox();
            this.checkOneUse = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.groupPlayerStart = new System.Windows.Forms.GroupBox();
            this.textWidth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textHeight = new System.Windows.Forms.NumericUpDown();
            this.groupBoxTriger.SuspendLayout();
            this.groupReverb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textMax)).BeginInit();
            this.groupBoxChangeLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupPlayerStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layer";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Trigger Change Layer",
            "Sound Reverb",
            "Player Startpoint"});
            this.comboBoxType.Location = new System.Drawing.Point(15, 27);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(213, 21);
            this.comboBoxType.TabIndex = 1;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBoxType.SelectionChangeCommitted += new System.EventHandler(this.comboBoxType_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Entity Type";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // groupBoxTriger
            // 
            this.groupBoxTriger.Controls.Add(this.checkBoxSide);
            this.groupBoxTriger.Controls.Add(this.groupBoxChangeLayer);
            this.groupBoxTriger.Controls.Add(this.comboBoxDir);
            this.groupBoxTriger.Controls.Add(this.checkOneUse);
            this.groupBoxTriger.Controls.Add(this.numericUpDown1);
            this.groupBoxTriger.Controls.Add(this.label1);
            this.groupBoxTriger.Location = new System.Drawing.Point(15, 70);
            this.groupBoxTriger.Name = "groupBoxTriger";
            this.groupBoxTriger.Size = new System.Drawing.Size(213, 350);
            this.groupBoxTriger.TabIndex = 3;
            this.groupBoxTriger.TabStop = false;
            this.groupBoxTriger.Text = "Data";
            this.groupBoxTriger.Enter += new System.EventHandler(this.groupBoxTriger_Enter);
            // 
            // groupReverb
            // 
            this.groupReverb.Controls.Add(this.label7);
            this.groupReverb.Controls.Add(this.textZ);
            this.groupReverb.Controls.Add(this.label6);
            this.groupReverb.Controls.Add(this.textMin);
            this.groupReverb.Controls.Add(this.label5);
            this.groupReverb.Controls.Add(this.textMax);
            this.groupReverb.Controls.Add(this.label4);
            this.groupReverb.Controls.Add(this.comboBoxPreset);
            this.groupReverb.Location = new System.Drawing.Point(16, 54);
            this.groupReverb.Name = "groupReverb";
            this.groupReverb.Size = new System.Drawing.Size(213, 366);
            this.groupReverb.TabIndex = 9;
            this.groupReverb.TabStop = false;
            this.groupReverb.Text = "Data";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Z";
            // 
            // textZ
            // 
            this.textZ.Location = new System.Drawing.Point(10, 160);
            this.textZ.Name = "textZ";
            this.textZ.Size = new System.Drawing.Size(120, 20);
            this.textZ.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Min Distance";
            // 
            // textMin
            // 
            this.textMin.Location = new System.Drawing.Point(10, 118);
            this.textMin.Name = "textMin";
            this.textMin.Size = new System.Drawing.Size(120, 20);
            this.textMin.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Max Distance";
            // 
            // textMax
            // 
            this.textMax.Location = new System.Drawing.Point(10, 79);
            this.textMax.Name = "textMax";
            this.textMax.Size = new System.Drawing.Size(120, 20);
            this.textMax.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Preset Name";
            // 
            // comboBoxPreset
            // 
            this.comboBoxPreset.FormattingEnabled = true;
            this.comboBoxPreset.Location = new System.Drawing.Point(3, 36);
            this.comboBoxPreset.Name = "comboBoxPreset";
            this.comboBoxPreset.Size = new System.Drawing.Size(204, 21);
            this.comboBoxPreset.TabIndex = 0;
            // 
            // checkBoxSide
            // 
            this.checkBoxSide.AutoSize = true;
            this.checkBoxSide.Location = new System.Drawing.Point(9, 64);
            this.checkBoxSide.Name = "checkBoxSide";
            this.checkBoxSide.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSide.TabIndex = 7;
            this.checkBoxSide.Text = "One Sided?";
            this.checkBoxSide.UseVisualStyleBackColor = true;
            // 
            // groupBoxChangeLayer
            // 
            this.groupBoxChangeLayer.Controls.Add(this.label3);
            this.groupBoxChangeLayer.Controls.Add(this.numericUpDown2);
            this.groupBoxChangeLayer.Location = new System.Drawing.Point(6, 115);
            this.groupBoxChangeLayer.Name = "groupBoxChangeLayer";
            this.groupBoxChangeLayer.Size = new System.Drawing.Size(200, 118);
            this.groupBoxChangeLayer.TabIndex = 6;
            this.groupBoxChangeLayer.TabStop = false;
            this.groupBoxChangeLayer.Enter += new System.EventHandler(this.groupBoxChangeLayer_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Target Layer";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(9, 29);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 0;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // comboBoxDir
            // 
            this.comboBoxDir.FormattingEnabled = true;
            this.comboBoxDir.Location = new System.Drawing.Point(9, 88);
            this.comboBoxDir.Name = "comboBoxDir";
            this.comboBoxDir.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDir.TabIndex = 4;
            this.comboBoxDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxDir_SelectedIndexChanged);
            // 
            // checkOneUse
            // 
            this.checkOneUse.AutoSize = true;
            this.checkOneUse.Location = new System.Drawing.Point(9, 40);
            this.checkOneUse.Name = "checkOneUse";
            this.checkOneUse.Size = new System.Drawing.Size(141, 17);
            this.checkOneUse.TabIndex = 5;
            this.checkOneUse.Text = "Can be used only once?";
            this.checkOneUse.UseVisualStyleBackColor = true;
            this.checkOneUse.CheckedChanged += new System.EventHandler(this.checkOneUse_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(45, 14);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 426);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupPlayerStart
            // 
            this.groupPlayerStart.Controls.Add(this.label9);
            this.groupPlayerStart.Controls.Add(this.textHeight);
            this.groupPlayerStart.Controls.Add(this.label8);
            this.groupPlayerStart.Controls.Add(this.textWidth);
            this.groupPlayerStart.Location = new System.Drawing.Point(16, 63);
            this.groupPlayerStart.Name = "groupPlayerStart";
            this.groupPlayerStart.Size = new System.Drawing.Size(212, 357);
            this.groupPlayerStart.TabIndex = 9;
            this.groupPlayerStart.TabStop = false;
            this.groupPlayerStart.Text = "Data";
            // 
            // textWidth
            // 
            this.textWidth.Location = new System.Drawing.Point(6, 36);
            this.textWidth.Name = "textWidth";
            this.textWidth.Size = new System.Drawing.Size(120, 20);
            this.textWidth.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Width";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Height";
            // 
            // textHeight
            // 
            this.textHeight.Location = new System.Drawing.Point(6, 88);
            this.textHeight.Name = "textHeight";
            this.textHeight.Size = new System.Drawing.Size(120, 20);
            this.textHeight.TabIndex = 2;
            // 
            // EntityEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 458);
            this.Controls.Add(this.groupReverb);
            this.Controls.Add(this.groupPlayerStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxTriger);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxType);
            this.Name = "EntityEditor";
            this.ShowIcon = false;
            this.Text = "EntityEditor";
            this.Load += new System.EventHandler(this.EntityEditor_Load);
            this.groupBoxTriger.ResumeLayout(false);
            this.groupBoxTriger.PerformLayout();
            this.groupReverb.ResumeLayout(false);
            this.groupReverb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textMax)).EndInit();
            this.groupBoxChangeLayer.ResumeLayout(false);
            this.groupBoxChangeLayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupPlayerStart.ResumeLayout(false);
            this.groupPlayerStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.GroupBox groupBoxTriger;
        public System.Windows.Forms.ComboBox comboBoxType;
        public System.Windows.Forms.GroupBox groupBoxChangeLayer;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        public System.Windows.Forms.ComboBox comboBoxDir;
        public System.Windows.Forms.CheckBox checkOneUse;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox checkBoxSide;
        private System.Windows.Forms.GroupBox groupReverb;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxPreset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown textMax;
        public System.Windows.Forms.NumericUpDown textZ;
        public System.Windows.Forms.NumericUpDown textMin;
        private System.Windows.Forms.GroupBox groupPlayerStart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown textHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown textWidth;
    }
}