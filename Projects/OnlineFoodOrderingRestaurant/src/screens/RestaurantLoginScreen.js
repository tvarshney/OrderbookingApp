import React, { useState } from "react";
import { 
    KeyboardAvoidingView,
    ActivityIndicator,
    ScrollView,
    Dimensions,
    StyleSheet, 
    View,
    Text } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { colors } from "../utilities/colors";
import { Image, Input, Icon, Button } from "react-native-elements";
import useLogin from "../hooks/useLogin";

const { height } = Dimensions.get('window')
export default function RestaurantLoginScreen(props) {
  const {
    onLogin,
    //isValid,
    //loading,
    errors,
    setPassword,
    setUserName,
    username,
    password
  } = useLogin()
  const navigation = useNavigation();
  const [showPassword, setShowPassword] = useState(false)

  
  return (
    <KeyboardAvoidingView>
      <ScrollView
        bounces={false}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={{
        height: Platform.OS === 'ios' ? height * 1.0 : height * 1.05
      }}>
        <View style={{ flex: 1, backgroundColor: colors.white }}>
          <View style={styles.topContainer}>
            <View>
              <Image
                source={require('../../assets/images/Header.png')}
                PlaceholderContent={<ActivityIndicator />}
                style={{ width: 150, height: 140 }}
              />
            </View>
          </View>
          <View style={styles.lowerContainer}>
            <View style={styles.headingText}>
              <Text style={{ fontSize: 20, fontWeight: 'bold' }}>
                Sign in with your email
              </Text>
            </View>
            <View style={{ flex: 0.5, alignSelf: 'center' }}>
              <Input
                placeholder="Email"
                onChangeText={text => setUserName(text)}
                inputContainerStyle={styles.inputStyle}
                errorMessage={
                  errors && errors.username ? errors.username.join(',') : null
                }
                //onBlur={isValid}
                autoCapitalize="none"
                errorStyle={{ color: colors.textErrorColor }}
                keyboardType="email-address"
                defaultValue={username}
                returnKeyType="next"
                style={{ paddingLeft: 8 }}
              />
              <Input
                placeholder="Password"
                onChangeText={text => setPassword(text)}
                inputContainerStyle={styles.inputStyle}
                returnKeyType="go"
                style={{ paddingLeft: 8 }}
                containerStyle={{ marginTop: 15 }}
                secureTextEntry={!showPassword}
                rightIconContainerStyle={{ marginRight: 10 }}
                errorMessage={
                  errors && errors.password ? errors.password.join(',') : null
                }
                //onBlur={isValid}
                autoCapitalize="none"
                errorStyle={{ color: colors.textErrorColor }}
                defaultValue={password}
                rightIcon={
                  <Icon
                    onPress={() => setShowPassword(!showPassword)}
                    type="font-awesome"
                    color="gray"                    
                    name={showPassword ? 'eye' : 'eye-slash'}
                    size={20}
                  />
                }
              />
            </View>
            <View  style={{
                justifyContent: 'flex-start',
                alignItems: 'center',
                flex: 0.2
              }}>
                <Button
                  title="Sign in"
                  //disabled={loading}
                  onPress={onLogin}
                  buttonStyle={{
                    backgroundColor: '#FFFF',
                    borderColor: 'transparent',
                    borderWidth: 0,
                    borderRadius: 5,
                    width: 250,
                    height: 50
                  }}
                  style={{
                    shadowColor: '#000',
                    shadowOffset: {
                      width: 0,
                      height: 1
                    },
                    shadowOpacity: 0.22,
                    shadowRadius: 2.22,

                    elevation: 3
                  }}
                  titleStyle={{ color: 'black' }}>
                  {/*{loading ? (
                    <Spinner spinnerColor={colors.buttonText} />
                  ) : (
                    <Text textColor={colors.buttonText} H3 bold>
                      Login
                    </Text>
                  )}*/}
                  <Text textColor={colors.buttonText} H3 bold>
                      Login
                  </Text>
                </Button>
              </View>
            </View>
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
}

const styles = StyleSheet.create({
    flex: {
        flex: 1
      },
      scrollContainer: {
        justifyContent: 'center'
      },
      topContainer: {
        backgroundColor: colors.white,
        flex: 3,
        justifyContent: 'center',
        alignItems: 'center'
      },
      bgColor: {
        backgroundColor: colors.white
      },
      lowerContainer: {
        backgroundColor: colors.green,
        flex: 7,
        borderTopLeftRadius: 25,
        borderTopRightRadius: 25,
        justifyContent: 'space-around'
      },
      headingText: {
        flex: 0.2,
        justifyContent: 'center',
        alignItems: 'center'
      },
      inputStyle: {
        borderBottomWidth: 0,
        borderRadius: 8,
        height: 50,
        width: '90%',
        backgroundColor: colors.white,
        shadowColor: '#000',
        shadowOffset: {
          width: 0,
          height: 1
        },
        shadowOpacity: 0.22,
        shadowRadius: 2.22,
    
        elevation: 3
      }
    })