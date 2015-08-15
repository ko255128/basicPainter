using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Painter
{
    class PointerState : State
    {
        bool _isPressing = false; //滑鼠是被在按壓中
        bool _isMoving = false;   //是否在執行移動
        bool _isResizing = false; //是唪在執行變更大小
        Timer _timer = new Timer(); //計數器
        int timeCount = 0; //計數值
        Shape.ChangePoint changePoint; //變更控制點類型
        Shape _focusShape; //目前專注的圖形
        Point _FirstPressPoint; //第一次按壓所記錄的座標
         
        int TWO_HUNDERDS_MINISECOND_PEER_MINISECOND = 2; //兩百毫秒(單位毫秒)

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs) //計數氣事件
        {
            timeCount++;//計數值++
        }
        public PointerState(Model modle) //建構元
        {
            this._model = modle;
            _timer.Interval = TWO_HUNDERDS_MINISECOND_PEER_MINISECOND;
            _timer.Tick += TimerEventProcessor;
        }

        public override void ClickMouse(Point locationOfTopPoint) //點擊滑鼠的事件
        {
            _timer.Stop(); //計數暫停
            if (timeCount < TWO_HUNDERDS_MINISECOND_PEER_MINISECOND) //若計數值不到2 表示按住時間上未達200ms 表示為Click
                _model.FindFocusShape(locationOfTopPoint.X, locationOfTopPoint.Y);
            timeCount = 0; //計數歸零
        }

        public override void PressMouse(Point locationOfTopPoint) //按下滑鼠左鍵的事件
        {
            if (_model.FocusShape != null && !_isPressing) //在案壓住並且有Shape被選中
            {
                _timer.Start(); //開始計數
                _FirstPressPoint = locationOfTopPoint;
                _isPressing = true;
                _focusShape = _model.FocusShape;
                if (IsPressOnControlPoint(locationOfTopPoint))
                {
                    _isResizing = true;
                }
                else if (_focusShape.ContainsInShape(locationOfTopPoint.X, locationOfTopPoint.Y))
                {
                    _isMoving = true;
                }
                else
                {
                    _model.DisPoseFocusShape();
                }
            }
        }

        private bool IsPressOnControlPoint(Point locationOfTopPoint) //判別是否按壓在控制大小點上
        {
            if (_focusShape.ContainsInLeftTopControlPoint(locationOfTopPoint.X, locationOfTopPoint.Y))
            {
                changePoint = Shape.ChangePoint.LeftTop;
                return true;
            }
            else if (_focusShape.ContainsInLeftBottomControlPoint(locationOfTopPoint.X, locationOfTopPoint.Y))
            {
                changePoint = Shape.ChangePoint.LeftBottom;
                return true;
            }
            else if (_focusShape.ContainsInRightTopControlPoint(locationOfTopPoint.X, locationOfTopPoint.Y))
            {
                changePoint = Shape.ChangePoint.RightTop;
                return true;
            }
            else if (_focusShape.ContainsInRightBottomControlPoint(locationOfTopPoint.X, locationOfTopPoint.Y))
            {
                changePoint = Shape.ChangePoint.RightBottom;
                return true;
            }
            return false;
        }

        public override void MoveMouse(Point locationOfTopPoint) //移動滑鼠的事件
        {
            if (_isPressing&&_isMoving)
            {
                _focusShape.MovingXOffset = locationOfTopPoint.X - _FirstPressPoint.X;
                _focusShape.MovingYOffset = locationOfTopPoint.Y - _FirstPressPoint.Y;
            }
            else if (_isPressing && _isResizing)
            {
                ResizeShape(locationOfTopPoint);
            }
            _model.NotifyObserver();
        }

        private void ResizeShape(Point locationOfTopPoint) //改變大小
        {
            if (changePoint == Shape.ChangePoint.LeftTop)
                _focusShape.ChangeLeftTopControlPoint(locationOfTopPoint);
            else if (changePoint == Shape.ChangePoint.LeftBottom)
                _focusShape.ChangeLeftBottomControlPoint(locationOfTopPoint);
            else if (changePoint == Shape.ChangePoint.RightTop)
                _focusShape.ChangeRightTopControlPoint(locationOfTopPoint);
            else if (changePoint == Shape.ChangePoint.RightBottom)
                _focusShape.ChangeRightBottomControlPoint(locationOfTopPoint);
        }

        public override void ReleaseMouse(Point locationOfTopPoint) //放開滑鼠左鍵的事件
        {
            if (_isMoving) //更改位置
            {
                _isMoving = false;
                if (locationOfTopPoint.X != _FirstPressPoint.X && locationOfTopPoint.Y != _FirstPressPoint.Y)
                {
                    _focusShape.MovingXOffset = locationOfTopPoint.X - _FirstPressPoint.X;
                    _focusShape.MovingYOffset = locationOfTopPoint.Y - _FirstPressPoint.Y;
                    _focusShape.ExcuseMoving();
                    _model.MoveShapeCommand(locationOfTopPoint.X - _FirstPressPoint.X, locationOfTopPoint.Y - _FirstPressPoint.Y);
                }
                _model.NotifyObserver();
            }
            if (_isResizing)//變更大小
            {
                _isResizing = false;
                {
                    ResizeShape(locationOfTopPoint);
                    _model.CommandResizeShape(_FirstPressPoint, locationOfTopPoint, changePoint);
                }
                _model.NotifyObserver();
            }
            _isPressing = false;
        }
    }
}
