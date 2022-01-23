using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zadanie_Calc_Wpf
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged //срабатывает каждый раз, когда могло измениться состояние CanExecute
        {
            add => CommandManager.RequerySuggested += value; // передача управления событием системе
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> Execute, Func<object,bool> CanExecute=null) // конструктор команды
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            canExecute = CanExecute;
        }

        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true; //проверяет доступность команды


        public void Execute(object parameter) => execute(parameter); // выполняется, когда команда будет вызвана
    }
}
