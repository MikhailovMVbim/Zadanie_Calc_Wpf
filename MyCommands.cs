using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zadanie_Calc_Wpf
{
    class MyCommands
    {
        public static RoutedCommand Exit { get; set; }

        static MyCommands ()
        {
            InputGestureCollection input = new InputGestureCollection();
            input.Add(new KeyGesture(Key.T, ModifierKeys.Control, "Ctrl+T"));
            Exit = new RoutedCommand("Exit", typeof(MyCommands), input);
        }
    }
}
