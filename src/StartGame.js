import react, { useState } from "react"
import Club2 from "./image/2Club.png";
import Diamond2 from "./image/2Diamond.png";
import Heart2 from "./image/2Spade.png";
import Spade2 from "./image/2Heart.png";
import Club3 from "./image/3Club.png";
import Diamond3 from "./image/3Diamond.png";
import Heart3 from "./image/3Spade.png";
import Spade3 from "./image/3Heart.png";
import Club4 from "./image/4Club.png";
import Diamond4 from "./image/4Diamond.png";
import Heart4 from "./image/4Spade.png";
import Spade4 from "./image/4Heart.png";
import Club5 from "./image/5Club.png";
import Diamond5 from "./image/5Diamond.png";
import Heart5 from "./image/5Spade.png";
import Spade5 from "./image/5Heart.png";
import Club6 from "./image/6Club.png";
import Diamond6 from "./image/6Diamond.png";
import Heart6 from "./image/6Spade.png";
import Spade6 from "./image/6Heart.png";
import Club7 from "./image/7Club.png";
import Diamond7 from "./image/7Diamond.png";
import Heart7 from "./image/7Spade.png";
import Spade7 from "./image/7Heart.png";
import Club8 from "./image/8Club.png";
import Diamond8 from "./image/8Diamond.png";
import Heart8 from "./image/8Spade.png";
import Spade8 from "./image/8Heart.png";
import Club9 from "./image/9Club.png";
import Diamond9 from "./image/9Diamond.png";
import Heart9 from "./image/9Spade.png";
import Spade9 from "./image/9Heart.png";
import Club10 from "./image/10Club.png";
import Diamond10 from "./image/10Diamond.png";
import Heart10 from "./image/10Spade.png";
import Spade10 from "./image/10Heart.png";
import ClubJ from "./image/JClub.png";
import DiamondJ from "./image/JDiamond.png";
import HeartJ from "./image/JSpade.png";
import SpadeJ from "./image/JHeart.png";
import ClubQ from "./image/QClub.png";
import DiamondQ from "./image/QDiamond.png";
import HeartQ from "./image/QSpade.png";
import SpadeQ from "./image/QHeart.png";
import ClubK from "./image/KClub.png";
import DiamondK from "./image/KDiamond.png";
import HeartK from "./image/KSpade.png";
import SpadeK from "./image/KHeart.png";
import ClubA from "./image/AClub.png";
import DiamondA from "./image/ADiamond.png";
import HeartA from "./image/ASpade.png";
import SpadeA from "./image/AHeart.png";
import { generateImage } from './components';



export function BeginGame() {
  let [cards, setCards] = useState([]);

  async function withdrawInitialCards() {
    const response = await fetch('https://localhost:44321/API/BlackJack/AddCard?n=1&for=dealer')
    setCards(response.json())

    
  }

  function hit() {

  }

  function checkConsole() {
    console.log(cards)
  }

  const cardImages = {
    Club2,
    Diamond2,
    Heart2,
    Spade2,
    Club3,
    Diamond3,
    Heart3,
    Spade3,
    Club4,
    Diamond4,
    Heart4,
    Spade4,
    Club5,
    Diamond5,
    Heart5,
    Spade5,
    Club6,
    Diamond6,
    Heart6,
    Spade6,
    Club7,
    Diamond7,
    Heart7,
    Spade7,
    Club8,
    Diamond8,
    Heart8,
    Spade8,
    Club9,
    Diamond9,
    Heart9,
    Spade9,
    Club10,
    Diamond10,
    Heart10,
    Spade10,
    ClubJ,
    DiamondJ,
    HeartJ,
    SpadeJ,
    ClubQ,
    DiamondQ,
    HeartQ,
    SpadeQ,
    ClubK,
    DiamondK,
    HeartK,
    SpadeK,
    ClubA,
    DiamondA,
    HeartA,
    SpadeA,
  }


  return (
    <div>
      <div>
        <button onClick={withdrawInitialCards}>Begin Game</button>
      </div>

      <div>
        <p>
          Dealer's card
        </p>
        <img src={cardImages[cards[0]]} alt="cardImage" height={100} />
      </div>

      <div>
        <p>
          Player's card
        </p>
        <img src={cardImages[cards[0]]} alt="Arief's way" height={100} />
        <img src={cardImages[cards[0]]} alt="Arief's way" height={100} />
      </div>

      <div>
        <button onClick={hit}> Hit </button>
        <button> Stand </button>
        <button onClick={checkConsole}>Check Console</button>
      </div>
    </div>);
}