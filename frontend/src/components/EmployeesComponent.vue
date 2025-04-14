<template>
    <div class="p-4">
      <h2 class="text-2xl font-bold mb-4">Список сотрудников</h2>
      <div v-if="isLoading" class="text-center">Загрузка...</div>
      <ul v-else class="bg-white shadow-md rounded-lg p-4">
        <li class="p-2 border-b font-bold flex justify-between items-center">
            <div class="flex flex-1">
              <span class="w-1/3">Имя</span>
              <span class="w-1/3">Должность</span>
              <span class="w-1/6">Телефон</span>
              <span class="w-1/3">Email</span>
            </div>
          </li>
        <li v-for="employee in employees" :key="employee.id" class="p-2 border-b last:border-b-0 flex justify-between items-center">
          <div class="flex flex-1">
            <span class="w-1/3">{{ employee.name }}</span>
            <span class="w-1/2">{{ employee.phone }}</span>
            <span class="w-1/4">{{ employee.email }}</span>
            <span class="w-1/3">{{ employee.role }}</span>
          </div>
          <div class="space-x-2">
            <button @click="editEmployee(employee)" class="text-blue-500 hover:text-blue-700">
              ✏️
            </button>
            <button @click="deleteEmployee(employee.id)" class="text-red-500 hover:text-red-700">
              🗑️
            </button>
          </div>
        </li>
      </ul>
      <button @click="showAddForm" class="mt-4 w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600">
        Добавить сотрудника
      </button>
      <!-- Модальное окно -->
      <div v-if="showForm" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
        <div class="bg-white p-6 rounded-lg shadow-lg w-96">
          <h3 class="text-xl font-bold mb-4">{{ isEditing ? 'Редактирование услуги' : 'Добавление услуги' }}</h3>
          <div class="space-y-4">
            <div class="relative">
              <label for="name" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Имя</label>
              <input v-model="newEmployee.name" id="name" type="text" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="phone" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Телефон</label>
              <input v-model="newEmployee.phone" id="phone" type="phone" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="email" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Email</label>
              <input v-model="newEmployee.email" id="email" type="email" class="w-full p-2 border rounded" />
            </div>
            <div class="relative">
              <label for="role" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Должность</label>
              <input v-model="newEmployee.role" id="role" type="text" class="w-full p-2 border rounded" />
            </div>
            </div>
          <button @click="saveEmployee" class="w-full mt-6 bg-green-500 text-white py-2 rounded-lg hover:bg-green-600">
            {{ isEditing ? 'Сохранить' : 'Добавить сотрудника' }}
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
    name: "EmployeesComponent",
    data() {
      return {
        employees: [],
        showForm: false,
        isEditing: false,
        editingEmployeeId: null,
        isLoading: true,
        newEmployee: {
          id: 0,
          name: "",
          phone: "",
          email: "",
          role: ""
        }
      };
    },
    created() {
      this.fetchEmployees();
    },
    methods: {
      async fetchEmployees() {
        this.isLoading = true;
        try {
          console.log('Отправка get запроса');
          const response = await axios.get('https://localhost:7288/api/Employee');
          if (response.data.code === "0") {
            this.employees = response.data.responseData || [];
          } else {
            console.error('Ошибка API:', response.data.message);
            this.employees = [];
          }
        } catch (error) {
          console.error('Ошибка при загрузке клиентов:', error.message);
          this.employees = [];
        } finally {
          this.isLoading = false;
        }
      },
      showAddForm() {
        this.isEditing = false;
        this.editingEmployeeId = null;
        this.newEmployee = {
          id: 0,
          name: "",
          phone: "",
          email: "",
          role: ""
        };
        this.showForm = true;
      },
      editEmployee(employee) {
        this.isEditing = true;
        this.editingEmployeeId = employee.id;
        this.newEmployee = {
          id: employee.id || "",
          name: employee.name || "",
          phone: employee.phone || "",
          email: employee.email || "",
          role: employee.role || ""
        };
        this.showForm = true;
      },
      async saveEmployee() {
        try {
          let response;
          if (this.isEditing) {
            const originalEmployee = this.employees.find(employee => employee.id === this.editingEmployeeId);
            const employeeDataChanged = (
                this.newEmployee.name !== originalEmployee.name ||
                this.newEmployee.phone !== originalEmployee.phone ||
                this.newEmployee.email !== originalEmployee.email ||
                this.newEmployee.role !== originalEmployee.role
              );
              const updatedEmployeeData = {
                id: this.editingEmployeeId,
                name: this.newEmployee.name,
                phone: this.newEmployee.phone,
                email: this.newEmployee.email,
                role: this.newEmployee.role
              };
            if(employeeDataChanged) {
                console.log('pyt');
                await axios.put(`https://localhost:7288/api/Employee/${this.editingEmployeeId}`, updatedEmployeeData)
            }
            const getResponse = await axios.get(`https://localhost:7288/api/Employee/${this.editingEmployeeId}`);
              console.log(getResponse);
              if (getResponse.data.code === "0") {
                const updatedEmployee = getResponse.data.responseData;
                const employeeIndex = this.employees.findIndex(employee => employee.id === this.editingEmployeeId);
                if (employeeIndex !== -1) {
                  this.employees[employeeIndex] = { id: this.editingEmployeeId, ...updatedEmployee };
                  this.employees = [...this.employees];
                }
              } else {
                console.error('Ошибка при получении клиента после PUT:', getResponse.data.message);
              }
          } else {
            response = await axios.post('https://localhost:7288/api/Employee/', this.newEmployee);
            if (response.data.code === "201") {
              const newEmployee = response.data.responseData;
              console.log('Получен новая услуга от API:', newEmployee);
              if (newEmployee && newEmployee.id) {
                this.employees = [...this.employees, newEmployee];
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
      async deleteEmployee(id) {
        try {
          console.log(id);
          const response = await axios.delete(`https://localhost:7288/api/Employee/${id}`);
          console.log(response.status);
          if (response.status === 204) {
            this.employees = this.employees.filter(employee => employee.id !== id);
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
        this.editingEmployeeId = null;
        this.newEmployee = {
          id: 0,
          name: "",
          phone: "",
          email: "",
          role: ""
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