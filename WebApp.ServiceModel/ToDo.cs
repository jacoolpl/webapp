using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace WebApp.ServiceModel
{
    

    [Route("/ToDo")]
    [Route("/ToDo/{Title}")]
    public class ToDo : IReturn<ToDoResponse>
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Expiry { get; set; }
        public String Description { get; set; }
        public int Complete { get; set; }
    }

    public class ToDoResponse
    {
        public string Result { get; set; }
    }

    public class GetAllToDo 
    {
        //public string Result { get; set; }
    }

    [Route("/DeleteToDo/{Id}/delete")]
    public class DeleteToDo : IReturnVoid
    {
        public int Id { get; set; }
    }

    [Route("/GetSpecificToDo/{Id}")]
    public class GetSpecificToDo : IReturn<ToDoResponse>
    {
        public int Id { get; set; }
    }

    [Route("/CreateToDo")]
    public class CreateToDo : IReturn<ToDoResponse>
    {
        public string Title { get; set; }
        public DateTime Expiry { get; set; }
        public String Description { get; set; }
        public int Complete { get; set; }
    }

    [Route("/UpdateToDo")]
    public class UpdateToDo : IReturn<ToDoResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Expiry { get; set; }
        public String Description { get; set; }
        public int Complete { get; set; }
    }

    [Route("/MarkTodoDone")]
    public class MarkTodoDone : IReturn<ToDoResponse>
    {
        public int Id { get; set; }

    }

    //GetIncomingToDo
    public class GetIncomingToDo
    {
        public DateTime ExpiryFrom { get; set; }

        public DateTime ExpiryTo { get; set; }
    }
}



