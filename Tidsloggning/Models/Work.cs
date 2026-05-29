using MongoDB.Bson;

namespace Tidsloggning.Models
{
    public class Work
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public ObjectId EmployeeId { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }

    }
}
