import React from 'react';
import { View, Text, TouchableOpacity, Image } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { useSelector } from 'react-redux';
import { selectCartItems, selectCartTotal } from '../slices/cartSlice';

export default function CartIcon() {
  const navigation = useNavigation();
  const cartItems = useSelector(selectCartItems);
  const cartTotal = useSelector(selectCartTotal);

  if (!cartItems.length) return null;

  return (
    <View style={styles.cartIcon}>
      <TouchableOpacity
        onPress={() => navigation.navigate('Cart')}
        style={styles.cartButton}
      >
        <View style={styles.cartBadge}>
          <Text style={styles.cartBadgeText}>{cartItems.length}</Text>
        </View>
        <Text style={styles.cartButtonText}>View Your Cart</Text>
        <Text style={styles.cartTotal}>Rs: {cartTotal}</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = {
  cartIcon: {
    position: 'absolute',
    bottom: 5,
    width: '100%',
    zIndex: 50,
  },
  cartButton: {
    backgroundColor: '#6FCF97',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginHorizontal: 5,
    borderRadius: 20,
    padding: 10,
    shadowColor: 'black',
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
  },
  cartBadge: {
    backgroundColor: '#000',
    padding: 8,
    borderRadius: 50,
    height:'100%',
    width:'11%'    
  },
  cartBadgeText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
    textAlign:'center'
  },
  cartButtonText: {
    flex: 1,
    textAlign: 'center',
    fontSize: 18,
    fontWeight: 'bold',
    color: 'black',
  },
  cartTotal: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'black',
  },
};
