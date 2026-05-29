using MongoDB.Bson;

namespace Tidsloggning.Models

{
    public class Employee
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Birthyear { get; set; }
        public string WorkId { get; set; } // ha kvar de ifall den skulle behövas senare
        
    }
}
