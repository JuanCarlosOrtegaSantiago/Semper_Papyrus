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
            //client = new MongoClient(new MongoUrl(@"mongodb://mongodb+srv://UserSemper:IURISuser@bdsemper.xg6rg.mongodb.net/bdsemper?retryWrites=true&w=majority"));
            client = new MongoClient(new MongoUrl(@"mongodb://UserSemper:IURISuser@bdsemper-shard-00-00.xg6rg.mongodb.net:27017,bdsemper-shard-00-01.xg6rg.mongodb.net:27017,bdsemper-shard-00-02.xg6rg.mongodb.net:27017/bdsemper?ssl=true&replicaSet=atlas-xo23j9-shard-0&authSource=admin&retryWrites=true&w=majority"));
            db = client.GetDatabase("bdsemper");
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
