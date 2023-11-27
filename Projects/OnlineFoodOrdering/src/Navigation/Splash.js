import { StatusBar } from 'expo-status-bar';
import { useEffect } from 'react';
import { StyleSheet, Text, View } from 'react-native';

export default function Splash({ navigation }) {
    useEffect(() => {
        setTimeout(() => {
            navigation.navigate('Login');
        }, 2000);
    }, []);
    return (
        <View>
            <Text>Splash</Text>
            <StatusBar style="auto" />
        </View>
    );
}