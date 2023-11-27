import React from 'react';
import { useNavigation } from '@react-navigation/native';
import { StatusBar } from 'expo-status-bar';
import { useEffect } from 'react';
import { Image, Text, View } from 'react-native';

export default function OrderPrepairing() {
  const navigation = useNavigation();

  useEffect(() => {
    setTimeout(() => {
      // Move to delivery screen
      navigation.navigate('Delivery');
    }, 3000);
  }, []);

  return (
    <View style={styles.container}>
      <Image source={require('../../assets/images/delivery.gif')} style={styles.image} />
    </View>
  );
}

const styles = {
  container: {
    flex: 1,
    backgroundColor: 'white',
    justifyContent: 'center',
    alignItems: 'center',
  },
  image: {
    width: 350,
    height: 350,
  },
};
