import { View, Text } from "react-native";
import React from "react";
import DrawerNavigator from "../drawer/DrawerNavigator";


export default function RestaurantParent() {
    return (
        <View style={{flex: 1}}>
            <DrawerNavigator/>
        </View>
    )
}