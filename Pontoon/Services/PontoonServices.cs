using Microsoft.EntityFrameworkCore;
using Pontoon.Data;

namespace Pontoon.Services
{  
/// <summary>
/// Class that stores the logic of the game
/// </summary>
    public class PontoonServices
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PontoonServices(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }


        public List<Card> GetListOfCardsFrom(string someone)
        {
            int personId;
            switch (someone)
            {
                case "Dealer":
                    personId = 1;
                    break;
                case "Player":
                    personId = 2;
                    break;
                case "PlayerSplit1":
                    personId = 3;
                    break;
                case "PlayerSplit2":
                    personId = 4;
                    break;
                default:
                    personId =0;
                    break;
            }

            var person = _applicationDbContext.CardSequences
                       .Include(x => x.ListOfCards).FirstOrDefault(x => x.Id == personId);

            var listOfCards = person.ListOfCards;

            return listOfCards;
        }


        public List<int> GetWithdrawnCardIds()
        {
            List<Card> dealer = GetListOfCardsFrom("Dealer");
            List<Card> player = GetListOfCardsFrom("Player");
            List<Card> playerSplit1 = GetListOfCardsFrom("PlayerSplit1");
            List<Card> playerSplit2 = GetListOfCardsFrom("PlayerSplit2");

            List<Card>[] arrayOfLists = new List<Card>[4] {dealer,player,playerSplit1,playerSplit2 };

            List<int> cardIds = new List<int>();
            

            foreach (var listOfCards in arrayOfLists)
            {
                foreach (Card card in listOfCards)
                {
                    cardIds.Add(card.Id);
                }
            }
                return cardIds;
                       
        }

        public Card DrawRandomCard()
        {
            List<int> listOfDrawnCardsIds = GetWithdrawnCardIds();

            Random random = new Random();
            int number = 0;
            
                do
                {
                    number = random.Next(1, 417);
                } while (listOfDrawnCardsIds.Contains(number));
                listOfDrawnCardsIds.Add(number);

           Card withdrawnCard = _applicationDbContext.Cards.FirstOrDefault(x => x.Id == number);
            return withdrawnCard;

        }


        /// <summary>
        /// This method calculate the PayOut if the player has Pontoon Wins ( Ace & 10-valued cards in the initial deal)
        /// </summary>
        /// <returns> int value of payout </returns>
        public int PontoonPayOut(string type)
        {
            Wager wagerSet = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);

            int mainPayOut = 5 * wagerSet.MainWager / 2;

            UpdateWalletAndWager(mainPayOut, type);

            return mainPayOut;

        }

        /// <summary>
        /// Checking if the initial dealt cards are Pontoon
        /// </summary>
        /// <param name="cardA"></param>
        /// <param name="cardB"></param>
        /// <returns></returns>
        public bool CheckPontoon(Card cardA, Card cardB)
        {
            if (cardA.Value + cardB.Value == 11)
            {
                if (CheckIfCardIsAce(cardB) || CheckIfCardIsAce(cardA))
                {

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checking if a particular given card is an Ace card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool CheckIfCardIsAce(Card card)
        {
            return card.Rank == "A" ? true : false;
        }


        /// <summary>
        /// Get all the displayCodes from a List of cards
        /// </summary>
        /// <param name="listOfCards"></param>
        /// <returns></returns>
        public List<string> GetDisplayCodes(List<Card> listOfCards)
        {
            List<string> displayCodes = new List<string>();

            foreach (var item in listOfCards)
            {
                displayCodes.Add(item.DisplayCode);
            }

            return displayCodes;
        }


        /// <summary>
        /// Sum the total values of Cards in a given list.
        /// This method only applies where there is no ace in the list  or the smallest value is desired
        /// </summary>
        /// <param name="listOfCards"></param>
        /// <param name="aceOut">This parameter checks if there is an Ace in the list of cards</param>
        /// <returns></returns>
        public int SumCardTotal(List<Card> listOfCards)
        {
            int total = listOfCards.Sum(x => x.Value);

            return total;
        }
        /// <summary>
        /// Sum the total values of Cards in a given list where there is an ace in the list
        /// </summary>
        /// <param name="listOfCards"></param>
        /// <returns></returns>
        public int SumCardTotalAce(List<Card> listOfCards)
        {
            int total = SumCardTotal(listOfCards);

            if (AceInListChecker(listOfCards) && total < 12)
            {
                return total + 10;
            }

            return total;
        }

        /// <summary>
        /// This method add the Money in the Wallet based on your winnings
        /// </summary>
        /// <param name="mainPayOut"></param>
        public void UpdateWalletAndWager(int PayOut, string type)
        {
            Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);
            Wager wagerSet = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);
            wallet.Money = wallet.Money + PayOut;
            _applicationDbContext.Wallets.Update(wallet);

            if (type == "Main")
            {
                wagerSet.MainWager = 0;
            }
            else if (type == "Pairs")
            {
                wagerSet.PairsWager = 0;
            }

            _applicationDbContext.Wagers.Update(wagerSet);
            _applicationDbContext.SaveChanges();

        }

        /// <summary>
        /// Checking if there's an Ace in player's card and if double value is required (Ace can either be 1 or 11),
        /// Hence there could be 2 possible values
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string AceDisplayDoubleValue(List<Card> cards, int total)
        {
            bool thereIsAce = AceInListChecker(cards);

            if (thereIsAce  && total < 12)
            {
                int total2 = total + 10;

                return "/ " + total2.ToString();
            }

            return "";
        }

        /// <summary>
        /// Check if there's an Ace in the list of Cards
        /// </summary>
        /// <param name="cardsList"></param>
        /// <returns>true or false</returns>
        public bool AceInListChecker(List<Card> cardsList) 
        {
            int amountOfAce = 0;
            foreach (var item in cardsList)
            {
                if (CheckIfCardIsAce(item))
                {
                    amountOfAce++;
                }
            }

            return amountOfAce>0 ? true : false;
        }

        /// <summary>
        /// This method is being called if the player's cards total value reached 21
        /// </summary>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        public int CalculatePayOut(int multiplier)
        {
            Wager wager = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);

            int payOut = wager.MainWager * multiplier;

    
            UpdateWalletAndWager(payOut,"Main");

            return payOut;
        }

        /// <summary>
        /// Compare Dealer's total and Player's total value in the end of the round (after player does stand)
        /// </summary>
        /// <param name="dealerTotal"></param>
        /// <param name="playerTotal"></param>
        /// <returns></returns>
        public int CompareDealerPlayer(int dealerTotal, int playerTotal)
        {
            if (dealerTotal > 21)
            {
                int payOut = CalculatePayOut(2);
                return payOut;
            }
            else if (playerTotal < dealerTotal)
            {
                int payOut = CalculatePayOut(0);
                return payOut;
            }
            else if (playerTotal == dealerTotal)
            {
                int payOut = CalculatePayOut(1);
                return payOut;
            }
            else if (dealerTotal < playerTotal)
            {
                int payOut = CalculatePayOut(2);
                return payOut;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// This method checks whether Hit button should be hidden or not in the userInterface
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public bool HitBtnStatus(int total, string total2)
        {
            if (total >= 21 || total2 == "/ 21")
            {
                return true;
            }

            return false;
        }


        public int CheckIfTotal21(int total, string total2) //you need this if players card reaches 21 but neither Pontoon nor 5cards
        {
            if (total == 21 || total2 == "/ 21")
            {
                return CalculatePayOut(2);
            }
            return 0;

        }
        /// <summary>
        /// Splitting card -  I N C O M P L E T E
        /// </summary>
        public void SplitCards()
        {
            // ISSUE:
            /* 1. Must be able to split player into playerSplit1 and playerSplit2
             * 2. Clear player
             * 3. Do the logic
             * 4. clear PS1 and PS2
             * 
             */


            var playersCards = _applicationDbContext.CardSequences.FirstOrDefault(x => x.Id == 2);

            int secondCardId = playersCards.ListOfCards[1].Id;
            Card secondCard = playersCards.ListOfCards[1];

            var playersCardsB = _applicationDbContext.CardSequences.FirstOrDefault(y => y.Id == 3);

            playersCards.ListOfCards.Remove(secondCard);

            var transferredCard = _applicationDbContext.Cards.FirstOrDefault(x => x.Id == secondCardId);
            playersCardsB.ListOfCards.Add(transferredCard);

            _applicationDbContext.SaveChanges();


        }
        /// <summary>
        /// Checking the pay odds for the Pairs bet
        /// </summary>
        /// <param name="cardA"></param>
        /// <param name="cardB"></param>
        /// <returns></returns>
        public int CheckPairsPayOdds(Card cardA, Card cardB)
        {
            if (cardA.Rank != cardB.Rank)
            {
                return 0;
            }

            if (cardA.Colour != cardB.Colour)
            {
                return 6;
            }

            if (cardA.DisplayCode != cardB.DisplayCode)
            {
                return 13;
            }

            return 25;
        }

        /// <summary>
        /// Calculate and process payout for Pairs bet
        /// </summary>
        /// <param name="cardA"></param>
        /// <param name="cardB"></param>
        /// <returns></returns>
        public int ProcessPairsBet(Card cardA, Card cardB)
        {
            int odds = CheckPairsPayOdds(cardA, cardB);

            Wager wagerSet = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);

            int pairsPayOut = odds * wagerSet.PairsWager;

            UpdateWalletAndWager(pairsPayOut, "Pairs");

            return pairsPayOut;
        }


        public void CleanSlate()
        {
            for (int i = 1; i < 5; i++)       
            {
                var person = _applicationDbContext.CardSequences
                       .Include(x => x.ListOfCards).FirstOrDefault(x => x.Id == i);

                person.ListOfCards = new List<Card>();

                _applicationDbContext.SaveChanges();
            }

        }


        public bool DoubleBet()
        {
            Wager wager = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);
            Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x=> x.Id == 1);

               int mainWager = wager.MainWager;
               int money = wallet.Money;

               int newMoney = money - mainWager;

                if (newMoney>=0)
                {
                    wager.MainWager = mainWager * 2;
                    wallet.Money = newMoney;
                    _applicationDbContext.SaveChanges();

                    return true;
                }

                return false;                 

        }

        public class BettingDTO
        {
            public int Money { get; set; }
            public int MainWager { get; set; }        
            public int PairsWager { get; set; }
        
        }

        public BettingDTO MonetaryChecker()
        {
            Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);

            Wager wagers = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id ==1);

            BettingDTO dto = new BettingDTO()
            {
                Money = wallet.Money,
                MainWager = wagers.MainWager,
                PairsWager = wagers.PairsWager,
            };

            return dto;
        }
        
    }
}
