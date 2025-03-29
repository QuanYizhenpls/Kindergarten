using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KInderDbContext.Connections
{
    /// <summary>
    /// Класс, представляющий контекст для работы с базой данных SQLite.
    /// </summary>
    public class SQLiteDbContext : AppDbContext
    {
        /// <summary>
        /// Конструктор класса SQLiteDbContext.
        /// </summary>
        public SQLiteDbContext()
        {
            Debug.WriteLine($"{this.GetType().Name} создан!");
        }

        /// <summary>
        /// Строка подключения к базе данных SQLite.
        /// </summary>
        private const string _connectionString = "Data Source = localDb.db";

        /// <summary>
        /// Настраивает параметры подключения к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Объект, позволяющий настроить параметры контекста базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlite(_connectionString));
        }
    }
}
