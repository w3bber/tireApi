<template>
    <div class="p-4">
      <h2 class="text-2xl font-bold mb-4">Каталог услуг</h2>
      <div v-if="isLoading" class="text-center">Загрузка...</div>
      <ul v-else class="bg-white shadow-md rounded-lg p-4">
        <li class="p-2 border-b font-bold flex justify-between items-center">
            <div class="flex flex-1">
              <span class="w-1/3">Название</span>
              <span class="w-1/3">Описание</span>
              <span class="w-1/6">Цена</span>
              <span class="w-1/3">Длительность (мин)</span>
            </div>
          </li>
        <li v-for="service in services" :key="service.id" class="p-2 border-b last:border-b-0 flex justify-between items-center">
          <div class="flex flex-1">
            <span class="w-1/3">{{ service.name }}</span>
            <span class="w-1/2">{{ service.description }}</span>
            <span class="w-1/4">{{ service.price }}</span>
            <span class="w-1/3">{{ service.duration }}</span>
          </div>
          <div class="space-x-2">
            <button @click="editService(service)" class="text-blue-500 hover:text-blue-700">
              ✏️
            </button>
            <button @click="deleteService(service.id)" class="text-red-500 hover:text-red-700">
              🗑️
            </button>
          </div>
        </li>
      </ul>
      <button @click="showAddForm" class="mt-4 w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600">
        Добавить услугу
      </button>
      <!-- Модальное окно -->
      <div v-if="showForm" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
        <div class="bg-white p-6 rounded-lg shadow-lg w-96">
          <h3 class="text-xl font-bold mb-4">{{ isEditing ? 'Редактирование услуги' : 'Добавление услуги' }}</h3>
          <div class="space-y-4">
            <!-- Поля услуги -->
            <div class="relative">
              <label for="name" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Название</label>
              <input v-model="newService.name" id="name" type="text" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="description" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Краткое описание</label>
              <input v-model="newService.description" id="description" type="text" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="price" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Стоимость</label>
              <input v-model="newService.price" id="price" type="number" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="duration" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Длительность выполнения в минутах</label>
              <input v-model="newService.duration" id="duration" type="number" class="w-full p-2 border rounded" />
            </div>
            </div>
          <button @click="saveService" class="w-full mt-6 bg-green-500 text-white py-2 rounded-lg hover:bg-green-600">
            {{ isEditing ? 'Сохранить' : 'Добавить услугу' }}
          </button>
          <button @click="closeForm" class="mt-2 w-full bg-gray-500 text-white py-2 rounded-lg hover:bg-gray-600">
            Отмена
          </button>
        </div>
      </div>
    </div>
</template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: "ServicesComponent",
    data() {
      return {
        services: [],
        showForm: false,
        isEditing: false,
        editingServiceId: null,
        isLoading: true,
        newService: {
          id: 0,
          name: "",
          description: "",
          price: 0,
          duration: 0
        }
      };
    },
    created() {
      this.fetchServices();
    },
    methods: {
      async fetchServices() {
        this.isLoading = true;
        try {
          console.log('Отправка get запроса');
          const response = await axios.get('https://localhost:7288/api/ServiceType');
          if (response.data.code === "0") {
            this.services = response.data.responseData || [];
          } else {
            console.error('Ошибка API:', response.data.message);
            this.services = [];
          }
        } catch (error) {
          console.error('Ошибка при загрузке клиентов:', error.message);
          this.services = [];
        } finally {
          this.isLoading = false;
        }
      },
      showAddForm() {
        this.isEditing = false;
        this.editingServiceId = null;
        this.newService = {
          id: 0,
          name: "",
          description: "",
          price: 0,
          duration: 0
        };
        this.showForm = true;
      },
      editService(service) {
        this.isEditing = true;
        this.editingServiceId = service.id;
        this.newService = {
          id: service.id || "",
          name: service.name || "",
          description: service.description || "",
          price: service.price || "",
          duration: service.duration || ""
        };
        this.showForm = true;
      },
      async saveService() {
        try {
          let response;
          if (this.isEditing) {
            const originalService = this.services.find(service => service.id === this.editingServiceId);
            const serviceDataChanged = (
                this.newService.name !== originalService.name ||
                this.newService.description !== originalService.description ||
                this.newService.price !== originalService.price ||
                this.newService.duration !== originalService.duration
              );
              const updatedServiceData = {
                id: this.editingServiceId,
                name: this.newService.name,
                description: this.newService.description,
                price: this.newService.price,
                duration: this.newService.duration
              };
            if(serviceDataChanged) {
                console.log('pyt');
                await axios.put(`https://localhost:7288/api/ServiceType/${this.editingServiceId}`, updatedServiceData)
            }
            const getResponse = await axios.get(`https://localhost:7288/api/ServiceType/${this.editingServiceId}`);
              if (getResponse.data.code === "0") {
                const updatedService = getResponse.data.responseData;
                const serviceIndex = this.services.findIndex(service => service.id === this.editingServiceId);
                if (serviceIndex !== -1) {
                  this.services[serviceIndex] = { id: this.editingServiceId, ...updatedService };
                  this.services = [...this.services];
                }
              } else {
                console.error('Ошибка при получении клиента после PUT:', getResponse.data.message);
              }
          } else {
            response = await axios.post('https://localhost:7288/api/ServiceType/', this.newService);
            if (response.data.code === "201") {
              const newService = response.data.responseData;
              console.log('Получен новая услуга от API:', newService);
              if (newService && newService.id) {
                this.services = [...this.services, newService];
              } else {
                console.error('Ответ API не содержит корректного объекта нового клиента с ID.');
              }
            } else {
              console.error('Ошибка API:', response.data.message);
            }
          }
          this.closeForm();
        } catch (error) {
          console.error('Ошибка при сохранении услуги:', error.message);
        }
      },
      async deleteService(id) {
        try {
          console.log(id);
          const response = await axios.delete(`https://localhost:7288/api/ServiceType/${id}`);
          console.log(response.status);
          if (response.status === 204) {
            this.services = this.services.filter(service => service.id !== id);
          } else {
            console.error('Ошибка API:', response.status.error);
          }
        } catch (error) {
          console.error('Ошибка при удалении услуги:', error.message);
        }
      },
      closeForm() {
        this.showForm = false;
        this.isEditing = false;
        this.editingServiceId = null;
        this.newService = {
          id: 0,
          name: "",
          description: "",
          price: 0,
          duration: 0
        };
      }
    }
  };
  </script>
  
  <style scoped>
  /* Стили для "включения" меток в рамку */
  input:focus {
    outline: none;
    border-color: #3b82f6; /* Цвет границы при фокусе (синий) */
    box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.5); /* Тень при фокусе */
  }
  
  input {
    transition: border-color 0.2s ease-in-out;
  }
  </style>