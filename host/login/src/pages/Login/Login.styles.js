import styled from "styled-components";

export const Wrapper = styled.div`
  height: 100%;
  padding: 5% 30%;
  animation: animateMovieInfo 1s;

  @keyframes animateMovieInfo {
    from {
      opacity: 0
    }
    to {
      opacity: 1;
    }
  }
`;

export const Content = styled.div`
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  color: var(--darkGrey);
  max-width: var(--maxWidth);
  margin: 0 auto;
  background: rgba(0, 0, 0, 0.2);
  border-radius: 20px;
  width: 100%;
  padding: 20% 30%;


  @media screen and (max-width: 768px) {
    display: block;
    max-height: none;
  }

  input {
    width: 100%;
    height: 30px;
    border: 1px solid var(--darkGrey);
    border-radius: 20px;
    margin: 10px 0;
    padding: 10px;
  }
  
  a{
    color: var(--lightGrey);
  }
`;