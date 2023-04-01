namespace Xpenses.API.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
    }
}
