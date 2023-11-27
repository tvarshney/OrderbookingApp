import React from 'react';
import {
  SafeAreaView,
  View,
  StyleSheet,
  Image,
  Text,
  Linking,
  TouchableOpacity,
} from 'react-native';

import {
  DrawerContentScrollView,
  DrawerItemList,
  DrawerItem,
} from '@react-navigation/drawer';
import { useNavigation, useRoute } from '@react-navigation/native';

export default function CustomDrawerMenu (props) {  
    const navigation = useNavigation();
    const Register = () =>{
        navigation.navigate('Register');
    }
  const BASE_PATH =
    'https://raw.githubusercontent.com/AboutReact/sampleresource/master/';
  const proileImage = 'react_logo.png';

  return (
    <SafeAreaView style={{flex: 1}}>
        <View style={{backgroundColor: '#6FCF97', height:180}}>
            {/*Top Large Image */}
            <Image
                source={{uri: BASE_PATH + proileImage}}
                style={styles.sideMenuProfileIcon}
            />
            <TouchableOpacity onPress={Register}>
                <Text style={styles.text}>Login/Create Account</Text>
            </TouchableOpacity>
        </View>
        <DrawerContentScrollView {...props}>
            <DrawerItemList {...props} />        
        </DrawerContentScrollView>      
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  sideMenuProfileIcon: {
    resizeMode: 'center',
    width: 100,
    height: 100,
    borderRadius: 100 / 2,
    alignSelf: 'center',
    marginTop:30,
    borderColor:'#fff',
    borderWidth:2
  },
  iconStyle: {
    width: 15,
    height: 15,
    marginHorizontal: 5,
  },
  customItem: {
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
  },
  text:{
    marginTop:10,
    fontSize:18,
    color:"#000",
    alignSelf:'center'
  },
});