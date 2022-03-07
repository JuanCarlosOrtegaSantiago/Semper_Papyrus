using IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local
{
    public class SaveMyClasificacionDeLey
    {
        
        public SQLiteAsyncConnection _database;

        public SaveMyClasificacionDeLey(string dbPath)
        {
            //Establishing the conection
            _database = new SQLiteAsyncConnection(dbPath);
            //DeleteTable();
            CreateTable();
        }
        public bool DeleteTable()
        {
            _database.DropTableAsync<MyClasificacionDeLey>().Wait();
            return true;
        }
        public bool CreateTable()
        {
            _database.CreateTableAsync<MyClasificacionDeLey>().Wait();
            return true;
        }

        // Show the registers
        public Task<List<MyClasificacionDeLey>> GetPeopleAsync()
        {
            return SQLiteNetExtensionsAsync.Extensions.ReadOperations.GetAllWithChildrenAsync<MyClasificacionDeLey>(_database);
            //return _database.Table<MyUser>().ToListAsync();
        }

        // Save registers
        public async void SavePersonAsync(MyClasificacionDeLey myClasificacionDeLey)
        {
            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.InsertWithChildrenAsync(_database, myClasificacionDeLey);
        }

        // Delete registers
        public async void DeletePersonAsync(MyClasificacionDeLey myClasificacionDeLey)
        {
            await SQLiteNetExtensionsAsync.Extensions.WriteOperations.DeleteAsync(_database, myClasificacionDeLey, true);
            //return _database.DeleteAsync(User);
        }

        // Save registers
        public async Task<bool> UpdateUserAsync(MyClasificacionDeLey myClasificacionDeLey )
        {
            try
            {
                await SQLiteNetExtensionsAsync.Extensions.WriteOperations.UpdateWithChildrenAsync(_database, myClasificacionDeLey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
