import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import SwipeableDrawer from "@material-ui/core/SwipeableDrawer";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";
import { Box, Divider } from "@material-ui/core";
import PeopleIcon from "@material-ui/icons/People";
import { Link } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
  list: {
    width: 250,
  },
}));

function Navbar() {
  const classes = useStyles();

  const [isOpened, setIsOpened] = React.useState(false);

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            edge="start"
            className={classes.menuButton}
            color="inherit"
            aria-label="menu"
            onClick={() => setIsOpened(!isOpened)}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" className={classes.title}>
            Панель управления
          </Typography>
          {/* <Link to="/login"> */}
            <Button variant="contained" color="secondary">
              Войти
            </Button>
          {/* </Link> */}
        </Toolbar>
      </AppBar>

      <div>
        <React.Fragment>
          <SwipeableDrawer
            anchor="left"
            open={isOpened}
            onClose={() => setIsOpened(false)}
            onOpen={() => setIsOpened(true)}
          >
            <div className={classes.list}>
              <List>
                <Box textAlign="cetner" p={2}>
                  Меню
                </Box>
                <Divider />
                <ListItem button onClick={() => console.log(123)}>
                  <ListItemText primary={"Новости"} />
                </ListItem>
                <ListItem button onClick={() => console.log(123)}>
                  <ListItemText primary={"Студенты"} />
                </ListItem>
                <ListItem button onClick={() => console.log(123)}>
                  <ListItemText primary={"Преподаватели"} />
                </ListItem>
                <ListItem button onClick={() => console.log(123)}>
                  <ListItemText primary={"Факультеты"} />
                </ListItem>
              </List>
            </div>
          </SwipeableDrawer>
        </React.Fragment>
      </div>
    </div>
  );
}

export default Navbar;
