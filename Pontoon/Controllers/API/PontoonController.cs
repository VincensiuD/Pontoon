using Pontoon.Data;
using Pontoon.Models;
using Pontoon.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoonController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly PontoonServices _pontoonServices;

        private List<int> withdrawnCardsIds = new List<int>();
        private List<string> dealerCardsDisplayCodes = new List<string>();
        private List<string> playerCardsDisplayCodes = new List<string>();
        private int mainBetResult = 0;
        private bool hitBtn = false;
        private bool splitBtn = false;
        private int dealerTotal = 0;
        private int playerTotal = 0;
        private string playerTotal2 = "";
     //   private int playerAceOut = 0;
     //   private int dealerAceOut = 0;


        public PontoonController(ApplicationDbContext applicationDbContext,  PontoonServices pontoonServices)
        {
            _applicationDbContext = applicationDbContext;
            _pontoonServices = pontoonServices;
        }



        [HttpGet, Route("PlaceYourBet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int DisplayMoney()
        {
            Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);
            int money = wallet.Money;
            return money;
        }

        [HttpPost, Route("Wagers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Bet(Wager wagerFromUser)
        {
            Wager wagerSet = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);

            wagerSet.PairsWager = wagerFromUser.PairsWager;
            wagerSet.MainWager = wagerFromUser.MainWager;

            _applicationDbContext.Wagers.Update(wagerSet);

            Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);

            wallet.Money = wallet.Money - (wagerSet.MainWager + wagerSet.PairsWager);
            _applicationDbContext.Wallets.Update(wallet);
            _applicationDbContext.SaveChanges();

            return CreatedAtAction(nameof(Created), wagerSet);
        }

        [HttpGet, Route("InitialDeal")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InitialCards()
        {
            var allCardsList = await _applicationDbContext.Cards.ToListAsync();
            Wager wager = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);
            if ((allCardsList != null) && (wager.PairsWager >= 0) && (wager.MainWager != 0))
            {
                try
                {
                    _pontoonServices.CleanSlate();

                    var dealerCards = _pontoonServices.GetListOfCardsFrom("Dealer");
                    var playerCards = _pontoonServices.GetListOfCardsFrom("Player");
                   
                    Card dealerCard1 = _pontoonServices.DrawRandomCard();
                    dealerCards.Add(dealerCard1);

                    Card playerCard1 = _pontoonServices.DrawRandomCard();
                    Card playerCard2 = _pontoonServices.DrawRandomCard();
                    playerCards.Add(playerCard1);
                    playerCards.Add(playerCard2);

                    //delete this later
                    int useless = withdrawnCardsIds.Count();

                    //Random random = new Random();

                    //int number = 0;
                    //for (int i = 0; i < 3; i++)
                    //{
                    //    do
                    //    {
                    //        number = random.Next(1, 417);
                    //    } while (withdrawnCardsIds.Contains(number));
                    //    withdrawnCardsIds.Add(number);

                    //}

                    dealerCardsDisplayCodes.Add(dealerCard1.DisplayCode);

                    _applicationDbContext.SaveChanges();

                    int pairsBetResult = _pontoonServices.ProcessPairsBet(playerCard1, playerCard2);
               

                    bool pontoonOrNot = _pontoonServices.CheckPontoon(playerCard1, playerCard2);


                    if (pontoonOrNot)
                    {
                        mainBetResult = _pontoonServices.PontoonPayOut("Main");
                        hitBtn = true;
                        splitBtn = true;
                        playerTotal = 21;
                    }
                    else
                    {
                        playerTotal = _pontoonServices.SumCardTotal(playerCards);

                        if (_pontoonServices.CheckIfCardIsAce(playerCard1) || _pontoonServices.CheckIfCardIsAce(playerCard2))
                        {
                            playerTotal2 = _pontoonServices.AceDisplayDoubleValue(playerCards,playerTotal);
                        }

                    }


                    Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);


                    for (int i = 0; i < playerCards.Count; i++)
                    {
                        playerCardsDisplayCodes.Add(playerCards[i].DisplayCode);
                    }
                    

                    var InitialCardsDTO = new
                    {
                        dealerCardsDisplayCodes,
                        playerCardsDisplayCodes,
                        pairsBetResult,
                        mainBetResult,
                        dealerTotal = dealerCard1.Value,
                        playerTotal,
                        playerTotal2,
                        wallet = wallet.Money,
                        hitBtn,
                        splitBtn

                    };



                    return Ok(InitialCardsDTO);
                }
                catch (Exception)
                {

                    return StatusCode(400);
                }
            }

            return StatusCode(401);
        }


        [HttpGet, Route("AddCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneCard()
        {
            var allCardsList = await _applicationDbContext.Cards.ToListAsync();
            if (allCardsList != null)
            {
                try
                {
                    var playerCards = _pontoonServices.GetListOfCardsFrom("Player");

                    Card withdrawedCard = _pontoonServices.DrawRandomCard();

                    playerCards.Add(withdrawedCard);
                    _applicationDbContext.SaveChanges();


                   foreach (var item in playerCards)
                    {
                        playerCardsDisplayCodes.Add(item.DisplayCode);
                    }

                    int playerTotal = _pontoonServices.SumCardTotal(playerCards);
          
                    string playerTotal2 = _pontoonServices.AceDisplayDoubleValue(playerCards, playerTotal);

                    hitBtn = _pontoonServices.HitBtnStatus(playerTotal);

                    int mainBetResult = _pontoonServices.CheckIfTotal21(playerTotal, playerTotal2);

                    if (playerTotal < 21 && playerCards.Count == 5)
                    {
                       mainBetResult =  _pontoonServices.CalculatePayOut(2);
                       hitBtn = true;                        
                        
                    }
                    Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);


                    var withdrawedCardDTO = new
                    {
                        playerCardsDisplayCodes,
                        mainBetResult,
                        playerTotal,
                        playerTotal2,
                        hitBtn,
                        wallet=wallet.Money,

                    };

                    return Ok(withdrawedCardDTO);
                }
                catch (Exception)
                {

                    return StatusCode(400);
                }
            }
            return NotFound();
        }   


        [HttpGet, Route("SplittingCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SplittingCard()
        {
            var allCardsList = await _applicationDbContext.Cards.ToListAsync();
            Wager wager = _applicationDbContext.Wagers.FirstOrDefault(x => x.Id == 1);
            if ((allCardsList != null) && (wager.MainWager != 0))
            {
                try
                {
                    List<Card> dealerCards = _pontoonServices.GetListOfCardsFrom("Dealer");
                    List<Card> playerCards = _pontoonServices.GetListOfCardsFrom("Player");

                    int[] playerCardsIds = new int[2];

                    for (int i = 0; i < 2; i++)
                    {
                        playerCardsIds[i] = playerCards[i].Id;
                    }

                    playerCards = new List<Card>();

                    List<Card> playerCardsA = _pontoonServices.GetListOfCardsFrom("PlayerSplit1");
                    List<Card> playerCardsB = _pontoonServices.GetListOfCardsFrom("PlayerSplit2");

                    playerCardsA.Add(allCardsList.FirstOrDefault(x => x.Id == playerCardsIds[0]));
                    playerCardsB.Add(allCardsList.FirstOrDefault(x => x.Id == playerCardsIds[1]));


                    Card withdrawnCard = _pontoonServices.DrawRandomCard();

                    //dealerCardsDisplayCodes.Add(dealer.ListOfCards[0].DisplayCode);

                    //foreach (var item in playerA.ListOfCards)
                    //{
                    //    PlayersCards playersCards = new PlayersCards()
                    //    {
                    //        DisplayCode = item.DisplayCode
                    //    };

                    //    _applicationDbContext.PlayersCards.Add(playersCards);
                    //}
                    //_applicationDbContext.SaveChanges();


                    int mainBetResult = 0;

                    //    bool pontoonOrNot = _pontoonServices.checkPontoon(playerCard1, playerCard2);


                    //if (pontoonOrNot)
                    //{
                    //    mainBetResult = _pontoonServices.PontoonPayOut();
                    //    hitBtn = true;
                    //    splitBtn = true;
                    //    playerTotal = 21;
                    //}
                    //else
                    //{
                    //    if (_pontoonServices.checkIfCardIsAce(playerCard1) || _pontoonServices.checkIfCardIsAce(playerCard2))
                    //    {
                    //        playerAceOut++;

                    //        playerTotal = playerCard1.Value1 + playerCard2.Value1;
                    //        playerTotal2 = "/ " + (playerTotal + 10).ToString();
                    //    }

                    //    else
                    //    {
                    //        playerTotal = playerCard1.Value1 + playerCard2.Value1;
                    //    }



                    //}


                    Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);



                    //foreach (var item in _applicationDbContext.PlayersCards.ToList())
                    //{
                    //    playerCardsDisplayCodes.Add(item.DisplayCode);
                    //}

                    var InitialCardsDTO = new
                    {
                        dealerCardsDisplayCodes,
                        playerCardsDisplayCodes,
                        mainBetResult,
                        // dealerTotalA = dealerCard1.Value1,
                        playerTotal,
                        playerTotal2,
                        wallet = wallet.Money,
                        hitBtn,

                    };



                    return Ok(InitialCardsDTO);
                }
                catch (Exception)
                {

                    return StatusCode(400);
                }
            }

            return StatusCode(401);
        }


       




        [HttpGet, Route("DealerPlay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DealerPlay()
        {
            try
            {

                var dealerCards = _pontoonServices.GetListOfCardsFrom("Dealer");

                var playerCards = _pontoonServices.GetListOfCardsFrom("Player");
             
                bool playerAceExist = _pontoonServices.AceInListChecker(playerCards);

                int playerTotal = _pontoonServices.SumCardTotal(playerCards);

                if (playerAceExist)
                {
                    playerTotal =  _pontoonServices.SumCardTotalAce(playerCards);
                }

                string dealerTotal2string = "";

                do
                {
                    dealerTotal = 0;

                    Card withdrawnCard = _pontoonServices.DrawRandomCard();

                    dealerCards.Add(withdrawnCard); 
                    _applicationDbContext.SaveChanges();

                   
                    bool dealerAceExist = _pontoonServices.AceInListChecker(dealerCards);

                    if (dealerAceExist)
                    {
                       dealerTotal = _pontoonServices.SumCardTotalAce(dealerCards);                     
                    }
                    else
                    {
                        dealerTotal = _pontoonServices.SumCardTotal(dealerCards);
                    }

                } while (dealerTotal < 17);

                int mainBetResult = _pontoonServices.CompareDealerPlayer(dealerTotal, playerTotal);


                _applicationDbContext.SaveChanges();

                foreach (var item in dealerCards)
                {
                    dealerCardsDisplayCodes.Add(item.DisplayCode);
                }

                Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);
                var StandDTO = new
                {
                    dealerCardsDisplayCodes,
                    mainBetResult,
                    dealerTotal,
                    dealerTotal2string,
                    wallet = wallet.Money,

                };

                return Ok(StandDTO);
            }
            catch (Exception)
            {

                return StatusCode(400);
            }
        }
    }
}
