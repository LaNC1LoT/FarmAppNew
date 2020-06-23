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
import {logout, restoreAuth, RESTORE_AUTH} from '../../store/auth/authActions';
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


const SignIn = ({error}:{
  error:null|Error
}) => {
  const [login_text, setLogin] = useState('');
  const [pass_text, setPass] = useState('');
  const [message, setMessage] = useState('');
  const [open, setOpen] = useState(false);
  const classes = useStyles();
  const history = useHistory();
  const dispatch = useDispatch();
  const selector = useSelector(authSelector);

  const {enqueueSnackbar} = useSnackbar();
  const submitButton = useRef(null);


  const handleOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  }

  const onSuccess = (result?: null | object) => {
    console.log("result", result)
    //сохраняем полученые данные что бы мы могли авторизовать пользователя при повторном подключении
    localStorage.setItem('auth', JSON.stringify(result))
    //сохраняет время получения токена для вычисления его жизни
    localStorage.setItem('getTokenTime', new Date().getTime().toString())
    history.push('/farm-app/main/')
  }

  const  downHandler=({ key }:{key:any})=> {
    if (key === "Enter") {
      // @ts-ignore
      submitButton?.current?.click()
    }
  }

  useEffect(() => {
    //проверка если мы уже были авторизованны ранее
    const rememberMe = localStorage.getItem('auth')
    const tokenLife = localStorage.getItem('getTokenTime')
    if (rememberMe !== null && tokenLife !== null) {
      const response = JSON.parse(rememberMe)
      //проверка жив ли еще токен
      //если прошло меньше 5 дней
      if (new Date().getTime() <= parseInt(tokenLife) + 5 * 24 * 60 * 60 * 1000) {
        dispatch(restoreAuth(response))
        history.push('/farm-app/main/')
      } else {
        //если прошло  5 дней чистим хранилище и выходим
        localStorage.clear();
        dispatch(logout())
      }
    }


      window.addEventListener('keydown', downHandler);
      // Remove event listeners on cleanup
      return () => {
        window.removeEventListener('keydown', downHandler);
      };

  }, [])

  useEffect(() => {
    if(error?.message){
      setMessage(error.message)
      handleOpen();
    }
  },[error])

  const handleClick = () => {
    handleClose()
    if (login_text?.length !== 0 && validator.isEmail(login_text) && pass_text?.length !== 0) {
      dispatch(callApiLogin({login: login_text, password: pass_text}, onSuccess))
    } else {
      setMessage('Логин или пароль введен неверно.')
      handleOpen();
    }
  }

  const register=()=>{
      history.push('/farm-app/reg/')
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
              Авторизация
            </Typography>
            {/*<form className={classes.form} noValidate>*/}
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              id="text"
              type="text"
              label="Логин"
              name="text"
              autoFocus
              onChange={x => setLogin(x.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Пароль"
              type="password"
              id="password"
              autoComplete="current-password"
              onChange={x => setPass(x.target.value)}
            />
            <Button color="primary" onClick={register}>регистрация</Button>

            {
              selector.loadState ?
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
                  Авторизироваться
                </Button>
            }

            {/*</form>*/}
          </div>
        </Container>
      </div>
    </>
  )
}

export default connect((state: IAppState) => {
  const {auth} = state;
  return {
    error: auth.error
  }
})(SignIn)
