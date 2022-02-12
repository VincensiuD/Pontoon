import React from "react";
import {Route,Routes} from 'react-router-dom';
import { FrontPageFunction } from "./frontPage";
import {BettingPageFunction} from './bettingPage';
import { InitialGame } from "./initialGame";
import { ErrorPage } from "./errorPage";
import { SplitGame } from "./splitGame";
export function Router(){
    return(
        <Routes>
            <Route path="/" element={<FrontPageFunction/>}/> 
            <Route path="/bettingPage" element={<BettingPageFunction/>}/> 
            <Route path="/initialGame" element={<InitialGame/>}/>
            <Route path="/errorPage" element={<ErrorPage/>}/> 
            <Route path="/splitGame" element={<SplitGame/>}/>           
        </Routes>
    );
}

