import react, { useState, useEffect } from "react";
import { Mainstyle } from "./styleLibrary";
import { WagerSection } from "./wagerSection";
import { useNavigate } from 'react-router-dom';
export function BettingPageFunction() {

    let [pairsWager, setPairsWager] = useState(0);
    let [mainWager, setMainWager] = useState(0);
    let [money, setMoney] = useState(0);
    let navigate = useNavigate();


    useEffect(() => {
        console.log("fetching money")
        fetch('https://localhost:44321/API/BlackJack/PlaceYourBet')
            .then(response => response.json())
            .then(data => setMoney(data))
    }, []);

    function addWager(typeOfBet, increment) {
        
        if (0 < increment) {
            // Increase
            if (typeOfBet == "main" && money - increment >= 0) {
                setMoney(money - (increment * 1));
                setMainWager(mainWager + (increment * 1));
            }
            else if (typeOfBet == "pair" && money - increment >= 0) {
                setMoney(money - (increment * 1))
                setPairsWager(pairsWager + (increment * 1))
            }
        } else {
            // Decrease
            if (typeOfBet == "main" && mainWager + increment >= 0) {
                setMoney(money - (increment * 1));
                setMainWager(mainWager + (increment * 1));
            }
            else if (typeOfBet == "pair" && pairsWager + increment >= 0) {
                setMoney(money - (increment * 1))
                setPairsWager(pairsWager + (increment * 1))
            }
        }
    }

    function dealCard() {

        const bettingDetails = { PairsWager: pairsWager, MainWager: mainWager }
        //change the code below later
        if (mainWager == 0) {
            alert("You need to bet on main wager to start the game!")
        }
        else if (mainWager < pairsWager) {
            alert("Main bet has to be larger than pairs bet!");
        }
        else {
            async function submitBet(){
            let response = await fetch('https://localhost:44321/API/BlackJack/Wagers', {
                method: 'POST', // or 'PUT'
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(bettingDetails),
            })

            if (response.ok) {
                navigate("/initialGame"); //if failed, return to fail page
                 }
            else{
                navigate("/errorPage"); //if succeesful, return to succeed page
                }
            }
            submitBet().catch((error) => {
            console.error('Error:', error);
            });

            
        }
                     
    
}

    return (
        <div>
            <h3>Place your bet</h3>
            <WagerSection title="Pair bet" type="pair" value={pairsWager} onModify={addWager} increments={[-100, -10, 10, 100]} />
            <WagerSection title="Main bet" type="main" value={mainWager} onModify={addWager} increments={[-100, -10, 10, 100]} />
            <br />
            <br />
            <div>
                <p>Wallet: </p>
                <label>{money}</label>
            </div>
            <div>
                <button onClick={dealCard}>DealCard</button>
            </div>
        </div>
    );
}
/**
 * <div className="pairsBet">
                <p>Pairs bet</p>
                <br />
                <button onClick={() => minusWager("pair", -100)}> -100</button>
                <button onClick={() => minusWager("pair", -10)}> -10</button>
                <WagerButtonFunction onClick={(type, modifier) => minusWager(type, modifier)} type="pair" modifier="-100" />
                <label style={Mainstyle.wagerLabel}>
                    {pairsWager}
                </label>
                <button onClick={() => addWager("pair", 10)}>+10</button>
                <button onClick={() => addWager("pair", 100)}>+100</button>
            </div>
            <br />
            <br />
            <div className="mainBet">
                <p>Main bet</p>
                <br />
                <button onClick={() => minusWager("main", -100)}> -100</button>
                <button onClick={() => minusWager("main", -10)}> -10</button>
                <label style={Mainstyle.wagerLabel}>
                    {mainWager}
                </label>
                <button onClick={() => addWager("main", 10)}>+10</button>
                <button onClick={() => addWager("main", 100)}>+100</button>
            </div>
 */