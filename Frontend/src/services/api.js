import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';


const api = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const studentService = {
    register: (data) => api.post('/students/register', data),
    login: (data) => api.post('/students/login', data),
    enroll: (studentId, courseId) => api.post('/students/enroll', { studentId, courseId }),
    getById: (id) => api.get(`/students/${id}`),
};

export const organizationService = {
    create: (data) => api.post('/organizations', data),
    getAll: () => api.get('/organizations'),
};

export const homeworkService = {
    getForStudent: (studentId) => api.get(`/homeworks/student/${studentId}`),
    getForTeacher: (teacherId) => api.get(`/homeworks/teacher/${teacherId}`),
    submit: (formData) => api.post('/homeworks/submit', formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
    }),
    getSubmissions: (homeworkId) => api.get(`/homeworks/${homeworkId}/submissions`),
};

export const courseService = {
    getAll: () => api.get('/courses')
};

export const teacherService = {
    register: (data) => api.post('/teachers/register', data),
    login: (data) => api.post('/teachers/login', data),
    getHomeworks: (teacherId) => api.get(`/teachers/${teacherId}/homeworks`),
    createHomework: (data) => api.post('/teachers/homeworks', data)
};

export default api;
