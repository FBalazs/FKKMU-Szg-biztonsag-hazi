
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Footer from './Footer';
import Navbar from './Navbar';
import React, { useState, useEffect } from 'react'

const useStyles = makeStyles((theme) => ({
  icon: {
    marginRight: theme.spacing(2),
  },
  heroContent: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(8, 0, 6),
  },
  heroButtons: {
    marginTop: theme.spacing(4),
  },
  cardGrid: {
    paddingTop: theme.spacing(8),
    paddingBottom: theme.spacing(8),
  },
  card: {
    height: '100%',
    display: 'flex',
    flexDirection: 'column',
  },
  cardMedia: {
    paddingTop: '56.25%', // 16:9
  },
  cardContent: {
    flexGrow: 1,
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
}));

const cards = [1, 2, 3, 4, 5, 6, 7, 8, 9];
//array amiben object van
//[{title: title, animations: animations},{title: title, animations: animations}]



export default function Album() {
  const classes = useStyles();
  

  const [state, setState] = useState({
    animations : null
    
  });

  
    //event.preventDefault();
    const token="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiQ3VzdG9tZXIiLCJuYmYiOjE2MDYwNjUxNDksImV4cCI6MTYwNjY2OTk0OSwiaWF0IjoxNjA2MDY1MTQ5fQ.pgTyKQEBCUZkd5xBTTAyjD6gJuwgz2nmC-_dk148bIA"
    const requestOptions = {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + token,
      },
    };

    fetch("https://localhost:8080/api/animations/", requestOptions)
    .then(response => {
        response.json().then(data =>{setState({animations:data}); }) 
    });

    console.log(state.animations[0])

  

  
  return (
    <React.Fragment >
      <CssBaseline />
      <Navbar />
      
      <main>
        {/* Hero unit */}
        <div className={classes.heroContent}>
          <Container maxWidth="sm">
            <Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom>
              Animations
            </Typography>
          </Container>
        </div>
        
        <Container className={classes.cardGrid} maxWidth="md">
          {/* End hero unit */}
          <Grid container spacing={4}>
            {cards.map((card) => (
              <Grid item key={card} xs={12} sm={6} md={4} lg={3}>
                <Card className={classes.card}>
                  <CardMedia
                    className={classes.cardMedia}
                    image={state.animations[0]}
                    title="Image title"
                  />
                  <CardContent className={classes.cardContent}>
                    <Typography gutterBottom variant="h5" component="h2">
                      Heading
                    </Typography>
                    {/* <Typography>
                      This is a media card. You can use this section to describe the content.
                    </Typography> */}
                  </CardContent>
                  <CardActions>
                      <Button size="small" color="primary" href={"/album/" + card}>
                        View
                      </Button>
                    <Button size="small" color="primary" href="#">
                      Download
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </main>
      <Footer />
    </React.Fragment>
  );
}