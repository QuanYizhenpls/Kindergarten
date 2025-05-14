using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Connections
{
    public sealed class DbContextSingleton
    {
        private static DbContextSingleton instance = null;
        private static readonly object padlock = new object();

        public SQLServerDbContext DbContext { get; private set; }

        DbContextSingleton()
        {
            DbContext = new SQLServerDbContext();
            Debug.WriteLine($"{this.GetType().Name} was created!");
        }

        public static DbContextSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DbContextSingleton();
                    }
                    return instance;
                }
            }
        }
    }
}
