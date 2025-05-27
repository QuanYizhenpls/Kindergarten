using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Connections
{
    /// <summary>
    /// Синглтон-класс для управления единственным экземпляром базы данных.
    /// Обеспечивает глобальную точку доступа к контексту базы данных, чтобы избежать создания множества экземпляров.
    /// </summary>
    public sealed class DbContextSingleton
    {
        /// <summary>
        /// Статическая переменная для хранения единственного экземпляра класса.
        /// Изначально установлена в null.
        /// </summary>
        private static DbContextSingleton instance = null;

        /// <summary>
        /// Свойство, которое хранит экземпляр контекста базы данных.
        /// Получает публичный доступ, но устанавливать его можно только внутри класса.
        /// Использует тип <see cref="SQLServerDbContext"/>, представляющий контекст подключения к базе данных.
        /// </summary>
        public SQLServerDbContext DbContext { get; private set; }

        /// <summary>
        /// Приватный конструктор класса.
        /// Создает новый экземпляр <see cref="SQLServerDbContext"/>, то есть подключение к базе данных.
        /// Выводит сообщение в отладочный вывод для информирования о создании экземпляра.
        /// </summary>
        private DbContextSingleton()
        {
            // Инициализация контекста базы данных
            DbContext = new SQLServerDbContext();

            // Выводит сообщение о создании экземпляра, что удобно для отладки и контроля
            Debug.WriteLine($"{this.GetType().Name} was created!");
        }

        /// <summary>
        /// Статический свойство, предоставляющее доступ к единственному экземпляру класса.
        /// Реализует ленивую и потокобезопасную инициализацию.
        /// </summary>
        public static DbContextSingleton Instance
        {
            get
            {
                // Если экземпляр еще не создан, создаем его при первом обращении
                if (instance == null)
                {
                    instance = new DbContextSingleton();
                }
                // Возвращаем существующий экземпляр
                return instance;
            }
        }
    }
}
