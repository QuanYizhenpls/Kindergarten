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
    /// Класс, представляющий контекст для работы с базой данных SQL Server.
    /// </summary>
    public class SQLServerDbContext : AppDbContext
    {
        /// <summary>
        /// Конструктор класса SQLServerDbContext.
        /// </summary>
        public SQLServerDbContext()
        {
            Debug.WriteLine($"{this.GetType().Name} was created!");
        }

        /// <summary>
        /// Строка подключения к базе данных SQL Server.
        /// </summary>
        private const string _connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = SupplyManagementDB";

        /// <summary>
        /// Настраивает параметры подключения к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Объект, позволяющий настроить параметры контекста базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(_connectionString));
        }
    }
}
