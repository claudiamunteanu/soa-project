import styled from "styled-components";

export const Wrapper = styled.div`
font-size: 20px;
.container{
    margin: 0;
}

.navbar-brand{
    font-size: 25px;
}

#responsive-navbar-nav{
    a{
        margin: auto 10px;
        color: black;
        position: relative;
        height: 44px;

    }

    a:after{
        content:attr(value);
        font-size:12px;
        color: #fff;
        background: red;
        border-radius:50%;
        position:relative;
        right: -12px;
        top:-32px;
        opacity:0.9;
        display: block;
        text-align: center;
    }
}

.button{
    margin-left: 20px;
}
`;