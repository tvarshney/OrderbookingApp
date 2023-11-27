import { View, Text, ScrollView, TouchableOpacity, Image } from 'react-native'
import React, { useState, useEffect } from 'react'
//import { themeColors } from '../theme'
import RestaurantVerticalCard from './restaurantVericalCard'

export default function FeaturedColumn({title, description, restaurants}) {
    return(
        <View>
            <View className="flex-row justify-between items-center px-4">
                <View>
                    <Text className="font-bold text-lg">{title}</Text>
                    <Text className="font-gray-500 text-xs">{description}</Text>
                </View>
                <TouchableOpacity>
                    <Text //style={{color: themeColors.text}} 
                    className="font-semibold">See All</Text>
                </TouchableOpacity>
            </View>
            <ScrollView 
                vertical
                showsVerticalScrollIndicator={false}
                contentContainerStyle={{
                  paddingHorizontal: 15,
                }}
                className="overflow-visible py-5">
                {
                    restaurants.map((restaurant, index)=>{
                        return(
                            <RestaurantVerticalCard
                                item={restaurant}
                                key={index}/>
                        )
                    })
                }
            </ScrollView>
        </View>
    )
}