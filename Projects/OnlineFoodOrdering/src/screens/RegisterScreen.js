import React, { useState } from 'react';
import { useNavigation } from '@react-navigation/native';
import { StatusBar } from 'expo-status-bar';
import { TextInput, Text, TouchableOpacity, View, Image, KeyboardAvoidingView, ScrollView, StyleSheet } from 'react-native';
import * as Icon from "react-native-feather";
import { SafeAreaView } from 'react-native-safe-area-context';
import { CountryPicker } from "react-native-country-codes-picker";

export default function RegisterScreen() {
  const [show, setShow] = useState(false);
  const [countryCode, setCountryCode] = useState('+91');

  return (
    <SafeAreaView
      edges={['bottom', 'left', 'right']}
      style={styles.safeAreaViewStyles}
    >
      <KeyboardAvoidingView
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        style={styles.flex}
      >
        <ScrollView
          style={styles.flex}
          contentContainerStyle={styles.scrollViewContent}
          showsVerticalScrollIndicator={false}
          alwaysBounceVertical={false}
        >
          <View style={styles.mainContainer}>
            <View style={styles.subContainer}>
              <View style={styles.logoContainer}>
                <Image
                  source={require('../../assets/login-icon.png')}
                  style={styles.logoImage}
                />
              </View>
              <View>
                <Text style={styles.heading}>
                  Let's get started!
                </Text>
                <Text style={styles.subHeading}>
                  Create your Account to continue.
                </Text>
              </View>
              <View style={styles.form}>
                <View style={styles.inputContainer}>
                  <Icon.User height={25} width={25} stroke="gray" />
                  <TextInput
                    style={styles.input}
                    placeholder="First name"
                    placeholderTextColor="gray"
                    onChangeText={() => '#'}
                  />
                </View>
                <View style={styles.inputContainer}>
                  <Icon.User height={25} width={25} stroke="gray" />
                  <TextInput
                    style={styles.input}
                    placeholder="Last name"
                    placeholderTextColor="gray"
                    onChangeText={() => '#'}
                  />
                </View>
                <View style={styles.inputContainer}>
                  <Icon.Mail height={25} width={25} stroke="gray" />
                  <TextInput
                    style={styles.input}
                    placeholder="Email"
                    placeholderTextColor="gray"
                    onChangeText={() => '#'}
                  />
                </View>
                <View style={styles.inputContainer}>
                  <Icon.Lock height={25} width={25} stroke="gray" />
                  <TextInput
                    style={styles.input}
                    placeholder="Password"
                    placeholderTextColor="gray"
                    onChangeText={() => '#'}
                  />
                  <Icon.Eye height={25} width={25} stroke="#6FCF97" />
                </View>
                <View style={styles.phoneInputContainer}>
                  <TouchableOpacity
                    onPress={() => setShow(true)}
                    style={styles.countryButton}
                  >
                    <Icon.ChevronDown height={25} width={25} stroke="gray" />
                    <Text style={styles.countryButtonText}>
                      {countryCode}
                    </Text>
                  </TouchableOpacity>
                  <CountryPicker
                    show={show}
                    pickerButtonOnPress={(item) => {
                      setCountryCode(item.dial_code);
                      setShow(false);
                    }}
                  />
                  <View style={styles.inputContainer}>
                    <Icon.Phone height={25} width={25} stroke="gray" />
                    <TextInput
                      style={{flex: 1,
                        marginLeft: 2,}}
                      placeholder="Phone"
                      placeholderTextColor="gray"
                      onChangeText={() => '#'}
                    />
                  </View>                  
                </View>
                <TouchableOpacity
                  onPress={"Login"}
                  activeOpacity={0.7}
                  style={styles.registerButton}
                >
                  <Text style={styles.registerButtonText}>
                    Register
                  </Text>
                  <Icon.ArrowRight height={25} width={25} stroke="white" />
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  flex: {
    flex: 1,
  },
  safeAreaViewStyles: {
    flex: 1,
    backgroundColor: 'transparent',
  },
  header: {
    backgroundColor: '#fff',
    height: 80,
    alignItems: 'flex-start',
    width: "100%",
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 20,
  },
  headerIcons: {
    width: '50%',
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  scrollViewContent: {
    flexGrow: 1,
  },
  mainContainer: {
    flex: 1,
    alignItems: 'center',
    marginTop:20,
    marginBottom:20,
  },
  subContainer: {
    width: '85%',
    height: '100%',
  },
  logoContainer: {
    width: 120,
    height: 130,
    alignSelf: 'center',
    marginTop: 10,
  },
  logoImage: {
    width: '100%',
    height: '100%',
  },
  heading: {
    textAlign: 'center',
    marginTop: 20,
    marginBottom: 10,
    fontSize: 28,
    fontWeight: 'bold',
    color: 'black',
  },
  subHeading: {
    textAlign: 'center',
    color: 'gray',
  },
  form: {
    width: '100%',
    paddingTop: 50,
  },
  inputContainer: {
    flex: 1,
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
    flexDirection: 'row',
    alignItems: 'center',
  },
  input: {
    flex: 1,
    marginLeft: 2,
  },
  phoneInputContainer: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  countryButton: {
    flexDirection:'row',
    borderColor: '#efefef',
    borderWidth: 1,
    borderRadius: 10,
    backgroundColor: '#fff',
    height: 60,
    //width: '20%',
    marginBottom: 15,
    marginRight: 5,
    alignItems: 'center',
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
  countryButtonText: {
    color: 'black',
    fontSize: 16,
    marginTop: 5,
  },
  registerButton: {
    flex: 1,
    flexDirection: 'row',
    width: '40%',
    alignItems: 'center',
    backgroundColor: '#000',
    padding: 15,
    borderRadius: 10,
    alignSelf: 'center',
    marginTop: 20,
  },
  registerButtonText: {
    fontSize: 20,
    color: '#fff',
    marginRight: 10,
  },
});
