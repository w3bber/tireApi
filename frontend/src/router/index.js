import { createRouter, createWebHistory } from 'vue-router';
import ClientsComponent from '../components/ClientsComponent.vue';
import ServicesComponent from '@/components/ServicesComponent.vue';
import EmployeesComponent from '@/components/EmployeesComponent.vue';
import AppointmentsComponent from '@/components/AppointmentsComponent.vue';
import HelloComponent from '@/components/HelloComponent.vue';

const routes = [
  { path: '/clients', component: ClientsComponent },
  { path: '/services', component: ServicesComponent },
  { path: '/employees', component: EmployeesComponent },
  { path: '/appointments', component: AppointmentsComponent },
  { path: '/', component: HelloComponent }

];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
