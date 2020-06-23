import React, { useState, FC } from "react";
import Snackbar from "@material-ui/core/Snackbar";

export interface IProps {
    text: string;
    handleClose: () => void;
    open: boolean;
}

export const PositionedSnackbar: FC<IProps> = ({ text, handleClose, open,children }) => {
    return (
        <Snackbar
            anchorOrigin={{ vertical: 'top', horizontal: 'center', }}
            open={open}
            autoHideDuration={3000}
            onClose={handleClose}
            message={text}
        />
    );
}
