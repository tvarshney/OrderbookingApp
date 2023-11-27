import { StyleSheet, View, Text, ScrollView, Image, TouchableOpacity, StatusBar } from 'react-native';
import React, { useEffect } from 'react';
import { useNavigation, useRoute } from '@react-navigation/native';
import * as Icon from 'react-native-feather';
import DishRow from '../../components/dishRow';
import CartIcon from '../../components/cartIcon';
import { useDispatch } from 'react-redux';
import { setRestaurant } from '../../slices/restaurantSlice';
import { themeColors } from '../../theme/Index';

export default function RestaurantScreen() {
  const { params } = useRoute();
  const navigation = useNavigation();
  let item = params;
  const dispatch = useDispatch();

  useEffect(() => {
    if (item && item.restaurantId) {
      dispatch(setRestaurant({ ...item }));
    }
  }, []);

  const uniqueItems = Array.from(new Set(item.foods.map((item) => item.foodId))).map((id) => {
    return item.foods.find((item) => item.foodId === id);
  });

  return (
    <View style={{ flex: 1 }}>
      <CartIcon />
      <StatusBar style="light" />
      <ScrollView>
        <View style={{ position: 'relative' }}>
          <Image style={styles.image} source={require('../../assets/images/pizza.png')} />
          <TouchableOpacity onPress={() => navigation.goBack()} style={styles.backButton}>
            <Icon.ArrowLeft strokeWidth={3} stroke={themeColors.bgColor(1)}/>
          </TouchableOpacity>
          <View style={styles.restaurantInfo}>
            <View style={{ alignItems: 'center' }}>
              <Text style={styles.restaurantName}>{item.restaurantName}</Text>
              <Text>Delivery: {item.deliveryTime} Minutes</Text>
              <View style={styles.starRating}>
                <Image source={require('../../assets/images/fullStar.png')} style={styles.starIcon} />
                <Text>0</Text>
                <Text>(4)</Text>
              </View>
            </View>
          </View>
        </View>
        <View style={styles.restaurantDetails}>
          <Text style={styles.restaurantName}>{item.restaurantName}</Text>
          <View style={styles.additionalInfo}>
            <View style={styles.starRating}>
              <Image source={require('../../assets/images/fullStar.png')} style={styles.starIcon} />
              <Text style={styles.ratingText}>
                <Text style={styles.greenText}>{item.stars}</Text>
                <Text style={styles.grayText}> ({item.reviews} review) . </Text>
                <Text style={styles.boldText}>{item.category}</Text>
              </Text>
            </View>
            <View style={styles.locationInfo}>
              <Icon.MapPin color="gray" height="15" width="15" />
              <Text style={styles.locationText}>
                Nearby. {item.address + ', ' + item.city + ', ' + item.country + ', ' + item.postalCode}
              </Text>
            </View>
          </View>
          <Text style={styles.descriptionText}>{item.description}</Text>
        </View>
        <View style={styles.menuContainer}>
          <Text style={styles.menuTitle}>Menu</Text>
          {/* Dishes */}
          {uniqueItems.map((food, index) => (
            <DishRow item={{ ...food }} key={index} />
          ))}
        </View>
      </ScrollView>
    </View>
  );
}

const styles = StyleSheet.create({
  image: {
    width: '100%',
    height: 300,
  },
  backButton: {
    position: 'absolute',
    top: 30,
    left: 15,
    backgroundColor: 'white',
    padding: 10,
    borderRadius: 50,
  },
  restaurantInfo: {
    position: 'absolute',
    alignSelf:'center',
    marginVertical:80,
    backgroundColor: 'white',
    padding: 10,
    shadowColor: 'black',
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    borderRadius: 10,
    marginLeft: 30,
  },
  restaurantName: {
    fontSize: 24,
    fontWeight: 'bold',
  },
  starRating: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 10,
  },
  starIcon: {
    height: 20,
    width: 20,
    tintColor: 'green',
  },
  restaurantDetails: {
    backgroundColor: 'white',
    borderTopLeftRadius: 40,
    borderTopRightRadius: 40,
    marginTop:-40,
    paddingTop: 20,
    paddingLeft: 10,
  },
  additionalInfo: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginVertical: 10,
  },
  ratingText: {
    fontSize: 12,
  },
  greenText: {
    color: 'green',
  },
  grayText: {
    color: 'gray',
  },
  boldText: {
    fontWeight: 'bold',
  },
  locationInfo: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  locationText: {
    color: 'gray',
  },
  descriptionText: {
    color: 'gray',
    marginTop: 10,
  },
  menuContainer: {
    backgroundColor: 'white',
    paddingBottom: 36,
    paddingLeft: 20,
  },
  menuTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom:10,
  },
});
