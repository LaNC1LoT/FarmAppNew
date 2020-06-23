import React, {useState} from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import {Hidden} from '@material-ui/core';
import classnames from "classnames";
import {ItemListDrawer} from '../../components/itemDrawer/ItemListDrawer';
import {ItemDrawer} from '../../components/itemDrawer/ItemDrawer';
import {IDictionary} from '../../utils/interfaces';
import Profile from '../../components/profile/Profile';
import Sales from '../sales/Sales';
import {Route, Redirect, Switch} from 'react-router-dom';
import {Role} from '../administration/role/Role';
import {Greeting} from '../greeting/Greeting';
import ChartComp from '../reports/chart/Chart';
import Pharmacy from '../directories/pharmacy/Pharmacy';
import Preparations from '../directories/preparation/Preparations';
import ATH from '../directories/ath/ATH';
import Produced from '../directories/produced.tsx/Produced';
import Region from '../directories/region/Region';
import {User} from '../administration/user/User';
import {Method} from '../administration/method/Method';
import {AccessRole} from '../administration/accessRoles/AccessRole';
import {connect} from "react-redux";
import {IAppState} from "../../core/mainReducer";
import RegionTypes from '../directories/regionTypes/RegionTypes';
import FormDosage from '../directories/ formDosage/FormDosage';

const logo = require('../../logo.png')
const folder = require('../../svg/folder.svg')
const money = require('../../svg/money.svg')
const charts = require('../../svg/charts.svg')
const code = require('../../svg/code.svg')
const medicains = require('../../svg/medicains.svg')
const meeting = require('../../svg/meeting.svg')
const profile = require('../../svg/profile.svg')
const pharmacy = require('../../svg/pharmacy.svg')
const proiz = require('../../svg/proiz.svg')
const regions = require('../../svg/regions.svg')
const network = require('../../svg/network.svg')
const settings = require('../../svg/settings.svg')


const drawerWidth = 240;

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
    },
    drawer: {
      [theme.breakpoints.up('sm')]: {
        width: drawerWidth,
        flexShrink: 0,
      },
    },
    appBar: {
      [theme.breakpoints.up('sm')]: {
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: drawerWidth,
      },
    },
    menuButton: {
      marginRight: theme.spacing(2),
      [theme.breakpoints.up('sm')]: {
        display: 'none',
      },
    },
    toolbar: theme.mixins.toolbar,
    toolbarLogo: {
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
    },
    drawerPaper: {
      width: drawerWidth,
    },
    content: {
      flexGrow: 1,
      padding: theme.spacing(3),
    },
    profile: {
      marginLeft: 'auto',
      display: 'flex',
    },
  }),
);

interface ResponsiveDrawerProps {
  container?: any;
  user?: any;
}

const ResponsiveDrawer = (props: ResponsiveDrawerProps) => {
  const {container, user} = props;
  const classes = useStyles();
  const [mobileOpen, setMobileOpen] = useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };


  const directories: IDictionary<string>[] = [
    {
      'pharmacy': 'Аптеки',
      'svg': pharmacy
    },
    {
      'preparations': 'Препараты',
      'svg': medicains
    },
    {
      'code': 'Код АТН',
      'svg': code
    },
    {
      'produced': 'Производители',
      'svg': proiz
    },
    {
      'region': 'Регионы',
      'svg': regions
    },
    {
      'regionTypes': 'Тип региона',
      // 'svg': regions
    },
    {
      'formDosage': 'Форма выпуска',
      // 'svg': regions
    },

  ]
  const administration: IDictionary<string>[] = [
    {
      'users': 'Пользователи',
      'svg': network
    },
    {
      'roles': 'Роли',
      'svg': meeting
    },
    {
      'methods': 'Методы',
      'svg': folder
    },
    {
      'access': 'Доступ по ролям',
      'svg': settings
    },]
  const reports: IDictionary<string>[] = [
    {
      'charts': 'Графики',
      'svg': charts
    }]
  const id = user?.role?.id
  const drawer = id === 1 ? (
    <div>
      <div className={classnames(classes.toolbar, classes.toolbarLogo)}>
        <img style={{height: '40px'}} src={logo} alt="logo"/>
      </div>
      <Divider/>
      <ItemDrawer
        svg={profile}
        title={"Главная"}
        link={"main"}
      />
      <ItemDrawer
        svg={money}
        title={"Продажи"}
        link={"sales"}
      />
      <ItemListDrawer title={"Отчеты"} listItems={reports}/>
      <ItemListDrawer title={"Справочники"} listItems={directories}/>
      <ItemListDrawer title={"Администрирование"} listItems={administration}/>
    </div>
  ) : (
    <div>
      <div className={classnames(classes.toolbar, classes.toolbarLogo)}>
        <img style={{height: '40px'}} src={logo} alt="logo"/>
      </div>
      <Divider/>
      <ItemDrawer
        svg={profile}
        title={"Главная"}
        link={"main"}
      />
      <ItemDrawer
        svg={money}
        title={"Продажи"}
        link={"sales"}
      />
      <ItemListDrawer title={"Отчеты"} listItems={reports}/>
      <ItemListDrawer title={"Справочники"} listItems={directories}/>
    </div>
  )

  const rememberMe = localStorage.getItem('auth')

  const NotFoundRedirect = () => {
    if (rememberMe !== null) {
      return <Redirect to='/farm-app/main/'/>
    } else {
      return <Redirect to='/auth/'/>
    }
  }


  return (
    <div className={classes.root}>
      <CssBaseline/>
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            className={classes.menuButton}
          >
            <MenuIcon/>
          </IconButton>
          <Typography variant="h6" noWrap>

          </Typography>
          <Typography className={classes.profile}>
            <Profile/>
          </Typography>
        </Toolbar>
      </AppBar>
      <nav className={classes.drawer} aria-label="mailbox folders">
        <Hidden smUp implementation="css">
          <Drawer
            container={container}
            variant="temporary"
            anchor={'left'}
            open={mobileOpen}
            onClose={handleDrawerToggle}
            classes={{
              paper: classes.drawerPaper,
            }}
            ModalProps={{
              keepMounted: true,
            }}
          >
            {drawer}
          </Drawer>
        </Hidden>
        <Hidden xsDown implementation="css">
          <Drawer
            classes={{
              paper: classes.drawerPaper,
            }}
            variant="permanent"
            open
          >
            {drawer}
          </Drawer>
        </Hidden>
      </nav>
      <main className={classes.content}>
        <div className={classes.toolbar}/>
        <Typography paragraph>
          <Route path={"/farm-app/main/"}>
            <Greeting/>
          </Route>
          <Route path={"/farm-app/sales/"}>
            <Sales/>
          </Route>
          <Route path={"/farm-app/charts/"}>
            <ChartComp/>
          </Route>
          <Route path={"/farm-app/pharmacy/"}>
            <Pharmacy/>
          </Route>
          <Route path={"/farm-app/preparations/"}>
            <Preparations/>
          </Route>
          <Route path={"/farm-app/code/"}>
            <ATH/>
          </Route>
          <Route path={"/farm-app/produced/"}>
            <Produced/>
          </Route>
          <Route path={"/farm-app/region/"}>
            <Region/>
          </Route>
          <Route path={"/farm-app/regionTypes/"}>
            <RegionTypes/>
          </Route>
          <Route path={"/farm-app/formDosage/"}>
            <FormDosage/>
          </Route>

          <Route path={"/farm-app/users/"}>
            <User/>
          </Route>
          <Route path={"/farm-app/roles/"}>
            <Role/>
          </Route>
          <Route path={"/farm-app/methods/"}>
            <Method/>
          </Route>
          <Route path={"/farm-app/access/"}>
            <AccessRole/>
          </Route>
          <Route component={NotFoundRedirect}/>
        </Typography>
      </main>
    </div>

  );
}


export default connect((state: IAppState) => {
  const {auth} = state;
  return {
    user: auth.user,
  }
})(ResponsiveDrawer)
