import { api } from '../config/api';

export interface Product {
  id: number;
  name: string;
  price: number;
  catogryId: number;
}

export interface ProductsResponse {
  isSuccess: boolean;
  message: string;
  data: Product[];
}

export const productService = {
  async getAllProducts(): Promise<Product[]> {
    const response = await api.get<Product[]>('/Product');
    return response.data;
  },

  async getProductById(id: number): Promise<Product> {
    const response = await api.get<Product>(`/Product/${id}`);
    return response.data;
  },

  async createProduct(product: Omit<Product, 'id'>): Promise<Product> {
    const response = await api.post<Product>('/Product', product);
    return response.data;
  },
};
