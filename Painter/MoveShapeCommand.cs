using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Painter
{
    class MoveShapeCommand : Command
    {
        private Model _model;//Model
        private int _xOffset;//移動前後X差值
        private int _yOffset;//移動前後Y差值

        public MoveShapeCommand(Model model, Shape shape,int xOffset,int yOffset) //建構元
        {
            this._model = model;
            this._targetShape = shape;
            this._xOffset = xOffset;
            this._yOffset = yOffset;
        }

        public override void Redo()  //取消復原
        {
            _targetShape.MoveShape(_xOffset, _yOffset);
        }

        public override void Undo()  //復原
        {
            _targetShape.MoveShape(-_xOffset, -_yOffset);
        }
    }
}
