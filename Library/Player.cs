namespace Library
{
    public class Player
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Account Account { get; private set; }

        public Player(int id, string firstName, string lastName, string email, string password,  string currency)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Account = new Account(UniqueIdGenerator.GenerateUniqueId(), currency);
            
        }

        public bool IsPasswordValid(string password)
        {
            return Password.Equals(password);
        }

        public void Deposit(decimal amount, string currency)
        {
            Account.Deposit(amount, currency);
        }

        public void Withdraw(decimal amount, string currency)
        {
            Account.Withdraw(amount, currency);
        }
        

    }
}