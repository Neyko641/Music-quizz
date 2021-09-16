import React from "react";
import '../../Style/bootstrap.min.css';


/*const handleSignIn = event => {

}*/

const SignIn = ({ onPageChange }) => {
    return (
       <div className="row">
            <div className="col">
                <button className="btn btn-secondary" onClick={onPageChange}>SignIn</button>
            </div>
            
            <div className="col">
                <form>
                    <div className="mb-3">
                        <label for="InputEmail" className="form-label">E-mail address</label>
                        <input type="email" className="form-control" id="InputEmail" aria-describedby="emailHelp"/>
                        <div id="emailHelp" className="form-text">We'll never share your email with anyone else.</div>
                    </div>
                    <div className="mb-3">
                        <label for="InputPassword" className="form-label">Password</label>
                        <input type="password" className="form-control" id="InputPassword"/>
                    </div>
                    <div className="mb-3 form-check">
                        <input type="checkbox" className="form-check-input" id="Check"/>
                        <label className="form-check-label" for="Check">Check me out</label>
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>
    );
}
export default SignIn;