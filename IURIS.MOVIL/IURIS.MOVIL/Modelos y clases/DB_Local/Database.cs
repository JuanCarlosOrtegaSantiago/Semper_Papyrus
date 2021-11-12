using IURIS.COMMON.Entidades.UsuariosDeAplicacion;
using IURIS.MOVIL.Modelos_y_clases.DB_Local;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases
{
    public class Database
    {
        public SQLiteAsyncConnection _database;
        string _Path;

        public Database(string dbPath)
        {
            //Establishing the conection
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MyUser>().Wait();
            _Path = dbPath;
        }
        public bool DeleteTable()
        {
            _database.DropTableAsync<MyUser>().Wait();
            return true;
        }

        // Show the registers
        public Task<List<MyUser>> GetPeopleAsync()
        {
            return SQLiteNetExtensionsAsync.Extensions.ReadOperations.GetAllWithChildrenAsync<MyUser>(_database);
            //return _database.Table<MyUser>().ToListAsync();
        }

        // Save registers
        public async void SavePersonAsync(MyUser User)
        {
            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.InsertWithChildrenAsync(_database, User);
        }

        // Delete registers
        public async void DeletePersonAsync(MyUser User)
        {
            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.DeleteAsync(_database, User, true);
            //return _database.DeleteAsync(User);
        }

        // Save registers
        public async Task<bool> UpdateUserAsync(MyUser User)
        {
            try
            {
                await SQLiteNetExtensionsAsync.Extensions.WriteOperations.UpdateWithChildrenAsync(_database, User);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
