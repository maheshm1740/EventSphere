import axiosClient from './axiosClient';

export const adminApi = {
  getEvents: async () => {
    const response = await axiosClient.get('/api/Event/all');
    return response.data;
  },

  createCategory: async (categoryData) => {
    const response = await axiosClient.post('/api/Category/create', categoryData);
    return response.data;
  },

  getCategories: async () => {
    const response = await axiosClient.get('/api/Category');
    return response.data;
  },

  deleteCategory: async (id) => {
    const response = await axiosClient.delete(`/api/Category/${id}`);
    return response.data;
  },

  createOrganizer: async (organizerData) => {
    const response = await axiosClient.post('/api/User/add-organizer', organizerData);
    return response.data;
  },

  getUsers: async () => {
    const response = await axiosClient.get('/api/User/all');
    return response.data;
  },
};
