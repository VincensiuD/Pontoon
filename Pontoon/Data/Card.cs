namespace Pontoon.Data
{
    public class Card
    {
         public int Id { get; set; }
         public string Rank { get; set; }
         public string Suit { get; set; }
         public string DisplayCode { get { return Suit + Rank; } }
         public string Colour
        {
            get
            {
                if (Suit == "Diamond" || Suit == "Heart")
                {
                    return "RED";
                }

                return "BLK";
            }
        
        }
         public int Value 
        {
            get
            {
                if(Rank == "J" || Rank =="Q" || Rank=="K")
                {
                    return 10;
                }
                else if(Rank == "A")
                {
                    return 1;
                }

                return int.Parse(Rank);
            }
        
        }


        public Card()
        {

        }



        public Card(int Id, string Rank, string Suit)
        {
            Id = Id;
            Rank = Rank;
            Suit = Suit;
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

