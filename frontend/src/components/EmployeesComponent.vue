<template>
  <div class="p-4">
    <h2 class="text-2xl font-bold mb-4">Список сотрудников</h2>
    <div v-if="isLoading" class="text-center">Загрузка...</div>
    <div v-else-if="employees.length === 0" class="text-red-500"> Нет записей для отображения. </div>
    <table v-else class="min-w-full bg-white shadow-md rounded-lg overflow-hidden">
      <thead class="bg-gray-200">
        <tr>
          <th class="px-4 py-2 text-left">Имя</th>
          <th class="px-4 py-2 text-left">Должность</th>
          <th class="px-4 py-2 text-left">Телефон</th>
          <th class="px-4 py-2 text-left">Email</th>
          <th class="px-4 py-2 text-left"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="employee in employees" :key="employee.id" class="border-t">
          <td class="px-4 py-2">{{ employee.name }}</td>
          <td class="px-4 py-2">{{ employee.role }}</td>
          <td class="px-4 py-2">{{ employee.phone }}</td>
          <td class="px-4 py-2">{{ employee.email }}</td>
          <td class="px-4 py-2 text-center">
            <button @click="editEmployee(employee)" class="text-blue-600 hover:text-blue-800 mr-2" title="Редактировать">
              ✏️
            </button>
            <button @click="deleteEmployee(employee.id)" class="text-red-600 hover:text-red-800" title="Удалить">
              🗑️
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <button @click="showAddForm" class="mt-4 w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600">
      Добавить сотрудника
    </button>
    <!-- Модальное окно -->
    <div v-if="showForm" class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
      <div class="bg-white p-6 rounded-lg shadow-lg w-96">
        <h3 class="text-xl font-bold mb-4">{{ isEditing ? 'Редактирование сотрудника' : 'Добавление сотрудника' }}</h3>
        <div class="space-y-4">
          <div class="relative">
            <label for="name" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Имя</label>
            <input v-model="newEmployee.name" id="name" type="text" class="w-full p-2 border rounded" :class="{'border-red-500': errors.name}" />
            <p v-if="errors.name" class="text-red-500 text-sm mt-1">{{ errors.name }}</p>
          </div>
          <div class="relative">
            <label for="phone" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Телефон</label>
            <input v-model="newEmployee.phone" id="phone" type="phone" class="w-full p-2 border rounded" :class="{'border-red-500': errors.phone}" />
            <p v-if="errors.phone" class="text-red-500 text-sm mt-1">{{ errors.phone }}</p>
          </div>
          <div class="relative">
            <label for="email" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Email</label>
            <input v-model="newEmployee.email" id="email" type="email" class="w-full p-2 border rounded" :class="{'border-red-500': errors.email}" />
            <p v-if="errors.email" class="text-red-500 text-sm mt-1">{{ errors.email }}</p>
          </div>
          <div class="relative">
            <label for="role" class="absolute -top-2 left-2 px-1 bg-white text-sm text-gray-600">Должность</label>
            <input v-model="newEmployee.role" id="role" type="text" class="w-full p-2 border rounded" :class="{'border-red-500': errors.role}" />
            <p v-if="errors.role" class="text-red-500 text-sm mt-1">{{ errors.role }}</p>
          </div>
          </div>
        <button @click="validateAndSave" class="w-full mt-6 bg-green-500 text-white py-2 rounded-lg hover:bg-green-600">
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
    },
    errors: {
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
  validateForm() {
    let isValid = true;
    this.errors = {
      name: "",
      phone: "",
      email: "",
      role: ""
    };

    if (!this.newEmployee.name.trim()) {
      this.errors.name = "Поле 'Имя' обязательно для заполнения";
      isValid = false;
    }

    if (!this.newEmployee.phone.trim()) {
      this.errors.phone = "Поле 'Телефон' обязательно для заполнения";
      isValid = false;
    }

    if (!this.newEmployee.email.trim()) {
      this.errors.email = "Поле 'Email' обязательно для заполнения";
      isValid = false;
    } else if (!this.validateEmail(this.newEmployee.email)) {
      this.errors.email = "Введите корректный email";
      isValid = false;
    }

    if (!this.newEmployee.role.trim()) {
      this.errors.role = "Поле 'Должность' обязательно для заполнения";
      isValid = false;
    }

    return isValid;
  },
  
  validateEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  },
  
  async validateAndSave() {
    if (this.validateForm()) {
      await this.saveEmployee();
    }
  },
  
  async fetchEmployees() {
    this.isLoading = true;
    try {
      const response = await axios.get(`${process.env.VUE_APP_API_URL}/Employee`);
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
    this.errors = {
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
    this.errors = {
      name: "",
      phone: "",
      email: "",
      role: ""
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
            await axios.put(`${process.env.VUE_APP_API_URL}/Employee/${this.editingEmployeeId}`, updatedEmployeeData)
        }
        const getResponse = await axios.get(`${process.env.VUE_APP_API_URL}/Employee/${this.editingEmployeeId}`);
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
        response = await axios.post(`${process.env.VUE_APP_API_URL}/Employee/`, this.newEmployee);
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
      const response = await axios.delete(`${process.env.VUE_APP_API_URL}/Employee/${id}`);
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
    this.errors = {
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

.border-red-500 {
border-color: #ef4444;
}
</style>