using Pontoon.Data;

namespace Pontoon.Models
{
    public class PontoonDTO
    {



        public int Money { get; set; }
        public List<string> DealerCardsDisplayCodes { get; set; }
        public List<string> PlayerCardsDisplayCodes { get; set; }
        public int PairsBetResult { get; set; }
        public int MainBetResult { get; set; }
        public int DealerTotal { get; set; }
        public int PlayerTotal { get; set; }
        public string PlayerTotal2 { get; set; }
        public string DealerTotal2 {get; set;}
        public bool HitBtn { get; set; }
        public bool SplitBtn { get; set; }
        public bool DoubleBtn { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> PlayerCardsADisplayCodes  { get; set; }
        public List<string> PlayerCardsBDisplayCodes { get; set; }
        public int PairsWager { get; set; }
        public int MainWager { get; set; }  


    }

   

}
