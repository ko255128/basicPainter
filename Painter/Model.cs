using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Painter
{
    public class Model
    {
        private Shapes _shapes = new Shapes();    //所有圖形
        private Shape _focusShape = null; //上一次加入的圖形
        private CommandManager _commandManager;
        enum DrawingType { Pointer = 0, Ellipse = 1, Rectangle, Line }; //圖形類別
        State _state;//狀態
        public event ModelChangedEventHandler ModelChanged;    //Mode 更改的事件
        public delegate void ModelChangedEventHandler(); //Mode更改的事件
        enum ChangePoint { LeftTop, LeftBottom, RightTop, RightBottom }; //圖形類型

        public bool IsNoMoreRedo  //屬性：回傳是否已無法執行ReDo
        {
            get
            {
                return _commandManager.IsNoMoreRedo;
            }
        }

        public void CommandResizeShape(Point oldPoint, Point newPoint, Shape.ChangePoint changePoint) //增加ReSize命令
        {
            _commandManager.ResizeShape(FocusShape, oldPoint, newPoint, changePoint);
        }

        public void MoveShapeCommand(int xOffSet,int yOffset) //增加移動Shape的命令
        {
            _commandManager.MoveShape(FocusShape, xOffSet, yOffset);
        }

        public void DeleteShapeCommand()
        {
            _commandManager.DeleteShape(FocusShape, _shapes.FindShapeInedx(FocusShape));
            NotifyObserver();
        }

        public bool IsNoMoreUndo //屬性：回傳是否已無法執行UnDo
        {
            get
            {
                return _commandManager.IsNoMoreUndo;
            }
        }

        public void CommandUndo() //執行Undo
        {
            _commandManager.CommandUndo();
            NotifyObserver();
        }

        public void CommandRedo() //執行Redo
        {
            _commandManager.CommandRedo();
            NotifyObserver();
        }

        public void InsertShape(Shape shape,int index) //插入Shape
        {
            _shapes.InsertShape(index, shape);
        }

        public void DeleteShape(int index) //刪除Shape
        {
            _shapes.DeleteShape(index);
        }

        public Model()  //建構元
        {
            ChangeState(); //變換State
            _commandManager = new CommandManager(this);
        }

        public void CommandAddShape()  //增加 增加Shape命令
        {
            _commandManager.AddShape(FocusShape, _shapes.NumberOfShape - 1);
        }

        public Shape FocusShape //取得目前專注的圖形
        {
            get
            {
                return _shapes.FocusShape;
            }
        }

        private DrawingType _shapeType; //宣告圖形類別

        public void PressMouse(Point locationOfTopPoint) //按下滑鼠的動作
        {
            _state.PressMouse(locationOfTopPoint);
        }

        public void MoveMouse(Point locationOfTopPoint)  //移動滑鼠的動作
        {
            _state.MoveMouse(locationOfTopPoint);
        }

        public void ReleaseMouse(Point locationOfTopPoint) //放開滑鼠的動作
        {
            _state.ReleaseMouse(locationOfTopPoint);
        }

        public void ClickMouse(Point locationOfTopPoint)  //點擊滑鼠的動作
        {
            _state.ClickMouse(locationOfTopPoint);
        }

        public void AddShape(Point locationOfTopPoint) //增加圖形
        {
            _shapes.AddShape((int)_shapeType, locationOfTopPoint);
            _focusShape = _shapes.FocusShape;
        }

        public void FindFocusShape(int mouseX, int mouseY)//找尋目前鼠標所指的圖形
        {
            _shapes.FindFocusShape(mouseX, mouseY);
            if (_focusShape != _shapes.FocusShape)
                NotifyObserver();
            _focusShape = _shapes.FocusShape; 
        }

        public void Draw(Graphics graphics)  //畫圖形
        {
            _shapes.Draw(graphics);
        }

        public void NotifyObserver() //通知Mode已更新
        {
            if (ModelChanged != null)
                ModelChanged();
        }

        private void ChangeState() //更換State
        {
            if (_shapeType == DrawingType.Pointer)
            {
                _state = new PointerState(this);
            }
            else
            {
                _state = new DrawingState(this);
            }
            NotifyObserver();
        }

        public void DisPoseFocusShape() //取消選取狀態
        {
            _shapes.DisposeFocusShape();
            _focusShape = null;
        }

        public void ClickRectangleToolStripMenuItem() //按下Rectangle工具列按鈕的動作
        {
            _shapeType = DrawingType.Rectangle;
            ChangeState();
        }

        public void CLickEllipseToolStripMenuItem() //按下Ellipse工具列按鈕的動作
        {
            _shapeType = DrawingType.Ellipse;
            ChangeState();
        }

        public void ClickPointerToolStripMenuItem() //按下Pointer工具列按鈕的動作
        {
            _shapeType = DrawingType.Pointer;
            ChangeState();
        }

        public void ClickLineToolStripMenuItem()  //按下Line工具列按鈕的動作
        {
            _shapeType = DrawingType.Line;
            ChangeState();
        }
    }
}
