using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter
{
    class Ellipse : Shape
    {
        private Pen _pen = new Pen(Color.Blue, PEN_SIZE); //筆刷

        public Ellipse(Point locationOfTopPoint)  //建構元
        {
            this._locationOfTopPoint = locationOfTopPoint;
            SetLocationOfPaintFirstPoint();
        }

        public override void HookDraw(Graphics graphics)  //畫圓
        {
            graphics.DrawEllipse(_pen, _locationOfTopPoint.X + _moveingXOffset, _locationOfTopPoint.Y + _moveingYOffset, Wideth, Height);//畫出園 (指標位置為圓心)
            graphics.FillEllipse(Brushes.LightBlue, _locationOfTopPoint.X + _moveingXOffset, _locationOfTopPoint.Y + _moveingYOffset, Wideth, Height);
        }

        public override bool ContainsInShape(int x, int y)  //檢查是否與圖案有交集
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddEllipse(new Rectangle(_locationOfTopPoint.X + _moveingXOffset, _locationOfTopPoint.Y + _moveingYOffset, Wideth, Height));
            return path.IsVisible(x, y);
        }
    }
}
