using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter
{
    class Line : Shape
    {
        private Pen _pen = new Pen(Color.Black, PEN_SIZE); //筆刷

        public Line(Point locationOfTopPoint) //建構元 
        {
            this._locationOfTopPoint = locationOfTopPoint;
            SetLocationOfPaintFirstPoint();
            _isLine = true;
        }

        public override void HookDraw(Graphics graphics)  //畫圓
        {
            Point drawingFirstPoint = new Point(_locationOfPaintFirstPoint.X + _moveingXOffset, _locationOfPaintFirstPoint.Y + _moveingYOffset);
            Point drawingEndPoint = new Point(_locationOfPaintEndPoint.X + _moveingXOffset, _locationOfPaintEndPoint.Y + _moveingYOffset);
            graphics.DrawLine(_pen, drawingFirstPoint, drawingEndPoint);
        }

        public override bool ContainsInShape(int x, int y) //檢查座標是否圖形內
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddLine(_locationOfPaintFirstPoint.X + _moveingXOffset, _locationOfPaintFirstPoint.Y + _moveingYOffset,_locationOfPaintEndPoint.X + _moveingXOffset, _locationOfPaintEndPoint.Y + _moveingYOffset);
            path.Widen(_pen);
            return path.IsVisible(x, y);
        }
    }
}
