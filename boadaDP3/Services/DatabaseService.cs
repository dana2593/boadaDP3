using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using boadaDP3.Models;

namespace boadaDP3.Services
{
    public class DatabaseService
    {
        private SQLiteConnection? _database;
        private readonly string _databasePath;

        public DatabaseService()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "equipos.db");
        }

        private void InitializeDatabase()
        {
            if (_database == null)
            {
                _database = new SQLiteConnection(_databasePath);
                _database.CreateTable<Equipo>();
            }
        }

        public async Task<List<Equipo>> GetEquiposAsync()
        {
            return await Task.Run(() =>
            {
                InitializeDatabase();
                return _database!.Table<Equipo>().OrderByDescending(e => e.FechaCreacion).ToList();
            });
        }

        public async Task<int> SaveEquipoAsync(Equipo equipo)
        {
            return await Task.Run(() =>
            {
                InitializeDatabase();
                if (equipo.Id != 0)
                {
                    return _database!.Update(equipo);
                }
                else
                {
                    return _database!.Insert(equipo);
                }
            });
        }

        public async Task<int> DeleteEquipoAsync(Equipo equipo)
        {
            return await Task.Run(() =>
            {
                InitializeDatabase();
                return _database!.Delete(equipo);
            });
        }
    }
}
