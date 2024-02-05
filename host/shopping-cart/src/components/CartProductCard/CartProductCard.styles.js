import styled from "styled-components";

export const Wrapper = styled.div`
    margin: 20px 0;

    img{
        height: 100px;
        width: 100px;
        border: 1px solid black;
        margin-right: 10px;
    }

    p{
        color: black;
        margin: 0;
    }

    .total-price{
        font-size: 20px;
        font-weight: 600;
    }

    .col{
        display: flex;
        flex-direction: row;
        align-items: center;
    }

    .price-col{
        display: flex;
        flex-direction: column;
        justify-content: center;
    }

    .modify-quantity-button{
    border: 1px solid #d6e1ec;
    width: 30px;
    height: 30px;
    padding: 0 0 3px 0;
    line-height: 0;
  }

  .quantity-label{
    margin-left: 10px;
    margin-right: 10px;
    padding-bottom: 5px;
    width: 20px;
    text-align: center;
  }
`;