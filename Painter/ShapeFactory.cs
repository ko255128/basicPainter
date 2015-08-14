using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Painter
{
    class ShapeFactory  //圖形工廠
    {
        enum DrawingType { Ellipse = 1, Rectangle, Line }; //圖形類型

        public Shape CreateShpae(int mode, Point locationOfTopPoint) //產生圖形
        {
            if (mode == (int)DrawingType.Ellipse)
            {
                return new Ellipse(locationOfTopPoint);
            }
            else if (mode == (int)DrawingType.Rectangle)
            {
                return new Rectangles(locationOfTopPoint);
            }
            else
            {
                return new Line(locationOfTopPoint);
            }
        }
    }
}
