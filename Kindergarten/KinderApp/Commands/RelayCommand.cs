using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KinderApp.Commands
{
    /// <summary>
    /// Реализация ICommand для упрощенного связывания команд с пользовательским интерфейсом.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Делегат для выполнения команды.
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Делегат для определения возможности выполнения команды.
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Событие, сигнализирующее о возможности выполнения команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Конструктор команды.
        /// </summary>
        /// <param name="execute">Делегат для выполнения команды.</param>
        /// <param name="canExecute">Делегат для проверки возможности выполнения.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null!)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Проверяет, может ли команда выполниться в текущем состоянии.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если команда может выполниться; иначе, False.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }
    }

}
