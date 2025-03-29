using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace KInderDbContext.Connections
{
    /// <summary>
    /// Контроллер для управления контекстами базы данных. Использует паттерн Singleton.
    /// </summary>
    internal class DbController
    {
        /// <summary>
        /// Приватный конструктор, предотвращающий создание экземпляров класса извне.
        /// </summary>
        private DbController()
        {
            try
            {
                SetSqliteConnection();
                Debug.WriteLine($"{GetType().Name} создан! Тип контекста - {_appDbContext.GetType().Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Приватное поле для хранения экземпляра контекста базы данных.
        /// </summary>
        private AppDbContext _appDbContext = null!;

        /// <summary>
        /// Приватное статическое поле для хранения единственного экземпляра DbController 
        /// </summary>
        private static DbController _instance = null!;

        /// <summary>
        /// Возвращает текущий контекст базы данных.
        /// </summary>
        /// <returns>Экземпляр AppDbContext.</returns>
        internal AppDbContext GetContext()
        {
            return _appDbContext;
        }

        /// <summary>
        /// Возвращает единственный экземпляр класса DbController .
        /// </summary>
        /// <returns>Единственный экземпляр DbController.</returns>
        internal static DbController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbController();
                return _instance;
            }
            else
                return _instance;
        }

        /// <summary>
        /// Устанавливает соединение с базой данных SQLite.
        /// </summary>
        internal void SetSqliteConnection()
        {
            _appDbContext = new SQLiteDbContext();
        }

        /// <summary>
        /// Устанавливает соединение с базой данных SQL Server.
        /// </summary>
        internal void SetSqlServerConnection()
        {
            _appDbContext = new SQLServerDbContext();
        }
    }
}
