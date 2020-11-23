import React from 'react';
import { AppBar, Toolbar, Button, Box } from '@material-ui/core';



class NavBar extends React.Component {
  
    state = { 
        selectedFile: null  // Initially, no file is selected 
      }; 
       
      // On file select (from the pop up) 
      onFileChange = event => { 
        this.setState({ selectedFile: event.target.files[0] });       
      }; 
       
      onFileUpload = () => {       
        const formData = new FormData(); 
       
        // Update the formData object 
        formData.append( 
          "myFile", 
          this.state.selectedFile, 
          this.state.selectedFile.name 
        ); 
       
        console.log(this.state.selectedFile); 
        //saját api hívása
        //axios.post("api/uploadfile", formData); 
      }; 
       
      // File content to be displayed after 
      // file upload is complete 
      //fileData = () => { 
      // 
      //  if (this.state.selectedFile) { 
      //      
      //    return ( 
      //      <div> 
      //        <h2>File Details:</h2> 
      //        <p>File Name: {this.state.selectedFile.name}</p> 
      //        <p>File Type: {this.state.selectedFile.type}</p> 
      //        <p> 
      //          Last Modified:{" "} 
      //          {this.state.selectedFile.lastModifiedDate.toDateString()} 
      //        </p> 
      //      </div> 
      //    ); 
      //  } else { 
      //    return ( 
      //      <div> 
      //        <br /> 
      //        <h4>Choose before Pressing the Upload button</h4> 
      //      </div> 
      //    ); 
      //  } 
      //}; 

    render() {
        return (
            <AppBar position="static">
                <Toolbar container="true" align-content="space-between">
                    
                    <Box display='flex' flexGrow={1}>
                        <Button color="inherit" href="/Album">Home </Button>

                        <input color="inherit" style={{marginTop: 8}} type="file" onChange={this.onFileChange} /> 

                        <Button color="inherit" onClick={this.onFileUpload} >Upload </Button>
                    </Box>
                    <Button color="inherit" href="/">Log Out </Button>
                    
                </Toolbar>
            </AppBar>
        );
    }
}

export default (NavBar);