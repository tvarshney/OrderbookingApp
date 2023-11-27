import React from "react";
import { View, Text, ScrollView, Image, Dimensions, Platform, ActivityIndicator, AppState } from "react-native";
import { useNavigation } from "@react-navigation/native";
import TabBars from "../../components/TabBars";
import useOrders  from "../hooks/useOrders";
import HomeOrderDetails from "../context/HomeOrderDetails";
import LottieView from 'lottie-react-native';


const {width, height } = Dimensions.get('window');
export default function RestaurantHomeScreen(props) {
  const navigation = useNavigation();
  const {
    activeOrders,
    processingOrders,
    deliveredOrders,
    rejectedOrders,
    ordersData,
    refetch,
    active,
    setActive
  } = useOrders()
  
  return (
    <>
      
        <>
          <View style={styles.topContainer}>
            <Image
              source={require('../../assets/images/orders.png')}
              PlaceholderContent={<ActivityIndicator />}
              style={{ width: 250, height: 100 }}
            />
          </View>
          <View
            style={[
              styles.lowerContainer,
              {
                backgroundColor:"#6FCF97"                  
              }
            ]}>
            <TabBars   
                newAmount={activeOrders}
                processingAmount={processingOrders}
                rejectedAmount={rejectedOrders}
                deliveredAmount={deliveredOrders}
                activeBar={active}
                setActiveBar={setActive}
                refetch={refetch}
                orders={
                  ordersData &&
                  ordersData.filter(
                    order => order.status === 'Order Placed'
                  )
                }         
            />
            
              <ScrollView style={styles.scrollView}>
                <View style={{ marginBottom: 30 }}>                                                      
                        {active === 0 && activeOrders > 0
                          ? ordersData &&
                            ordersData
                            .filter(order => order.status === 'Order Placed')
                            .map((order, index) => {
                                return(
                                    <HomeOrderDetails
                                        key={index}
                                        activeBar={active}
                                        setActiveBar={setActive}
                                        navigation={props.navigation}
                                        order={order}
                                    />
                                )
                            })
                        : active === 0 && (                        
                        <View
                            style={{
                            minHeight: height - height * 0.45,
                            justifyContent: 'center',
                            alignItems: 'center'
                            }}>
                            <Text style={styles.text}>
                                No Orders Yet
                            </Text>
                            <LottieView
                            style={{
                                width: width - 100
                            }}
                            source={require('../../assets/images/loader.json')}
                            autoPlay
                            loop
                            />
                        </View>
                        )}
                    {active === 1 && processingOrders > 0
                          ? ordersData &&
                            ordersData
                            .filter(order => 
                                ['ACCEPTED', 'ASSIGNED', 'PICKED'].includes(
                                    order.status
                                  )
                                )
                            .map((order, index) => {
                                return(
                                    <HomeOrderDetails
                                        key={index}
                                        activeBar={active}
                                        setActiveBar={setActive}
                                        navigation={props.navigation}
                                        order={order}
                                    />
                                )
                            })
                        : active === 1 && (    
                            <View
                                style={{
                                minHeight: height - height * 0.45,
                                justifyContent: 'center',
                                alignItems: 'center'
                                }}>
                                <Text style={styles.text}>
                                    No Orders Yet
                                </Text>
                                <LottieView
                                style={{
                                    width: width - 100
                                }}
                                source={require('../../assets/images/loader.json')}
                                autoPlay
                                loop
                                />
                            </View>
                        )}
                        {active === 2  && deliveredOrders > 0
                        ? ordersData &&
                        ordersData
                        .filter(order => order.status === 'DELIVERED')
                        .map((order, index) => {
                          return (
                            <HomeOrderDetails
                              key={index}
                              activeBar={active}
                              setActiveBar={setActive}
                              navigation={props.navigation}
                              order={order}
                            />
                          )
                        })
                      : active === 2 && (
                      <View
                        style={{
                          minHeight: height - height * 0.45,
                          justifyContent: 'center',
                          alignItems: 'center'
                        }}>
                        <Text style={styles.text}>
                            No Orders Yet
                        </Text>
                        <LottieView
                          style={{
                            width: width - 100
                          }}
                          source={require('../../assets/images/loader.json')}
                          autoPlay
                          loop
                        />
                      </View>
                    )}
                    {active === 3  && rejectedOrders > 0
                        ? ordersData &&
                        ordersData
                        .filter(order => order.status === 'REJECTED')
                        .map((order, index) => {
                          return (
                            <HomeOrderDetails
                              key={index}
                              activeBar={active}
                              setActiveBar={setActive}
                              navigation={props.navigation}
                              order={order}
                            />
                          )
                        })
                      : active === 3 && (
                      <View
                        style={{
                          minHeight: height - height * 0.45,
                          justifyContent: 'center',
                          alignItems: 'center'
                        }}>
                        <Text style={styles.text}>
                            No Orders Yet
                        </Text>
                        <LottieView
                          style={{
                            width: width - 100
                          }}
                          source={require('../../assets/images/loader.json')}
                          autoPlay
                          loop
                        />
                      </View>
                    )}
                </View>
              </ScrollView>

          </View>
        </>

    </>
  );
}

const styles = {
    topContainer: {
        backgroundColor: "white",
        flex: 0.3,
        justifyContent: 'center',
        alignItems: 'center'
      },
      lowerContainer: {
        backgroundColor: "white",
        shadowColor: "gray",
        shadowOffset: {
          width: 0,
          height: 12
        },
        shadowOpacity: 0.58,
        shadowRadius: 16.0,
        elevation: 24,
        flex: 0.7,
        borderTopRightRadius: 30,
        borderTopLeftRadius: 30
      },
      scrollView: {
        backgroundColor: '#6FCF97',
        //marginBottom: Platform === 'ios' ? height * 0.1 : height * 0.1
      },
      text:{
        fontWeight: 'bold',
        fontSize: 24,
      }
    };  
