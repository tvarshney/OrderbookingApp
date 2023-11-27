import { StyleSheet, View, Text, ScrollView, TouchableOpacity, Image } from 'react-native'
import React, { useState, useEffect } from 'react'
//import { getCategories } from '../api'
//import { urlFor } from '../sanity';
//import { themeColors } from '../theme';
import { categories } from '../constants';

export default function Categories() {
    const [activeCategory, setActiveCategory] = useState(null);
    return(
        <View style={styles.categoriesContainer}>
            <ScrollView
                horizontal
                showsHorizontalScrollIndicator={false}
                contentContainerStyle={{
                paddingHorizontal: 15,
                }}>
                {categories.map((category, index) => {
                const isActive = category.id == activeCategory;
                const btnStyle = isActive
                    ? [styles.categoryButton, styles.categoryButtonActive]
                    : [styles.categoryButton, styles.categoryButtonInactive];
                const textClass = isActive
                    ? [styles.categoryName, styles.categoryNameActive]
                    : [styles.categoryName, styles.categoryNameInactive];

                return (
                    <View key={index} style={{ alignItems: 'center', marginRight: 6 }}>
                    <TouchableOpacity
                        onPress={() => setActiveCategory(category.id)}
                        style={btnStyle}>
                        <Image style={styles.categoryImage} source={category.image} />
                    </TouchableOpacity>
                    <Text style={textClass}>{category.name}</Text>
                    </View>
                );
                })}
            </ScrollView>
        </View>
    )
}

const styles = StyleSheet.create({
    categoriesContainer: {
      marginTop: 4,
    },
    categoryButton: {
      padding: 10,
      borderRadius: 100,
      shadowColor: '#000',
      shadowOffset: { width: 0, height: 2 },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
      elevation: 5,
      marginRight: 6,
    },
    categoryButtonActive: {
      backgroundColor: '#666',
    },
    categoryButtonInactive: {
      backgroundColor: '#ccc',
    },
    categoryName: {
      fontSize: 14,
      fontWeight: '500',
    },
    categoryNameActive: {
      color: '#333',
    },
    categoryNameInactive: {
      color: '#777',
    },
    categoryImage: {
      width: 45,
      height: 45,
    },
  })