import axiosClient from './axiosClient';

export const organizerApi = {
  createEvent: async (eventData) => {
    const response = await axiosClient.post('/api/Event/create', eventData);
    return response.data;
  },

  updateEvent: async (id, eventData) => {
    const response = await axiosClient.put(`/api/Event/update/${id}`, eventData);
    return response.data;
  },

  deleteEvent: async (id) => {
    const response = await axiosClient.delete(`/api/Event/${id}`);
    return response.data;
  },

  getMyEvents: async () => {
    const response = await axiosClient.get('/api/Event/organizer-events');
    return response.data;
  },

  getEventAttendees: async (id) => {
    const response = await axiosClient.get(`/api/Event/user/registered/${id}/attendees`);
    return response.data;
  },
};
