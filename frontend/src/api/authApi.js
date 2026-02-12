import axiosClient from './axiosClient';

export const authApi = {
  login: async (email, password) => {
    const response = await axiosClient.post('/api/Auth/login', { email, password });
    return response.data;
  },

  register: async (name, email, password) => {
    const response = await axiosClient.post('/api/Auth/register', { name, email, password });
    return response.data;
  },
};
