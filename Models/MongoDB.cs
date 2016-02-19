using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
namespace publicAutoTutorWebApi.Models
{
    public class MongoDB
    {
        public void test()
        { 
        
        }
    }
    public class OprationMongo
    {
        private MongoDatabase mongoDatabase;
        /// <summary>MongoDB collection name for classes</summary>
        public const string CLASS_COLLECTION = "Classes";

        /// <summary>MongoDB collection name for students</summary>
        public const string USER_COLLECTION = "Users";

        /// <summary>MongoDB collection name for lessons</summary>
        public const string LESSON_COLLECTION = "Lessons";

        public string ServerURL { get; set; }

         /// <summary>
        /// To construct an instance of this class, you supply a valid
        /// MongoDB url that includes a database name
        /// (e.g. mongodb://localhost:27017/testdb)
        /// </summary>
        /// <param name="url">MongoDB URL (including database name)</param>
        public void ConnDatabase(string url) {
            this.ServerURL = url;
            
            var mongoUrl = new MongoUrl(url);
            var client = new MongoClient(mongoUrl);
            var server = client.GetServer();
            mongoDatabase = server.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// This very hacky function manually insures all indexes that we want
        /// in the MongoDB collections that we manage.  Should really only be
        /// called once in a blue moon.  Currently called on app startup by our
        /// web api app.
        /// </summary>
        public void InsureIndexes()
        {
            mongoDatabase.GetCollection(USER_COLLECTION).CreateIndex("Email");
            mongoDatabase.GetCollection(LESSON_COLLECTION).CreateIndex("LessonID");
            mongoDatabase.GetCollection(CLASS_COLLECTION).CreateIndex("_id");
            mongoDatabase.GetCollection(CLASS_COLLECTION).CreateIndex("TeacherEmail");

        }

        public bool validateUserEmail(string UserEmail)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

            var results = collect.FindOne(Query.EQ("Email", UserEmail));
            if (results == null)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }

        public bool validateUserEmailandPwd(string UserEmail,string Password)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

            var results = collect.FindOne((Query.And(Query.EQ("Email", UserEmail), Query.EQ("Password", Password))));
            if (results == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool addUserInfo(Users userInfo)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);
            var result=collect.Insert<Users>(userInfo);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }
        }


        public bool updateUserInfo(Users userInfo)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);
            
            var query = new QueryDocument { { "Email", userInfo.Email } };
            var update = new UpdateDocument { 
            { "$set", new QueryDocument { 
            { "FristName", userInfo.FristName},
            { "LastName", userInfo.LastName},
            { "Address", userInfo.Address},
            { "City", userInfo.City},
            { "State", userInfo.State},
            { "Zip", userInfo.Zip},
            { "Phone", userInfo.Phone}
            } } };
             
            var result=collect.Update(query, update);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool updateUserPassword(Users userInfo)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

            var query = new QueryDocument { { "Email", userInfo.Email } };
            var update = new UpdateDocument { 
            { "$set", new QueryDocument { 
            { "Password", userInfo.Password}
           
            } } };
            
            var result = collect.Update(query, update);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public bool updateUserStatus(Users userInfo)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

            var query = new QueryDocument { { "Email", userInfo.Email } };
            var update = new UpdateDocument { 
            { "$set", new QueryDocument { 
            { "Status", userInfo.Status}
           
            } } };
            
            var result = collect.Update(query, update);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public bool updateUserRole(Users userInfo)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

            var query = new QueryDocument { { "Email", userInfo.Email } };
            var update = new UpdateDocument { 
            { "$set", new QueryDocument { 
            { "Role", userInfo.Role}
           
            } } };
            
            var result = collect.Update(query, update);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool ModifyUserRoleAndStatus(Users userInfo)
        {
            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

            var query = new QueryDocument { { "Email", userInfo.Email } };
            var update = new UpdateDocument { 
            { "$set", new QueryDocument { 
            { "Role", userInfo.Role},
            { "Status", userInfo.Status}
           
            } } };

            var result = collect.Update(query, update);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }
        }



        public List<Lessons> getAllLessonsInfo()
        {
          
            var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
            var list = collect.FindAllAs<Lessons>().ToList();
            return list;

        }
        public Lessons getLessonsInfoById(string lessonID)
        {
           
            var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
            var results = collect.FindOneAs<Lessons>(Query.EQ("LessonID", lessonID));

            return results;

        }

        public bool addLessonInfo(Lessons lessonInfo)
        {

            lessonInfo._id = lessonInfo.LessonID;
            var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
          
            var result = collect.Insert<Lessons>(lessonInfo);
            var Affected = result.DocumentsAffected.ToString();
            if (Affected != "0")
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        public bool updateLessonInfo(Lessons lessonInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);

                var query = new QueryDocument { { "LessonID", lessonInfo.LessonID } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "LessonGroup", lessonInfo.LessonGroup},
                { "LessonName", lessonInfo.LessonName},
                { "Author", lessonInfo.Author},
                { "LessonURL", lessonInfo.LessonURL},
                { "ScriptURL", lessonInfo.ScriptURL},
                { "LessonForder", lessonInfo.LessonForder},
                { "ServerUrl", lessonInfo.ServerUrl}
                } } };

                    var result = collect.Update(query, update);
                    var Affected = result.DocumentsAffected.ToString();
                    if (Affected != "0")
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
            
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString()); 
                return false;
               
            }
        }


    }
}