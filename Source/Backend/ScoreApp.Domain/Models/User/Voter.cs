
namespace ScoreApp.Domain
{
    public class Voter : User
    {
        public Voter(User user)
        {
            FirstName = user.FirstName;
            Id = user.Id;
            Image = user.Image;
            LastName = user.LastName;
        }

        public bool IsInFavor { get; set; }
    }
}
