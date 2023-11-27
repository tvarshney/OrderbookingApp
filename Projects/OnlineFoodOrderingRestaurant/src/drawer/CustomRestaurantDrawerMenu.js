import React from 'react';
import {
  SafeAreaView,
  View,
  StyleSheet,
  Image,
  Text,
  Linking,
  TouchableOpacity,
  ActivityIndicator,
  Switch,
} from 'react-native';

import {
  DrawerContentScrollView,
  DrawerItemList,
  DrawerItem,
} from '@react-navigation/drawer';
import { useNavigation, useRoute } from '@react-navigation/native';
import { Icon } from 'react-native-elements/dist/icons/Icon';
import { colors } from '../utilities/colors';

export default function CustomRestaurantDrawerMenu (props) {  
    const navigation = useNavigation();
    const Register = () =>{
        navigation.navigate('Register');
    }
  const BASE_PATH =
    'https://raw.githubusercontent.com/AboutReact/sampleresource/master/';
  const proileImage = 'react_logo.png';

  return (
    <SafeAreaView style={{flex: 1, backgroundColor: 'black'}}>
        <View style={styles.topContainer}>
          <View style={styles.profileContainer}>
            <View style={styles.avatar}>
              <Image
                source={{uri: BASE_PATH + proileImage}}
                containerStyle={styles.item}
                style={{ borderRadius: 5 }}
                PlaceholderContent={<ActivityIndicator />}
              />
            </View>
            <View style={{ width: '50%' }}>
              <Text                
                style={{ marginLeft: 10, textAlign: 'left', fontSize:18, fontWeight:'bold', color:'white' }}>
                  Restaurant Name
                {/*data && data.restaurant.name*/}
              </Text>
            </View>
          </View>
        </View>
        <View style={styles.middleContainer}>
          <View style={styles.status}>
            <Text style={{fontSize:16, fontWeight:"bold", fontSize:18, fontWeight:'bold',color:"white"}}>
              Status
            </Text>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <Text style={{ marginRight: 5, color:"white" }}>
              Online{/*isAvailable ? 'Online' : 'Closed'*/}
              </Text>
              <Switch
                trackColor={{
                  false: colors.fontSecondColor,
                  true: colors.green
                }}
                thumbColor={/*isAvailable ? colors.headerBackground : */'#f4f3f4'}
                ios_backgroundColor="#3e3e3e"
                onValueChange={"toggleSwitch"}
                value={"isAvailable"}
                style={{ marginTop: /*Platform.OS === 'android' ? -15 :*/ -5 }}
              />
            </View>
          </View>
          <View style={styles.status}>
            <Text style={{fontSize:16, fontWeight:"bold", fontSize:18, fontWeight:'bold', color:"white"}}>
              Notifications
            </Text>
            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
              <Text style={{ marginRight: 5, color:"white" }}>
              On{/*notificationStatus ? 'On' : 'Off'*/}
              </Text>
              <Switch
                trackColor={{
                  false: colors.fontSecondColor,
                  true: colors.green
                }}
                thumbColor={/*isAvailable ? colors.headerBackground : */'#f4f3f4'}
                ios_backgroundColor="#3e3e3e"
                onValueChange={"toggleSwitch"}
                value={"isAvailable"}
                style={{ marginTop: /*Platform.OS === 'android' ? -15 :*/ -5 }}
              />
            </View>
          </View>            
        </View>
        <View style={{flex:1}}>
          <DrawerContentScrollView {...props}>           
            <TouchableOpacity
              style={styles.menulable}
              activeOpacity={0.8}
              onPress={() =>
                Linking.canOpenURL(PRODUCT_URL).then(() => {
                  Linking.openURL(PRODUCT_URL)
                })
              }>
              <View style={styles.menuicon}>
                <Icon
                  type="font-awesome"
                  color="white"
                  name="product-hunt"
                  size={26}
                />
              </View>
              <Text style={styles.menuitemtext}>
                Product page
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.menulable}
              activeOpacity={0.8}
              onPress={() =>
                Linking.canOpenURL(PRODUCT_URL).then(() => {
                  Linking.openURL(PRODUCT_URL)
                })
              }>
              <View style={styles.menuicon}>
                <Icon
                  type="font-awesome"
                  color="white"
                  name="lock"
                  size={26}
                />
              </View>
              <Text style={styles.menuitemtext}>
                Privacy policy
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.menulable}
              activeOpacity={0.8}
              onPress={() =>
                Linking.canOpenURL(PRODUCT_URL).then(() => {
                  Linking.openURL(PRODUCT_URL)
                })
              }>
              <View style={styles.menuicon}>
                <Icon
                  type="font-awesome"
                  color="white"
                  name="info-circle"
                  size={26}
                />
              </View>
              <Text style={styles.menuitemtext}>
                  About us
              </Text>
            </TouchableOpacity>   
          </DrawerContentScrollView> 
        </View> 
        <View style={{flex:1}}>           
            <TouchableOpacity
              style={styles.logout}
              activeOpacity={0.8}
              onPress={() =>
                Linking.canOpenURL(PRODUCT_URL).then(() => {
                  Linking.openURL(PRODUCT_URL)
                })
              }>
              <View style={styles.icon}>
                <Icon
                 type="entypo" color="white" name="log-out"
                  size={26}
                />
              </View>
              <Text style={styles.menutext}>
                Logout
              </Text>
            </TouchableOpacity>                
        </View>     
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  topContainer: {
    flex: 0.35,
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#000000bf',
    marginVertical:70
  },
  profileContainer: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'flex-start',
    width: '80%',
    alignItems: 'center',
  },
  avatar: {
    width: 100,
    height: 100,
    borderWidth: 6,
    borderColor: colors.white,
    borderRadius: 15,
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center'
  },
  item: {
    aspectRatio: 1,
    width: '100%',
    flex: 1
  },
  middleContainer: {
    flex: 0.5,
    backgroundColor: '#000000bf',
    height:180
  },
  status: {
    marginBottom: '5%',
    marginHorizontal: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center'
  },
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
  menulable: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
    marginHorizontal: '10%',
    justifyContent: 'flex-start',
    marginVertical: 10
  },
  menuicon: {
    width: '15%'
  },
  menuitemtext: {
    marginLeft: 20,
    color: colors.white,
    fontSize: 18,
    fontWeight: 'bold',
  },
  logout: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
    marginHorizontal: '10%',
    justifyContent: 'flex-start',
    marginVertical: '37%'
  },
  icon: {
    width: '15%'
  },
  menutext: {
    marginLeft: 20,
    color: colors.white,
    fontSize: 18,
    fontWeight: 'bold',
  },
});