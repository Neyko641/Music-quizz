import React from "react";
import '../Style/bootstrap.min.css'

const OptionButton = ({ onPageChange }) => {
    return (<div>
        <button className = "btn btn-secondary" onClick={onPageChange}>StartDash</button>
    </div>);
}

export default OptionButton