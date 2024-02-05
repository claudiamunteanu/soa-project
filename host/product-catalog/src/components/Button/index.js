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

export default Button