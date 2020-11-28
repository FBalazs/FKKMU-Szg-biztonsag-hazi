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
import GetApp from '@material-ui/icons/GetApp';
import DeleteIcon from '@material-ui/icons/Delete';
import {requestOptions} from './config';

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
    maxWidth: "100%"
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
  cardActions: {
    display: "flex",
    justifyContent: "space-between"
  }
});


class Album extends React.Component {
 
  constructor(props) {
    super(props);
    
    this.state = {
      animations: []
    };  
    this.getAnimations();
  }

  getAnimations(){
    requestOptions['method'] = "GET"
    fetch("https://localhost:8080/api/animations/", requestOptions)
    .then(response => {
         response.json().then(data =>{this.setState({animations:data}); console.log(data)}) 
    });
  }

  deleteHandler(id){
    requestOptions['method'] = "DELETE"
    console.log("delete", id)
    var newArray = this.state.animations.filter(function (obj) {
      return obj.id !== id;
    });
    this.setState({animations: newArray});
    fetch("https://localhost:8080/api/animations/" + id, requestOptions)
    .then(response => { console.log(response) });
  }

  saveByteArray(reportName, byte) {
      var blob = new Blob([byte], {type: "application/caff"});
      var link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      var fileName = reportName;
      link.download = fileName;
      link.click();
  };

  downloadHandler(id){
    requestOptions['method'] = "GET"
    console.log("download", id)
    fetch("https://localhost:8080/api/animations/"+ id, requestOptions)
    .then(response => { response.blob().then(data =>{this.saveByteArray(id + "_animation.caff", data)})});
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
              {this.state.animations.map((animation) => (
                <Grid item key={animation.id} xs={12} sm={6} md={4} lg={3}>
                  <Card className={classes.card}>
                    <CardMedia
                      className={classes.cardMedia}
                      image={animation.url}
                      title="Image title"
                    />
                    <CardContent className={classes.cardContent}>
                      <Typography  variant="h5" component="h2">
                        {animation.title}
                      </Typography>
                    </CardContent>
                    <CardActions className={classes.cardActions}>
                      <Button size="small" color="primary" variant="outlined" href={"/album/" + animation.id}>
                        View
                      </Button>
                      <Button size="small" color="primary"  startIcon={<GetApp />} variant="contained" onClick={() => this.downloadHandler(animation.id)}> 
                        Download
                      </Button>
                      { sessionStorage.getItem("role") === "Admin" &&
                        <Button size="small" color="secondary" startIcon={<DeleteIcon />} variant="contained"  onClick={ () => this.deleteHandler(animation.id) } >
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
