using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Tidsloggning.Models;

namespace Tidsloggning.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Work>("Works");
            List<Work> works = collection.Find(w => true).ToList();

            return View(works);
        }
        public IActionResult Create()
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var employeeCollection = database.GetCollection<Employee>("Employees");
            List<Employee> employees = employeeCollection.Find(e => true).ToList();

            return View(employees);
        }
        [HttpPost]
        public IActionResult Create(Work work)
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Work>("Works");
            collection.InsertOne(work);

            return Redirect("/Work");
        }
        public IActionResult Show(string id)
        {
            ObjectId workId = new ObjectId(id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var Collection = database.GetCollection<Work>("Works");
            Work work = Collection.Find(w => w.Id == workId).FirstOrDefault();
            
            return View(work);
        }
        public IActionResult Edit(string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var Collection = database.GetCollection<Work>("Works");
            Work work = Collection.Find(w => w.Id == workId).FirstOrDefault();

            return View(work);
        }
        [HttpPost]
        public IActionResult Edit(string Id, Work work)
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Work>("Works");
            var original = collection.Find(w => w.Id == work.Id).FirstOrDefault(); 
            work.EmployeeId = original.EmployeeId;

            collection.ReplaceOne(w => w.Id == work.Id, work);

            return Redirect("/Work");
        }
        public IActionResult Delete(string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var Collection = database.GetCollection<Work>("Works");
            Collection.DeleteOne(w => w.Id == workId);

            return Redirect("/Work");
        }


        }
}
