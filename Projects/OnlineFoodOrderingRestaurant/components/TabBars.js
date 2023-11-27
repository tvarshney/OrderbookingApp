import React from 'react'
import { StyleSheet, View, Pressable, Text } from 'react-native'
import { colors } from '../src/utilities/colors'
//import { Badge } from 'react-native-elements/dist/badge/Badge'
import { Badge } from 'react-native-elements';



export default function TabBars(props) {
    const { setActiveBar, newAmount, processingAmount, deliveredAmount, rejectedAmount } = props
    const handleProcess = async () => {
        setActiveBar(1)
      }
      const handleDelivered = async () => {
        setActiveBar(2)
      }
      const handleRejected = async () => {
        setActiveBar(3)
      }
  
  return (
    <View style={styles.barContainer}>
      <Pressable
        onPress={() => setActiveBar(0)}
        style={[
          styles.barContent,
          {
            backgroundColor: props.activeBar === 0 ? 'black' : colors.white
          }
        ]}>
        {props.activeBar !== 0 ? (
          <Badge
            status="primary"
            value={newAmount}
            containerStyle={{ position: 'absolute', top: -5, left: 0 }}
            badgeStyle={{ backgroundColor: colors.darkgreen }}
            textStyle={{ color: colors.white }}
          />
          
        ) : <Badge
            status="primary"
            value={newAmount}
            containerStyle={{ position: 'absolute', top: -5, left: 0 }}
            badgeStyle={{ backgroundColor: colors.darkgreen }}
            textStyle={{ color: colors.white }}
          />}

        <Text
          style={{ color: props.activeBar === 0 ? colors.green : 'black' }}>
          New Orders
        </Text>
      </Pressable>
      <Pressable
        onPress={handleProcess}
        style={[
          styles.barContent,
          {
            backgroundColor: props.activeBar === 1 ? 'black' : colors.white
          }
        ]}>
        {props.activeBar !== 1 ? (
          <Badge
            status="primary"
            value={processingAmount}
            containerStyle={{ position: 'absolute', top: -5, left: 0 }}
            badgeStyle={{ backgroundColor: colors.darkgreen }}
            textStyle={{ color: colors.white }}
          />
        ) : <Badge
          status="primary"
          value={processingAmount}
          containerStyle={{ position: 'absolute', top: -5, left: 0 }}
          badgeStyle={{ backgroundColor: colors.darkgreen }}
          textStyle={{ color: colors.white }}
        />}
        <Text
          style={{ color: props.activeBar === 1 ? colors.green : 'black' }}>
          Processing
        </Text>
      </Pressable>
      <Pressable
        onPress={handleDelivered}
        style={[
          styles.barContent,
          {
            backgroundColor: props.activeBar === 2 ? 'black' : colors.white
          }
        ]}>
          {props.activeBar !== 2 ? (
          <Badge
            status="primary"
            value={deliveredAmount}
            containerStyle={{ position: 'absolute', top: -5, left: 0 }}
            badgeStyle={{ backgroundColor: colors.darkgreen }}
            textStyle={{ color: colors.white }}
          />
        ) : <Badge
          status="primary"
          value={deliveredAmount}
          containerStyle={{ position: 'absolute', top: -5, left: 0 }}
          badgeStyle={{ backgroundColor: colors.darkgreen }}
          textStyle={{ color: colors.white }}
        />}
        <Text
          style={{ color: props.activeBar === 2 ? colors.green : 'black' }}>
          Delivered
        </Text>
      </Pressable>
      <Pressable
        onPress={handleRejected}
        style={[
          styles.barContent,
          {
            backgroundColor: props.activeBar === 3 ? 'black' : colors.white
          }
        ]}>
          {props.activeBar !== 3 ? (
          <Badge
            status="primary"
            value={rejectedAmount}
            containerStyle={{ position: 'absolute', top: -5, left: 0 }}
            badgeStyle={{ backgroundColor: colors.darkgreen }}
            textStyle={{ color: colors.white }}
          />
        ) : <Badge
          status="primary"
          value={rejectedAmount}
          containerStyle={{ position: 'absolute', top: -5, left: 0 }}
          badgeStyle={{ backgroundColor: colors.darkgreen }}
          textStyle={{ color: colors.white }}
        />}
        <Text
          style={{ color: props.activeBar === 3 ? colors.green : 'black' }}>
          Reject
        </Text>
      </Pressable>
    </View>
  )
}
const styles = StyleSheet.create({
    topContainer: {
      backgroundColor: '#ffffffab',
      flex: 0.3,
      justifyContent: 'center',
      alignItems: 'center'
    },
    barContainer: {
      display: 'flex',
      flexDirection: 'row',
      width: '90%',
      alignSelf: 'center',
      justifyContent: 'space-around',
      padding: 10,
      backgroundColor: colors.white,
      borderRadius: 10,
      shadowColor: '#000',
      shadowOffset: {
        width: 0,
        height: 2
      },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
  
      elevation: 5,
      marginTop: -30
    },
    barContent: {
      padding: 10,
      borderRadius: 10
    }
  });