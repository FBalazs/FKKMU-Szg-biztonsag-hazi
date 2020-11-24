import React from 'react';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { withStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Footer from './Footer';
import Navbar from './Navbar';
import Comments from './Comments'

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
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
});


class Item extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      animation: {"id": 1, title:"Test title", url:"https://localhost:8080/images/1.webp"}//null
    };
    
      // const animid = props.match.params.itemid
      // fetch("https://localhost:8080/api/animations/"+animid, {
      //   headers: {
      //     Authorization: 'Bearer ' + sessionStorage.getItem("token"),
      //   }
      // })
      // .then(response => {
      //   if(response.status == "200"){
      //     response.json().then(data => {
      //       this.setState({
      //         animation: data
      //       });
      //     })
      //   }
      //   console.log(response)
      // });
    
  }

  render(){
    const { classes } = this.props;
    return (
    <React.Fragment>
      <CssBaseline />
      <Navbar />
      <main>
        <div className={classes.heroContent}>
          <Container maxWidth="sm">
              <Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom>
                {this.state.animation.title}
            </Typography>
          </Container>
        </div>

        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4}>
              <Grid item  xs={12} >
                <Card >
                  <CardMedia
                    className={classes.cardMedia}
                    image={this.state.animation.url}
                    title={this.state.animation.title}
                  />
                  <CardActions>
                    <Button size="small" color="primary" href="#" variant="contained">
                      Download
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
          </Grid>
        </Container>
        <Comments/>

      </main>
      <Footer />
    </React.Fragment>
  );}
}

export default withStyles(useStyles)(Item);