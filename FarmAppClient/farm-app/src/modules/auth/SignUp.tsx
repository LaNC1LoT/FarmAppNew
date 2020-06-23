import React, {useEffect, useState, useRef} from 'react';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';
import {makeStyles} from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
// import boy from "../../resources/boy.svg"
import boy from "./boy.svg"
import {connect, useDispatch, useSelector} from 'react-redux';
import {authSelector} from '../../store/auth/authSelector';
import {callApiLogin} from '../../store/auth/authStateActionsAsync';
import {useHistory} from 'react-router-dom';
import {CircularProgress} from '@material-ui/core';
import {PositionedSnackbar} from '../../components/snackbar/SnackbarResult';
import {useSnackbar, VariantType} from 'notistack';
import {logout, restoreAuth, registration} from '../../store/auth/authActions';
import {IAppState} from "../../core/mainReducer";


var validator = require('validator');

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
    height: 45
  },
  authText: {
    marginTop: 20,
    fontWeight: "bold"
  },
  img: {
    width: 150,
    height: 150,
  },
  centerScreen: {

    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    textAlign: 'center',

    margin: 'auto'
  }
}));


const SignUp = ({error, preloader}: {
  error: any | null,
  preloader: boolean
}) => {
  const [login, setLogin] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [password, setPass] = useState('');
  const [message, setMessage] = useState('');
  const [open, setOpen] = useState(false);
  const [firstStep, setFirstStep] = useState(false);
  const classes = useStyles();
  const history = useHistory();
  const dispatch = useDispatch();
  const selector = useSelector(authSelector);
  const submitButton = useRef(null);
  const {enqueueSnackbar} = useSnackbar();


  // const handleClickVariant = (variant: VariantType) => () => {
  //     enqueueSnackbar('Логин или пароль введен неверно.', { variant });
  // };
  const handleClickVariant = (message: string, variant: VariantType) => {
    enqueueSnackbar(message, {variant});
  };

  const handleOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  }

  const downHandler = ({key}: { key: any }) => {
    if (key === "Enter") {
      // @ts-ignore
      submitButton?.current?.click()
    }
  }
  useEffect(() => {


    window.addEventListener('keydown', downHandler);
    // Remove event listeners on cleanup
    return () => {
      window.removeEventListener('keydown', downHandler);
    };
  }, [])

  useEffect(() => {
    if (error?.message) {
      setMessage(error.message)
      handleOpen();
    }
  }, [error])

  useEffect(() => {
    if (firstStep && preloader === false && error === null) {
      setFirstName('');
      setLastName('');
      setPass('');
      setMessage('');
      setFirstStep(false)
      setMessage('Регистрация прошла успешно!')
      handleOpen();

      setTimeout(() => {
        history.push('/farm-app/auth/')
      }, 3000)


    }
  }, [preloader, error])


  const handleClick = () => {

    handleClose()
    if (login?.length !== 0 && validator.isEmail(login) && password?.length !== 0 && lastName?.length != 0 && firstName?.length != 0) {
      dispatch(registration({login, password, firstName, lastName}))
      setFirstStep(true)
    } else {
      setMessage('Данные введены неверно.')
      handleOpen();
    }
  }

  return (
    <>
      <PositionedSnackbar
        text={message}
        handleClose={handleClose}
        open={open}
      />
      <div>
        <Container component="main" maxWidth="xs">
          <CssBaseline/>
          <div className={classes.paper}>
            <img className={classes.img} src={boy} alt="boy"/>
            <Typography className={classes.authText} component="h1" variant="h4">
              Регистрация
            </Typography>
            <TextField
              variant="outlined"
              margin="normal"
              value={firstName}
              required
              fullWidth
              id="text"
              type="text"
              label="Имя"
              name="text"
              autoFocus
              onChange={x => setFirstName(x.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              value={lastName}
              fullWidth
              id="text"
              type="text"
              label="Фамилия"
              name="text"
              autoFocus
              onChange={x => setLastName(x.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              value={login}
              id="text"
              type="text"
              label="Email"
              name="text"
              autoFocus
              onChange={x => setLogin(x.target.value)}
            />

            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              value={password}
              name="password"
              label="Пароль"
              type="password"
              id="password"
              autoComplete="current-password"
              onChange={x => setPass(x.target.value)}
            />
            {
              selector.isFetchReg ?
                <CircularProgress className={classes.centerScreen}/>
                :
                <Button
                  onClick={handleClick}
                  className={classes.submit}
                  variant="contained"
                  color="primary"
                  fullWidth
                  ref={submitButton}

                >
                  Зарегистрироваться
                </Button>
            }
            <Button
              onClick={() => history.push('/farm-app/auth/')}
              className={classes.submit}
              variant="contained"
              color="primary"
              fullWidth
            >
              Назад
            </Button>
          </div>
        </Container>
      </div>
    </>
  )
}

export default connect((state: IAppState) => {
  const {auth} = state;
  return {
    preloader: auth.isFetchReg,
    error: auth.errorReg
  }
})(SignUp)
