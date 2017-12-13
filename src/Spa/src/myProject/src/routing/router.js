import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import authComponent from '../auth/auth.vue';
import layout from '../components/Layout.vue'
Vue.use(Router)

export default new Router({
  mode: 'history',
  routes:[{
  component :layout,
  path :  '/',
  children: [
    {
      path: '/',
      name: 'Hello',
      component: HelloWorld
    },
    {
      path: 'auth',
      component: authComponent
    }
  ] 
  }]
}); 
