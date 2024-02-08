import React from "react";
import "./Card.css";
type Props = {
  companyName: string;
  ticker: string;
  price: number;
};

const Card: React.FC<Props> = ({
  companyName,
  ticker,
  price,
}: Props): JSX.Element => {
  return (
    <div className="card">
      <img src="" alt="Apple" />
      <div className="details"></div>
      <h2>{companyName}</h2>
      <p>${price}</p>
      <p className="info">{ticker}</p>
    </div>
  );
};

export default Card;
