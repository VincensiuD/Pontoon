import './App.css';
import { BeginGame } from './StartGame';
import react from 'react';
import { Router } from './components';
import {Mainstyle} from './components/styleLibrary'
function App() {

// let [playerCard2,setPlayerCard2] = useState([]);

// useEffect(() => {
//   fetch('https://localhost:44321/API/BlackJack/NewGame')
//   .then(response => response.json())
//   .then(data => setInitialCards(data)) 
// },[]);


  return (
       <div className="App">
          <p style={Mainstyle.mainTitle}>PONTOON</p>
          
          <Router/>        
       </div>
       
  );
}

export default App;


