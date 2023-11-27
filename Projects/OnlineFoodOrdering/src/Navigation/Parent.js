import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import DrawerNavigator from '../drawer/DrawerNavigator';

export default function Parent() {
  return (
    <View style={{flex: 1}}>
      <DrawerNavigator/>
    </View>
  );
}