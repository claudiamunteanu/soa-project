import React, { useEffect } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';

const NotificationComponent = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
          .withUrl("/api/notificationHub")
          .build();

      connection.start().then(function () {
          console.log("Connected to the hub");
      }).catch(function (err) {
          console.error(err.toString());
      });

        connection.on("ReceiveNotification", (type, message, additionalInfo) => {
            const notificationMessage = additionalInfo ? `${message} - ${additionalInfo}` : message;
            switch(type){
                case 'orderAdded':
                    navigate("/order-confirmation",{
                        state: {
                          fromApp: true,
                          orderNumber: additionalInfo,
                        }
                      } );
                    localStorage.removeItem("cart");
                    dispatch({type: 'RESET_CART_SIZE'});
                    break;
                case 'orderUpdate':
                    toast.info(notificationMessage);
                    break;
                default:
                    toast.info(notificationMessage);
                    break;
            }
        });

        return () => {
            connection.stop();
        };
    }, []);

    return (
        <ToastContainer
                    position="top-center"
                    autoClose={5000}
                    hideProgressBar={false}
                    newestOnTop={false}
                    closeOnClick
                    rtl={false}
                    pauseOnFocusLoss
                    draggable
                    pauseOnHover
                />
    );
};

export default NotificationComponent;