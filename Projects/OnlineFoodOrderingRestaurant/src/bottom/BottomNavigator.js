import { View, Text } from "react-native";
import React from "react";
import {createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import RestaurantHomeScreen from "../screens/RestaurantHomeScreen";
import { useNavigation } from "@react-navigation/native";
import { Icon } from 'react-native-elements/dist/icons/Icon';
import CustomRestaurantDrawerMenu from "../drawer/CustomRestaurantDrawerMenu";
import { colors } from "../utilities/colors";

const Bottom = createBottomTabNavigator()

export default function BottomNavigator() {
    const navigation = useNavigation();

    return(
        <Bottom.Navigator 
            initialRouteName="Home"
            tabBarLabelStyle={{
                color: colors.green,
                fontSize: 16,
              }}
            screenOptions={({ route }) => ({
                tabBarIcon: ({ focused}) => {
                    let iconName; 
                    let iconColor;         
                    if (route.name === 'Profile') {
                      iconName = focused ? 'user' : 'user';
                      iconColor = focused? colors.green : 'white';
                    } else if (route.name === 'Home') {
                      iconName = focused ? 'home' : 'home';
                      iconColor = focused? colors.green : 'white';
                    }          
                    return (
                      <Icon
                        type="font-awesome"
                        color={iconColor}
                        name={iconName}
                        size={40}
                      />
                    );
                  },
          
            headerShown: false,
            tabBarStyle: {
              height: 80,
              paddingHorizontal: 5,
              paddingTop: 0,
              backgroundColor: 'black',
              position: 'absolute',
              borderTopWidth: 0,
          },
        })}>
            <Bottom.Screen 
            name="Profile"
            component={CustomRestaurantDrawerMenu}
            listeners={() => ({
              tabPress: (e) => {
                e.preventDefault();
                navigation.openDrawer();
              },
            })}
            options={{
              headerShown: false,              
              tabBarLabel: 'Profile', // Set the label text
              tabBarLabelStyle: {
                fontSize: 14, // Adjust the font size as needed
                color: "white", // Text color
              },
            }} />
            <Bottom.Screen 
            name="Home"
            component={RestaurantHomeScreen}
            options={{
              headerShown: false,              
              tabBarLabel: 'Home', // Set the label text
              tabBarLabelStyle: {
                fontSize: 14, // Adjust the font size as needed
                color: colors.green, // Text color
              },
            }}/>
            
        </Bottom.Navigator>
    )
}

