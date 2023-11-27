import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, ScrollView, Text, TouchableOpacity, View } from 'react-native';
import { ApiURL } from '../../constants';
import * as Icon from "react-native-feather";
import { useNavigation } from '@react-navigation/native';
import { useDispatch, useSelector } from 'react-redux';
import { removeFromCart, selectCartItems, selectCartTotal } from '../../slices/cartSlice';
import { useEffect, useState } from 'react';
import uuid from 'react-native-uuid';
import { themeColors } from '../../theme/Index';
import { selectRestaurant } from '../../slices/restaurantSlice';

export default function CartScreen() {
    const restaurant = useSelector(selectRestaurant);
    const navigation = useNavigation();
    const cartItems = useSelector(selectCartItems);
    const cartTotal = useSelector(selectCartTotal);
    const [groupedItems, setGroupedItems] = useState({});
    const dispatch = useDispatch();
    const deliveryFee = 2;
    //console.log(restaurant);
    useEffect(() => {
        const items = cartItems.reduce((group, item) => {
            if (group[item.foodId]) {
                group[item.foodId].push(item);
            } else {
                group[item.foodId] = [item];
            }
            return group;
        }, {})
        setGroupedItems(items);
    }, [cartItems])
    function generateRandomString(length) {
        const charset = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
        let result = '';
        const charsetLength = charset.length;

        for (let i = 0; i < length; i++) {
            const randomIndex = Math.floor(Math.random() * charsetLength);
            result += charset.charAt(randomIndex);
        }

        return result;
    }
    const FoodItems = [];

    Object.entries(groupedItems).forEach(([key, items]) => {
        let food = items[0];
        FoodItems.push(items.length + "-" + food.title);
        //console.log(food, "food");
    });
    const placedOrder = () => {
        const postOrderUrl = ApiURL.host + ApiURL.postOrder;
        const orderID = generateRandomString(7);
        const currentDate = new Date();
        // Get the current date and time as a string
        const currentDateTimeString = currentDate.toISOString();
        const data = {
            "id": uuid.v4(),
            "orderId": orderID,
            "items": JSON.stringify(FoodItems),
            "paymantMode": "COD",
            "status": "Order Placed",
            "orderDateTime": currentDateTimeString,
            "address": "string",
            "restaurantId": restaurant.restaurantId,
            "createdDate": currentDateTimeString
        }
        //console.log(data,"data")
        fetch(postOrderUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then((response) => {
            
            if (response.ok === true) { // You can adjust the condition based on your API response
              navigation.navigate('OrderPrepairing');
            } else {
              // Handle the case when the POST request was not successful
              // For example, display an error message or take appropriate action
            }
          })
          .catch((error) => {
            // Handle any network or other errors here
            console.error('Error:', error);
          });
    }
    return (
        <View style={styles.container}>
            {/*Back Button*/}
            <View style={styles.header}>
                <TouchableOpacity
                    onPress={() => navigation.goBack()}                    
                    style={styles.backButton}
                >
                    <Icon.ArrowLeft strokeWidth={3} stroke="white" />
                </TouchableOpacity>
                <View style={styles.headerText}>
                    <Text style={styles.headerTitle}>Your cart</Text>
                    <Text style={styles.headerSubtitle}>{restaurant.restaurantName}</Text>
                </View>
            </View>
            {/* Delevery Time*/}
            <View style={styles.deliveryTime}>
                <Image source={require('../../assets/images/bikeGuy.png')} style={styles.deliveryIcon} />
                <Text className="flex-1 pl-4">Deliver in {restaurant.deliveryTime} minutes</Text>
                <TouchableOpacity>
                    <Text style={styles.changeText}>
                        Change
                    </Text>
                </TouchableOpacity>
            </View>
            {/* Dishes*/}
            <ScrollView
                showsVerticalScrollIndicator={false}
                contentContainerStyle={{
                    paddingBottom: 50
                }}
                style={styles.dishes}>
                {
                    Object.entries(groupedItems).map(([key, items]) => {
                        let food = items[0];
                        return (
                            <View key={key}
                                style={styles.dishRow}>
                                <Image style={styles.dishImage} source={food.image} />
                                <Text style={styles.dishTitle}>{food.title}</Text>
                                <Text style={styles.dishPrice}>Rs: {food.price}</Text>
                                <Text style={styles.dishQuantity}>
                                    X {items.length}
                                </Text>
                                <TouchableOpacity
                                    style={styles.minusButton}
                                    onPress={() => dispatch(removeFromCart({ id: food.foodId }))}                                    
                                >
                                    <Icon.Minus strokeWidth={2} height={20} width={20} stroke='white' />
                                </TouchableOpacity>
                            </View>
                        )
                    })
                }
            </ScrollView>
            {/* Total View */}
            <View style={styles.totalView}>
                <View style={styles.totalRow}>
                    <Text style={styles.subtotalText}>Subtotal</Text>
                    <Text style={styles.subtotalValue}>Rs: {cartTotal}</Text>
                </View>
                <View style={styles.totalRow}>
                    <Text style={styles.deliveryText}>Delevery Fee</Text>
                    <Text style={styles.deliveryValue}>Rs: {deliveryFee}</Text>
                </View>
                <View style={styles.totalRow}>
                    <Text style={styles.orderTotalText}>Order Total</Text>
                    <Text style={styles.orderTotalValue}>Rs: {cartTotal + deliveryFee}</Text>
                </View>
                <View>
                    <TouchableOpacity
                        onPress={placedOrder}
                        style={styles.placeOrderButton}
                    >
                        <Text style={styles.placeOrderText}>
                            Place Order
                        </Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
    );
}
const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: 'white',
    },
    header: {
      paddingTop: 8,
      shadowColor: 'black',
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.25,
      position: 'relative',
      alignItems:'center',
      paddingBottom:15,
    },
    backButton: {
      backgroundColor: '#6FCF97',
      position: 'absolute',
      zIndex: 10,
      top: 30,
      left: 10,
      padding: 10,
      borderRadius: 50,
      
    },
    headerText: {
      textAlign: 'center',
      marginTop: 20,
    },
    headerTitle: {
      fontSize: 25,
      fontWeight: 'bold',
      textAlign: 'center',
    },
    headerSubtitle: {
      color: 'gray',
      textAlign: 'center',
      fontSize: 15,
    },
    deliveryTime: {
      backgroundColor: themeColors.bgColor(0.2),
      flexDirection: 'row',
      paddingHorizontal: 4,
      alignItems: 'center',
    },
    deliveryIcon: {
      width: 80,
      height: 80,
      borderRadius: 40,
    },
    changeText: {
      color: themeColors.text,
      fontWeight: 'bold',  
      marginLeft:10
    },
    dishes: {
      backgroundColor: 'white',
      paddingTop: 5,
    },
    dishRow: {
      flexDirection: 'row',
      alignItems: 'center',
      paddingHorizontal: 8,
      backgroundColor: 'white',
      borderRadius: 20,
      margin: 10,
      shadowColor: 'black',
      elevation: 5,
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.25,
    },
    dishImage: {
      width: 70,
      height: 70,
      borderRadius: 35,
    },
    dishTitle: {
      flex: 1,
      fontWeight: 'bold',
      color: 'gray',
    },
    dishPrice: {
      fontWeight: 'bold',
      fontSize: 16,
      marginRight:5,
    },
    dishQuantity: {
      fontWeight: 'bold',
      color: themeColors.text,
      fontSize: 16,
      marginRight:10,
    },
    minusButton: {
      backgroundColor: 'gray',
      padding: 5,
      borderRadius: 50,
    },
    totalView: {
      backgroundColor: themeColors.bgColor(0.2),
      paddingHorizontal: 8,
      borderRadius: 20,
      marginRight:10
    },
    totalRow: {
      flexDirection: 'row',
      justifyContent: 'space-between',
      padding: 8,
    },
    subtotalText: {
      color: 'gray',
    },
    subtotalValue: {
      color: 'gray',
      fontWeight: 'bold',
    },
    deliveryText: {
      color: 'gray',
    },
    deliveryValue: {
      color: 'gray',
      fontWeight: 'bold',
    },
    orderTotalText: {
      fontWeight: 'bold',
    },
    orderTotalValue: {
      fontWeight: 'bold',
    },
    placeOrderButton: {
      backgroundColor: '#6FCF97',
      padding: 12,
      borderRadius: 50,
      marginBottom:10
    },
    placeOrderText: {
      color: 'white',
      textAlign: 'center',
      fontWeight: 'bold',
      fontSize: 18,
    },
  });
  