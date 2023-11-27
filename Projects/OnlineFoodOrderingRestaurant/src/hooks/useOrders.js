import { useState, useContext, useEffect } from 'react'
import Restaurant from '../context/restaurant';
import { ApiURL } from '../context/Api';


export default function useOrders() {
  const [active, setActive] = useState(0);
  const { loading, error, refetch, networkStatus } = useContext(
    Restaurant.Context
  )
  const [ordersData, setOrdersData] = useState([]);
  const getOrdersListUrl = ApiURL.host+ApiURL.getOrdersList;
  useEffect(() => {
    fetch(getOrdersListUrl).then((result)=>{
        result.json().then((resp)=>{
            setOrdersData(resp)
        })
    })
  }, []);
  const activeOrders =
  ordersData &&
  ordersData.filter(order => order.status === 'Order Placed')
      .length
  const processingOrders =
  ordersData &&
  ordersData.filter(order =>
      ['ACCEPTED', 'ASSIGNED', 'PICKED'].includes(order.status)
    ).length
  const deliveredOrders =
  ordersData &&
  ordersData.filter(order => order.status === 'DELIVERED')
      .length
  const rejectedOrders =
  ordersData &&
  ordersData.filter(order => order.status === 'REJECTED')
      .length

  return {
    loading,
    error,
    ordersData,
    activeOrders,
    processingOrders,
    deliveredOrders,
    rejectedOrders,
    networkStatus,
    refetch,   
    active,
    setActive
  }
}
