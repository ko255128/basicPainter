using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Painter
{
    class CommandManager
    {
        const int NO_COMMAND = -1; //無任何命令 其指標位置設定為-1表無命令
        List<Command> _commands = new List<Command>(); //命令列表
        int _lastcommdandIndex = NO_COMMAND; //最後一個命令其索引
        Model _model; // Model

        public bool IsNoMoreRedo  //是否無法取消復原
        {
            get
            {
                if (_lastcommdandIndex + 1 == _commands.Count)
                    return true;
                else
                    return false;
            }
        }

        public bool IsNoMoreUndo //是否無法復原
        {
            get
            {
                if (_lastcommdandIndex == NO_COMMAND)
                    return true;
                else
                    return false;
            }
        }

        public void ResizeShape(Shape targetShape, Point oldPoint, Point newPoint, Shape.ChangePoint changePoint)  //Resize命令
        {
            NewCommandExcute();
            _commands.Add(new ResizeShapeCommand(_model, targetShape, changePoint, newPoint, oldPoint));
        }

        public CommandManager(Model model) //建構元
        {
            this._model = model;
        }

        private void NewCommandExcute() //新命令前置動作 (清除index後之命令)
        {
            if (_lastcommdandIndex + 1 != _commands.Count && _commands.Count > 0)
                _commands.RemoveRange(_lastcommdandIndex + 1, _commands.Count - _lastcommdandIndex - 1);
            _lastcommdandIndex++;
        }

        public void AddShape(Shape targetShape,int shapeIndex) //增加Shape命令
        {
            if (!targetShape.IsAPoint) //可形成圖形 則新增命令
            {
                NewCommandExcute();
                _commands.Add(new AddShapeCommand(_model, targetShape, shapeIndex));
                targetShape.ResetLocationOfPaintPoint();
            }
            else //無法形成圖形 刪除點
            {
                _model.DeleteShape(shapeIndex);
            }
        }

        public void MoveShape(Shape targetShape,int xOffset,int yOffset) //移動圖形命令
        {
            NewCommandExcute();
            _commands.Add(new MoveShapeCommand(_model,targetShape,xOffset,yOffset));
        }

        public void DeleteShape(Shape targetShape, int shapeIndex) //刪除圖形命令
        {
            NewCommandExcute();
            _model.DeleteShape(shapeIndex);
            _model.DisPoseFocusShape();
            _commands.Add(new DeleteShapeCommand(_model,targetShape,shapeIndex));
        }

        public void CommandRedo() //取消復原
        {
            _commands[_lastcommdandIndex + 1].Redo();
            _lastcommdandIndex++;
        }

        public void CommandUndo() //復原
        {
            _commands[_lastcommdandIndex].Undo();
            _lastcommdandIndex--;
        }
    }
}
