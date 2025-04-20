<!-- src/views/HomeView.vue -->
<template>
    <div class="home">
      <main class="container mx-auto px-6">
      <h2 v-if="$route.path === '/'" class="text-2xl md:text-3xl font-bold text-gray-800 text-center leading-relaxed max-w-3xl mx-auto"> 
          Добро пожаловать в TireShop — сайт для удобного управления шиномонтажом. 
          Здесь вы можете добавлять и редактировать информацию о клиентах, автомобилях, создавать записи и отслеживать ход работы вашего предприятия. 
          Пожалуйста, выберите интересующий вас пункт выше.
      </h2>
    </main>
    </div>
  </template>