using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Painter
{
    public class Shapes
    {
        private List<Shape> _shapes = new List<Shape>();  //所有圖形
        private ShapeFactory _shapeFactory = new ShapeFactory(); //宣告圖形工廠
        public Shape _focusShape = null;//目前選取的圖形

        public int NumberOfShape //回傳總圖形數
        {
            get
            {
                return _shapes.Count;
            }
        }

        public void InsertShape(int index, Shape shape) //插入圖形
        {
            _shapes.Insert(index, shape);
        }

        public void DeleteShape(int index) //刪除圖形
        {
            _shapes.RemoveAt(index);
        }

        public int FindShapeInedx(Shape targetShape) //找尋圖形Index
        {
            for (int i = 0; i < _shapes.Count; i++)
            {
                if (_shapes[i] == targetShape)
                    return i;
            }
            return -1; //not found
        }

        public Shape FocusShape //目前選取的圖形屬性
        {
            get
            {
                return _focusShape;
            }
        }

        public void FindFocusShape(int mouseX, int mouseY) //找尋被選取的圖形
        {
            if (_focusShape != null)
                DisposeFocusShape();
            for (int i = _shapes.Count() - 1; i >= 0; i--)
            {
                if (_shapes[i].ContainsInShape(mouseX, mouseY))
                {
                    _focusShape = _shapes[i];
                    _focusShape.IsFocus = true;
                    break;
                }
            }
        }

        public void DisposeFocusShape() //取消選取
        {
            if (_focusShape != null)
                _focusShape.IsFocus = false;
            _focusShape = null;
        }

        public void Draw(Graphics graphics)  //畫出所有圖形
        {
            foreach (Shape shape in _shapes)
            {
                shape.Draw(graphics);
            }
        }

        public void AddShape(int mode, Point locationOfTopPoint) //加入圖形
        {
            Shape newShape = _shapeFactory.CreateShpae(mode, locationOfTopPoint);
            _shapes.Add(newShape);
            _focusShape = newShape;
            _focusShape.IsFocus = true;
        }
    }
}
