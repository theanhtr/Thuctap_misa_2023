import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "../views/employee/employee-list/EmployeeList.vue";
import Dashboard from "../views/dashboard/Dashboard.vue";

const routes = [
  {
    path: "/",
    redirect: "/employee",
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: Dashboard,
  },
  {
    path: "/employee",
    name: "employee",
    component: EmployeeList,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

export default router;
