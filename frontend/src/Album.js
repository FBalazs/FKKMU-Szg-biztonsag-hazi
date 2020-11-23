import React from 'react';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import Footer from './Footer';
import Navbar from './Navbar';
import { withStyles } from '@material-ui/core/styles';

const useStyles = theme => ({
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
});

const role= "customer"
const cards = [1, 2, 3, 4, 5, 6, 7, 8, 9];
//array amiben object van
//[{title: title, animations: animations},{title: title, animations: animations}]

 const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJyb2xlIjoiQ3VzdG9tZXIiLCJuYmYiOjE2MDYxNTAxOTcsImV4cCI6MTYwNjc1NDk5NywiaWF0IjoxNjA2MTUwMTk3fQ.tEnr30UCEguUO2ALOAiHHRHr0R7TFfIlxCYqvdOCNoI"
 const requestOptions = {
   method: 'GET',
   headers: {
     'Content-Type': 'application/json',
     Authorization: 'Bearer ' + token,
   },
 };

class Album extends React.Component {
 
  constructor(props) {
    super(props);
    this.state = {
      animations: []
    };
  }

  componentDidMount(){
    fetch("https://localhost:8080/api/animations/", requestOptions)
    .then(response => {
         response.json().then(data =>{this.setState({animations:data}); console.log(this.state.animations)}) 
    });
  }

  render(){
    const { classes } = this.props;
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
                      image={this.state.animations[0]}
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
                      { role !="customer" &&
                      <Button size="small" color="primary" href="#" >
                        Delete
                      </Button>
                      }
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
}

export default withStyles(useStyles)(Album);
