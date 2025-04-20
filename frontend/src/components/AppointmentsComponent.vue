<template>
    <div class="p-4">
      <h2 class="text-xl font-bold mb-4">Список записей</h2>
      
      <div v-if="isLoading" class="text-gray-500">Загрузка...</div>
  
      <div v-else-if="appointments.length === 0" class="text-red-500">
        Нет записей для отображения.
      </div>
  
      <table v-else class="min-w-full bg-white shadow-md rounded-lg overflow-hidden">
        <thead class="bg-gray-200">
          <tr>
            <th class="px-4 py-2 text-left">Номер записи</th>
            <th class="px-4 py-2 text-left">Дата создания заявки</th>
            <th class="px-4 py-2 text-left">Статус</th>
            <th class="px-4 py-2 text-left">Автомобиль</th>
            <th class="px-4 py-2 text-left">Список услуг</th>
            <th class="px-4 py-2 text-left">Сотрудник</th>
            <th class="px-4 py-2 text-left">Должность</th>
            <th class="px-4 py-2 text-left"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="appointment in appointments" :key="appointment.id" class="border-t">
            <td class="px-4 py-2">{{ appointment.id }}</td>
            <td class="px-4 py-2">
              {{ formatDate(appointment.date) }}
            </td>
            <td class="px-4 py-2">{{ appointment.status }}</td>
            <td class="px-4 py-2">{{ appointment.car.brand + " " + appointment.car.model + " (" + appointment.car.numbers + ")" }}</td>
            <td class="px-4 py-2">
              <ul>
                <li v-for="service in appointment.serviceTypes" :key="service.id">
                  {{ service.name }}
                </li>
              </ul>
            </td>
            <td class="px-4 py-2">{{ appointment.employee?.name || '—' }}</td>
            <td class="px-4 py-2">{{ appointment.employee?.role || '—' }}</td>
            <td class="px-4 py-2 text-center">
              <button @click="editAppointment(appointment)" class="text-blue-600 hover:text-blue-800 mr-2" title="Редактировать">
                ✏️
              </button>
              <button @click="deleteAppointment(appointment.id)" class="text-red-600 hover:text-red-800" title="Удалить">
                🗑️
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <button @click="showAddForm" class="mt-4 w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600">
            Добавить запись
        </button>
        <div v-if="formVisible" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
  <div class="bg-white p-6 rounded-lg shadow-lg w-96">
    <h3 class="text-xl font-bold mb-4">{{ isEditing ? 'Редактирование записи' : 'Добавление записи' }}</h3>
    <div class="space-y-4">
      <div class="relative">
        <label for="car" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Автомобиль</label>
        <select id="car" v-model="newAppointment.car" class="w-full p-2 border rounded">
          <option value="" disabled>Выберите автомобиль</option>
          <option v-for="car in cars" :key="car.id" :value="car.id">{{ car.brand + " " + car.model + " " + car.numbers }}</option>
        </select>
      </div>

      <div class="relative">
        <label for="employee" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Сотрудник</label>
        <select id="employee" v-model="newAppointment.employee" class="w-full p-2 border rounded">
          <option value="" disabled>Выберите сотрудника</option>
          <option v-for="employee in employees" :key="employee.id" :value="employee.id">{{ employee.name + " - " + employee.role }}</option>
        </select>
      </div>

      <div class="relative">
        <label class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Услуги</label>
        <div class="w-full p-2 border rounded max-h-40 overflow-y-auto">
          <div v-for="service in services" :key="service.id" class="flex items-center mb-2">
            <input
              type="checkbox"
              :id="'service-' + service.id"
              :value="service.id"
              v-model="newAppointment.services"
              class="mr-2"
            />
            <label :for="'service-' + service.id" class="text-sm">{{ service.name }}</label>
          </div>
        </div>
      </div>

      <div class="relative">
        <label for="status" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Статус</label>
        <select id="status" v-model="newAppointment.status" class="w-full p-2 border rounded">
          <option value="" disabled>Выберите статус</option>
          <option v-for="status in statuses" :key="status.name" :value="status.name">{{ status.name }}</option>
        </select>
      </div>

      <button @click="saveAppointment" class="w-full mt-6 bg-green-500 text-white py-2 rounded-lg hover:bg-green-600">
        {{ isEditing ? 'Сохранить' : 'Добавить запись' }}
      </button>
      <button @click="closeForm" class="mt-2 w-full bg-gray-500 text-white py-2 rounded-lg hover:bg-gray-600">
        Отмена
      </button>
    </div>
  </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    name: 'AppointmentsTable',
    data() {
      return {
        appointments: [],
        isLoading: true,
        cars: [],
        employees: [],
        services: [],
        statuses: [
            { name: 'Новая' },
            { name: 'В работе' },
            { name: 'Завершена' }
        ],
        newAppointment: {
            status: '',
            car: '',
            employee: '',
            services: []
        },
        formVisible: false,
        isEditing: false,
        editingId: null
      };
    },
    created() {
      this.fetchAppointments();
    },
    methods: {
      async fetchAppointments() {
        this.isLoading = true;
        try {
          const response = await axios.get(`${process.env.VUE_APP_API_URL}/Appointment`);
          if (response.data.code === "0") {
            this.appointments = response.data.responseData || [];
          } else {
            console.error('Ошибка API:', response.data.message);
            this.appointments = [];
          }
        } catch (error) {
          console.error('Ошибка при получении записей:', error.message);
          this.appointments = [];
        } finally {
          this.isLoading = false;
        }
      },
      async showAddForm() {
        try {
          const [carResponse, employeeResponse, serviceResponse] = await Promise.all([
            axios.get(`${process.env.VUE_APP_API_URL}/Car`),
            axios.get(`${process.env.VUE_APP_API_URL}/Employee`),
            axios.get(`${process.env.VUE_APP_API_URL}/ServiceType`)
          ]);
  
          this.cars = carResponse.data.code === "0" ? carResponse.data.responseData || [] : [];
          this.employees = employeeResponse.data.code === "0" ? employeeResponse.data.responseData || [] : [];
          this.services = serviceResponse.data.code === "0" ? serviceResponse.data.responseData || [] : [];
  
          if (!this.cars.length || !this.employees.length || !this.services.length) {
            console.error('Не удалось загрузить данные для формы');
          }
        } catch (error) {
          console.error('Ошибка при загрузке данных:', error.message);
        }
        this.newAppointment = { status: '', car: '', employee: '', services: [] };
        this.isEditing = false;
        this.formVisible = true;
      },
      async editAppointment(appointment) {
      try {
        if (!this.cars.length || !this.employees.length || !this.services.length) {
          const [carResponse, employeeResponse, serviceResponse] = await Promise.all([
            axios.get(`${process.env.VUE_APP_API_URL}/Car`),
            axios.get(`${process.env.VUE_APP_API_URL}/Employee`),
            axios.get(`${process.env.VUE_APP_API_URL}/ServiceType`)
          ]);

          this.cars = carResponse.data.code === "0" ? carResponse.data.responseData || [] : [];
          this.employees = employeeResponse.data.code === "0" ? employeeResponse.data.responseData || [] : [];
          this.services = serviceResponse.data.code === "0" ? serviceResponse.data.responseData || [] : [];

          if (!this.cars.length || !this.employees.length || !this.services.length) {
            alert('Не удалось загрузить данные для формы');
            return;
          }
        }
        this.newAppointment = {
          car: appointment.car.id || '', 
          employee: appointment.employee.id || '',
          services: Array.isArray(appointment.serviceTypes) ? appointment.serviceTypes.map(s => s.id) : [],
          status: appointment.status || ''
        };
        this.editingId = appointment.id;
        this.isEditing = true;
        this.formVisible = true;
      } catch (error) {
        console.error('Ошибка при загрузке данных для редактирования:', error.message);
        alert('Ошибка при открытии формы редактирования');
      }
    },
    async saveAppointment() {
      try {
        const payload = {
          id: this.isEditing ? this.editingId : 0,
          status: this.newAppointment.status,
          employeeId: this.newAppointment.employee,
          carId: this.newAppointment.car,
          serviceTypeIds: this.newAppointment.services
        };
        if (this.isEditing) {
          await axios.put(`${process.env.VUE_APP_API_URL}/Appointment/${this.editingId}`, payload);
        } else {
          await axios.post(`${process.env.VUE_APP_API_URL}/Appointment`, payload);
        }
        this.closeForm();
        this.fetchAppointments();
      } catch (error) {
        console.error('Ошибка при сохранении записи:', error.message);
      }
    },
      async deleteAppointment(id) {
        try {
          const response = await axios.delete(`${process.env.VUE_APP_API_URL}/Appointment/${id}`);
          if (response.status === 204) {
            this.appointments = this.appointments.filter(ap => ap.id !== id);
          } else {
            console.error('Ошибка API:', response.statusText);
          }
        } catch (error) {
          console.error('Ошибка при удалении записи:', error.message);
        }
      },
      closeForm() {
        this.formVisible = false;
        this.isEditing = false;
        this.editingId = null;
        this.newAppointment = { car: '', employee: '', services: [] };
      },
      formatDate(dateStr) {
        const date = new Date(dateStr);
        return date.toLocaleDateString('ru-RU', {
          year: 'numeric',
          month: '2-digit',
          day: '2-digit',
          hour: '2-digit',
          minute: '2-digit'
        });
      }
    }
  };
  </script>
  
  <style scoped>
  table th,
  table td {
    border-bottom: 1px solid #e5e7eb;
  }
  select[multiple] {
    height: 100px;
  }
  </style>