import styled from "styled-components";

export const Wrapper = styled.div`
    background-color: white;
    margin: 20px 20px 20px 0;
    padding: 30px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    border: 1px solid #d6e1ec;
    border-radius: 8px;
    
    p{
        color: black;
        font-size: 20px;
    }

    .price-row{
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
    }

    .total-price-row{
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        font-weight: 600;
        margin-top: 16px;

        p{
            margin: 0;
        }
    }

    .divider{
        background-color: #cccccc;
        height: 1px;
    }

    button{
        margin-top: 30px;
    }
`;