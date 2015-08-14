using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Painter
{
    public partial class PainterForm : Form
    {
        private Model _model;  //宣告 presentatoinModel
        ToolStripMenuItem _file = new ToolStripMenuItem("File");
        ToolStripMenuItem _edit = new ToolStripMenuItem("Edit");
        ToolStripMenuItem _about = new ToolStripMenuItem("About");
        ToolStripMenuItem _help = new ToolStripMenuItem("Help");
        ToolStripMenuItem _shapes = new ToolStripMenuItem("Shapes");
        ToolStripMenuItem _exit = new ToolStripMenuItem("Exit");
        ToolStripMenuItem _lineToolStripMenuItem = new ToolStripMenuItem("Line");
        ToolStripMenuItem _pointerToolStripMenuItem = new ToolStripMenuItem("Pointer");
        ToolStripMenuItem _ellipseToolStripMenuItem = new ToolStripMenuItem("Ellipse");
        ToolStripMenuItem _rectangleToolStripMenuItem = new ToolStripMenuItem("Rectangle");
        ToolStripMenuItem _redoToolStripMenuItem = new ToolStripMenuItem("Redo");
        ToolStripMenuItem _undoToolStripMenuItem = new ToolStripMenuItem("Undo");
        ToolStripMenuItem _deleteMenuItem = new ToolStripMenuItem("Delete");
        ToolStrip _toolStrip = new ToolStrip();
        ToolStripButton _redoToolStripButton = new ToolStripButton("Redo");
        ToolStripButton _undoToolStripButton = new ToolStripButton("Undo");
        ToolStripButton _pointerToolStripButton = new ToolStripButton("Pointer");
        ToolStripButton _ellipseToolStripButton = new ToolStripButton("Ellipse");
        ToolStripButton _rectangleToolStripButton = new ToolStripButton("Rectangle");
        ToolStripButton _lineToolStripButton = new ToolStripButton("Line");
        ToolStripButton _deleteStripButton = new ToolStripButton("Delete");
        ToolStripSeparator _firstToolStripSeparator = new ToolStripSeparator();
        ToolStripSeparator _secondToolStripSeparator = new ToolStripSeparator();

        MenuStrip _menuStrip = new MenuStrip();
        System.ComponentModel.ComponentResourceManager _resources = new System.ComponentModel.ComponentResourceManager(typeof(PainterForm));

        private void AddAllMenuItem() //加入所有MenuItem
        {
            this.Controls.Add(_menuStrip);
            _menuStrip.Items.Add(_file);
            _menuStrip.Items.Add(_edit);
            _menuStrip.Items.Add(_help);
            _file.DropDown.Items.Add(_exit);
            _edit.DropDown.Items.Add(_undoToolStripMenuItem);
            _edit.DropDown.Items.Add(_redoToolStripMenuItem);
            _edit.DropDown.Items.Add(new ToolStripSeparator());
            _edit.DropDown.Items.Add(_shapes);
            _edit.DropDown.Items.Add(new ToolStripSeparator());
            _edit.DropDown.Items.Add(_deleteMenuItem);
            _shapes.DropDown.Items.Add(_pointerToolStripMenuItem);
            _shapes.DropDown.Items.Add(_ellipseToolStripMenuItem);
            _shapes.DropDown.Items.Add(_rectangleToolStripMenuItem);
            _shapes.DropDown.Items.Add(_lineToolStripMenuItem);
            _help.DropDown.Items.Add(_about);
            AddAllMenuitemImage();
            this.Icon = ((System.Drawing.Icon)(_resources.GetObject("$this.Icon")));
        }

        private void AddAllMenuitemImage()
        {
            _pointerToolStripMenuItem.Image = global::Painter.Properties.Resources.pointer;
            _ellipseToolStripMenuItem.Image = global::Painter.Properties.Resources.ellipse;
            _rectangleToolStripMenuItem.Image = global::Painter.Properties.Resources.rectangle;
            _about.Image = global::Painter.Properties.Resources.About;
            _exit.Image = global::Painter.Properties.Resources.EXIT;
            _deleteMenuItem.Image = global::Painter.Properties.Resources.delete;
            _redoToolStripMenuItem.Image = global::Painter.Properties.Resources.redo;
            _undoToolStripMenuItem.Image = global::Painter.Properties.Resources.undo;
        }

        private void AddToolStrip() //加入所有TollStrip 以及其Button
        {
            this.Controls.Add(_toolStrip);
            _toolStrip.Items.Add(_undoToolStripButton);
            _toolStrip.Items.Add(_redoToolStripButton);
            _toolStrip.Items.Add(_firstToolStripSeparator);
            _toolStrip.Items.Add(_pointerToolStripButton);
            _toolStrip.Items.Add(_ellipseToolStripButton);
            _toolStrip.Items.Add(_rectangleToolStripButton);
            _toolStrip.Items.Add(_lineToolStripButton);
            _toolStrip.Items.Add(_secondToolStripSeparator);
            _toolStrip.Items.Add(_deleteStripButton);
            SetToolStripImages();
        }

        private void SetToolStripImages()  //設定TollStrip中的所有圖案
        {
            _undoToolStripButton.Image = global::Painter.Properties.Resources.undo;
            _redoToolStripButton.Image = global::Painter.Properties.Resources.redo;
            _pointerToolStripButton.Image = global::Painter.Properties.Resources.pointer;
            _ellipseToolStripButton.Image = global::Painter.Properties.Resources.ellipse;
            _rectangleToolStripButton.Image = global::Painter.Properties.Resources.rectangle;
            _lineToolStripButton.Image = global::Painter.Properties.Resources.line;
            _deleteStripButton.Image = global::Painter.Properties.Resources.delete;
        }

        private void AddAction() //加入所有動作
        {
            this.MouseClick += new MouseEventHandler(ClickMouse);
            this.MouseDown += new MouseEventHandler(PressMouse);
            this.MouseMove += new MouseEventHandler(MoveMouse);
            this.MouseUp += new MouseEventHandler(ReleaseMouse);
            _exit.Click += new EventHandler(ClickExitToolStripMenuItem);
            _pointerToolStripMenuItem.Click += new EventHandler(ClickPointerToolStripMenuItem);
            _pointerToolStripButton.Click += new EventHandler(ClickPointerToolStripMenuItem);
            _ellipseToolStripMenuItem.Click += new EventHandler(ClickEllipseToolStripMenuItem);
            _ellipseToolStripButton.Click += new EventHandler(ClickEllipseToolStripMenuItem);
            _rectangleToolStripMenuItem.Click += new EventHandler(ClickRectangleToolStripMenuItem);
            _rectangleToolStripButton.Click += new EventHandler(ClickRectangleToolStripMenuItem);
            _about.Click += new EventHandler(ClickAboutToolStripMenuItem);
            _lineToolStripMenuItem.Click += new EventHandler(ClickLineToolStripMenuItem);
            _lineToolStripButton.Click += new EventHandler(ClickLineToolStripMenuItem);
            _undoToolStripButton.Click += new EventHandler(ClickUndoToolStripButton);
            _undoToolStripMenuItem.Click += new EventHandler(ClickUndoToolStripButton);
            _redoToolStripButton.Click += new EventHandler(ClickRedoToolStripButton);
            _redoToolStripMenuItem.Click += new EventHandler(ClickRedoToolStripButton);
            _deleteStripButton.Click += new EventHandler(ClickDeleteToolStripButton);
            _deleteMenuItem.Click += new EventHandler(ClickDeleteToolStripButton);

            _model.ModelChanged += this.UpdateView;
        }

        public void ClickDeleteToolStripButton(object sender, EventArgs e)//按下Delete的動作
        {
            _model.DeleteShapeCommand();
        }

        public void PressMouse(object sender, MouseEventArgs e) //按下滑鼠的事件
        {
            _model.PressMouse(e.Location);
        }

        public void MoveMouse(object sender, MouseEventArgs e) //移動滑鼠事件
        {
            _model.MoveMouse(e.Location);
            if (_model.FocusShape != null && !_pointerToolStripButton.Enabled) //若是在Pointer 狀態下 (其指標變化)
            {
                if (_model.FocusShape.ContainsInLeftBottomControlPoint(e.X, e.Y) || _model.FocusShape.ContainsInRightTopControlPoint(e.X, e.Y)) // 碰觸到 左下右上點
                    this.Cursor = Cursors.SizeNESW;
                else if (_model.FocusShape.ContainsInLeftTopControlPoint(e.X, e.Y) || _model.FocusShape.ContainsInRightBottomControlPoint(e.X, e.Y)) //碰觸到 左上右下點
                    this.Cursor = Cursors.SizeNWSE;
                else if (_model.FocusShape.ContainsInShape(e.X, e.Y))//碰觸到圖形
                    this.Cursor = Cursors.SizeAll;
                else
                    this.Cursor = Cursors.Default;
            }
            
        }

        public void ReleaseMouse(object sender, MouseEventArgs e) //放開滑鼠按鈕的事件
        {
            _model.ReleaseMouse(e.Location);
        }

        public void ClickLineToolStripMenuItem(object sender, EventArgs e) //按下Line工具列按鈕的事件
        {
            EnableAllMenuItemInShapes();
            _lineToolStripMenuItem.Enabled = false;
            _model.ClickLineToolStripMenuItem();
            this.Cursor = Cursors.Cross;
        }

        public PainterForm(Model model) //建構元
        {
            const int HEIGHT = 480;
            const int WIDTH = 640;
            this.Size = new Size(WIDTH, HEIGHT);
            this._model = model;
            AddToolStrip();
            AddAllMenuItem();
            AddAction();
            this.Icon = ((System.Drawing.Icon)(_resources.GetObject("$this.Icon")));
            this.Text = "Basic Painter";
            this.DoubleBuffered = true;
            _pointerToolStripButton.PerformClick();
        }

        protected override void OnPaint(PaintEventArgs e)  //畫圖
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaint(e);
            _model.Draw(e.Graphics);
        }

        private void EnableAllMenuItemInShapes()  //Enable所有在Shape中的MenuItem
        {
            this._pointerToolStripMenuItem.Enabled = true;
            this._ellipseToolStripMenuItem.Enabled = true;
            this._rectangleToolStripMenuItem.Enabled = true;
            this._lineToolStripMenuItem.Enabled = true;
            this._pointerToolStripButton.Enabled = true;
            this._ellipseToolStripButton.Enabled = true;
            this._rectangleToolStripButton.Enabled = true;
            this._lineToolStripButton.Enabled = true;
        }

        private void ClickPointerToolStripMenuItem(object sender, EventArgs e) //按下Pointer的事件
        {
            EnableAllMenuItemInShapes();
            this._pointerToolStripMenuItem.Enabled = false;
            this._pointerToolStripButton.Enabled = false;
            _model.ClickPointerToolStripMenuItem();
            this.Cursor = Cursors.Default;
        }

        private void ClickEllipseToolStripMenuItem(object sender, EventArgs e) //按下Ellipse事件
        {
            EnableAllMenuItemInShapes();
            this._ellipseToolStripMenuItem.Enabled = false;
            this._ellipseToolStripButton.Enabled = false;
            _model.CLickEllipseToolStripMenuItem();
            this.Cursor = Cursors.Cross;
        }
            
        private void ClickRectangleToolStripMenuItem(object sender, EventArgs e) //按下Rectangle事件
        {
            EnableAllMenuItemInShapes();
            this._rectangleToolStripMenuItem.Enabled = false;
            this._rectangleToolStripButton.Enabled = false;
            _model.ClickRectangleToolStripMenuItem();
            this.Cursor = Cursors.Cross;
        }

        private void ClickAboutToolStripMenuItem(object sender, EventArgs e)  //按下About事件
        {
            using (Form dialog = new AboutBasicPainterView())
            {
                // Show the form as a modal dialog
                dialog.ShowDialog(this);
            }
        }

        private void ClickExitToolStripMenuItem(object sender, EventArgs e)  //按下Exit事件
        {
            this.Close();
        }

        private void ClickRedoToolStripButton(object sender, EventArgs e) //按下Redo事件
        {
            _model.CommandRedo();
        }

        private void ClickUndoToolStripButton(object sender, EventArgs e) //按下 Undo事件
        {
            _model.CommandUndo();
        }

        private void ClickMouse(object sender, MouseEventArgs e)  //按下滑鼠的事件
        {
            _model.ClickMouse(e.Location);
        }

        private void UpdateView() //更新View
        {
            _redoToolStripButton.Enabled = !_model.IsNoMoreRedo;
            _redoToolStripMenuItem.Enabled = !_model.IsNoMoreRedo;
            _undoToolStripButton.Enabled = !_model.IsNoMoreUndo;
            _undoToolStripMenuItem.Enabled = !_model.IsNoMoreUndo;
            _deleteStripButton.Enabled = !(_model.FocusShape == null);
            _deleteMenuItem.Enabled = _deleteStripButton.Enabled;
            Invalidate();
        }
    }
}
