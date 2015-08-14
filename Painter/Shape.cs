using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter
{
    public abstract class Shape
    {
        protected Point _locationOfTopPoint;  //上方的點.
        protected Point _locationOfBottomPoint = new Point(); //底部的點
        protected Point _locationOfPaintFirstPoint = new Point(); //畫圖的起始點
        protected Point _locationOfPaintEndPoint = new Point(); //畫圖的終點

        protected bool _isLine = false; //是否為線
        protected int _moveingXOffset = 0; //移動X偏差值
        protected int _moveingYOffset = 0; //移動Y偏差值

        const int CONTROL_POINT_SIZE = 10; //控制點大小
        const int CONTROL_POINT_OFFSET = 5; //控制點偏差值

        public enum ChangePoint { LeftTop, LeftBottom, RightTop, RightBottom }; //控制典型別

        public void ExcuseMoving() //執行位移 (將Offset圖形中後 Offset歸零)
        {
            _locationOfPaintFirstPoint.X += _moveingXOffset;
            _locationOfPaintFirstPoint.Y += _moveingYOffset;
            _locationOfPaintEndPoint.X += _moveingXOffset;
            _locationOfPaintEndPoint.Y += _moveingYOffset;
            _moveingXOffset = 0;
            _moveingYOffset = 0;
        }

        private void LeftControlPointProcess(int locationX) //左邊控制點處理
        {
            if (_locationOfPaintFirstPoint.X < _locationOfPaintEndPoint.X)
            {
                if (locationX >= _locationOfPaintEndPoint.X)
                    _locationOfPaintFirstPoint.X = _locationOfPaintEndPoint.X - 1;
                else
                    _locationOfPaintFirstPoint.X = locationX;
            }
            else
            {
                if (locationX >= _locationOfPaintFirstPoint.X)
                    _locationOfPaintEndPoint.X = _locationOfPaintFirstPoint.X - 1;
                else
                    _locationOfPaintEndPoint.X = locationX;
            }
        }

        private void RightControlPointProcess(int locationX) //右邊控制點處理
        {
            if (_locationOfPaintFirstPoint.X < _locationOfPaintEndPoint.X)
            {
                if (locationX <= _locationOfPaintFirstPoint.X)
                    _locationOfPaintEndPoint.X = _locationOfPaintFirstPoint.X + 1;
                else
                    _locationOfPaintEndPoint.X = locationX;
            }
            else
            {
                if (locationX <= _locationOfPaintEndPoint.X)
                    _locationOfPaintFirstPoint.X = _locationOfPaintEndPoint.X + 1;
                else
                    _locationOfPaintFirstPoint.X = locationX;
            }
        }

        private void TopControlPointProcess(int locationY) //上方控制點處理
        {
            if (locationY >= _locationOfPaintEndPoint.Y)
                _locationOfPaintFirstPoint.Y = _locationOfPaintEndPoint.Y - 1;
            else
                _locationOfPaintFirstPoint.Y = locationY;
        }

        private void BottomControlPointProcess(int locationY) //下方控制點處理
        {
            if (locationY <= _locationOfPaintFirstPoint.Y)
                _locationOfPaintEndPoint.Y = _locationOfPaintFirstPoint.Y + 1;
            else
                _locationOfPaintEndPoint.Y = locationY;
        }

        public void ChangeLeftTopControlPoint(Point newPoint) //更改左上方控制點位置
        {
            LeftControlPointProcess(newPoint.X);
            TopControlPointProcess(newPoint.Y);
        }

        public void ChangeLeftBottomControlPoint(Point newPoint) //更改左下方控制點位置
        {
            LeftControlPointProcess(newPoint.X);
            BottomControlPointProcess(newPoint.Y);
        }

        public void ChangeRightTopControlPoint(Point newPoint) //更改右上方控制點位置
        {
            RightControlPointProcess(newPoint.X);
            TopControlPointProcess(newPoint.Y);
        }

        public void ChangeRightBottomControlPoint(Point newPoint) //更改右下方控制點位置
        {
            RightControlPointProcess(newPoint.X);
            BottomControlPointProcess(newPoint.Y);
        }

        public int MovingXOffset // 屬性 設定X偏差值
        {
            set
            {
                _moveingXOffset = value;
            }
        }

        public int MovingYOffset //屬性 設定Y偏差值
        {
            set
            {
                _moveingYOffset = value;
            }
        }

        public bool IsAPoint //屬性 回傳是否圖形為一個點
        {
            get
            {
                if (_locationOfTopPoint.X == _locationOfBottomPoint.X && _locationOfTopPoint.Y == _locationOfBottomPoint.Y)
                    return true;
                else
                    return false;
            }
        }

        public void MoveShape(int xOffset, int yOffset) //移動圖形
        {
            _locationOfPaintFirstPoint.X += xOffset;
            _locationOfPaintFirstPoint.Y += yOffset;
            _locationOfPaintEndPoint.X += xOffset;
            _locationOfPaintEndPoint.Y += yOffset;
        }

        public void ResetLocationOfPaintPoint() //重繪圖點
        {
            if (!_isLine)
            {
                _locationOfPaintEndPoint.X = _locationOfBottomPoint.X;
                _locationOfPaintEndPoint.Y = _locationOfBottomPoint.Y;
                _locationOfPaintFirstPoint.X = _locationOfTopPoint.X;
                _locationOfPaintFirstPoint.Y = _locationOfTopPoint.Y;
            }
            else
            {
                if (_locationOfPaintEndPoint.Y <= _locationOfPaintFirstPoint.Y)
                {
                    SwapTwoPoint(ref _locationOfPaintFirstPoint,ref  _locationOfPaintEndPoint);
                }
            }
        }

        private void SwapTwoPoint(ref Point swaPoint, ref Point anotherSwapPoint) // 將兩個POINT 座標交換
        {
            int temp;
            temp = swaPoint.X;
            swaPoint.X = anotherSwapPoint.X;
            anotherSwapPoint.X = temp;
            temp = swaPoint.Y;
            swaPoint.Y = anotherSwapPoint.Y;
            anotherSwapPoint.Y = temp;
        }

        protected void SetLocationOfPaintFirstPoint() //設定第一繪畫點
        {
            _locationOfPaintFirstPoint.X = _locationOfTopPoint.X;
            _locationOfPaintFirstPoint.Y = _locationOfTopPoint.Y;
        }

        protected int Wideth //取得寬度
        {
            get
            {
                return _locationOfBottomPoint.X - _locationOfTopPoint.X;
            }
        }

        protected int Height //取得高度
        {
            get
            {
                return _locationOfBottomPoint.Y - _locationOfTopPoint.Y;
            }
        }

        private bool _isFocus = false; //是否是被選取的

        public Point LocationOfPaintEndPoint //設定結束點
        {
            set
            {
                _locationOfPaintEndPoint = value;
            }
            get
            {
                return _locationOfPaintEndPoint;
            }
        }

        public bool IsFocus //設定是否被選取
        {
            set
            {
                _isFocus = value;
            }
            get
            {
                return _isFocus;
            }
        }

        protected const float PEN_SIZE = 2.0f;   //筆刷大小

        public void Draw(Graphics graphics) //畫圖
        {
            const float DASH_PATTERN = 3.0f;
            const int WIDETH_AND_HEIGH_OFF_SET = 2;
            const int LOCATION_OFF_SET = 1;
            Pen dashPen = new Pen(Color.Green, PEN_SIZE);
            Pen controlLinePen = new Pen(Color.Green, PEN_SIZE);
            dashPen.DashPattern = new float[] { DASH_PATTERN, DASH_PATTERN };
            TranselateLocationPoint();
            HookDraw(graphics);
            if (_isFocus)
            {
                graphics.DrawRectangle(dashPen, _locationOfTopPoint.X - LOCATION_OFF_SET + _moveingXOffset, _locationOfTopPoint.Y - LOCATION_OFF_SET + _moveingYOffset, Wideth + WIDETH_AND_HEIGH_OFF_SET, Height + WIDETH_AND_HEIGH_OFF_SET);
                graphics.DrawRectangle(controlLinePen, _locationOfTopPoint.X - CONTROL_POINT_OFFSET + _moveingXOffset, _locationOfTopPoint.Y - CONTROL_POINT_OFFSET + _moveingYOffset, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE);
                graphics.DrawRectangle(controlLinePen, _locationOfBottomPoint.X - CONTROL_POINT_OFFSET + _moveingXOffset, _locationOfTopPoint.Y - CONTROL_POINT_OFFSET + _moveingYOffset, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE);
                graphics.DrawRectangle(controlLinePen, _locationOfTopPoint.X - CONTROL_POINT_OFFSET + _moveingXOffset, _locationOfBottomPoint.Y - CONTROL_POINT_OFFSET + _moveingYOffset, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE);
                graphics.DrawRectangle(controlLinePen, _locationOfBottomPoint.X - CONTROL_POINT_OFFSET + _moveingXOffset, _locationOfBottomPoint.Y - CONTROL_POINT_OFFSET + _moveingYOffset, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE);
            }
        }

        public bool ContainsInLeftTopControlPoint(int x, int y) //檢查座標x,y是否在圖形內
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddRectangle(new Rectangle(_locationOfTopPoint.X - CONTROL_POINT_OFFSET, _locationOfTopPoint.Y - CONTROL_POINT_OFFSET, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE));
            return path.IsVisible(x, y);
        }

        public bool ContainsInRightTopControlPoint(int x, int y) //檢查座標x,y是否在圖形內
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddRectangle(new Rectangle(_locationOfBottomPoint.X - CONTROL_POINT_OFFSET, _locationOfTopPoint.Y - CONTROL_POINT_OFFSET, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE));
            return path.IsVisible(x, y);
        }

        public bool ContainsInLeftBottomControlPoint(int x, int y) //檢查座標x,y是否在圖形內
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddRectangle(new Rectangle(_locationOfTopPoint.X - CONTROL_POINT_OFFSET, _locationOfBottomPoint.Y - CONTROL_POINT_OFFSET, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE));
            return path.IsVisible(x, y);
        }

        public bool ContainsInRightBottomControlPoint(int x, int y) //檢查座標x,y是否在圖形內
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            path.AddRectangle(new Rectangle(_locationOfBottomPoint.X - CONTROL_POINT_OFFSET, _locationOfBottomPoint.Y - CONTROL_POINT_OFFSET, CONTROL_POINT_SIZE, CONTROL_POINT_SIZE));
            return path.IsVisible(x, y);
        }

        void ReplacePaintFirstPointToPoint(ref Point point) //將點用FirstPoint覆蓋
        {
            point.X = _locationOfPaintFirstPoint.X;
            point.Y = _locationOfPaintFirstPoint.Y;
        }

        void ReplacePaintEndPointToPoint(ref Point point)//將點用EndPoint覆蓋
        {
            point.X = _locationOfPaintEndPoint.X;
            point.Y = _locationOfPaintEndPoint.Y;
        }

        protected virtual void TranselateLocationPoint() //座標轉換
        {
            ReplacePaintEndPointToPoint(ref _locationOfBottomPoint);
            ReplacePaintFirstPointToPoint(ref _locationOfTopPoint);
            if (_locationOfPaintEndPoint.X < _locationOfPaintFirstPoint.X && _locationOfPaintEndPoint.Y <= _locationOfPaintFirstPoint.Y)
            {
                ReplacePaintEndPointToPoint(ref _locationOfTopPoint);
                ReplacePaintFirstPointToPoint(ref _locationOfBottomPoint);
            }
            else if (_locationOfPaintEndPoint.X < _locationOfPaintFirstPoint.X && _locationOfPaintEndPoint.Y > _locationOfPaintFirstPoint.Y)
            {
                _locationOfTopPoint.X = _locationOfPaintEndPoint.X;
                _locationOfBottomPoint.X = _locationOfPaintFirstPoint.X;
            }
            else if (_locationOfPaintEndPoint.X >= _locationOfPaintFirstPoint.X && _locationOfPaintEndPoint.Y < _locationOfPaintFirstPoint.Y)
            {
                _locationOfTopPoint.Y = _locationOfPaintEndPoint.Y;
                _locationOfBottomPoint.Y = _locationOfPaintFirstPoint.Y;
            }
        }

        public abstract void HookDraw(Graphics graphics);  //畫圖 (根據不同圖形畫出不同的圖)

        public abstract bool ContainsInShape(int x, int y); //判斷座標是否圖形內
    }
}
