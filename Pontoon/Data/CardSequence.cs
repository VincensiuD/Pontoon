using System.Collections.Generic;

namespace Pontoon.Data
{
    public class CardSequence
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public List<Card> ListOfCards { get; set; } 
        
    }
}
