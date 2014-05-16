
namespace ScoreApp.Domain
{
    public class Pagination
    {
        public static readonly Pagination Default = Pagination.Create(1, 10);
        public int Page { get; private set; }
        public int ItemsPerPage { get; private set; }

        private Pagination(int page, int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }

        public static Pagination Create(int page, int itemsPerPage)
        {
            return new Pagination(page, itemsPerPage);
        }
    }
}
