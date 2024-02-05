import styled from "styled-components";

export const Wrapper = styled.div`
  button{
    background: var(--darkGrey);
    width: 25%;
    min-width: 100px;
    height:35px;
    border-radius: 30px;
    color: var(--white);
    border: 0;
    font-size: var(--fontSmall);
    margin: 20px auto;
    transition: all 0.3s;
    outline: none;
    cursor: pointer;

    :hover{
      opacity: 0.8;
    }
    
    :disabled{
      background: var(--disabledGreyBackground);
      color: var(--disabledGreyFont);
      cursor: default;

      :hover{
        opacity: 1;
      }
    }
  }
`;