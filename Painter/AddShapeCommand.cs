using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Painter
{
    public class AddShapeCommand : Command
    {
        private Model _model;  //Model

        public AddShapeCommand(Model model, Shape targetShape, int shapeIndex) //建構元
        {
            this._model = model;
            this._targetShape = targetShape;
            this._shapeIndex = shapeIndex;
        }

        public override void Redo() //取消復原
        {
            _model.InsertShape(_targetShape, _shapeIndex);
        }

        public override void Undo() //復原
        {
            _model.DeleteShape(_shapeIndex);
            _model.DisPoseFocusShape();
        }
    }
}
