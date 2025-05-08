using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderData.Entities;
using Microsoft.EntityFrameworkCore;

namespace KinderDbContext.Connections
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
        private const string _connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = KinderDB";

        /// <summary>
        /// Настраивает параметры подключения к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Объект, позволяющий настроить параметры контекста базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(_connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agreement>(entity =>
            {
                entity.ToTable("Agreements"); // указываем название таблицы
                entity.HasKey(agreement => agreement.Agreement_Id);
                entity.HasMany(a => a.Employees)
                .WithMany();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees"); // указываем название таблицы
                entity.HasKey(p => p.Employee_Id); // указываем первичный ключ
                entity.HasMany(p => p.Groups) // указываем навигационное свойство для связи
                .WithMany(); // указываем свойСВО для установки Id связи
            });

            modelBuilder.Entity<EmployeeData>(entity =>
            {
                entity.ToTable("EmployeeDatas"); // указываем название таблицы
                entity.HasKey(p => p.EmployeeData_Id); // указываем первичный ключ
                
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups"); // указываем название таблицы
                entity.HasKey(e => e.Group_Id);
                entity.HasMany(a => a.Kindergartners)
                .WithMany();
            });

            modelBuilder.Entity<Kindergartner>(entity =>
            {
                entity.ToTable("Customers"); // указываем название таблицы
                entity.HasKey(k => k.Kindergartner_Id); // указываем первичный ключ
                entity.HasData(new Kindergartner() // стартовые значения в таблице
                {
                    Kindergartner_Id = Guid.NewGuid(),
                    FIO = "Иванов Иван Иванович",
                    DateOfBirth = "21.10.2022",
                    ParentsContactInfo = "+76059486793"
                });
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plans"); // указываем название таблицы
                entity.HasKey(e => e.Plan_Id);
                entity.HasMany(a => a.Groups)
                .WithMany();
                entity.HasMany(a => a.Employees)
                .WithMany();
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salaries"); // указываем название таблицы
                entity.HasKey(s => s.Salary_Id);
                entity.HasMany(a => a.Employees)
                .WithMany();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // указываем название таблицы
                entity.HasKey(customer => customer.Id); // указываем первичный ключ
                entity.HasData(new User() // стартовые значения в таблице
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Имя",
                    Lastname = "Фамилия",
                    Middlename = "Отчество",
                    Login = "Login",
                    Password = "Password"
                });
            });
    
        } 
    
    }
}
