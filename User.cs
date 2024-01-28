namespace voting_uygulamasi
{
    public class User
    {
        private string name;
        private string surname;
        private string userName;
        private bool voted;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string UserName { get => userName; set => userName = value; }
        public bool Voted { get => voted; set => voted = value; }
    }
}