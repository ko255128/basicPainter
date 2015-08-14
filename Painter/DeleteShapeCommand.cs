using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Painter
{
    class DeleteShapeCommand : Command
    {
        private Model _model; // Model

        public DeleteShapeCommand(Model model,Shape shape,int shapeIndex) //建構元
        {
            this._model = model;
            this._shapeIndex = shapeIndex;
            this._targetShape = shape;
        }

        public override void Redo() //取消復原動作
        {
            _model.DeleteShape(_shapeIndex);
        }

        public override void Undo() //復原動作
        {
            _model.InsertShape(_targetShape, _shapeIndex);
        }
    }
}
