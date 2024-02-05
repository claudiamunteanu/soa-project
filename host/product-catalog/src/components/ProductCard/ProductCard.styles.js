import styled from "styled-components";

export const Wrapper = styled.div`
    background-color: white;
    margin: 20px;
    padding: 20px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    font-size: 20px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    border: 1px solid #d6e1ec;
    border-radius: 2%;

    img{
        height: 100%;
        width: 100%;
        margin-bottom: 10px;
    }

    .col{
        margin: auto;
    }

  .trigger-button {
    width: 100%;
    margin-left: 0;
    margin-top: 5px;
  }

  .add-to-cart-button{
        margin-top: 10px;
        width: 50%;
        float: right;
    }

  .modify-quantity{
    display: flex;
    flex-direction: row;
    justify-content: end;
    align-items: center;
    margin-top: 10px;
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

  .product-name{
    font-size: 22px;
    font-weight: 600;
  }

  .product-price{
    font-weight: 600;
  }

  .product-stock{
    font-size: 18px;
  }
`;