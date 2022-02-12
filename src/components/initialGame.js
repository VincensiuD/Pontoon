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

function checkConsole() {
    console.log(playerCards);
}

async function split(){

    navigate("/splitGame");

}

async function stand(){
    let response = await fetch('https://localhost:44321/API/BlackJack/DealerPlay'); 
    if (!response.ok) {
        navigate("/errorPage");
        console.log("triggered")
       }   
     else{
    let data = await response.json();
    console.log(data);
    setDealerCards(data.listOfDealerCards);
    setMainPayOut(data.mainBetResult);
    setDealerTotal(data.dealerTotal);
    setWallet(data.wallet);
    setHitBtn(true);
    setSplitBtn(true);
    }

}


async function hit(){

   let response = await fetch('https://localhost:44321/API/BlackJack/AddCard'); 
    if (!response.ok) {
        navigate("/errorPage");
        console.log("triggered")
       }   
     else{
    let data = await response.json();
    console.log(data);
    setPlayerCards(data.listOfPlayerCards);
    setMainPayOut(data.mainBetResult);
    setPlayerTotal(data.playerTotal);
    setPlayerTotal2(data.playerTotal2);
    setHitBtn(data.hitBtn);
    setSplitBtn(true);
    setWallet(data.wallet);
    }

} 

useEffect(() => {
    
async function initial3cards() {
    let response = await fetch('https://localhost:44321/API/BlackJack/InitialDeal') 

    if (!response.ok) {
       navigate("/errorPage");
      }   
    else{
       let data = await response.json();
       setPlayerCards(data.listOfPlayerCards);
       setDealerCards(data.listOfDealerCards);
       setPairsPayOut(data.pairsBetResult);
       setMainPayOut(data.mainBetResult);
       setWallet(data.wallet);
       setSplitBtn(data.splitBtn);
       setHitBtn(data.hitBtn);
       setPlayerTotal(data.playerTotal);
       setDealerTotal(data.dealerTotal);
       setPlayerTotal2(data.playerTotal2);
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
                <button disabled={hitBtn} onClick={hit}>Hit</button>
                <button disabled={hitBtn} onClick={stand} >Stand</button>
                <button disabled={hitBtn}>Double</button>
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