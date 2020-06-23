import React, { useState, FC } from "react"
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Collapse from '@material-ui/core/Collapse';
import ExpandLess from '@material-ui/icons/ExpandLess';
import ExpandMore from '@material-ui/icons/ExpandMore';
import { useStylesDrawer, LinkStyles } from "./ItemDrawerStyles";
import { Link, matchPath, useLocation } from "react-router-dom";
import { IDictionary } from "../../utils/interfaces";

interface IProps {
  title: string;
  listItems:  IDictionary<string>[]
}

export const ItemListDrawer: FC<IProps> = ({ title, listItems }) => {
  const classes = useStylesDrawer();
  const links = LinkStyles();
  const [open, setOpen] = useState(true);

  const location = useLocation();
  const handleClick = () => {
    setOpen(!open);
  };

  return (
    <List
      component="nav"
      aria-labelledby="nested-list-subheader"
      className={classes.root}
    >
      <ListItem button onClick={handleClick}>
        <ListItemText primary={title} />
        {open ? <ExpandLess /> : <ExpandMore />}
      </ListItem>

      <Collapse in={open} timeout="auto" unmountOnExit>
        {listItems.map((item, index) => (
          <List key={index} component="div" disablePadding>
            <Link className={links.link} to={`/farm-app/${Object.keys(item)[0]}`}>
              <ListItem
                button
                className={classes.nested}
                selected={!!matchPath(location.pathname, `/farm-app/${Object.keys(item)[0]}`)}>
                <ListItemText primary={Object.values(item)[0]} key={index} />
                {Object.values(item)[1] && <img style={{height: '30px'}} src={Object.values(item)[1]} alt={`${Object.values(item)[1]}`} />}
              </ListItem>
            </Link>
          </List>
        ))}
      </Collapse>

    </List>
  )
}
