using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter
{
    class Rectangles : Shape
    {
        private Pen _pen = new Pen(Color.Red, PEN_SIZE); //筆刷
        public Rectangles(Point locationOfTopPoint) //建構元
        {
            this._locationOfTopPoint = locationOfTopPoint;
            SetLocationOfPaintFirstPoint();
        }

        public override void HookDraw(Graphics graphics) //畫方形
        {
            graphics.DrawRectangle(_pen, _locationOfTopPoint.X + _moveingXOffset, _locationOfTopPoint.Y + _moveingYOffset, Wideth, Height); //畫方形 (使指標在方形正中間)
            graphics.FillRectangle(Brushes.LightPink, _locationOfTopPoint.X + _moveingXOffset, _locationOfTopPoint.Y + _moveingYOffset, Wideth, Height);
        }

        public override bool ContainsInShape(int x, int y) //檢查座標x,y是否在圖形內
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddRectangle(new Rectangle(_locationOfTopPoint, new Size(Wideth, Height)));
            return path.IsVisible(x, y);
        }
    }
}
