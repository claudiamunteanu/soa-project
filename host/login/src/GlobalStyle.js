import {createGlobalStyle} from 'styled-components';

export const GlobalStyle = createGlobalStyle `
    :root{
      --maxWidth: 1280px;
      --white: #fff;
      --black: #000;
      --lightGrey: #f8f4f4;
      --grey: #bcbcbc;
      --medGrey: #353535;
      --darkGrey: #1c1c1c;
      --fontSuperBig: 2.5rem;
      --fontBig: 1.5rem;
      --fontMed: 1.2rem;
      --fontSmall: 1rem;
      --prussianBlue: #13293D;
      --sapphineBlue: #006494;
      --carolinaBlue: #1B98E0;
      --maizeCrayola: #F3CA40;
      --indigoDye: #1D3E5E;
      --skyBlue: #87CEEB;
      --disabledGreyBackground: #aaaaaa;
      --disabledGreyFont: #777777;
      --lapisLazuli: #336699;
    }
    
    #root{
      height: auto;
      min-height: 100vh;
      display: flex;
      flex-direction: column;
    }
    
    *{
      box-sizing: border-box; //sets how the total width and height of an element is calculated.
      font-family: 'Candara',sans-serif !important;
    }
    
    html,body{
      height: auto;
      min-height: 100%;
    }

    body {
      margin: 0;
      padding: 0;
      width: 100%;
      background: linear-gradient(
              to bottom,
              #FFFAF1 41%,
              #F7ECCB 100%
      );
      
      h1{
        font-size: 2rem;
        font-weight: 600;
        color: var(--white);
      }

      h3{
        font-size: 1.1rem;
        font-weight: 600;
      }

      p{
        font-size: 1rem;
        color: var(--white);
      }
    }
`