import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Slide from '@material-ui/core/Slide';
import CloudUpload from '@material-ui/icons/CloudUpload';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});


export default function AlertDialogSlide() {
  const [open, setOpen] = React.useState(false);
  const [selectedFile, setFile] = React.useState(null);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

   const handleChange = (file) => {
    let reader = new FileReader();
    console.log(file); //I can see the file's info
    reader.onload= () => {
        var binary = '';
        var bytes = new Uint8Array(reader.result);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        console.log(btoa(binary))
        setFile(binary)
      
   
    }
    reader.readAsArrayBuffer(file)

   };

   const handleSubmit = (event) => {
    
    //console.log(selectedFile)
    fetch('https://localhost:8080/api/animations', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'Bearer ' + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({"fileBytes" : selectedFile}),
        
    })
    .then(response => { 
        console.log(response); 
        if(response.status === 200){
            setOpen(false);
            window.location ="/album"
        }
        //else{ 
          //setOpen(false);
          //window.location ="/album"
        //}
    })
  
    event.preventDefault();
  };

  

  return (
    <div>
      <Button variant="contained" startIcon={<CloudUpload/>} onClick={handleClickOpen}>
        Upload
      </Button>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        onClose={handleClose}
        aria-labelledby="alert-dialog-slide-title"
        aria-describedby="alert-dialog-slide-description"
      >
        <DialogTitle id="alert-dialog-slide-title">{"Upload new animation"}</DialogTitle>

        <form onSubmit={handleSubmit} noValidate>
            <DialogContent>
            <DialogContentText id="alert-dialog-slide-description">
                <input color="inherit" style={{marginTop: 8}} type="file" onChange={e => handleChange(e.target.files[0])} /> 
            </DialogContentText>
            </DialogContent>
            <DialogActions>
            <Button onClick={handleSubmit} variant="contained" color="primary">
                Upload
            </Button>
            </DialogActions>
        </form>

      </Dialog>
    </div>
  );
}