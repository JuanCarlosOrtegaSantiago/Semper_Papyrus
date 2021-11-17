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
            //mongodb://User-Finall:<password>@data-iuris0final-shard-00-00.w3ofl.mongodb.net:27017,data-iuris0final-shard-00-01.w3ofl.mongodb.net:27017,data-iuris0final-shard-00-02.w3ofl.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-wuw70l-shard-0&authSource=admin&retryWrites=true&w=majority
            client = new MongoClient(new MongoUrl(@"mongodb://User-Finall:U53R_bCJnJStCUK58pQgX_F1n4L@data-iuris0final-shard-00-00.w3ofl.mongodb.net:27017,data-iuris0final-shard-00-01.w3ofl.mongodb.net:27017,data-iuris0final-shard-00-02.w3ofl.mongodb.net:27017/Data-Iuris0Final?ssl=true&replicaSet=atlas-wuw70l-shard-0&authSource=admin&retryWrites=true&w=majority"));
            db = client.GetDatabase("Data-Iuris0Final");

        }
        public RepositorioGenerico(bool DatosPrueba)
        {
            client = new MongoClient(new MongoUrl(@"mongodb://user_dev:userDEV@data-dev-shard-00-00.sgnuf.mongodb.net:27017,data-dev-shard-00-01.sgnuf.mongodb.net:27017,data-dev-shard-00-02.sgnuf.mongodb.net:27017/Data-Dev?ssl=true&replicaSet=atlas-utazvk-shard-0&authSource=admin&retryWrites=true&w=majority"));

            db = client.GetDatabase("Data-Dev");
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
