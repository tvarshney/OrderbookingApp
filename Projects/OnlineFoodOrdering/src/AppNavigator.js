import { createStackNavigator } from '@react-navigation/stack';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import Splash from './Navigation/Splash';
import Parent from './Navigation/Parent';
import { NavigationContainer } from '@react-navigation/native';
import LoginScreen from './screens/LoginScreen';
import RegisterScreen from './screens/RegisterScreen';
import RestaurantScreen from './screens/RestaurantScreen';
import CartScreen from './screens/CartScreen';
import OrderPrepairing from './screens/OrderPrepairingScreen';
import DeliveryScreen from './screens/DeliveryScreen';
const Stack = createStackNavigator();

export default function AppNavigator() {
  return (
    <NavigationContainer>
            <Stack.Navigator>
                <Stack.Screen name="Splash" component={Splash} options={{headerShown:false}}/>
                <Stack.Screen name="Login" component={LoginScreen} options={{headerShown:false}}/>
                <Stack.Screen name="Parent" component={Parent} options={{headerShown:false}}/>       
                <Stack.Screen name="Register" component={RegisterScreen} options={{headerShown:true}}/> 
                <Stack.Screen name="Restaurant" component={RestaurantScreen} options={{headerShown:false}}/>             
                <Stack.Screen name="Cart" options={{presentation: 'modal', headerShown:false}} component={CartScreen} />
                <Stack.Screen name="OrderPrepairing" options={{presentation: 'fullScreenModal'}} component={OrderPrepairing} />
                <Stack.Screen name="Delivery" options={{presentation: 'fullScreenModal', headerShown:false}} component={DeliveryScreen} />                                    
            </Stack.Navigator>
        </NavigationContainer>
  );
}
