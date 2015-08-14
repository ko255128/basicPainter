using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Painter
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 
        static private Model _model = new Model(); //宣告Model
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PainterForm(_model)); //執行PainterForm
        }
    }
}
