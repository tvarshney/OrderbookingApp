import { StyleSheet, View, Text, TextInput, ScrollView, TouchableOpacity } from 'react-native';
import React, { useEffect, useState } from 'react';
import { SafeAreaView } from 'react-native-safe-area-context';
import { StatusBar } from 'expo-status-bar';
import * as Icon from "react-native-feather";
//import { ApiURL, heading, heading2 } from '../constants';
import FeaturedRow from '../../components/featuredRow';
import FeaturedColumn from '../../components/featuredColumn';
import { useNavigation, useRoute } from '@react-navigation/native';
import Categories from '../../components/categories';
import { ApiURL, heading, heading2 } from '../../constants';

export default function HomeScreen() { 
  const navigation = useNavigation();  
  const [restaurantData, setRestaurantData] = useState([]);
  //const Port = 'https://6fef-103-173-124-142.ngrok.io/';
  const getRestaurantListUrl = ApiURL.host+ApiURL.getRestaurantsList;
  // Inside your component
  const route = useRoute();
  console.log('Route Data:', route);
  const loginUser = route.params?.userData;
  console.log('Received Data:', loginUser);
  useEffect(() => {
      fetch(getRestaurantListUrl).then((result)=>{
          result.json().then((resp)=>{
              setRestaurantData(resp)
          })
      })
    }, []);
  console.log(restaurantData,"Set Data"); 
  return(  
    <ScrollView showsVerticalScrollIndicator={false}
              contentContainerStyle={{
                  paddingBottom: 20
              }}> 
        <SafeAreaView>
            <StatusBar barStyle="dark-content"/>                       
            {/* main */} 
          
              {/* search bar */}
                <View style={styles.searchBarContainer}>
                    <View style={styles.searchInputContainer}>
                    <Icon.Search height={25} width={25} stroke="black" />
                    <TextInput placeholder="Search for restaurants" style={styles.input} />
                    </View>
                </View>
              {/* categories */}
              <Categories/>              
              {/* featured */}
              <View className="mt-5">
                  { 
                      heading.map((item, index)=>{
                          return(
                              <FeaturedRow
                                  key={index}
                                  title={item.title}
                                  restaurants={restaurantData}
                                  description={item.description}
                              />
                          )
                      })                        
                  }
              </View>
              <View className="mt-5">
                  { 
                      heading2.map((item, index)=>{
                          return(
                              <FeaturedColumn
                                  key={index}
                                  title={item.title}
                                  restaurants={restaurantData}
                                  description={item.description}
                              />
                          )
                      })                        
                  }
              </View>                  
        </SafeAreaView>
      </ScrollView>
  )
}

const styles = StyleSheet.create({
    searchBarContainer: {
      backgroundColor: '#6FCF97',
      height: 100,
      borderBottomLeftRadius: 25,
      borderBottomRightRadius: 25,
      flexDirection: 'row',
      alignItems: 'center',
      paddingHorizontal: 10,
      justifyContent: 'space-between',
      marginTop:-30
    },
    searchInputContainer: {
      flex: 1,
      flexDirection: 'row',
      alignItems: 'center',
      padding: 10,
      borderRadius: 25,
      borderWidth: 1,
      borderColor: 'white',
      backgroundColor:'white'
    },
    input: {
      flex: 1,
      marginLeft: 10,
    },
  });
  
  
  