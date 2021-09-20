import React from "react";
import '../../Style/bootstrap.min.css';

const Play = ({ onPageChange }) => {
    return (
    <div>
        <button className="btn btn-secondary" onClick={onPageChange}>Play</button>
    </div>
    );
}
export default Play;