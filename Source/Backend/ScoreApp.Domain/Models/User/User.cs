
namespace ScoreApp.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
