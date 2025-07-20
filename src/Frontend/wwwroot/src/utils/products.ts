/**
 * Sample product data for the checkout system
 */
import type { Product } from '../types/checkout';

// Sample products for demonstration
export const products: Product[] = [
  {
    sku: 'A',
    name: 'Product A',
    price: 50,
    image: 'https://via.placeholder.com/150?text=A',
  },
  {
    sku: 'B',
    name: 'Product B',
    price: 30,
    image: 'https://via.placeholder.com/150?text=B',
  },
  {
    sku: 'C',
    name: 'Product C',
    price: 20,
    image: 'https://via.placeholder.com/150?text=C',
  },
  {
    sku: 'D',
    name: 'Product D',
    price: 15,
    image: 'https://via.placeholder.com/150?text=D',
  },
];

export const findProductBySku = (sku: string): Product | undefined => {
  return products.find(product => product.sku === sku);
};