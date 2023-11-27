import { View, Text } from "react-native";
import React from "react";
import { createStackNavigator } from "@react-navigation/stack";
import { NavigationContainer } from "@react-navigation/native";
import Splash from "./navigation/RestaurantSplash";
import Parent from "./navigation/RestaurantParent";



const Stack = createStackNavigator();

export default function AppNavigator() {
    return (
        <NavigationContainer>
            <Stack.Navigator>
                <Stack.Screen name="Splash" component={Splash} options={{headerShown:false}}/>
                <Stack.Screen name="Parent" component={Parent} options={{headerShown:false}}/>
            </Stack.Navigator>
        </NavigationContainer>
    )
}