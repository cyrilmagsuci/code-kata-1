import axios from 'axios';

// Create an axios instance with default configuration
const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000', // Use environment variable with fallback
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add request interceptor for handling requests
apiClient.interceptors.request.use(
  (config) => {
    // You can add authentication tokens or other request modifications here
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Add response interceptor for handling responses
apiClient.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    // Handle API errors here
    console.error('[API Error]:', error.response ? error.response.data : error.message);
    return Promise.reject(error);
  }
);

export default apiClient;