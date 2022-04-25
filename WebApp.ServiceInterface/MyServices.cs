using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.ServiceModel;

namespace WebApp.ServiceInterface;

public class MyServices : Service
{
    DateBase db =  null;
    public MyServices()
    {
        db = new DateBase();
    }

    public async Task<object> Any(GetAllToDo request)
    {
        var result = await db.DbGetAllToDoAsync();
        return result;
    }

    //public object Any(ToDo request)
    //{
    //    var result = db.Connect();
    //    return new ToDoResponse { Result = $"Hello, {result}!" };
    //}

    public object Any(DeleteToDo request)
    {
        db.DeleteToDo(request.Id);
        return new ToDoResponse { Result = $"DeleteToDo, {request.Id}!" };
    }

    public object Any(GetSpecificToDo request)
    {
        var result = db.GetSpecificToDo(request.Id);
        return result;
    }

    public object Any(CreateToDo request)
    {
        var result = db.CreateToDo(request.Title, request.Expiry, request.Description, request.Complete);
        return result;
    }

    public object Any(UpdateToDo request)
    {
        var result = db.UpdateToDo(request.Id, request.Title, request.Expiry, request.Description, request.Complete);
        return result;
    }

    //Mark Todo as Done
    public object Any(MarkTodoDone request)
    {
        var result = db.MarkTodoDone(request.Id);
        return result;
    }

    public async Task<object> Any(GetIncomingToDo request)
    {
        var result = await db.GetIncomingToDo(request.ExpiryFrom, request.ExpiryTo);
        return result;
    }
}