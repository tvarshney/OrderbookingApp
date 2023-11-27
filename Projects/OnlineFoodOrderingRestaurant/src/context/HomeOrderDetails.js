import { StyleSheet, View, Text, Pressable } from "react-native";
import React, { useContext, useState, useRef, useEffect } from "react";
import { Badge } from 'react-native-elements';
import { colors } from "../utilities/colors";
import CountDown from "react-native-countdown-component";
import moment from 'moment';
import { MAX_TIME } from "../utilities/enums";


export default function HomeOrderDetails(props) {
    const { activeBar, navigation } = props
    const {
        orderId,
        orderAmount,
        paymantMode,
        orderDateTime,
        id,
        preparationTime,
        createdDate,
        isRinged
      } = props?.order
      const timeNow = new Date()

      const date = new Date(orderDateTime)
        const acceptanceTime = moment(date).diff(timeNow, 'seconds')
        // current
        const createdTime = new Date(createdDate)
        var remainingTime = moment(createdTime)
            .add(MAX_TIME, 'seconds')
            .diff(timeNow, 'seconds')
        //const configuration = useContext(Configuration.Context)

        // prepTime
        const prep = new Date(preparationTime)
        const diffTime = prep - timeNow
        const totalPrep = diffTime > 0 ? diffTime / 1000 : 0

        // accept time

      const [isAcceptButtonVisible, setIsAcceptButtonVisible] = useState(
        !moment().isBefore(date)
      )
      const timer = useRef()
      const decision = !isAcceptButtonVisible
        ? acceptanceTime
        : remainingTime > 0
          ? remainingTime
          : 0
      if (decision === acceptanceTime) {
        remainingTime = 0
      }
      useEffect(() => {
        let isSubscribed = true
        ;(() => {
          timer.current = setInterval(() => {
            const isAcceptButtonVisible = !moment().isBefore(orderDateTime)
            isSubscribed && setIsAcceptButtonVisible(isAcceptButtonVisible)
            if (isAcceptButtonVisible) {
              timer.current && clearInterval(timer.current)
            }
          }, 10000)
        })()
        return () => {
          timer.current && clearInterval(timer.current)
          isSubscribed = false
        }
      }, [])
      const status =
        activeBar === 0 ? 'New Order' :
        activeBar === 1 ? 'Processing' :
        activeBar === 2 ? 'Delivered' :
        'Reject';

    return (
        <Pressable
            style={[
                Styles.card,
                {
                backgroundColor:
                    activeBar === 0
                    ? colors.white
                    : activeBar === 1
                        ? colors.white
                        : colors.darkgreen
                }
            ]}
            onPress={() => {
                navigation.navigate('OrderDetail', {
                activeBar,
                orderData: props?.order,
                remainingTime,
                createdAt,
                MAX_TIME,
                acceptanceTime,
                preparationTime
                })
            }}>
            {activeBar === 0 ? (
                <Badge
                    status="success"
                    containerStyle={{ position: 'absolute', top: 0, right: 0 }}
                    badgeStyle={{
                        backgroundColor: colors.rounded,
                        width: 10,
                        height: 10,
                        borderRadius: 10
                    }}
                />
            ) : null}

            <View style={Styles.itemRowBar}>
                <Text style={Styles.heading}>
                Order Id:
                </Text>
                <Text style={Styles.text}>
                {orderId}
                </Text>
            </View>
            <View style={Styles.itemRowBar}>
                <Text style={Styles.heading}>Order amount:</Text>
                <Text
                style={
                    Styles.text
                }>{"Rs:"}</Text>
            </View>
            <View style={Styles.itemRowBar}>
                <Text style={Styles.heading}>Payment method:</Text>
                <Text style={Styles.text}>{paymantMode}</Text>
            </View>
            <View style={Styles.itemRowBar}>
                <Text style={Styles.heading}>Time:</Text>
                <Text style={Styles.text}>
                {moment(date).format('lll')}
                </Text>
            </View>
            <View
                style={{
                borderBottomColor: colors.fontSecondColor,
                borderBottomWidth: 1
                }}
            />
            <View style={Styles.timerBar}>
                {activeBar === 0 && (
                <View
                    style={{
                    display: 'flex',
                    flexDirection: 'row',
                    alignItems: 'center',
                    flexBasis: '50%'
                    }}>
                </View>
                )}
                {activeBar === 1 && (
                <View>
                    
                </View>
                )}
                <View>
                <Pressable
                    style={[
                    Styles.btn,
                    {
                        backgroundColor:
                        activeBar === 0
                            ? 'black'
                            : activeBar === 1
                            ? colors.green
                            : colors.white
                    }
                    ]}
                    onPress={() =>
                    navigation.navigate('OrderDetail', {
                        activeBar,
                        orderData: props?.order,
                        remainingTime,
                        createdAt,
                        MAX_TIME,
                        acceptanceTime,
                        preparationTime,
                        isRinged
                    })
                    }>
                    <Text
                    
                    style={{
                        color:
                        activeBar === 0
                            ? colors.green
                            : activeBar === 1
                            ? colors.orderUncomplete
                            : 'black'
                    }}>
                    {status}
                    </Text>
                </Pressable>
                </View>
            </View>
        </Pressable>
    )
}

const Styles = StyleSheet.create({
    card: {
      display: 'flex',
      width: '80%',
      alignSelf: 'center',
      marginTop: 20,
      padding: 20,
      borderRadius: 15,
      shadowColor: 'gray',
      shadowOffset: {
        width: 0,
        height: 7
      },
      shadowOpacity: 0.43,
      shadowRadius: 9.51,
  
      elevation: 15
    },
    heading: {
      flex: 0.45
    },
    text: {
      flex: 0.55
    },
    itemRowBar: {
      flexDirection: 'row',
      justifyContent: 'space-between',
      paddingBottom: 5
    },
    btn: {
      padding: 10,
      borderRadius: 10,
      justifyContent: 'center',
      alignItems: 'center',
      alignSelf: 'center'
    },
    timerBar: {
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
      flex: 1,
      justifyContent: 'space-around',
      marginTop: 10
    }
  });
  
  