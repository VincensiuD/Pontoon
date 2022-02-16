namespace Pontoon.Data
{
    public class Session
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get ; set; }
        public Wallet Wallet { get; set; }
        public Wager Wager { get; set; }
        public CardSequence CardSequence { get; set; }
       // public List<Card> EigthDeckCards { get; set; }
       public Card[] CardDecks
        {
            get;
            set;
        }
    }
}
