
namespace ScoreApp.Domain
{
    public class Voter : User
    {
        public Voter(User user, bool isInfavor)
        {
            FirstName = user.FirstName;
            Id = user.Id;
            Image = user.Image;
            LastName = user.LastName;
            IsInFavor = isInfavor;
        }

        public bool IsInFavor { get; set; }
    }
}
