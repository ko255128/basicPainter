using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Painter
{
    class ResizeShapeCommand : Command
    {
        Model _model;//Model
        Shape.ChangePoint _changePoint; //更換控制點型別
        Point _newPoint; //新點
        Point _oldPoint; //舊點
        
        

        public ResizeShapeCommand(Model model, Shape shape, Shape.ChangePoint changePoint, Point newPoint,Point oldPoint) //建構元
        {
            _model = model;
            _targetShape = shape;
            _changePoint = changePoint;
            _newPoint = newPoint;
            _oldPoint = oldPoint;
        }

        public override void Redo() //取消復原
        {
            switch (_changePoint)
            {
                case Shape.ChangePoint.LeftTop: 
                    _targetShape.ChangeLeftTopControlPoint(_newPoint);
                    break;
                case Shape.ChangePoint.LeftBottom:
                    _targetShape.ChangeLeftBottomControlPoint(_newPoint);
                    break;
                case Shape.ChangePoint.RightTop:
                    _targetShape.ChangeRightTopControlPoint(_newPoint);
                    break;
                case Shape.ChangePoint.RightBottom:
                    _targetShape.ChangeRightBottomControlPoint(_newPoint);
                    break;
            }
        }

        public override void Undo() //復原
        {
            switch (_changePoint)
            {
                case Shape.ChangePoint.LeftTop:
                    _targetShape.ChangeLeftTopControlPoint(_oldPoint);
                    break;
                case Shape.ChangePoint.LeftBottom:
                    _targetShape.ChangeLeftBottomControlPoint(_oldPoint);
                    break;
                case Shape.ChangePoint.RightTop:
                    _targetShape.ChangeRightTopControlPoint(_oldPoint);
                    break;
                case Shape.ChangePoint.RightBottom:
                    _targetShape.ChangeRightBottomControlPoint(_oldPoint);
                    break;
            }
        }

    }
}
