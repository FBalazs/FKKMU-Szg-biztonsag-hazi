import React from 'react'
import { Redirect, Route } from 'react-router-dom'


const AdminRoute = ({ component: Component, ...rest }) => {

  const isAdmin = sessionStorage.getItem("role") === 'Admin'
  
  return (
    <Route
      {...rest}
      render={props =>
        isAdmin ? (
          <Component {...props} />
        ) : (
          <Redirect to={{ pathname: '/Album', state: { from: props.location } }} />
        )
      }
    />
  )
}

export default AdminRoute