namespace voting_uygulamasi
{
    public class Category
    {
        private string categoryName;
        private int numberOfVote;

        public string CategoryName { get => categoryName; set => categoryName = value; }
        public int NumberOfVote { get => numberOfVote; set => numberOfVote = value; }
    }
}