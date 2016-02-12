using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace publicAutoTutorWebApi.Controllers
{
    public class MongoDBController : ApiController
    {
      
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        static bool connStatus=false;

        [HttpGet]
        [ActionName("SelectByID")]
       public async void Get(string id)
        {
            var collection = _database.GetCollection<BsonDocument>("test");
            var filter = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var result = await collection.Find(filter).ToListAsync();
           
         
        }

        [HttpGet]
        [ActionName("SelectAll")]
        public async void Get()
        {
            if (connStatus == false)
            {
                buildConn bn = new buildConn();
                bn.databaseName = "PublicAutoTutor";
                connStatus=conn(bn.databaseName);
                if (connStatus == true)
                {
                    var collection = _database.GetCollection<BsonDocument>("test");
                    var filter = new BsonDocument();
                    var count = 0;
                   // var result = await collection.Find(filter).ToListAsync();
                    using (var cursor = await collection.FindAsync(filter))
                    {
                        while (await cursor.MoveNextAsync())
                        {
                            var batch = cursor.Current;
                            foreach (var document in batch)
                            {
                                BsonElement author = document.GetElement("address");
                                Console.WriteLine("Name: {0}, Value: {1}", author.Name, author.Value);
                               
                                // process document
                               // string userID = document.GetValue("_id", "").AsString;

                                count++;
                            }
                        }
                    }
                
                
                }
                else
                {

                    Console.WriteLine("database connect fail");
                }

            }
            else
            {

                Console.WriteLine("database lost connect");
            }
            
        }



        [HttpPost]
        [ActionName("Add")]
        public string Post(string id, buildConn bconn)
        {
            conn(bconn.databaseName);
            Console.WriteLine("database connect successfully");
            return "Customer added successfully!";
        }

        [HttpPut]
        [ActionName("Modify")]
        public string Put(string id, buildConn obj)
        {
          
            return "Customer updated successfully!";
        }

        [HttpDelete]
        [ActionName("Remove")]
        public string Delete(string id)
        {
           
            return "Customer deleted successfully!";
        }

        public bool conn(string databaseName)
        {
            try{
                 _client = new MongoClient();
                 _database = _client.GetDatabase(databaseName);
                 return true;
            
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;
            }
           
            
          
        }
    }
    public class buildConn
    {
        public string status { get; set; }
        public string databaseName { get; set; }


      
    }

  

    public class building
    { 
     public string  Building { get; set; }
         public string  coord { get; set; }
         public string  street { get; set; }
         public string  zipcode { get; set; }

    }
}
