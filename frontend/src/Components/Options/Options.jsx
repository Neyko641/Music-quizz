import React from "react";
import '../../Style/bootstrap.min.css'

const Options = ({ onPageChange }) => {
    return (<div>
        <button className = "btn btn-secondary" onClick={onPageChange}>Options</button>
    </div>);
}

export default Options