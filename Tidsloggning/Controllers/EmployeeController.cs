using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Tidsloggning.Models;

namespace Tidsloggning.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Employee>("Employees");
            List<Employee> employees = collection.Find(e => true).ToList();

            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Employee>("Employees");
            collection.InsertOne(employee);
            return Redirect("/Employee");
        }
        public IActionResult Show(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient(); 
            var database = dbClient.GetDatabase("Tidsloggning"); 
            var employeeCollection = database.GetCollection<Employee>("Employees"); 
            var workCollection = database.GetCollection<Work>("Works"); 

            
            Employee employee = employeeCollection.Find(e => e.Id == ObjectId.Parse(Id)).FirstOrDefault(); 
            EmployeeIndexViewModel model = new EmployeeIndexViewModel(); 
            model.Id = employee.Id.ToString();
            model.EmployeeName = employee.Name;
            model.EmployeeBirthyear = employee.Birthyear; 
            var works = workCollection.Find(w => w.EmployeeId == employee.Id).ToList(); 

            if (works.Any()) 
            {
                model.WorkTitle = string.Join(", ", works.Select(w => w.Title)); 
                model.WorkTimes = string.Join(", ", works.Select(w => w.Time)); 
                model.WorkTime = works.Sum(w => w.Time);
                model.WorkDescription = string.Join(", ", works.Select(w => w.Description)); 
            }
            else 
            {
                model.WorkTitle = "No work in progress"; 
                model.WorkTimes = "-"; 
                model.WorkTime = 0; 
                model.WorkDescription = "-";
            }

            return View(model); 
        }

        public IActionResult Delete(string Id)
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Employee>("Employees");
            collection.DeleteOne(e => e.Id == ObjectId.Parse(Id));

            return Redirect("/Employee");


        }
        public IActionResult Edit(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Employee>("Employees");
            Employee employee = collection.Find(e => e.Id == ObjectId.Parse(Id)).FirstOrDefault();

            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(string Id, Employee employee)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("Tidsloggning");
            var collection = database.GetCollection<Employee>("Employees");
            
            employee.Id = employeeId;
            collection.ReplaceOne(e => e.Id == employeeId, employee);

            return Redirect("/Employee");
        }






        }
}