using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Painter
{
    abstract class State
    {
        protected Model _model; //model
        public abstract void PressMouse(Point locationOfTopPoint); //按下滑鼠的事件
        public abstract void MoveMouse(Point locationOfTopPoint); //移動畫鼠的事件
        public abstract void ReleaseMouse(Point locationOfTopPoint); //放開滑鼠的事件
        public abstract void ClickMouse(Point locationOfTopPoint); //點擊滑鼠的事件
    }
}
