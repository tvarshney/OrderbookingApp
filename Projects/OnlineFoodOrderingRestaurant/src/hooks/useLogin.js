import { useState, useRef, useEffect } from 'react'
//import { useMutation, gql } from '@apollo/client'
//import { FlashMessage } from '../../components'
import { ApiURL } from '../context/Api'
import { useNavigation } from '@react-navigation/native'
//import { login } from '../apollo'
//import {AuthContext} from '../../src/hooks/auth'

export default function useLogin() {
  const [errors, setErrors] = useState()
  //const { login } = useContext(AuthContext)
  const [username, setUserName] = useState('Bakeshop')
  const [password, setPassword] = useState('12345')
  const usernameRef = useRef()
  const passwordRef = useRef()
  const navigation = useNavigation();
  const [restaurantUsersData, setRestaurantUsersData] = useState([]);
  const getUsersListUrl = ApiURL.host+ApiURL.getUsersListUrl;
  useEffect(() => {
    fetch(getUsersListUrl).then((result)=>{
        result.json().then((resp)=>{
            setRestaurantUsersData(resp)
        })
    })
  }, []);
  
  const onLogin = () => {
    // Check if username and password are empty
    if (!username || !password) {
      setErrors('Please enter both username and password.');
      return;
    }

    // Find a user with the entered username
    const user = restaurantUsersData.find(user => user.emailId.toLowerCase() === username.toLowerCase());

    if (user) {
      // Check if the password matches
      if (user.password === password) {
        // Successful login        
        setUserName(username);
        setPassword(password);
        alert('Login successful!');
        navigation.navigate('Home Screen');
        // You can redirect the user to another page or perform other actions here
      } else {
        setErrors('Incorrect password.');
        alert('Incorrect password.');
      }
    } else {
      setErrors('User not found.');
      alert('User not found.');
    }
  };
  /*const [mutate, { loading, error }] = useMutation(
    gql`
      ${loginQuery}
    `,
    { onCompleted, onError }
  )*/

  /*function onCompleted({ restaurantLogin }) {
    login(restaurantLogin.token, restaurantLogin.restaurantId)
  }*/

  /*function onError(error) {
    FlashMessage({ message: error ? error.message : null })
  }*/

  /*const onLogin = async () => {
    if (isValid()) {
      // const username = await usernameRef.current.value
      // const password = await passwordRef.current.value
      mutate({ variables: { username, password } })
    }
  }*/
  return {
    onLogin,
    //isValid,
    //loading,
    errors,
    //error,
    usernameRef,
    passwordRef,
    setPassword,
    setUserName,
    username,
    password
  }
}
