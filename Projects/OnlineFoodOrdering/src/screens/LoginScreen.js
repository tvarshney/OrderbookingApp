import { useNavigation } from '@react-navigation/native';
import { StatusBar } from 'expo-status-bar';
import { TextInput, Text, TouchableOpacity, View, Image, KeyboardAvoidingView, ScrollView, StyleSheet } from 'react-native';
import * as Icon from "react-native-feather";
import { SafeAreaView } from 'react-native-safe-area-context';
import { ApiURL } from '../../constants';
import React, { useEffect, useState } from 'react';

export default function LoginScreen() {
    const navigation = useNavigation();
    const [usersData, setUsersData] = useState([]);
    const [username, setUsername] = useState('test@gmail.com');
    const [password, setPassword] = useState('Ankit@123');
    //const Port = 'https://6fef-103-173-124-142.ngrok.io/';
    const getUsersUrl = ApiURL.host+ApiURL.getUsersList;

    useEffect(() => {
        fetch(getUsersUrl).then((result)=>{
            result.json().then((resp)=>{
              setUsersData(resp)
            })
        })
      }, []);
    const Login = () => {
      console.log(username, password, "click");
      // Check if usersData has been fetched and is not empty
      console.log(usersData);
      if (usersData && usersData.length > 0) {
        const user = usersData.find((user) => user.emailId === username);
    
        if (user && user.password === password) {
          const data = user;
          // Username and password are correct, navigate to the home screen
          navigation.navigate('Parent', {screen: 'Home', params:{userData: data}, });
        } else {
          // Username or password is incorrect, show an error message or handle it as needed
          console.log("Invalid username or password");
        }
      } else {
        // Users data not available, handle it as needed (e.g., show loading indicator)
        console.log("User data not available");
      }
    };
    const Register = () =>{
        navigation.navigate("Register")
    }
  return (
    <SafeAreaView
      edges={['bottom', 'left', 'right']}
      style={styles.safeAreaViewStyles}>
      <KeyboardAvoidingView
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        style={styles.flex}>       
        <ScrollView
          style={styles.flex}
          contentContainerStyle={{ flexGrow: 1 }}
          showsVerticalScrollIndicator={false}
          alwaysBounceVertical={false}>
          <View style={styles.mainContainer}>
            <View style={styles.subContainer}>
              <View style={styles.logoContainer}>
                <Image
                  source={require('../../assets/login-icon.png')}
                  style={styles.logoContainer}
                />
              </View>
              <View>
                <Text style={{
                    textAlign: 'center',
                    marginTop:20,
                    marginBottom:10,
                    fontSize:28,
                    fontWeight:'bold'
                    //...alignment.MBmedium
                  }}>
                    Login
                </Text>
                <Text style={{
                    textAlign: 'center',
                    color:'gray'
                    //...alignment.MBmedium
                  }}>
                    Please sign in to continue.
                </Text>
              </View>
              <View style={styles.form}>
                <View style={styles.textField}>
                  <View style={styles.inputContainer}>
                    <Icon.User height={25} width={25} stroke="gray" />
                    <TextInput
                      style={styles.input}
                      placeholder="Email"
                      placeholderTextColor="gray"
                      value={username}
                      onChangeText={(e) => setUsername(e)}
                    />
                  </View>
                </View>
                <View style={styles.textField}>
                  <View style={styles.inputContainer}>
                    <Icon.Lock height={25} width={25} stroke="gray" />
                    <TextInput
                      style={styles.input}
                      placeholder="Password"
                      placeholderTextColor="gray"
                      secureTextEntry={true} // To hide the password
                      value={password}
                      onChangeText={(p) => setPassword(p)}
                    />
                  </View>
                </View>
                <View style={styles.buttonContainer}>
                  <TouchableOpacity
                    onPress={Login}
                    activeOpacity={0.7}
                    style={styles.button}
                  >
                    <Text style={styles.buttonText}>Login</Text>
                    <Icon.ArrowRight height={25} width={25} stroke="white" />
                  </TouchableOpacity>
                </View>
                <View style={styles.linkContainer}>
                  <Text>Don't have an account?</Text>
                  <TouchableOpacity onPress={Register}>
                    <Text style={styles.linkText}>Register</Text>
                  </TouchableOpacity>
                </View>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}


const styles = StyleSheet.create({
  safeAreaViewStyles: {
    flex: 1,
    backgroundColor: 'transparent',
  },
  flex: {
    flex: 1,
  },
  mainContainer: {
    flex: 1,
    alignItems: 'center',
    backgroundColor: 'transparent',
    paddingTop: 90,
  },
  subContainer: {
    width: '85%',
    height: '100%',
  },
  logoContainer: {
    width: 120,
    height: 130,
    alignSelf: 'center',
  },
  form: {
    width: '100%',
    paddingTop: 50,
    height: '100%',
  },
  textField: {
    borderColor: '#efefef',
    borderWidth: 1,
    borderRadius: 10,
    backgroundColor: '#fff',
    height: 60,
    marginBottom: 15,
    padding: 14,
    shadowColor: '#545454',
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 5,
  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  input: {
    flex: 1,
    marginLeft: 10, // Adjust the margin as needed
    color: 'black', // Text color
  },
  buttonContainer: {
    marginTop: 20,
    alignItems: 'center',
  },
  button: {
    flex: 1,
    width: '33%',
    alignItems: 'center',
    backgroundColor: '#000',
    padding: 15,
    borderRadius: 10,
    flexDirection: 'row',
    justifyContent: 'center',
  },
  buttonText: {
    fontSize: 20,
    color: '#fff',
    marginRight: 10,
  },
  linkContainer: {
    alignItems: 'center',
    marginTop: 20,
  },
  linkText: {
    alignItems: 'center',
    marginTop: 10,
    fontWeight: 'bold',
  },
});
