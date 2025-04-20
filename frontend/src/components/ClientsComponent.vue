<template>
  <div class="p-4">
    <h2 class="text-2xl font-bold mb-4">Клиенты</h2>
    <div v-if="isLoading" class="text-center">Загрузка...</div>
    <div v-else-if="clients.length === 0" class="text-red-500"> Нет записей для отображения. </div>
    <table v-else class="min-w-full bg-white shadow-md rounded-lg overflow-hidden">
      <thead class="bg-gray-200">
        <tr>
          <th class="px-4 py-2 text-left">Имя</th>
          <th class="px-4 py-2 text-left">Телефон</th>
          <th class="px-4 py-2 text-left">Email</th>
          <th class="px-4 py-2 text-left">Адрес</th>
          <th class="px-4 py-2 text-left">Автомобиль</th>
          <th class="px-4 py-2 text-left"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="client in clients" :key="client.id" class="border-t">
          <td class="px-4 py-2">{{ client.name }}</td>
          <td class="px-4 py-2">{{ client.phone }}</td>
          <td class="px-4 py-2">{{ client.email }}</td>
          <td class="px-4 py-2">{{ client.address }}</td>
          <td class="px-4 py-2">
            <ul>
              <li v-for="car in client.cars" :key="car.id">
                {{ car.brand + " " + car.model + " (" + car.numbers + ")" }}
              </li>
            </ul>
          </td>
          <td class="px-4 py-2 text-center">
            <button @click="editClient(client)" class="text-blue-600 hover:text-blue-800 mr-2" title="Редактировать">
              ✏️
            </button>
            <button @click="deleteClient(client.id)" class="text-red-600 hover:text-red-800" title="Удалить">
              🗑️
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <button @click="showAddForm" class="mt-4 w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600">
      Добавить клиента
    </button>
    <!-- Модальное окно -->
    <div v-if="showForm" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div class="bg-white p-6 rounded-lg shadow-lg w-full max-w-md mx-4 max-h-[90vh] overflow-y-auto">
        <h3 class="text-xl font-bold mb-4">{{ isEditing ? 'Редактирование клиента' : 'Добавление клиента' }}</h3>
        <div class="space-y-4">
          <!-- Поля клиента -->
          <div class="relative">
            <label for="name" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Имя</label>
            <input v-model="newClient.name" id="name" type="text" class="w-full p-2 border rounded" />
            <p v-if="errors.name" class="text-red-500 text-sm mt-1">{{ errors.name }}</p>
          </div>
          <div class="relative">
            <label for="phone" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Номер телефона</label>
            <input v-model="newClient.phone" id="phone" type="text" class="w-full p-2 border rounded" />
            <p v-if="errors.phone" class="text-red-500 text-sm mt-1">{{ errors.phone }}</p>
          </div>
          <div class="relative">
            <label for="email" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Email</label>
            <input v-model="newClient.email" id="email" type="email" class="w-full p-2 border rounded" />
            <p v-if="errors.email" class="text-red-500 text-sm mt-1">{{ errors.email }}</p>
          </div>
          <div class="relative">
            <label for="address" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Адрес</label>
            <input v-model="newClient.address" id="address" type="text" class="w-full p-2 border rounded" />
            <p v-if="errors.address" class="text-red-500 text-sm mt-1">{{ errors.address }}</p>
          </div>

          <!-- Раздел автомобиля -->
          <h3 class="text-lg font-bold mt-6 mb-2">Автомобиль</h3>
          <div class="space-y-4">
            <div class="relative">
              <label for="brand" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Бренд</label>
              <input v-model="newClient.cars[0].brand" id="brand" type="text" class="w-full p-2 border rounded" />
              <p v-if="errors.brand" class="text-red-500 text-sm mt-1">{{ errors.brand }}</p>
            </div>
            <div class="relative">
              <label for="model" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Модель</label>
              <input v-model="newClient.cars[0].model" id="model" type="text" class="w-full p-2 border rounded" />
              <p v-if="errors.model" class="text-red-500 text-sm mt-1">{{ errors.model }}</p>
            </div>
            <div class="relative">
              <label for="tireSize" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Размер шин</label>
              <input v-model="newClient.cars[0].tireSize" id="tireSize" type="number" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="year" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Год выпуска</label>
              <input v-model="newClient.cars[0].year" id="year" type="number" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="numbers" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Номера</label>
              <input v-model="newClient.cars[0].numbers" id="numbers" type="text" class="w-full p-2 border rounded" />
              <p v-if="errors.numbers" class="text-red-500 text-sm mt-1">{{ errors.numbers }}</p>
            </div>
          </div>
        </div>

        <button @click="saveClient" class="w-full mt-6 bg-green-500 text-white py-2 rounded-lg hover:bg-green-600">
          {{ isEditing ? 'Сохранить' : 'Добавить клиента' }}
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
  name: "ClientsComponent",
  data() {
    return {
      clients: [],
      showForm: false,
      isEditing: false,
      editingClientId: null,
      isLoading: true,
      newClient: {
        id: 0,
        name: "",
        phone: "",
        email: "",
        address: "",
        cars: [{
          id: 0,
          brand: "",
          model: "",
          tireSize: 0,
          year: 0,
          numbers: "",
          clientId: 0
        }]
      },
      errors: {
        name: "",
        phone: "",
        email: "",
        address: "",
        brand: "",
        model: "",
        numbers: ""
      }
    };
  },
  created() {
    this.fetchClients();
  },
  methods: {
    async fetchClients() {
      this.isLoading = true;
      try {
        const response = await axios.get(`${process.env.VUE_APP_API_URL}/Client/`);
        if (response.data.code === "0") {
          this.clients = response.data.responseData || [];
        } else {
          console.error('Ошибка API:', response.data.message);
          this.clients = [];
        }
      } catch (error) {
        console.error('Ошибка при загрузке клиентов:', error.message);
        this.clients = [];
      } finally {
        this.isLoading = false;
      }
    },
    showAddForm() {
      this.isEditing = false;
      this.editingClientId = null;
      this.newClient = {
        id: 0,
        name: "",
        phone: "",
        email: "",
        address: "",
        cars: [{
          id: 0,
          brand: "",
          model: "",
          tireSize: 0,
          year: 0,
          numbers: "",
          clientId: 0
        }]
      };
      this.errors = {
        name: "",
        phone: "",
        email: "",
        address: "",
        brand: "",
        model: "",
        numbers: ""
      };
      this.showForm = true;
    },
    editClient(client) {
      this.isEditing = true;
      this.editingClientId = client.id;
      this.newClient = {
        id: client.id,
        name: client.name || "",
        phone: client.phone || "",
        email: client.email || "",
        address: client.address || "",
        cars: client.cars.length > 0 ? [{
          id: client.cars[0].id || 0,
          brand: client.cars[0].brand || "",
          model: client.cars[0].model || "",
          tireSize: client.cars[0].tireSize || 0,
          year: client.cars[0].year || 0,
          numbers: client.cars[0].numbers || "",
          clientId: client.id
        }] : [{
          id: 0,
          brand: "",
          model: "",
          tireSize: 0,
          year: 0,
          numbers: "",
          clientId: client.id
        }]
      };
      this.errors = {
        name: "",
        phone: "",
        email: "",
        address: "",
        brand: "",
        model: "",
        numbers: ""
      };
      this.showForm = true;
    },
    validateForm() {
      let isValid = true;
      this.errors = {
        name: "",
        phone: "",
        email: "",
        address: "",
        brand: "",
        model: "",
        numbers: ""
      };

      // Проверка имени
      if (!this.newClient.name.trim()) {
        this.errors.name = "Имя обязательно для заполнения";
        isValid = false;
      }

      // Проверка телефона (только цифры, не пустое)
      const phoneRegex = /^[0-9]+$/;
      if (!this.newClient.phone.trim()) {
        this.errors.phone = "Номер телефона обязателен для заполнения";
        isValid = false;
      } else if (!phoneRegex.test(this.newClient.phone.trim())) {
        this.errors.phone = "Номер телефона должен содержать только цифры";
        isValid = false;
      }

      // Проверка email (не пустое, содержит @)
      if (!this.newClient.email.trim()) {
        this.errors.email = "Email обязателен для заполнения";
        isValid = false;
      } else if (!this.newClient.email.includes("@")) {
        this.errors.email = "Email должен содержать символ @";
        isValid = false;
      }

      // Проверка адреса
      if (!this.newClient.address.trim()) {
        this.errors.address = "Адрес обязателен для заполнения";
        isValid = false;
      }

      // Проверка автомобиля
      if (!this.newClient.cars[0].brand.trim()) {
        this.errors.brand = "Бренд автомобиля обязателен для заполнения";
        isValid = false;
      }

      if (!this.newClient.cars[0].model.trim()) {
        this.errors.model = "Модель автомобиля обязательна для заполнения";
        isValid = false;
      }

      if (!this.newClient.cars[0].numbers.trim()) {
        this.errors.numbers = "Номера автомобиля обязательны для заполнения";
        isValid = false;
      }

      return isValid;
    },
    async saveClient() {
      if (!this.validateForm()) {
        return;
      }

      try {
        let response;
        if (this.isEditing) {
          const originalClient = this.clients.find(client => client.id === this.editingClientId);
          const clientDataChanged = (
            this.newClient.name !== originalClient.name ||
            this.newClient.phone !== originalClient.phone ||
            this.newClient.email !== originalClient.email ||
            this.newClient.address !== originalClient.address
          );
          const carDataChanged = (
            this.newClient.cars[0].brand !== originalClient.cars[0].brand ||
            this.newClient.cars[0].model !== originalClient.cars[0].model ||
            this.newClient.cars[0].tireSize !== originalClient.cars[0].tireSize ||
            this.newClient.cars[0].year !== originalClient.cars[0].year ||
            this.newClient.cars[0].numbers !== originalClient.cars[0].numbers
          );
          const updatedClientData = {
            id: this.editingClientId,
            name: this.newClient.name,
            phone: this.newClient.phone,
            email: this.newClient.email,
            address: this.newClient.address,
            cars: originalClient.cars
          };
          const updatedCarData = {
            id: this.newClient.cars[0].id || 0,
            brand: this.newClient.cars[0].brand || "",
            model: this.newClient.cars[0].model || "",
            tireSize: this.newClient.cars[0].tireSize || 0,
            year: this.newClient.cars[0].year || 0,
            numbers: this.newClient.cars[0].numbers || "",
            clientId: this.editingClientId
          };
          if (clientDataChanged && carDataChanged) {
            await axios.put(`${process.env.VUE_APP_API_URL}/Client/${this.editingClientId}`, updatedClientData);
            await axios.put(`${process.env.VUE_APP_API_URL}/Car/${this.newClient.cars[0].id}`, updatedCarData);
          } else if (clientDataChanged) {
            await axios.put(`${process.env.VUE_APP_API_URL}/Client/${this.editingClientId}`, updatedClientData);
          } else if (carDataChanged) {
            await axios.put(`${process.env.VUE_APP_API_URL}/Car/${this.newClient.cars[0].id}`, updatedCarData);
          }
          const getResponse = await axios.get(`${process.env.VUE_APP_API_URL}/Client/${this.editingClientId}`);
          if (getResponse.data.code === "0") {
            const updatedClient = getResponse.data.responseData;
            const clientIndex = this.clients.findIndex(client => client.id === this.editingClientId);
            if (clientIndex !== -1) {
              this.clients[clientIndex] = { id: this.editingClientId, ...updatedClient };
              this.clients = [...this.clients];
            }
          } else {
            console.error('Ошибка при получении клиента после PUT:', getResponse.data.message);
          }
        } else {
          response = await axios.post(`${process.env.VUE_APP_API_URL}/Client/`, this.newClient);
          if (response.data.code === "201") {
            const newClient = response.data.responseData;
            if (newClient && newClient.id) {
              this.clients = [...this.clients, newClient];
            } else {
              console.error('Ответ API не содержит корректного объекта нового клиента с ID.');
            }
          } else {
            console.error('Ошибка API:', response.data.message);
          }
        }
        this.closeForm();
      } catch (error) {
        console.error('Ошибка при сохранении клиента:', error.message);
      }
    },
    async deleteClient(id) {
      try {
        const response = await axios.delete(`${process.env.VUE_APP_API_URL}/Client/${id}`);
        if (response.status === 204) {
          this.clients = this.clients.filter(client => client.id !== id);
        } else {
          console.error('Ошибка API:', response.status.error);
        }
      } catch (error) {
        console.error('Ошибка при удалении клиента:', error.message);
      }
    },
    closeForm() {
      this.showForm = false;
      this.isEditing = false;
      this.editingClientId = null;
      this.newClient = {
        id: 0,
        name: "",
        phone: "",
        email: "",
        address: "",
        cars: [{
          id: 0,
          brand: "",
          model: "",
          tireSize: 0,
          year: 0,
          numbers: "",
          clientId: 0
        }]
      };
      this.errors = {
        name: "",
        phone: "",
        email: "",
        address: "",
        brand: "",
        model: "",
        numbers: ""
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