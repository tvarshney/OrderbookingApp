import { View, Text } from "react-native";
import React from "react";
import { createDrawerNavigator } from "@react-navigation/drawer";
import RestaurantHomeScreen from "../screens/RestaurantHomeScreen";
import Main from "../screens/Main";
import CustomRestaurantDrawerMenu from "./CustomRestaurantDrawerMenu";
import RestaurantLoginScreen from "../screens/RestaurantLoginScreen";

const Drawer = createDrawerNavigator();
export default function DrawerNavigator() {
    return (
        <Drawer.Navigator
        drawerContent={(props) => <CustomRestaurantDrawerMenu {...props}/>}>
            <Drawer.Screen name="Login Screen" component={RestaurantLoginScreen} options={{headerShown:false}}/>
            <Drawer.Screen name="Home Screen" component={Main} options={{headerShown:false}}/>            
        </Drawer.Navigator>
    )
}