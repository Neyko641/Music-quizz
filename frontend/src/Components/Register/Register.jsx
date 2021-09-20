import React from "react";
import '../../Style/bootstrap.min.css'

const Register = ({ onPageChange }) => {
    return (<div>
        <button className = "btn btn-secondary" onClick={onPageChange}>Register</button>
    </div>);
}

export default Register