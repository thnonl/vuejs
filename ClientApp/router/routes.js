import HomePage from 'components/home-page'
import Products from 'components/products'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'products', path: '/products', component: Products, display: 'Products', icon: 'list' }
]
