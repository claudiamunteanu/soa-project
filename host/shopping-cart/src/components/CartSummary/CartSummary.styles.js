import styled from "styled-components";

export const Wrapper = styled.div`
    background-color: white;
    margin: 20px 0 20px 20px;
    padding: 20px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    font-size: 20px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    border: 1px solid #d6e1ec;
    border-radius: 8px;

    .empty-message{
        padding: 20px 0;

        h3{
            margin-top: 20px;
        }
    }
`;