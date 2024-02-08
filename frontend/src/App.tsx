import React from "react";
import logo from "./logo.svg";
import "./App.css";
import Card from "./Components/Card/Card";

function App() {
  return (
    <div className="App">
      <Card companyName={""} ticker={""} price={0} />
    </div>
  );
}

export default App;
