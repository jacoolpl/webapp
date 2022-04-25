using ServiceStack.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;
using WebApp.ServiceModel;
using System;

namespace WebApp.ServiceInterface
{
    public class DateBase
    {
        readonly string connectionString = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=malutka;Database=ToDo;Pooling=true;MinPoolSize=0;MaxPoolSize=200";
        OrmLiteConnectionFactory dbFactory = null;

        public DateBase()
        {
            dbFactory = CreateFactory();
            Connect();

        }
        public void Connect()
        {
            using var db = dbFactory.Open();
            if (db.CreateTableIfNotExists<ToDo>())
            {
                db.Insert(new ToDo { Title = "Testowe TODO", Complete = 10, Description = "pierwsze TODO", Expiry = System.DateTime.Today.AddDays(3) });
                db.Insert(new ToDo { Title = "Testowe TODO2", Complete = 20, Description = "drugie TODO", Expiry = System.DateTime.Today.AddDays(5) });
            }
            //var result = db.SingleById<ToDo>(1);
            //result.PrintDump(); //= {Id: 1, Name:Seed Data}
            //return result.ToString();
        }

        private OrmLiteConnectionFactory CreateFactory()
        {
            var dbFactory = new OrmLiteConnectionFactory(
                            connectionString,
                            PostgreSqlDialect.Provider);
            return dbFactory;
        }

        public async Task<string> DbGetAllToDoAsync()
        {
            //var dbFactory = CreateFactory();
            using var db = dbFactory.Open();
            var result = await db.SelectAsync<ToDo>();
            var JsonResult = JsonSerializer.SerializeToString(result);
            return JsonResult;
        }

        public void DeleteToDo(int id)
        {
            using var db = dbFactory.Open();
            db.DeleteById<ToDo>(id);
        }

        public object GetSpecificToDo(int id)
        {
            using var db = dbFactory.Open();
            var result = db.SingleById<ToDo>(id);
            var JsonResult = JsonSerializer.SerializeToString(result);
            return JsonResult;
        }

        public object CreateToDo(string title, DateTime expiry, String description, int complete)
        {
            using var db = dbFactory.Open();
            var id  = db.Insert(new ToDo { Title = title, Expiry = expiry, Description = description, Complete = complete }, selectIdentity:true);
            var result = db.SingleById<ToDo>(id);
            var JsonResult = JsonSerializer.SerializeToString(result);
            return JsonResult;
        }

        //db.Update(new Person { Id = 1, FirstName = "Jimi", LastName = "Hendrix", Age = 27});
        public object UpdateToDo(int id, string title, DateTime expiry, String description, int complete)
        {
            using var db = dbFactory.Open();
            db.Update(new ToDo { Id = id, Title = title, Expiry = expiry, Description = description, Complete = complete });
            var result = db.SingleById<ToDo>(id);
            var JsonResult = JsonSerializer.SerializeToString(result);
            return JsonResult;
        }

        //UpdateToDoToDone
        public object MarkTodoDone(int id)
        {
            using var db = dbFactory.Open();
            db.UpdateOnly(() => new ToDo { Complete = 100 }, where: todo => todo.Id == id);
            var result = db.SingleById<ToDo>(id);
            var JsonResult = JsonSerializer.SerializeToString(result);
            return JsonResult;
        }

        //Get Incoming ToDo
        public async Task<string> GetIncomingToDo(DateTime expiryFrom, DateTime expiryTo)
        {
            using var db = dbFactory.Open();
            var result = await db.SelectAsync<ToDo>(x => x.Expiry >= expiryFrom && x.Expiry <= expiryTo);
            var JsonResult = JsonSerializer.SerializeToString(result);
            return JsonResult;
        }
    }
}
