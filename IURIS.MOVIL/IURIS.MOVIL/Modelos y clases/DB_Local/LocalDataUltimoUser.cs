using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local
{
    public class LocalDataUltimoUser
    {

        public SQLiteAsyncConnection _database;
        string _Path;

        public LocalDataUltimoUser(string dbPath)
        {
            //Establishing the conection
            _database = new SQLiteAsyncConnection(dbPath);
            CreateTable();
            _Path = dbPath;
        }

        public bool DeleteTable()
        {
            _database.DropTableAsync<MyUser>().Wait();
            return true;
        }
        public bool CreateTable()
        {
            _database.CreateTableAsync<UltimoUser>().Wait();
            return true;
        }

        // Show the registers
        public Task<List<UltimoUser>> GetUltimoUserAsync()
        {
            return SQLiteNetExtensionsAsync.Extensions.ReadOperations.GetAllWithChildrenAsync<UltimoUser>(_database);
            //return _database.Table<MyUser>().ToListAsync();
        }

        // Save registers
        public async void SaveUltimoUserAsync(UltimoUser ultimoUser)
        {
            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.InsertWithChildrenAsync(_database, ultimoUser);
        }

        // Save registers
        public async Task<bool> UpdateUltimoUserAsync(UltimoUser ultimoUser)
        {
            try
            {
                await SQLiteNetExtensionsAsync.Extensions.WriteOperations.UpdateWithChildrenAsync(_database, ultimoUser);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async void DeleteUltimoUserAsync(UltimoUser ultimoUser)
        {
            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.DeleteAsync(_database, ultimoUser, true);
            //return _database.DeleteAsync(User);
        }
    }
}
