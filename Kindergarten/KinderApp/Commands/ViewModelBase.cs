using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using System.Windows;
using System.Diagnostics;

namespace KinderApp.Commands
{
    /// <summary>
    /// Базовая реализация модели представления, реализующая интерфейс INotifyPropertyChanged.
    /// Позволяет уведомлять о изменениях свойств и управлять окнами приложения.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, вызываемое при изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Устанавливает значение поля и вызывает уведомление об изменении свойства.
        /// </summary>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <param name="field">Ссылающееся поле свойства.</param>
        /// <param name="value">Новое значение.</param>
        /// <param name="propertyName">Имя свойства (обычно указывается с помощью [CallerMemberName]).</param>
        /// <returns>True, если значение было изменено; иначе, False.</returns>
        protected bool Set<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Вызывает событие PropertyChanged для уведомления об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя измененного свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Открывает новое окно, закрывая текущее.
        /// </summary>
        /// <param name="window">Новое окно для отображения.</param>
        /// <returns>Открытое окно.</returns>
        protected Window OpenWindow(Window window)
        {
            var temp = Application.Current.MainWindow;
            Application.Current.MainWindow = window;
            temp.Close();
            window.Show();
            return window;
        }

        /// <summary>
        /// Открывает модальное диалоговое окно с эффектом размытия заднего плана.
        /// </summary>
        /// <param name="window">Диалоговое окно для отображения.</param>
        /// <returns>Диалоговое окно.</returns>
        protected Window OpenWindowDialog(Window window)
        {
            try
            {
                Application.Current.MainWindow.Effect = new BlurEffect();
                window.ShowDialog();
                Application.Current.MainWindow.Effect = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
            }

            return window;
        }

        /// <summary>
        /// Завершает работу приложения.
        /// </summary>
        protected void AppClose()
        {
            Application.Current.Shutdown();
        }
    }

}
