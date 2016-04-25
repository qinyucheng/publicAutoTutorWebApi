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
using System.Text.RegularExpressions;

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


        public List<Users> getAllUsersInfo()
        {

            var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);
            var list = collect.FindAllAs<Users>().ToList();
            return list;

        }
        public List<Users> searchUser(string searchKey)
        {
         
            try {
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);
                 //var query = Query.Or(Query.Matches("Email", BsonRegularExpression.Create(new Regex(searchKey, RegexOptions.IgnoreCase))), Query.Matches("FirstName", BsonRegularExpression.Create(new Regex(searchKey, RegexOptions.IgnoreCase))), Query.Matches("LastName", BsonRegularExpression.Create(new Regex(searchKey, RegexOptions.IgnoreCase))));
                var query = Query.Or(Query.Matches("Email", new BsonRegularExpression(searchKey, "i")), Query.Matches("FirstName", new BsonRegularExpression(searchKey, "i")), Query.Matches("LastName", new BsonRegularExpression(searchKey, "i")));
               

              
                 var results = collect.FindAs<Users>(query).ToList();

                return results;
            
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                List<Users> aa = new List<Users>();
                return aa;
                

            }
          

        }

        public bool validateUserEmail(string UserEmail)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

                var results = collect.FindOne(Query.EQ("Email", UserEmail));
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
          
        }

        public Users validateUserEmailandPwd(string UserEmail,string Password)
        {
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);
                var results = collect.FindOneAs<Users>((Query.And(Query.EQ("Email", UserEmail), Query.EQ("Password", Password))));
                return results;
        }

        public bool addUserInfo(Users userInfo)
        {
            try
            {
                userInfo._id = userInfo.Email;
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);
                var result = collect.Insert<Users>(userInfo);
                var Affected = result.DocumentsAffected.ToString();
                return true;
             
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
           
        }


        public bool updateUserInfo(Users userInfo)
        {
            try {
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

                var query = new QueryDocument { { "Email", userInfo.Email } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "FirstName", userInfo.FirstName},
                { "LastName", userInfo.LastName},
                { "Address", userInfo.Address},
                { "City", userInfo.City},
                { "State", userInfo.State},
                { "Zip", userInfo.Zip},
                { "Phone", userInfo.Phone}
                } } };

                var result = collect.Update(query, update);
                return true;

            }

            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
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
            try{
                    var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

                    var query = new QueryDocument { { "Email", userInfo.Email } };
                    var update = new UpdateDocument { 
                    { "$set", new QueryDocument { 
                    { "Status", userInfo.Status}
           
                    } } };
            
                    var result = collect.Update(query, update);
                    return true;
                }
           catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }
        public bool updateUserRole(Users userInfo)
        {
            try {
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

                var query = new QueryDocument { { "Email", userInfo.Email } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "Role", userInfo.Role}
           
                } } };

                var result = collect.Update(query, update);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
            
         
        }

        public bool ModifyUserRoleAndStatus(Users userInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(USER_COLLECTION);

                var query = new QueryDocument { { "Email", userInfo.Email } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "Role", userInfo.Role},
                { "Status", userInfo.Status}
           
                } } };

                var result = collect.Update(query, update);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
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

        public List<Lessons> getLessonsInfoByStatus(string status)
        {
            var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
            var list = collect.FindAs<Lessons>(Query.EQ("Status", status)).ToList();
            return list;

        }

        public List<Lessons> searchLessonsInfo(string searchKey)
        {
         
            try {
                var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
                //var query = Query.And(Query.Or(Query.Matches("LessonName", searchKey), Query.Matches("LessonGroup", searchKey)), (Query.EQ("Status", "active")));
                var query = Query.And(Query.Or(Query.Matches("LessonName", new BsonRegularExpression(searchKey, "i")), Query.Matches("LessonGroup", new BsonRegularExpression(searchKey, "i"))), (Query.EQ("Status", "active")));
               
                var results = collect.FindAs<Lessons>(query).ToList();

                return results;
            
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                List<Lessons> aa = new List<Lessons>();
                return aa;
                

            }
          

        }

        public List<Lessons> advanceSearchLessonsInfo(string searchKey)
        {

            try
            {
               
                var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
                //var query = Query.And(Query.Or(Query.Matches("LessonName", searchKey), Query.Matches("LessonGroup", searchKey)), (Query.EQ("Status", "active")));

                if (searchKey.ToLower() == "inactive" || searchKey.ToLower() == "active")
                {
                    var query = (Query.EQ("Status", searchKey.ToLower()));
                    var results = collect.FindAs<Lessons>(query).ToList();
                    return results;
               
                }
                else {
                   var  query = Query.Or(Query.Matches("LessonName", new BsonRegularExpression(searchKey, "i")), Query.Matches("LessonGroup", new BsonRegularExpression(searchKey, "i")), Query.Matches("Author", new BsonRegularExpression(searchKey, "i")), Query.Matches("Status", new BsonRegularExpression(searchKey, "i")));
                   var results = collect.FindAs<Lessons>(query).ToList();
                   return results;
                }
                
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                List<Lessons> aa = new List<Lessons>();
                return aa;


            }


        }
       
       
        


        public List<Lessons> getLessonsInfoByGroupAndStatus(string LessonGroup,string status)
        {
            var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
            var query = Query.And(
                Query.EQ("Status", status),
                Query.EQ("LessonGroup", LessonGroup)
            );
            var list = collect.FindAs<Lessons>(query).ToList();
            return list;

        }

        public bool addLessonInfo(Lessons lessonInfo)
        {
            try
            {
                lessonInfo._id = lessonInfo.LessonID;
                var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);

                var result = collect.Insert<Lessons>(lessonInfo);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
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
                { "ServerUrl", lessonInfo.ServerUrl},
                { "Status", lessonInfo.Status},
                { "Description", lessonInfo.Description},
                { "VideoURL", lessonInfo.VideoURL}
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

          public bool changeLessonStatus(Lessons lessonInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);

                var query = new QueryDocument { { "LessonID", lessonInfo.LessonID } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "Status", lessonInfo.Status}
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



      

        public bool deleteLessonById(string lessonID)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(LESSON_COLLECTION);
                var result = collect.Remove(Query.EQ("LessonID", lessonID));
                if (result.DocumentsAffected == 0)
                {
                    return false;
                }
                else if (result.DocumentsAffected == 1)
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
        //Classes MongoDB
        //get all classes for admin 
        public List<Classes> getAllClassInfo()
        {

            var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
            var list = collect.FindAllAs<Classes>().ToList();
            return list;

        }

        //get all classes by teacherEmail  for teacher 
        public List<Classes> getAllClassInfoByTeacherEmail(string teacherEmail)
        {

           
            var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
            var list = collect.FindAs<Classes>(Query.EQ("TeacherEmail", teacherEmail)).ToList();       
            return list;

        }

        //get all classes by classID  
        public List<Classes> getClassInfoByClassName(string ClassName)
        {

            var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
            //var list = collect.FindAs<Classes>( Query.And(Query.EQ("ClassName", ClassName),Query.EQ("TeacherEmail", TeacherEmail))).ToList();new BsonRegularExpression(searchKey, "i")
            var list = collect.FindAs<Classes>(Query.Matches("ClassName", new BsonRegularExpression("^"+ClassName+"$", "i"))).ToList();
            return list;

        }

        public List<Classes> advanceSearchClassInfo(string searchKey)
        {

            try
            {

                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                if (searchKey.ToLower() == "inactive" || searchKey.ToLower() == "active")
                {
                    var query = (Query.EQ("ClassStatus", searchKey.ToLower()));
                    var results = collect.FindAs<Classes>(query).ToList();
                    return results;

                }
                else
                {
                    var query = Query.Or(Query.Matches("ClassName", new BsonRegularExpression(searchKey, "i")), Query.Matches("ClassStatus", new BsonRegularExpression(searchKey, "i")),
                                Query.Matches("TeacherEmail", new BsonRegularExpression(searchKey, "i")));
                   
                    var results = collect.FindAs<Classes>(query).ToList();
                    return results;
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                List<Classes> aa = new List<Classes>();
                return aa;


            }

        }

        public bool addClass(Classes ClassesIfo)
        {
            try
            {

                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var result = collect.Insert<Classes>(ClassesIfo);
                var Affected = result.DocumentsAffected.ToString();
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }

        }

        public bool UpdateClassBasicInfo(Classes ClassesInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = new QueryDocument { { "_id", ClassesInfo._id } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "ClassName", ClassesInfo.ClassName},
                { "StudyStartTime", ClassesInfo.StudyStartTime},
                { "StudyEndTime", ClassesInfo.StudyEndTime},
                { "LastChangeTime", ClassesInfo.LastChangeTime},
                { "LessonGroup", ClassesInfo.LessonGroup}
               
                } } };
                var result = collect.Update(query, update);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }

        public bool UpdateClassStadyURL(Classes ClassesInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = new QueryDocument { { "_id", ClassesInfo._id } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "StudyURL", ClassesInfo.StudyURL}
                } } };
                var result = collect.Update(query, update);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }


        //add by yehui after create nameList js, directly insert studyUrl into class.
        public bool UpdateClassStudyURLFromServer(string studyUrl, string className)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = new QueryDocument { { "ClassName", className } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "StudyURL", studyUrl}
                } } };
                var result = collect.Update(query, update);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }

        // update ClassStatus
        public bool updateClassStatus(Classes ClassesInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = new QueryDocument { { "ClassName", ClassesInfo.ClassName } };
                var update = new UpdateDocument { 
                { "$set", new QueryDocument { 
                { "ClassStatus", ClassesInfo.ClassStatus}              
                } } };
                var result = collect.Update(query, update);
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }


        public bool updateClassStudentsInfo(Classes ClassesInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = Query<Classes>.EQ(p => p._id, ClassesInfo._id);
                var update = Update<Classes>.Set(i => i.StudentGroup, ClassesInfo.StudentGroup);
                   
                collect.Update(query, update);
                return true;
             
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }
        public bool updateClassInfo(Classes ClassesInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = Query<Classes>.EQ(p => p._id, ClassesInfo._id);
                var update = Update<Classes>.Set(i => i.StudentGroup, ClassesInfo.StudentGroup)
                            .Set(i => i.SeletedLeassons, ClassesInfo.SeletedLeassons)
                            .Set(i => i.StudyStartTime, ClassesInfo.StudyStartTime)
                            .Set(i => i.StudyEndTime, ClassesInfo.StudyEndTime)
                            .Set(i => i.LastChangeTime, ClassesInfo.LastChangeTime)
                            .Set(i => i.ClassStatus, "active")
                            .Set(i => i.StudyURL, ClassesInfo.StudyURL)
                            .Set(i => i.LessonGroup, ClassesInfo.LessonGroup);
                collect.Update(query, update);
                return true;

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;
            }


        }
        public bool updateClassSelectedLessonInfo(Classes ClassesInfo)
        {
            try
            {
                var collect = this.mongoDatabase.GetCollection(CLASS_COLLECTION);
                var query = Query<Classes>.EQ(p => p._id, ClassesInfo._id);
                var update = Update<Classes>.Set(i => i.SeletedLeassons, ClassesInfo.SeletedLeassons);
                collect.Update(query, update);
                return true;

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                return false;

            }
        }

        

    }
}