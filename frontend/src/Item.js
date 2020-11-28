import React from 'react';
import Card from '@material-ui/core/Card';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { withStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Footer from './Footer';
import Navbar from './Navbar';
import Comments from './Comments'
import { requestOptions } from './config';

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
    this.animid = props.match.params.itemid

    this.state = {"id": this.animid, title:"", url:""};
   
    requestOptions['method'] = "GET"
    fetch("https://localhost:8080/api/animations/", requestOptions)
      .then(response => {
        response.json().then(data => {
            data.forEach(element => {
              if(element.id === this.animid){
                this.setState({"id": this.animid, "title": element.title, "url": element.url})
                return false
              }
            });
          })
      });
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
                {this.state.title}
            </Typography>
          </Container>
        </div>

        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4}>
              <Grid item  xs={12} >
                <Card >
                  <CardMedia
                    className={classes.cardMedia}
                    image={this.state.url}
                    src="image"
                    title={this.state.title}
                  />
                </Card>
              </Grid>
          </Grid>
        </Container>
        <Comments anim_id={this.animid}/>

      </main>
      <Footer />
    </React.Fragment>
  );}
}

export default withStyles(useStyles)(Item);