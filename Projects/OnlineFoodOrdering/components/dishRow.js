import { View, Text, ScrollView, TouchableOpacity, Image } from 'react-native';
import React, { useState, useEffect } from 'react';
import * as Icon from 'react-native-feather';
import { useDispatch, useSelector } from 'react-redux';
import { addToCart, removeFromCart, selectCartItemsById } from '../slices/cartSlice';

export default function DishRow({ item }) {
  const dispatch = useDispatch();
  const totalItems = useSelector((state) => selectCartItemsById(state, item.foodId));

  const handleIncrease = () => {
    dispatch(addToCart({ ...item }));
  };

  const handleDecrease = () => {
    dispatch(removeFromCart({ id: item.foodId }));
  };

  return (
    <View style={styles.dishRow}>
      <Image style={styles.dishImage} source={require('../assets/images/pizzaDish.png')} />
      <View style={styles.infoContainer}>
        <View style={styles.dishInfo}>
          <Text style={styles.dishTitle}>{item.title}</Text>
          <Text style={styles.dishDescription}>{item.description}</Text>
        </View>
        <View style={styles.priceContainer}>
          <Text style={styles.priceText}>Rs: {item.price}</Text>
          <View style={styles.quantityContainer}>
            <TouchableOpacity
              onPress={handleDecrease}
              disabled={!totalItems.length}
              style={styles.quantityButton}
            >
              <Icon.Minus strokeWidth={2} height={20} width={20} stroke="white" />
            </TouchableOpacity>
            <Text style={styles.quantityText}>{totalItems.length}</Text>
            <TouchableOpacity onPress={handleIncrease} style={styles.quantityButton}>
              <Icon.Plus strokeWidth={2} height={20} width={20} stroke="white" />
            </TouchableOpacity>
          </View>
        </View>
      </View>
    </View>
  );
}

const styles = {
  dishRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'white',
    padding: 12,
    borderRadius: 20,
    shadowColor: 'black',
    shadowOffset: {
      width: 0,
      height: 2,
    },
    elevation: 5,
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    marginBottom: 10,
    marginHorizontal: 10,
  },
  dishImage: {
    width: 100,
    height: 100,
    borderRadius: 20,
  },
  infoContainer: {
    flex: 1,
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  dishInfo: {
    paddingLeft: 12,
    flex: 1,
  },
  dishTitle: {
    fontSize: 20,
    fontWeight: 'bold',
  },
  dishDescription: {
    color: 'gray',
  },
  priceContainer: {
    alignItems: 'center',
  },
  priceText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'gray',
  },
  quantityContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  quantityButton: {
    backgroundColor: 'gray',
    padding: 8,
    borderRadius: 50,
    marginHorizontal: 5,
  },
  quantityText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
};
