namespace Pontoon.Data
{
    public class Card
    {
         public int Id { get; set; }
         public string Rank { get; set; }
         public string Suit { get; set; }
         public string DisplayCode { get; set; }
         public string Colour { get; set; }
         public int Value { get; set; }


        public Card()
        {

        }

            //public Card(string rank, string shapes)
            //{

            //    Rank = rank;
            //    Suit = shapes;
            //    if (rank == "A")
            //    {
            //        Value = 1;
            //    }

            //    else if (rank == "J" || rank == "Q" || rank == "K")
            //    {
            //        Value = 10;
                    
            //    }

            //    else
            //    {
            //    Value = int.Parse(rank);              
            //    }
            //}
        }
    }

