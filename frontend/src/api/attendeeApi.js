import axiosClient from './axiosClient';

export const attendeeApi = {
  getEvents: async () => {
    const response = await axiosClient.get('/api/Event/all');
    return response.data;
  },

  getEventById: async (id) => {
    const response = await axiosClient.get(`/api/Event/${id}`);
    return response.data;
  },

  registerForEvent: async (id) => {
    const response = await axiosClient.post(`/api/Event/participate/${id}`);
    return response.data;
  },

  getRegisteredEvents: async () => {
    const response = await axiosClient.get('/api/Event/registered-events');
    return response.data;
  },

  getAttendedEvents: async () => {
    const response = await axiosClient.get('/api/Event/user/registered');
    return response.data;
  },
};
