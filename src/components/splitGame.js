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

let [mainWager,setMainWager] = useState(0);
let [pairsWager,setPairsWager] = useState(0);

async function hitA(){

   let response = await fetch('https://localhost:7100/API/Pontoon/AddCardSplit2'); 

    if (!response.ok) {
        navigate("/errorPage");
       }   
     else{
    let data = await response.json();
    console.log(data);
    setPlayerCardsA(data.playerCardsADisplayCodes);
    setMainPayOut(data.mainBetResult);
    setPlayerTotalA(data.playerTotalA);
    setPlayerTotal2A(data.playerTotal2A);
    setHitBtn(data.hitBtn);
    setMainWager(data.mainWager);
    }

} 

async function hitB(){

    let response = await fetch('https://localhost:7100/API/Pontoon/AddCardSplit1'); 
 
     if (!response.ok) {
         navigate("/errorPage");
        }   
      else{
     let data = await response.json();
     console.log(data);
     setPlayerCardsB(data.playerCardsBDisplayCodes);
     setMainPayOut(data.mainBetResult);
     setPlayerTotalB(data.playerTotalB);
     setPlayerTotal2B(data.playerTotal2B);
     setHitBtn(data.hitBtn);
     setMainWager(data.mainWager);
     setHideA(data.hitBtnA);
     setHideB(data.hitBtn);
    }


     
 
 } 

 async function standB(){

    setHideA(false);
    setHideB(true);
 }


 async function standA(){
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
    setMainWager(data.mainWager);
    setPairsWager(0);
    }
 }

 async function disp(){
     console.log(playerTotalA)
     console.log(hideA);
 }

useEffect(() => { 
async function initial3cards() {
    let response = await fetch('https://localhost:7100/API/Pontoon/SplittingCard') 

    if (!response.ok) {
       navigate("/errorPage");
      }   
    else{
       let data = await response.json();
       setPlayerCardsA(data.playerCardsADisplayCodes);
       setPlayerCardsB(data.playerCardsBDisplayCodes);
       setDealerCards(data.dealerCardsDisplayCodes);
       setHideB(false);
       setHideA(true);
       setWallet(data.money);
       setHitBtn(data.hitBtn);
       setMainWager(data.mainWager);
       setPlayerTotalA(data.playerTotalA);
       setDealerTotal(data.dealerTotal);
       setPlayerTotal2A(data.playerTotal2A);
       setPlayerTotalB(data.playerTotalB);
       setPlayerTotal2B(data.playerTotal2B);
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
                <div className="hand_A" style={{margin: 10}}>
                <p>
                    Player's A card total : {playerTotalA} {playerTotal2A}
                </p>
                {playerCardsA.map( (value,index) =>
                <img key={index} src={generateImage(value)} alt="player's A card images" height={100} />
                )}
                </div>   
                <div className="hand_B" style={{margin: 10}}>
                <p>
                    Player's B card total : {playerTotalB} {playerTotal2B}
                </p>
                {playerCardsB.map( (value,index) =>
                <img key={index} src={generateImage(value)} alt="player's B card images" height={100} />
                )}
                </div> 
            </div> 
            <div>
                <span>Main Wager:</span> <span>{mainWager}</span>
                <br/>
            </div>
            <div>
               <div>
                     <button  hidden={hideA} disabled={hitBtn} onClick={hitA}>Hit A</button>
                     <button hidden={hideA} disabled={hitBtn} onClick={standA}>Stand A</button>
                     <button hidden={hideA} disabled={hitBtn} >Double</button>
               </div>
               <div>
                     <button hidden={hideB} disabled={hitBtn} onClick={hitB}>Hit </button>
                     <button hidden={hideB} disabled={hitBtn} onClick={standB}>Stand</button>
                     <button hidden={hideB} disabled={hitBtn} >Double</button>
                     <button onClick={disp}>Console </button>
               </div>
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