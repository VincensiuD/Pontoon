import React from "react";
import { useNavigate,Link } from "react-router-dom";

export function ErrorPage(){

const navigate = useNavigate();

    return(
        <div>
            <p>uh oh error here</p>
            <button onClick={() => navigate('/bettingPage')}>Play Again</button>
        </div>
    );
}