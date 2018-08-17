import HomePage from 'components/home-page'
import Products from 'components/products'
import Users from 'components/users'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'products', path: '/products', component: Products, display: 'Products', icon: 'list' },
  { name: 'users', path: '/users', component: Users, display: 'Users', icon: 'users' }
]
