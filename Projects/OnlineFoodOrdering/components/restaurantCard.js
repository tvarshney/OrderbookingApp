import { View, Text, Image, TouchableWithoutFeedback, ViewBase, TouchableOpacity, StyleSheet } from 'react-native'
import React, { useState, useEffect } from 'react'
import * as Icon from "react-native-feather";
//import { themeColors } from '../theme';
import { useNavigation } from '@react-navigation/native';

export default function RestaurantCard({item}) {
    const navigation = useNavigation();
    const prices = item.foods.map(food => food.price);
    const minPrice = Math.min(...prices);

    return(
        <View style={{paddingRight:10}}>
            <TouchableOpacity style={styles.offerContainer}
                onPress={()=> navigation.navigate('Restaurant', {...item})}
            >
                <View style={styles.imageContainer}>
                    <Image source={require('../assets/images/pizza.png')} style={styles.restaurantImage}/>
                    <View style={styles.overlayContainer}>
                        <View style={styles.deliveryOverlay}>
                            <Text>{item.deliveryTime} Min</Text>
                        </View>
                    </View>
                </View>
                <View style={styles.descriptionContainer}>
                    <View style={styles.aboutRestaurant}>
                        <Text style={{ width: '77%', fontWeight:'bold',fontSize:20}}>{item.restaurantName}</Text>
                        <View style={[styles.aboutRestaurant, { width: '23%' }]}>
                            <Image source={require('../assets/images/fullStar.png')} 
                            style={{height:15, width:15, tintColor:'green'}}/>
                            <Text style={{marginLeft:2}}>4</Text>
                            <Text style={{marginLeft:2}}>(0)</Text>
                        </View>
                    </View>
                </View>
                <Text style={styles.offerCategoty}>
                    Rs: 
                    <Text style={styles.restaurantPriceContainer}>
                        {minPrice}
                        <Text>
                        {' '}
                            Min                    
                        </Text>
                    </Text>
                </Text>                
            </TouchableOpacity>
        </View>
    )
}

const styles = StyleSheet.create({
    offerContainer: {
        backgroundColor: 'white',
        //props != null ? props.cartContainer : 'white',
        elevation: 3,
        //shadowColor: theme.Pink.white,  
        height: 220,
        borderRadius: 25,
        width: 238,
        marginBottom: 5,
        marginTop:5,
      },
      imageContainer: {
        position: 'relative',
        alignItems: 'center',
        height: '60%'
      },
      restaurantImage: {
        width: 220,
        height: '100%',
        borderRadius: 25,
        marginTop: 5
      },
      overlayContainer: {
        position: 'absolute',
        justifyContent: 'space-between',
        top: 0,
        height: '100%',
        backgroundColor: 'rgba(0, 0, 0, 0)',
        width: 230
      },
      deliveryOverlay: {
        position: 'absolute',
        top: 12,
        right: 18,
        width: 50,
        height: 19,
        justifyContent: 'center',
        alignItems: 'center',
        zIndex: 1,
        borderRadius: 10,
        backgroundColor: 'white'
        //props != null ? props.menuBar : 'white'
      },
      descriptionContainer: {
        paddingTop: 10,
        paddingBottom: 5,
        paddingLeft: 10,
        paddingRight: 10,
        height: '40%',
        width: '100%'
      },
      aboutRestaurant: {
        alignItems: 'center',
        flexDirection: 'row',
        justifyContent: 'flex-end'
      },
      offerCategoty: {
        width: '100%',
        //...alignment.MTxSmall,
        marginTop:-30,
        //...alignment.MBxSmall
        marginBottom:5,
        marginLeft:10,
      },
      restaurantPriceContainer: {
        marginTop: 3,
        fontSize: 15
      }
})