using IURIS.COMMON.Entidades.CapaBase;
using IURIS.COMMON.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IURIS.DAL
{
    public class RepositorioGenerico<T> : IRepositorio<T> where T : BaseDTO
    {

        private MongoClient client;
        private IMongoDatabase db;

        public RepositorioGenerico()
        {
            client = new MongoClient(new MongoUrl(@"mongodb://AdminDbSemper:Semper1234@ds060009.mlab.com:60009/semperpapyrusbd?retryWrites=false"));
            db = client.GetDatabase("semperpapyrusbd");
        }

        private IMongoCollection<T> Collection()
        {
            try
            {
            return db.GetCollection<T>(typeof(T).Name);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> Read => Collection().AsQueryable().ToList();

        public bool Create(T Entidad)
        {
            string resul = "";

            Entidad.id = new ObjectId();
            try
            {
                Collection().InsertOne(Entidad);
                resul = "";
                return true;
            }
            catch (Exception ex)
            {
                resul = ex.Message;
                return false;
            }
        }

        public bool Delete(ObjectId id)
        {
            try
            {
                return Collection().DeleteOne(e => e.id == id).DeletedCount == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T EntidadModificada)
        {
            try
            {
                return Collection().ReplaceOne(e => e.id == EntidadModificada.id, EntidadModificada).ModifiedCount == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
