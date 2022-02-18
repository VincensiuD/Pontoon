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

namespace Pontoon.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoonController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly PontoonServices _pontoonServices;
     
        private List<string> dealerCardsDisplayCodes = new List<string>();
        private List<string> playerCardsDisplayCodes = new List<string>();
        private int mainBetResult = 0;
        private bool hitBtn = false;
        private bool splitBtn = false;
        private int dealerTotal = 0;
        private int playerTotal = 0;
        private string playerTotal2 = "";


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
                    //Card playerCard1 = _applicationDbContext.Cards.FirstOrDefault(x => x.Id == 1);
                    //Card playerCard2 = _applicationDbContext.Cards.FirstOrDefault(x => x.Id == 2);


                    playerCards.Add(playerCard1);
                    playerCards.Add(playerCard2);

                    
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

                    PontoonDTO dto = new PontoonDTO()
                    {
                        DealerCardsDisplayCodes = dealerCardsDisplayCodes,
                        PlayerCardsDisplayCodes = playerCardsDisplayCodes,
                        PairsBetResult = pairsBetResult,
                        MainBetResult = mainBetResult,
                        DealerTotal = dealerCard1.Value,
                        PlayerTotal = playerTotal,
                        PlayerTotal2 = playerTotal2,
                        Money = wallet.Money,
                        HitBtn = hitBtn,
                        SplitBtn = splitBtn

                    };

                    return Ok(dto);
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
                    PontoonDTO dto = HitMethod();

                    return Ok(dto);
                }
                catch (Exception)
                {

                    return StatusCode(401);
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
                    dealerCardsDisplayCodes.Add(dealerCards[0].DisplayCode);
                    List<Card> playerCards = _pontoonServices.GetListOfCardsFrom("Player");

                    int[] playerCardsIds = new int[2];

                  
                    playerCardsIds[0] = playerCards[1].Id;
                    playerCardsIds[1] = playerCards[0].Id;


                    playerCards = new List<Card>();
                 

                    List<Card> playerCardsA = _pontoonServices.GetListOfCardsFrom("PlayerSplit1");
                    List<Card> playerCardsB = _pontoonServices.GetListOfCardsFrom("PlayerSplit2");

                    playerCardsA.Add(allCardsList.FirstOrDefault(x => x.Id == playerCardsIds[0]));
                    List<string> playerCardsADisplayCodes = new List<string>();

                    playerCardsB.Add(allCardsList.FirstOrDefault(x => x.Id == playerCardsIds[1]));
                    List<string> playerCardsBDisplayCodes = new List<string>();

                    _applicationDbContext.SaveChanges();

                   Card withdrawnCardA = _pontoonServices.DrawRandomCard();
                   Card withdrawnCardB = _pontoonServices.DrawRandomCard();

                    playerCardsA.Add(withdrawnCardA);
                    playerCardsB.Add(withdrawnCardB);

                    _applicationDbContext.SaveChanges();

                    playerCardsADisplayCodes = _pontoonServices.GetDisplayCodes(playerCardsA);
                    playerCardsBDisplayCodes = _pontoonServices.GetDisplayCodes(playerCardsB);

                    Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);

                    PontoonDTO dto = new PontoonDTO()
                    {
                        DealerCardsDisplayCodes = dealerCardsDisplayCodes,
                        PlayerCardsADisplayCodes = playerCardsADisplayCodes,
                        PlayerCardsBDisplayCodes = playerCardsBDisplayCodes,
                        //MainBetResult,
                        //playerTotal,
                        //playerTotal2,
                        Money = wallet.Money,
                        HitBtn = false

                    };



                    return Ok(dto);
                }
                catch (Exception)
                {

                    return StatusCode(400);
                }
            }

            return StatusCode(401);
        }



        [HttpGet, Route("Doubling")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DoubleBet()
        {
            try
            {
                bool succesful = _pontoonServices.DoubleBet();

                if (succesful)
                {
                    PontoonDTO dto = HitMethod();

                    if (!dto.HitBtn) //means the player has not bust
                    {
                        PontoonDTO StandDto = StandMethod();

                        StandDto.PlayerTotal = dto.PlayerTotal;
                        StandDto.PlayerTotal2 = dto.PlayerTotal2;
                        StandDto.PlayerCardsDisplayCodes = dto.PlayerCardsDisplayCodes;

                        return Ok(StandDto);
                    }

                    return Ok(dto);
                }

                return StatusCode(400);
            }
            catch (Exception)
            {

                return StatusCode(400);
            }
        }



        [HttpGet, Route("DealerPlay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DealerPlay()
        {
            try
            {
                var dto =  StandMethod();

                return Ok(dto);
            }
            catch (Exception)
            {

                return StatusCode(401);
            }            

        }



        public PontoonDTO StandMethod()
        {

                var dealerCards = _pontoonServices.GetListOfCardsFrom("Dealer");

                var playerCards = _pontoonServices.GetListOfCardsFrom("Player");

                bool playerAceExist = _pontoonServices.AceInListChecker(playerCards);

                int playerTotal = _pontoonServices.SumCardTotal(playerCards);

                if (playerAceExist)
                {
                    playerTotal = _pontoonServices.SumCardTotalAce(playerCards);
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


            PontoonDTO dto = new PontoonDTO()
            {
                // PlayerCardsDisplayCodes = playerCardsDisplayCodes,
                DealerCardsDisplayCodes = dealerCardsDisplayCodes,
                Money = wallet.Money,
                MainBetResult = mainBetResult,
                DealerTotal2 = dealerTotal2string,
                DealerTotal = dealerTotal,
                HitBtn = true
            };

                return dto;
            
        }


        public PontoonDTO HitMethod()
        {
            
                var playerCards = _pontoonServices.GetListOfCardsFrom("Player");

            // Card withdrawedCard = _pontoonServices.DrawRandomCard();
            Card withdrawedCard = _applicationDbContext.Cards.FirstOrDefault(x => x.Id == 8);

            playerCards.Add(withdrawedCard);
                _applicationDbContext.SaveChanges();


                foreach (var item in playerCards)
                {
                    playerCardsDisplayCodes.Add(item.DisplayCode);
                }

                int playerTotal = _pontoonServices.SumCardTotal(playerCards);

                string playerTotal2 = _pontoonServices.AceDisplayDoubleValue(playerCards, playerTotal);

                int mainBetResult = _pontoonServices.CheckIfTotal21(playerTotal, playerTotal2);

                hitBtn = _pontoonServices.HitBtnStatus(playerTotal, playerTotal2);

                if(hitBtn)
                {
                var dealerCards = _pontoonServices.GetListOfCardsFrom("Dealer");
                dealerCardsDisplayCodes.Add(dealerCards[0].DisplayCode);
                }

               

                if (playerTotal < 21 && playerCards.Count == 5)
                {
                    mainBetResult = _pontoonServices.CalculatePayOut(2);
                    hitBtn = true;

                }
                Wallet wallet = _applicationDbContext.Wallets.FirstOrDefault(x => x.Id == 1);


            PontoonDTO dto = new PontoonDTO()
            {
                    DealerCardsDisplayCodes = dealerCardsDisplayCodes,
                    PlayerCardsDisplayCodes = playerCardsDisplayCodes,
                    MainBetResult = mainBetResult,
                    PlayerTotal = playerTotal,
                    PlayerTotal2 = playerTotal2,
                    HitBtn = hitBtn,
                    Money = wallet.Money,

                };

            return dto;
            
        }
    }
}
