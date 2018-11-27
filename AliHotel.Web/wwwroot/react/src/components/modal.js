import React from 'react';

const Modal = ({ show, children }) => {
    const showHideClassName = show ? 'modal display-block' : 'modal display-none';

    return (
        <div className={showHideClassName}>
            <section className="modal-main card border-info mb-3">
                {children}
            </section>
        </div>
    );
};

export default Modal;