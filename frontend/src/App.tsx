import React, { useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";
import Card from "./Components/Card/Card";

function App() {
  useEffect(() => {
    fetch("http://localhost:5032/api/stock")
      .then((data) => data.json())
      .then(console.log);
  }, []);
  return (
    <div className="App">
      <Card companyName={""} ticker={""} price={0} />
    </div>
  );
}

export default App;
