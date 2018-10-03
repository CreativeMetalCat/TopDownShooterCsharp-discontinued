using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Texture
{
    public Image image;
    public string Name = "";
    public Texture(Image image, string Name)
    {
        this.image = image;
        this.Name = Name;
    }
}

enum Tool
{
    Pointer,
    Rect,
    Ent
}