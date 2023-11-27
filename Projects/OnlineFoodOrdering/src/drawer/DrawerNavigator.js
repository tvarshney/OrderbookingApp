import { createDrawerNavigator } from '@react-navigation/drawer';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import HomeScreen from '../screens/HomeScreen';
import CustomDrawerMenu from './CustomDrawerMenu';

const Drawer = createDrawerNavigator();
export default function DrawerNavigator() {

  return (
    <Drawer.Navigator
    drawerContent={(props) => <CustomDrawerMenu {...props}/>}
    >
        <Drawer.Screen 
          name='Home' 
          component={HomeScreen} 
          options={{
            headerStyle: {
              backgroundColor: '#6FCF97', // Replace 'yourColorHere' with your desired color
            },
            
            //headerTintColor: 'white', // Change the color of the text in the header (e.g., white)
            //headerTitleStyle: {
              //fontWeight: 'bold', // You can adjust other title styles as needed
            //},
        }}/>
    </Drawer.Navigator>
  );
}