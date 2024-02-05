import React, { useState } from 'react';
import Popup from 'reactjs-popup';
import './FormModal.styles.css'
import Button from '../../Button';

const contentStyle = {};
const overlayStyle = { background: 'rgba(0,0,0,0.5)' };
const arrowStyle = { color: '#000' };

const FormModal = ({ buttonName, triggerText, modalTitle, callbackText, callback, product, updateProduct }) => {
    const [open, setOpen] = useState(false);
    const [productName, setProductName] = useState(product.name)
    const [productPrice, setProductPrice] = useState(product.price)
    const [productStock, setProductStock] = useState(product.stock)
    const [productPhoto, setProductPhoto] = useState(product.photo)

    const handleInput = e => {
        const name = e.currentTarget.name;
        const value = e.currentTarget.value;

        switch (name) {
            case 'productName':
                setProductName(value);
                break;
            case 'productPrice':
                setProductPrice(value);
                break;
            case 'productStock':
                setProductStock(value);
                break;
            case 'productPhoto':
                setProductPhoto(value);
                break;
            default:
                break;
        }
    };

    const restartModal = () => {
        setProductName(product.name);
        setProductPrice(product.price);
        setProductStock(product.stock);
        setProductPhoto(product.photo);
    }

    return (
        <Popup
            trigger={
                <div className="trigger-button">
                    <Button name={buttonName} className="trigger-button" callback={() => setOpen(true)} text={triggerText} />
                </div>
            }
            open={open}
            modal
            nested
            contentStyle={contentStyle}
            overlayStyle={overlayStyle}
            arrowStyle={arrowStyle}
            closeOnDocumentClick={true}
            onClose={() => { restartModal(); setOpen(false) }}
        >
            {close => (
                <div className="modal-content">
                    <div className="header"> {modalTitle} </div>
                    <div className="content">
                        <div className="promptText">
                            Name:
                        </div>
                        <input
                            value={productName}
                            name='productName'
                            onChange={handleInput}
                            maxLength='255'
                        />
                        <div className='number-inputs-row'>
                            <div>
                                <div className="promptText">
                                    Price:
                                </div>
                                <input
                                    type='number'
                                    value={productPrice}
                                    name='productPrice'
                                    onChange={handleInput}
                                    maxLength='255'
                                />
                            </div>
                            <div>
                                <div className="promptText">
                                    Stock:
                                </div>
                                <input
                                    type='number'
                                    value={productStock}
                                    name='productStock'
                                    onChange={handleInput}
                                    maxLength='255'
                                />
                            </div>
                        </div>
                        <div className="promptText">
                            Photo:
                        </div>
                        <input
                            value={productPhoto}
                            name='productPhoto'
                            onChange={handleInput}
                            maxLength='255'
                        />
                    </div>
                    <div className="actions">
                        <button
                            name="submitButton"
                            className="modal-button"
                            onClick={async () => {
                                let copiedProduct = JSON.parse(JSON.stringify(product));
                                copiedProduct.name = productName
                                copiedProduct.price = productPrice
                                copiedProduct.stock = productStock
                                copiedProduct.photo = productPhoto

                                let result = await callback(copiedProduct)
                                if (!result.error) {
                                    if(updateProduct){
                                        console.log(result)
                                        product = result.data
                                    }
                                    close()
                                }
                            }}
                        >
                            {callbackText}
                        </button>
                        <button className="modal-button" onClick={close}>Close</button>
                    </div>
                </div>
            )}
        </Popup>
    )
}

export default FormModal