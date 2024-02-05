import React, {useState} from 'react';
import Popup from 'reactjs-popup';
import './ConfirmModal.styles.css'
import Button from "../../Button";

const contentStyle = {};
const overlayStyle = {background: 'rgba(0,0,0,0.5)'};
const arrowStyle = {color: '#000'};

const ConfirmModal = ({triggerText, modalTitle, modalText, callback}) => {
    const [open, setOpen] = useState(false);
    return (
        <Popup
            trigger={
                <div className="trigger-button">
                    <Button className="trigger-button" callback={() => setOpen(true)} text={triggerText} />
                </div>
            }
            open={open}
            modal
            nested
            contentStyle={contentStyle}
            overlayStyle={overlayStyle}
            arrowStyle={arrowStyle}
            closeOnDocumentClick={true}
            onClose={() => setOpen(false)}
        >
            {close => (
                <div className="confirm-modal-content">
                    <div className="header"> {modalTitle} </div>
                    <div className="content">
                        {' '}
                        {modalText}
                    </div>
                    <div className="actions">
                        <button
                            className="confirm-modal-button"
                            onClick={ () => {
                                callback()
                                close()
                            }}
                        >
                            Yes
                        </button>
                        <button
                            className="confirm-modal-button"
                            onClick={() => {
                                console.log('modal closed ');
                                close();
                            }}
                        >
                            No
                        </button>
                    </div>
                </div>
            )}
        </Popup>
    )
}

export default ConfirmModal