import React from "react";


export function WagerButtonFunction(props) {

    function calculateWager() {
        if (props.type == "pair") {

        }

    }


    return (
        <button onClick={calculateWager}>{props.modifier}</button>
    );
}