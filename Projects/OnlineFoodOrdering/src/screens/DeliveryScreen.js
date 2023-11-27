import React from 'react';
import { StatusBar } from 'expo-status-bar';
import { Image, Text, TouchableOpacity, View } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import MapView, { Marker } from 'react-native-maps';
import { themeColors } from '../../theme/Index';
import * as Icon from "react-native-feather";
import { useDispatch, useSelector } from 'react-redux';
import { selectRestaurant } from '../../slices/restaurantSlice';
import { emptyCart } from '../../slices/cartSlice';

export default function DeliveryScreen() {
  const restaurant = useSelector(selectRestaurant);
  const navigation = useNavigation();
  const dispatch = useDispatch();

  const cancelOrder = () => {
    navigation.navigate('Home');
    dispatch(emptyCart());
  };

  return (
    <View style={styles.container}>
      {/* Map View */}
      <MapView
        initialRegion={{
        //latitude: restaurant.lat,
        //longitude: restaurant.lng,
          latitude: 28.631324865149082,
          longitude: 77.34625983780818,
          latitudeDelta: 0.01,
          longitudeDelta: 0.01,
        }}
        style={styles.map}
        mapType='standard'
      >
        <Marker
          coordinate={{
            //latitude: restaurant.lat,
            //longitude: restaurant.lng,
            latitude: 28.631324865149082,
            longitude: 77.34625983780818,
          }}
          title={restaurant.restaurantName}
          pinColor={themeColors.bgColor(1)}
        />
      </MapView>

      <View style={styles.content}>
        <View style={styles.infoContainer}>
          <View style={styles.infoText}>
            <Text style={styles.infoTitle}>Estimated Arrival</Text>
            <Text style={styles.infoValue}>20-30 Minutes</Text>
            <Text style={styles.infoSubtitle}>Your order is on its way!</Text>
          </View>
          <Image style={styles.infoImage} source={require('../../assets/images/bikeGuy2.gif')} />
        </View>

        <View style={styles.deliveryInfo}>
          <View style={styles.deliveryAvatar}>
            <View style={styles.avatarBackground}>
              <Image style={styles.deliveryAvatarImage} source={require('../../assets/images/deliveryGuy.png')} />
            </View>
            <View style={styles.deliveryInfoText}>
              <Text style={styles.deliveryName}>Syed Noman</Text>
              <Text style={styles.deliverySubtitle}>Your Rider</Text>
            </View>
          </View>
          <View style={styles.iconButtons}>
            <TouchableOpacity style={styles.phoneButton}>
              <Icon.Phone fill={'#6FCF97'} stroke={'#6FCF97'} strokeWidth={1} />
            </TouchableOpacity>
            <TouchableOpacity onPress={cancelOrder} style={styles.cancelButton}>
              <Icon.X stroke={'red'} strokeWidth={4} />
            </TouchableOpacity>
          </View>
        </View>
      </View>
    </View>
  );
}

const styles = {
  container: {
    flex: 1,
  },
  map: {
    flex: 1,
  },
  content: {
    position: 'relative',
    backgroundColor: 'white',
  },
  infoContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 20,
    paddingTop: 40,
  },
  infoText: {
    flex: 1,
  },
  infoTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#6FCF97',
  },
  infoValue: {
    fontSize: 20,
    fontWeight: 'extrabold',
    color: '#6FCF97',
  },
  infoSubtitle: {
    marginTop: 10,
    color: '#6FCF97',
    fontWeight: 'bold',
  },
  infoImage: {
    width: 100,
    height: 100,
  },
  deliveryInfo: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 20,
    backgroundColor: '#6FCF97',
    padding: 10,
    borderRadius: 20,
    margin: 10,
  },
  deliveryAvatar: {
    flexDirection: 'row',
    alignItems: 'center',
    marginLeft:-5
  },
  avatarBackground: {
    padding: 10,
    borderRadius: 100,
    backgroundColor: 'rgba(255, 255, 255, 0.4)',
  },
  deliveryAvatarImage: {
    width: 80,
    height: 80,
    borderRadius: 40,
  },
  deliveryInfoText: {
    marginLeft: 10,
  },
  deliveryName: {
    fontSize: 20,
    fontWeight: 'bold',
    color: 'white',
  },
  deliverySubtitle: {
    fontWeight: 'bold',
    color: 'white',
  },
  iconButtons: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginRight: 10,
  },
  phoneButton: {
    backgroundColor: 'white',
    padding: 15,
    borderRadius: 100,
    marginLeft:10
  },
  cancelButton: {
    backgroundColor: 'white',
    padding: 15,
    borderRadius: 100,
    marginLeft:5
  },
};
