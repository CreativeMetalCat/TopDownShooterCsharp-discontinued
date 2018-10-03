namespace world_editor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TexturesBox = new System.Windows.Forms.ComboBox();
            this.checkCollision = new System.Windows.Forms.CheckBox();
            this.checkSprite = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonEnt = new System.Windows.Forms.Button();
            this.buttonRect = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.EntGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEnt = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ObjectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.EntGroup.SuspendLayout();
            this.ObjectContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TexturesBox);
            this.groupBox1.Controls.Add(this.checkCollision);
            this.groupBox1.Controls.Add(this.checkSprite);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(1237, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 165);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Texture";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Texture";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // TexturesBox
            // 
            this.TexturesBox.FormattingEnabled = true;
            this.TexturesBox.Location = new System.Drawing.Point(6, 138);
            this.TexturesBox.Name = "TexturesBox";
            this.TexturesBox.Size = new System.Drawing.Size(188, 21);
            this.TexturesBox.TabIndex = 3;
            this.TexturesBox.TabStop = false;
            this.TexturesBox.SelectionChangeCommitted += new System.EventHandler(this.TexturesBox_SelectionChangeCommitted);
            this.TexturesBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckBox_KeyDown);
            // 
            // checkCollision
            // 
            this.checkCollision.AutoSize = true;
            this.checkCollision.Location = new System.Drawing.Point(110, 42);
            this.checkCollision.Name = "checkCollision";
            this.checkCollision.Size = new System.Drawing.Size(86, 17);
            this.checkCollision.TabIndex = 2;
            this.checkCollision.TabStop = false;
            this.checkCollision.Text = "Has Collision";
            this.checkCollision.UseVisualStyleBackColor = true;
            this.checkCollision.CheckedChanged += new System.EventHandler(this.checkCollision_CheckedChanged);
            this.checkCollision.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckBox_KeyDown);
            this.checkCollision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkCollision_KeyPress);
            // 
            // checkSprite
            // 
            this.checkSprite.AutoSize = true;
            this.checkSprite.Location = new System.Drawing.Point(110, 19);
            this.checkSprite.Name = "checkSprite";
            this.checkSprite.Size = new System.Drawing.Size(90, 17);
            this.checkSprite.TabIndex = 1;
            this.checkSprite.TabStop = false;
            this.checkSprite.Text = "DrawAsSprite";
            this.checkSprite.UseVisualStyleBackColor = true;
            this.checkSprite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckBox_KeyDown);
            this.checkSprite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkCollision_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.buttonEnt);
            this.groupBox2.Controls.Add(this.buttonRect);
            this.groupBox2.Controls.Add(this.buttonSelect);
            this.groupBox2.Location = new System.Drawing.Point(1237, 356);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 389);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Layer";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(102, 33);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.TabStop = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // buttonEnt
            // 
            this.buttonEnt.Image = ((System.Drawing.Image)(resources.GetObject("buttonEnt.Image")));
            this.buttonEnt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEnt.Location = new System.Drawing.Point(3, 99);
            this.buttonEnt.Name = "buttonEnt";
            this.buttonEnt.Size = new System.Drawing.Size(93, 37);
            this.buttonEnt.TabIndex = 2;
            this.buttonEnt.TabStop = false;
            this.buttonEnt.Text = "Entity";
            this.buttonEnt.UseVisualStyleBackColor = true;
            this.buttonEnt.Click += new System.EventHandler(this.buttonEnt_Click);
            // 
            // buttonRect
            // 
            this.buttonRect.Image = ((System.Drawing.Image)(resources.GetObject("buttonRect.Image")));
            this.buttonRect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRect.Location = new System.Drawing.Point(3, 56);
            this.buttonRect.Name = "buttonRect";
            this.buttonRect.Size = new System.Drawing.Size(93, 37);
            this.buttonRect.TabIndex = 1;
            this.buttonRect.TabStop = false;
            this.buttonRect.Text = "Rectangle";
            this.buttonRect.UseVisualStyleBackColor = true;
            this.buttonRect.Click += new System.EventHandler(this.buttonRect_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Image = global::world_editor.Properties.Resources.abstr1;
            this.buttonSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelect.Location = new System.Drawing.Point(3, 16);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(93, 37);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.TabStop = false;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // EntGroup
            // 
            this.EntGroup.Controls.Add(this.label1);
            this.EntGroup.Controls.Add(this.comboBoxEnt);
            this.EntGroup.Enabled = false;
            this.EntGroup.Location = new System.Drawing.Point(1237, 183);
            this.EntGroup.Name = "EntGroup";
            this.EntGroup.Size = new System.Drawing.Size(216, 167);
            this.EntGroup.TabIndex = 3;
            this.EntGroup.TabStop = false;
            this.EntGroup.Text = "EntGroup";
            this.EntGroup.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type";
            // 
            // comboBoxEnt
            // 
            this.comboBoxEnt.FormattingEnabled = true;
            this.comboBoxEnt.Location = new System.Drawing.Point(7, 35);
            this.comboBoxEnt.Name = "comboBoxEnt";
            this.comboBoxEnt.Size = new System.Drawing.Size(207, 21);
            this.comboBoxEnt.TabIndex = 0;
            this.comboBoxEnt.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnt_SelectedIndexChanged);
            this.comboBoxEnt.SelectionChangeCommitted += new System.EventHandler(this.comboBoxEnt_SelectionChangeCommitted);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ObjectContextMenu
            // 
            this.ObjectContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSelect,
            this.itemDelete});
            this.ObjectContextMenu.Name = "ObjectContextMenu";
            this.ObjectContextMenu.Size = new System.Drawing.Size(108, 48);
            this.ObjectContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ObjectContextMenu_Opening);
            // 
            // itemSelect
            // 
            this.itemSelect.Name = "itemSelect";
            this.itemSelect.Size = new System.Drawing.Size(107, 22);
            this.itemSelect.Text = "Select";
            // 
            // itemDelete
            // 
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.Size = new System.Drawing.Size(107, 22);
            this.itemDelete.Text = "Delete";
            this.itemDelete.Click += new System.EventHandler(this.itemDelete_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 821);
            this.Controls.Add(this.EntGroup);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.HelpButton = true;
            this.Name = "Form1";
            this.Text = "Map Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.EntGroup.ResumeLayout(false);
            this.EntGroup.PerformLayout();
            this.ObjectContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkSprite;
        private System.Windows.Forms.CheckBox checkCollision;
        private System.Windows.Forms.ComboBox TexturesBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonEnt;
        private System.Windows.Forms.Button buttonRect;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.GroupBox EntGroup;
        private System.Windows.Forms.ComboBox comboBoxEnt;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ContextMenuStrip ObjectContextMenu;
        private System.Windows.Forms.ToolStripMenuItem itemSelect;
        private System.Windows.Forms.ToolStripMenuItem itemDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}