<template>
    <header class="bg-white shadow-md">
      <div class="relative container mx-auto flex justify-between items-center py-4 px-6">
        <!-- Логотип -->
        <router-link to="/">
          <h1 class="text-xl font-bold text-blue-600">Tire-Shop</h1>
        </router-link>
        
  
        <!-- Навигация -->
        <nav class="absolute left-1/2 transform -translate-x-1/2 space-x-6">
          <router-link to="/clients" class="text-gray-700 hover:text-blue-500">Клиенты</router-link>
          <router-link to="/appointments" class="text-gray-700 hover:text-blue-500">Записи</router-link>
          <router-link to="/services" class="text-gray-700 hover:text-blue-500">Услуги</router-link>
          <router-link to="/employees" class="text-gray-700 hover:text-blue-500">Сотрудники</router-link>
        </nav>
        <!-- Кнопки входа -->
        <!-- <div class="space-x-2">
          <button class="px-4 py-2 bg-gray-600 text-white rounded-md hover:bg-gray-700">
            Sign In
          </button>
          <button class="px-4 py-2 bg-green-600 text-white rounded-md hover:bg-green-700">
            Register
          </button>
        </div> -->
      </div>
    </header>
  </template>
  
  <script>
  export default {
    name: "HeaderComponent",
  };

  </script>
  
  <style scoped>
  </style>