import React,  { useState, useEffect } from "react";
import { useNavigate,Link } from "react-router-dom";
import { generateImage } from "./picComponent";


export function InitialGame(){

let navigate = useNavigate();

let [playerCards, setPlayerCards] = useState([]);
let [dealerCards, setDealerCards] = useState([]);
let [pairsPayOut,setPairsPayOut] = useState(0);
let [mainPayOut,setMainPayOut] = useState(0);
let [wallet,setWallet] = useState(0);
let [hitBtn,setHitBtn] = useState(false);
let [splitBtn,setSplitBtn] = useState(false);
let [dealerTotal,setDealerTotal] = useState(0);
let [playerTotal,setPlayerTotal] = useState(0);
let [playerTotal2,setPlayerTotal2] = useState("");
let [message,setMessage] = useState("");
let [endGame,setEndGame] = useState(false);
let [mainWager,setMainWager] = useState(0);
let [pairsWager,setPairsWager] = useState(0);


function checkConsole() {
    console.log(playerCards);
}

async function split(){

    navigate("/splitGame");

}

async function stand(){
    let response = await fetch('https://localhost:7100/API/Pontoon/DealerPlay'); 
    if (!response.ok) {
        navigate("/errorPage");
        console.log("triggered")
       }   
     else{
    let data = await response.json();
    console.log(data);
    setDealerCards(data.dealerCardsDisplayCodes);
    setMainPayOut(data.mainBetResult);
    setDealerTotal(data.dealerTotal);
    setWallet(data.money);
    setHitBtn(true);
    setSplitBtn(true);
    setMainWager(data.mainWager);
    setPairsWager(0);
    }

}


async function double(){
     let response = await fetch('https://localhost:7100/API/Pontoon/Doubling'); 
     if (response.ok) {
     let data = await response.json();
     console.log(data);
     setWallet(data.money);
     setDealerCards(data.dealerCardsDisplayCodes);
     setDealerTotal(data.dealerTotal);
     setPlayerCards(data.playerCardsDisplayCodes);
     setMainPayOut(data.mainBetResult);
     setPlayerTotal(data.playerTotal);
     setPlayerTotal2(data.playerTotal2);
     setHitBtn(data.hitBtn);
     setSplitBtn(true);
     setMainWager(data.mainWager);
    setPairsWager(0);
    }
}

async function hit(){

   let response = await fetch('https://localhost:7100/API/Pontoon/AddCard'); 
    if (!response.ok) {
        navigate("/errorPage");
        console.log("triggered")
       }   
     else{
    let data = await response.json();
    console.log(data);
    setPlayerCards(data.playerCardsDisplayCodes);
    setMainPayOut(data.mainBetResult);
    setPlayerTotal(data.playerTotal);
    setPlayerTotal2(data.playerTotal2);
    setHitBtn(data.hitBtn);
    setSplitBtn(true);
    setWallet(data.money);
    setMainWager(data.mainWager);
    setPairsWager(0);
return hitBtn;
    }

} 

useEffect(() => {
    
async function initial3cards() {
    let response = await fetch('https://localhost:7100/API/Pontoon/InitialDeal') 

    if (!response.ok) {
       navigate("/errorPage");
      }   
    else{
       let data = await response.json();
       setPlayerCards(data.playerCardsDisplayCodes);
       setDealerCards(data.dealerCardsDisplayCodes);
       setPairsPayOut(data.pairsBetResult);
       setMainPayOut(data.mainBetResult);
       setWallet(data.money);
       setSplitBtn(data.splitBtn);
       setHitBtn(data.hitBtn);
       setPlayerTotal(data.playerTotal);
       setDealerTotal(data.dealerTotal);
       setPlayerTotal2(data.playerTotal2);
       setMainWager(data.mainWager);
       setPairsWager(data.pairsWager);
    }
       
 }
 initial3cards().catch((error) => {
    console.error('Error:', error);
    });
}, []); 
    return(
        <div>         
            <div>
                <p>
                    Dealer's card total : {dealerTotal}
                </p>
                {/* <img src={generateImage(dealerCards[0])} alt="dealer's card image" height={100} /> */}
                {dealerCards.map( (value,index) => 
                <img key={index} src={generateImage(value)} alt="dealer's card images" height={100} />
                )}
            </div>
            <div>
                <p>
                    Player's card total : {playerTotal} {playerTotal2}
                </p>
                {playerCards.map( (value,index) =>
                <img key={index} src={generateImage(value)} alt="player's card images" height={100} />
                )}
            </div>   
            <div>
                <span>Main Wager:</span> <span>{mainWager}</span>
                <br/>
            </div>
            <div>
                <button disabled={hitBtn} onClick={hit}>Hit</button>
                <button disabled={hitBtn} onClick={stand} >Stand</button>
                <button disabled={hitBtn} onClick={double}>Double</button>
                <button disabled={splitBtn} onClick={split}>Split</button>
            </div>
            <button onClick={checkConsole}>Check Console</button>
            <button onClick={() => navigate('/bettingPage')}>Play Again</button>
            <div>
                <span>Pairs Payout:</span> <span>{pairsPayOut}</span>
                <br/>
                <span>Main Payout:</span> <span>{mainPayOut}</span>
                <br/>
                <span>Wallet:</span> <span>{wallet}</span>
            </div>
        </div>
    )
}