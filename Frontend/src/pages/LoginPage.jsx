import { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import { useNavigate } from 'react-router-dom';
import { Mail, Lock, LogIn, Building2 } from 'lucide-react';
import { organizationService, studentService, courseService, teacherService } from '../services/api';

const LoginPage = () => {
    const [isTeacher, setIsTeacher] = useState(false);
    const [isRegister, setIsRegister] = useState(false);
    const [organizations, setOrganizations] = useState([]);
    const [selectedOrg, setSelectedOrg] = useState('');
    const [courses, setCourses] = useState([]);
    const [selectedCourse, setSelectedCourse] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        if (isRegister) {
            organizationService.getAll().then(res => setOrganizations(res.data));
            if (isTeacher) {
                // Teacher registration might need courses or just org
            }
        }
    }, [isRegister, isTeacher]);

    const handleLogin = async (e) => {
        e.preventDefault();
        const email = e.target.email.value;
        const password = e.target.password.value;

        if (isRegister) {
            const fullName = e.target.fullName.value;
            const city = e.target.city?.value || '';

            if (isTeacher) {
                const payload = {
                    organizationId: selectedOrg,
                    fullName,
                    email,
                    password,
                    city,
                    courseId: selectedCourse
                };
                try {
                    await teacherService.register(payload);
                    // For now, redirect to login or auto-login (not implemented full auth flow yet)
                    alert('Kayıt Başarılı! Lütfen giriş yapınız.');
                    setIsRegister(false);
                } catch (err) {
                    console.error('Teacher registration failed', err);
                    alert('Kayıt yapılamadı. Lütfen tekrar deneyin.');
                }
                return;
            }

            const grade = parseInt(e.target.grade?.value || '0', 10);
            const payload = {
                organizationId: selectedOrg,
                fullName,
                email,
                password,
                city,
                grade
            };
            try {
                await studentService.register(payload);
                alert('Kayıt Başarılı! Lütfen giriş yapınız.');
                setIsRegister(false);
            } catch (err) {
                console.error('Registration failed', err);
                alert('Kayıt yapılamadı. Lütfen tekrar deneyin.');
            }
            return;
        }

        // Mock Login Logic with Real Seeded IDs
        if (email === "eylul@ozatman.com") {
            localStorage.setItem('user', JSON.stringify({
                id: '99999999-9999-9999-9999-999999999999',
                name: 'Eylül Özatman',
                role: 'admin',
                organizationId: '11111111-1111-1111-1111-111111111111'
            }));
        } else if (email === "ali@blueschool.com") {
            localStorage.setItem('user', JSON.stringify({
                id: '88888888-8888-8888-8888-888888888888',
                name: 'Ali Yılmaz',
                role: 'teacher',
                organizationId: '22222222-2222-2222-2222-222222222222'
            }));
        } else if (email === "can@blueschool.com") {
            localStorage.setItem('user', JSON.stringify({
                id: '66666666-6666-6666-6666-666666666666',
                name: 'Can Yıldız',
                role: 'student',
                organizationId: '22222222-2222-2222-2222-222222222222'
            }));
        } else {
            // Fallback for new registrations (would normally use response from login endpoint)
            alert("Lütfen geçerli test hesaplarından birini kullanın (README'ye bakınız).");
            return;
        }
        navigate('/dashboard');
    };

    return (
        <div style={{
            minHeight: '100vh',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            background: 'radial-gradient(circle at top right, rgba(255,107,0,0.1) 0%, transparent 40%)'
        }}>
            <motion.div
                initial={{ opacity: 0, scale: 0.95 }}
                animate={{ opacity: 1, scale: 1 }}
                className="glass-card"
                style={{ width: '100%', maxWidth: '450px', margin: '20px' }}
            >
                <div style={{ textAlign: 'center', marginBottom: '32px' }}>
                    <h2 style={{ fontSize: '2rem', marginBottom: '8px' }}>
                        {isRegister ? 'Kayıt Ol' : 'Hoş Geldiniz'}
                    </h2>
                    <p style={{ color: 'var(--text-gray)' }}>
                        {isRegister ? 'Bilgilerinizi girerek kayıt olun' : 'Lütfen hesabınıza giriş yapın'}
                    </p>
                </div>

                {!isRegister && (
                    <div style={{
                        display: 'flex',
                        background: 'var(--bg-black)',
                        padding: '4px',
                        borderRadius: '10px',
                        marginBottom: '24px'
                    }}>
                        <ToggleButton active={!isTeacher} onClick={() => setIsTeacher(false)}>Öğrenci</ToggleButton>
                        <ToggleButton active={isTeacher} onClick={() => setIsTeacher(true)}>Öğretmen</ToggleButton>
                    </div>
                )}

                <form onSubmit={handleLogin} style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
                    {isRegister && (
                        <>
                            <input name="fullName" type="text" placeholder="Ad Soyad" style={{ width: '100%' }} required />
                            <div style={{ position: 'relative' }}>
                                <Building2 size={18} style={{ position: 'absolute', left: '16px', top: '15px', color: 'var(--text-gray)' }} />
                                <select
                                    style={{
                                        width: '100%',
                                        padding: '12px 16px 12px 45px',
                                        background: 'var(--bg-card)',
                                        color: 'white',
                                        border: '1px solid var(--border-color)',
                                        borderRadius: '8px',
                                        appearance: 'none',
                                        outline: 'none'
                                    }}
                                    required
                                    value={selectedOrg}
                                    onChange={(e) => setSelectedOrg(e.target.value)}
                                >
                                    <option value="" disabled>Kurum Seçiniz</option>
                                    {organizations.map(org => (
                                        <option key={org.id} value={org.id}>{org.name}</option>
                                    ))}
                                </select>
                            </div>
                            <input name="city" type="text" placeholder="Şehir" style={{ width: '100%' }} required />
                            {isTeacher ? (
                                <div style={{ position: 'relative' }}>
                                    <select
                                        style={{
                                            width: '100%',
                                            padding: '12px 16px',
                                            background: 'var(--bg-card)',
                                            color: 'white',
                                            border: '1px solid var(--border-color)',
                                            borderRadius: '8px',
                                            outline: 'none'
                                        }}
                                        required
                                        value={selectedCourse}
                                        onChange={(e) => setSelectedCourse(e.target.value)}
                                    >
                                        <option value="" disabled>Ders Seçiniz</option>
                                        {courses.map(c => (
                                            <option key={c.id} value={c.id}>{c.courseName}</option>
                                        ))}
                                    </select>
                                </div>
                            ) : (
                                <input name="grade" type="number" min={1} max={12} placeholder="Sınıf (örn: 9)" style={{ width: '100%' }} required />
                            )}
                        </>
                    )}
                    <div style={{ position: 'relative' }}>
                        <Mail size={18} style={{ position: 'absolute', left: '16px', top: '15px', color: 'var(--text-gray)' }} />
                        <input
                            name="email"
                            type="email"
                            placeholder="E-posta adresi"
                            style={{ width: '100%', paddingLeft: '45px' }}
                            required
                        />
                    </div>
                    <div style={{ position: 'relative' }}>
                        <Lock size={18} style={{ position: 'absolute', left: '16px', top: '15px', color: 'var(--text-gray)' }} />
                        <input
                            name="password"
                            type="password"
                            placeholder="Şifre"
                            style={{ width: '100%', paddingLeft: '45px' }}
                            required
                        />
                    </div>

                    <button type="submit" className="primary-btn" style={{ justifyContent: 'center', marginTop: '10px' }}>
                        {isRegister ? 'Kayıt Ol' : 'Giriş Yap'} <LogIn size={18} />
                    </button>
                </form>

                <p style={{ textAlign: 'center', marginTop: '24px', color: 'var(--text-gray)', fontSize: '0.9rem' }}>
                    {isRegister ? 'Zaten hesabınız var mı?' : 'Hesabınız yok mu?'}
                    <span
                        className="orange-text"
                        style={{ cursor: 'pointer', fontWeight: '600', marginLeft: '5px' }}
                        onClick={() => setIsRegister(!isRegister)}
                    >
                        {isRegister ? 'Giriş Yap' : 'Kayıt Ol'}
                    </span>
                </p>
            </motion.div>
        </div>
    );
};

const ToggleButton = ({ children, active, onClick }) => (
    <button
        type="button"
        onClick={onClick}
        style={{
            flex: 1,
            padding: '10px',
            background: active ? 'var(--primary-orange)' : 'transparent',
            color: active ? 'white' : 'var(--text-gray)',
            borderRadius: '8px',
            fontSize: '0.9rem',
            fontWeight: '600'
        }}
    >
        {children}
    </button>
);

export default LoginPage;
