import React, { useContext, useEffect, useState } from 'react'


const Context = React.createContext({})
const Provider = (props) => {
  const [printer, setPrinter] = useState();
  const [notificationToken, setNotificationToken] = useState();

  return (
    <Context.Provider
      value={{
        loading,
        error,
        subscribeToMoreOrders,
        refetch,
        networkStatus,
        printer,
        setPrinter,
        notificationToken
      }}>
      {props.children}
    </Context.Provider>
  )
}
export const useRestaurantContext = () => useContext(Context)
export default { Context, Provider }
