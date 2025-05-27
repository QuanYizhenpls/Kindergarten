using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KinderData.Entities;

namespace KinderDbContext.Connections
{
    /// <summary>
    /// Абстрактный класс, представляющий контекст приложения для работы с базами данных.
    /// Наследуется от DbContext (Entity Framework Core).
    /// </summary>
    public abstract class AppDbContext : DbContext
    {
        /// <summary>
        ///  Представляет набор данных (таблицу) "Agreements" в базе данных.
        /// </summary>
        public DbSet<Agreement> Agreements { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "Employees" в базе данных.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "EmployeeDatas" в базе данных.
        /// </summary>
        public DbSet<EmployeeData> EmployeeDatas { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "Groups" в базе данных.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "Kindergartners" в базе данных.
        /// </summary>
        public DbSet<Kindergartner> Kindergartners { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "Plans" в базе данных.
        /// </summary>
        public DbSet<Plan> Plans { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "Salaries" в базе данных.
        /// </summary>
        public DbSet<Salary> Salaries { get; set; }

        /// <summary>
        ///  Представляет набор данных (таблицу) "Users" в базе данных.
        /// </summary>
        public DbSet<User> Users { get; set; }


    }
}
