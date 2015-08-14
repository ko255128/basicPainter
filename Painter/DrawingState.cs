using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Painter
{
    class DrawingState : State  //繪畫狀態
    {
        private bool _isPressing = false;  //是否滑鼠左鍵被壓下
        private bool _firstMove = true;
        Point _firstPoint;
        public DrawingState(Model model) //建構元
        {
            this._model = model;
        }

        public override void ClickMouse(Point locationOfTopPoint) //實作案下滑鼠一下的實作
        {
            return;
        }

        public override void PressMouse(Point locationOfTopPoint) //實作壓住滑鼠的動作
        {
            if (!_isPressing)
            {
                _isPressing = true;
                _firstPoint = locationOfTopPoint;
                _firstMove = true;
            }
        }

        public override void MoveMouse(Point locationOfEndPoint)  //實作移動滑鼠的動作
        {
            if (_isPressing)
            {
                if (_firstMove)
                {
                    if (_model.FocusShape != null)
                        _model.DisPoseFocusShape();
                    _model.AddShape(_firstPoint);
                    _firstMove = false;
                }
                Shape processShape = _model.FocusShape;
                processShape.LocationOfPaintEndPoint = locationOfEndPoint;
                _model.NotifyObserver();
            }
        }

        public override void ReleaseMouse(Point locationOfTopPoint) //實作放開滑鼠左鍵的動作
        {
            if (!_firstMove)
            {
                _model.CommandAddShape();
            }
            _model.NotifyObserver();
            _isPressing = false;
        }
    }
}
