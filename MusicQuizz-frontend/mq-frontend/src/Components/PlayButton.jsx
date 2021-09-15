import React from "react";
import '../Style/bootstrap.min.css';

const PlayButton = ({ onPageChange }) => {
    return (
    <div>
        <button className="btn btn-secondary" onClick={onPageChange}>Hey!</button>
    </div>
    );
}
export default PlayButton;