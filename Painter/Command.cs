using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Painter
{
    public abstract class  Command
    {
        protected int _shapeIndex;  //圖形索引
        protected Shape _targetShape; //被命令之圖形
        abstract public void Redo(); //取消復原
        abstract public void Undo(); //復原
    }
}
