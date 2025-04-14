import { createRouter, createWebHistory } from 'vue-router';
import ClientsComponent from '../components/ClientsComponent.vue';
import ServicesComponent from '@/components/ServicesComponent.vue';
import EmployeesComponent from '@/components/EmployeesComponent.vue';
import AppointmentsComponent from '@/components/AppointmentsComponent.vue';

const routes = [
  { path: '/clients', component: ClientsComponent },
  { path: '/services', component: ServicesComponent },
  { path: '/employees', component: EmployeesComponent },
  { path: '/appointments', component: AppointmentsComponent }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
