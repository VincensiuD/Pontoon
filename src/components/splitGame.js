import React,  { useState, useEffect } from "react";
import { useNavigate,Link } from "react-router-dom";
import { generateImage } from "./picComponent";
import { Mainstyle } from "./styleLibrary";




export function SplitGame(){
const navigate = useNavigate();
let [playerCardsA, setPlayerCardsA] = useState([]);
let [playerCardsB, setPlayerCardsB] = useState([]);
let [dealerCards, setDealerCards] = useState([]);
let [mainPayOut,setMainPayOut] = useState(0);
let [wallet,setWallet] = useState(0);
let [hitBtn,setHitBtn] = useState(false);
let [dealerTotal,setDealerTotal] = useState(0);
let [playerTotalA,setPlayerTotalA] = useState(0);
let [playerTotal2A,setPlayerTotal2A] = useState("");
let [playerTotalB,setPlayerTotalB] = useState(0);
let [playerTotal2B,setPlayerTotal2B] = useState("");
let [hideA,setHideA] = useState(false);
let [hideB,setHideB] = useState(false);

async function hit(){

   let response = await fetch('https://localhost:44321/API/BlackJack/AddCard'); 
    if (!response.ok) {
        navigate("/errorPage");
        console.log("triggered")
       }   
     else{
    let data = await response.json();
    console.log(data);
    setPlayerCardsA(data.listOfPlayerCardsA);
    setPlayerCardsB(data.listOfPlayerCardsB);
    setMainPayOut(data.mainBetResult);
    setPlayerTotalA(data.playerTotalA);
    setPlayerTotal2A(data.playerTotal2A);
    setPlayerTotalB(data.playerTotalB);
    setPlayerTotal2B(data.playerTotal2B);
    setHitBtn(data.hitBtn);
    }

} 

useEffect(() => { 
async function initial3cards() {
    let response = await fetch('https://localhost:7100/API/Pontoon/SplittingCard') 

    if (!response.ok) {
       navigate("/errorPage");
      }   
    else{
       let data = await response.json();
       setPlayerCardsA(data.playerCardsBDisplayCodes);
       setPlayerCardsB(data.playerCardsADisplayCodes);
       setDealerCards(data.dealerCardsDisplayCodes);
       setHideB(false);
       setHideA(true);
      // setMainPayOut(data.mainBetResult);
       setWallet(data.wallet);
       setHitBtn(data.hitBtn);
      // setPlayerTotalA(data.playerTotalA);
       //setDealerTotal(data.dealerTotal);
       //setPlayerTotal2A(data.playerTotal2A);
       //setPlayerTotalB(data.playerTotalB);
       //setPlayerTotal2B(data.playerTotal2B);
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
                <img src={generateImage(dealerCards[0])} alt="dealer's card image" height={100} />
            </div>
            <div style={Mainstyle.split}>
                <div style={{margin: 10}}>
                <p>
                    Player's card total : {playerTotalA} {playerTotal2A}
                </p>
                {playerCardsA.map( (value,index) =>
                <img key={index} src={generateImage(value)} alt="player's card images" height={100} />
                )}
                </div>   
                <div style={{margin: 10}}>
                <p>
                    Player's card total : {playerTotalB} {playerTotal2B}
                </p>
                {playerCardsB.map( (value,index) =>
                <img key={index} src={generateImage(value)} alt="player's card images" height={100} />
                )}
                </div> 
            </div> 
            <div>
                <button disabled={hitBtn} onClick={hit}>Hit</button>
                <button disabled={hitBtn}>Stand</button>
                <button disabled={hitBtn}>Double</button>
            </div>

            <button onClick={() => navigate('/bettingPage')}>Play Again</button>
            
            <div>
                <span>Main Payout:</span> <span>{mainPayOut}</span>
                <br/>
                <span>Wallet:</span> <span>{wallet}</span>
            </div>
        </div>
    )
}