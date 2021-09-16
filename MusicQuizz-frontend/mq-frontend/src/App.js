import React, { useState } from "react";
import Play from "./Components/Play/Play";
import Options from "./Components/Options/Options";
import './App.css';
import './Style/bootstrap.min.css';
import SignIn from "./Components/SignIn/SignIn";
import Register from "./Components/Register/Register";

function App() {
  test();
  const [currentPage, newPage] = useState('Home');
  return (
    <div className="App">
      <p>{currentPage}</p>
      <div className="container">
        <Play onPageChange={() => newPage('Play')}/>
        <Options onPageChange={() => newPage('Options')}/>
        <SignIn onPageChange={() => newPage('SignIn')}/>
        <Register onPageChange={() => newPage('Register')}/>
      </div>
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
