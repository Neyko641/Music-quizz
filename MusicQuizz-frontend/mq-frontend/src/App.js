import React, { useState } from "react";
import PlayButton from "./Components/PlayButton";
import OptionButton from "./Components/OptionButton";
import './App.css';
import './Style/bootstrap.min.css';

function App() {
  test();
  const [currentPage, newPage] = useState('Home');
  return (
    <div className="App">
      <p>{currentPage}</p>
    <PlayButton onPageChange={() => newPage('Play')}/>
    <OptionButton onPageChange={() => newPage('Options')}/>
    </div>
  );
}

function test() {
  //https://localhost:44345/WeatherForecast
  //https://localhost:5001/WeatherForecast
  let bababui = true;
  if (bababui) {
    fetch('https://localhost:5001/WeatherForecast')
    .then(response => response.json())
    .then(data => console.log(data));
  }
}

export default App;
