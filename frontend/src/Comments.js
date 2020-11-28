import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import Box from '@material-ui/core/Box';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import DeleteIcon from '@material-ui/icons/Delete';
import SendIcon from '@material-ui/icons/Send';
import { requestOptions } from './config';

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
        // paddingTop: theme.spacing(8),
        paddingBottom: theme.spacing(8),
    },
    card: {
        height: '100%',
        display: 'flex',
        flexDirection: 'column',
        borderRadius: 0
    },
    cardMedia: {
        paddingTop: '56.25%', // 16:9
    },
    cardContent: {
        flexGrow: 1,
        textAlign: "left",
    },
    footer: {
        backgroundColor: theme.palette.background.paper,
        padding: theme.spacing(6),
    },
    newCommentField: {
        
    }
}));



function Comments(props) {
    const classes = useStyles();
    const [comments, setComments] = React.useState([]);
    const [newComment, setNewComment] = React.useState("");
    

    useEffect (() =>{
        fetch('https://localhost:8080/api/comments/' + props.anim_id, requestOptions)
        .then(response => { console.log(response);
            response.json().then(data => { setComments(data); console.log(data) })
        });
    },[props.anim_id]
    );

    const deleteHandler = (id) => {
        requestOptions['method'] = "DELETE"
        console.log("delete", id)
        
        fetch("https://localhost:8080/api/comments/" +id, requestOptions)
        .then(response => { console.log(response); 
            if (response.status===200){ 
            var newArray = comments.filter(function (obj) {
                return obj.id !== id;
                });
                setComments(newArray)}
        });
    }

    const sendComment = () => {
        console.log("send comment");
        console.log(newComment);
        console.log(sessionStorage.getItem("userid"));
        console.log(props.anim_id);
        if(newComment === ""){
            alert("Message cannot be empty")
            return false;
        }

        fetch('https://localhost:8080/api/comments', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + sessionStorage.getItem("token"),
                },
                body:  JSON.stringify({"CommentMessage": newComment ,"UserId": sessionStorage.getItem("userid"),"FileId":props.anim_id}),
            })
            .then(response => {
                
                console.log(response);
                 if (response.status === 200) { 
                    window.location.reload()
                 }
            })
    }



    return (
    <Container className={classes.cardGrid} maxWidth="md">
        <Grid container>
            <TextField className={classes.newCommentField}
            id = "filled-textarea"
            label = "Say something"
            rows={3}
            variant="outlined"
            fullWidth={true}
            onChange={(e) => {setNewComment(e.target.value);}}
            multiline/>

            <Box m={1} display="flex" justifyContent="flex-end" width="100%">
                <Button size="medium" color="primary" startIcon={<SendIcon />} variant="contained"  onClick={ sendComment } >
                            Send
                </Button>
            </Box>

            {comments.map((comment) => (
            <Grid key={comment.id} item xs={12}>
                <Card className={classes.card}> 
                 <CardActions className={classes.cardActions}>
                    <Box fontWeight="fontWeightBold" align="left" >
                        {comment.userId}
                    </Box>
                    { sessionStorage.getItem("role") === "Admin" &&
                    <Button size="small" color="secondary" startIcon={<DeleteIcon />} variant="contained"  onClick={ () => deleteHandler(comment.id) } >
                        Delete
                    </Button>
                    }
                </CardActions>
                <CardContent className={classes.cardContent}>
                   {comment.commentMessage}
                </CardContent>
               
                </Card>
            </Grid>
            ))}
        </Grid>
    </Container>
  );
}

export default Comments;