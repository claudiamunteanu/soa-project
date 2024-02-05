import React from "react";
import PropTypes from 'prop-types'
import {Wrapper} from "./Button.styles";

const Button = ({text, callback, isEnabled=true, name, className}) => {
    return (
        <Wrapper>
            <button onClick={callback} disabled={!isEnabled} name={name} className={className}>{text}</button>
        </Wrapper>
    )
}

Button.propTypes = {
    text: PropTypes.string,
    callback: PropTypes.func,
    isEnabled: PropTypes.bool,
    name: PropTypes.string,
    className: PropTypes.string
}

export default Button