import { StyleSheet, View, Text, ScrollView, TouchableOpacity, Image } from 'react-native'
import React, { useState, useEffect } from 'react'
//import { themeColors } from '../theme'
import RestaurantCard from './restaurantCard'

export default function FeaturedRow({title, description, restaurants}) {
    return(
        <View style={styles.container}>
            <View style={styles.header}>
                <View>
                <Text style={styles.title}>{title}</Text>
                <Text style={styles.description}>{description}</Text>
                </View>
                <TouchableOpacity>
                <Text style={styles.seeAll}>See All</Text>
                </TouchableOpacity>
            </View>
            <ScrollView
                horizontal
                showsHorizontalScrollIndicator={false}
                contentContainerStyle={styles.horizontalScrollView}
            >
                {restaurants.map((restaurant, index) => (
                <RestaurantCard item={restaurant} key={index} />
                ))}
            </ScrollView>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
      padding: 20,
    },
    header: {
      flexDirection: 'row',
      justifyContent: 'space-between',
      alignItems: 'center',
      paddingHorizontal: 20,
    },
    title: {
      fontWeight: 'bold',
      fontSize: 20,
    },
    description: {
      color: 'gray',
      fontSize: 12,
    },
    seeAll: {
      color: '#f97316', // Define your themeColors variable
      fontWeight: '600',
    },
    horizontalScrollView: {
      padding: 15,
    },
  });