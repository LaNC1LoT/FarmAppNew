import React, { useState } from 'react';
import IconButton from '@material-ui/core/IconButton';
import AccountCircle from '@material-ui/icons/AccountCircle';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import { Typography, makeStyles, Theme, createStyles, Button, ListItem } from '@material-ui/core';
import { logout } from '../../store/auth/authActions';
import { useDispatch, connect } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { IAppState } from '../../core/mainReducer';
import { User } from '../../store/auth/authState';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        card: {
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            margin: '10px',
        },
        img: {
            justifyContent: 'center',
        }
    }))


interface ProfileProps {
    user: User | null
}

const Profile = ({ user }: ProfileProps) => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };
    const dispath = useDispatch();
    const history = useHistory();
    const logoutHandler = () => {
        localStorage.clear();
        dispath(logout())
        history.push('/farm-app/auth/')
    }

    const classes = useStyles();

    return (
        <div>
            <IconButton
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                onClick={handleMenu}
                color="inherit"
            >
                <Typography>Карточка пользователя</Typography>
            </IconButton>
            <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                open={open}
                onClose={handleClose}

            >
                <div className={classes.card}>
                    <MenuItem className={classes.img}
                        onClick={handleClose}><AccountCircle
                            fontSize={'large'}
                        /></MenuItem>
                    <ListItem  >{user?.firstName} {user?.lastName}</ListItem  >
                    <ListItem   >{user?.role?.roleName}</ListItem  >

                    <Button onClick={logoutHandler}

                        variant="contained"
                        color="primary"
                        fullWidth
                    >Выйти</Button>
                </div>
            </Menu>
        </div>
    )
};

const mapStateToProps = (state: IAppState) => {
    return {
        user: state.auth.user,
    }
}



export default connect(mapStateToProps)(Profile)
