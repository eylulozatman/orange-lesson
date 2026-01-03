import { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import { useNavigate } from 'react-router-dom';
import {
    LayoutDashboard, BookOpen, Clock, Settings, LogOut,
    Building2, Users, FileText, PlusCircle, X
} from 'lucide-react';
import { organizationService, courseService, studentService } from '../services/api';

const Dashboard = () => {
    const [user, setUser] = useState(null);
    const [organizations, setOrganizations] = useState([]);
    const [myCourses, setMyCourses] = useState([]);
    const [availableCourses, setAvailableCourses] = useState([]);
    const [showEnrollModal, setShowEnrollModal] = useState(false);
    const [newOrgName, setNewOrgName] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        const savedUser = JSON.parse(localStorage.getItem('user'));
        if (!savedUser) {
            navigate('/login');
            return;
        }
        setUser(savedUser);

        if (savedUser.role === 'admin') {
            fetchOrganizations();
        } else {
            fetchMyCourses(savedUser);
        }
    }, [navigate]);

    const fetchOrganizations = async () => {
        try {
            const res = await organizationService.getAll();
            setOrganizations(res.data);
        } catch (err) {
            console.error("Failed to fetch orgs", err);
        }
    };

    const fetchMyCourses = async (currentUser) => {
        try {
            let res;
            if (currentUser.role === 'student') {
                res = await courseService.getByStudent(currentUser.id);
            } else if (currentUser.role === 'teacher') {
                res = await courseService.getByTeacher(currentUser.id);
            }
            if (res && res.data) {
                setMyCourses(res.data);
            }
        } catch (err) {
            console.error("Failed to fetch courses", err);
        }
    };

    const handleCreateOrg = async (e) => {
        e.preventDefault();
        await organizationService.create({ name: newOrgName });
        setNewOrgName('');
        fetchOrganizations();
    };

    const openEnrollModal = async () => {
        if (!user || !user.organizationId) return;
        try {
            // Fetch all org courses
            const res = await courseService.getByOrganization(user.organizationId);
            const allCourses = res.data;

            // Filter out already enrolled courses
            const enrolledIds = new Set(myCourses.map(c => c.id));
            const notEnrolled = allCourses.filter(c => !enrolledIds.has(c.id));

            setAvailableCourses(notEnrolled);
            setShowEnrollModal(true);
        } catch (err) {
            console.error("Failed to fetch available courses", err);
        }
    };

    const handleEnroll = async (courseId) => {
        try {
            await studentService.enroll(user.id, courseId);
            setShowEnrollModal(false);
            fetchMyCourses(user);
            alert("Derse başarıyla kayıt oldunuz!");
        } catch (err) {
            alert("Kayıt işlemi başarısız oldu.");
            console.error(err);
        }
    };

    if (!user) return null;

    const isAdmin = user.role === 'admin';
    const isStudent = user.role === 'student';

    return (
        <div style={{ display: 'flex', minHeight: '100vh' }}>
            {/* Sidebar */}
            <aside style={{
                width: '260px',
                background: 'var(--bg-card)',
                borderRight: '1px solid var(--border-color)',
                padding: '32px 16px',
                display: 'flex',
                flexDirection: 'column'
            }}>
                <div style={{ padding: '0 16px', marginBottom: '40px', display: 'flex', alignItems: 'center', gap: '10px' }}>
                    <div style={{
                        width: '32px',
                        height: '32px',
                        background: 'var(--primary-orange)',
                        borderRadius: '50% 50% 0 50%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        color: 'white',
                        fontWeight: 'bold'
                    }}>OL</div>
                    <span style={{ fontSize: '1.2rem', fontWeight: '700' }}>Orange<span className="orange-text">Lesson</span></span>
                </div>

                <nav style={{ flex: 1, display: 'flex', flexDirection: 'column', gap: '8px' }}>
                    <SidebarItem icon={<LayoutDashboard size={20} />} label={isAdmin ? "Panel" : "Genel Bakış"} active />
                    {isAdmin ? (
                        <>
                            <SidebarItem icon={<Building2 size={20} />} label="Kurumlar" />
                            <SidebarItem icon={<Users size={20} />} label="Kullanıcı Yönetimi" />
                        </>
                    ) : (
                        <>
                            <SidebarItem icon={<BookOpen size={20} />} label="Derslerim" />
                        </>
                    )}
                </nav>

                <SidebarItem icon={<LogOut size={20} />} label="Çıkış Yap" danger onClick={() => { localStorage.removeItem('user'); window.location.href = '/login'; }} />
            </aside>

            {/* Main Content */}
            <main style={{ flex: 1, padding: '40px', overflowY: 'auto' }}>
                <header style={{ marginBottom: '40px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <div>
                        <h1 style={{ fontSize: '1.8rem' }}>Merhaba, <span className="orange-text">{user.name}</span> 👋</h1>
                        <p style={{ color: 'var(--text-gray)' }}>
                            {isAdmin ? "Sistem yönetim paneli." : "Derslerinize hoş geldiniz."}
                        </p>
                    </div>
                </header>

                {isAdmin ? (
                    // ADMIN VIEW
                    <section style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(350px, 1fr))', gap: '24px' }}>
                        <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} className="glass-card">
                            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
                                <h3>Kurum Ekle</h3>
                                <PlusCircle className="orange-text" />
                            </div>
                            <form onSubmit={handleCreateOrg} style={{ display: 'flex', gap: '12px' }}>
                                <input
                                    type="text"
                                    placeholder="Kurum Adı"
                                    style={{ flex: 1 }}
                                    value={newOrgName}
                                    onChange={(e) => setNewOrgName(e.target.value)}
                                    required
                                />
                                <button type="submit" className="primary-btn" style={{ padding: '8px 16px' }}>Ekle</button>
                            </form>
                        </motion.div>

                        <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: 0.1 }} className="glass-card">
                            <h3>Mevcut Kurumlar</h3>
                            <div style={{ marginTop: '20px', display: 'flex', flexDirection: 'column', gap: '12px' }}>
                                {organizations.map(org => (
                                    <div key={org.id} style={{ padding: '12px', background: 'var(--bg-black)', borderRadius: '8px', border: '1px solid var(--border-color)', display: 'flex', justifyContent: 'space-between' }}>
                                        <span>{org.name}</span>
                                        <span className="orange-text" style={{ fontSize: '0.8rem' }}>{org.id.substring(0, 8)}</span>
                                    </div>
                                ))}
                            </div>
                        </motion.div>
                    </section>
                ) : (
                    // STUDENT / TEACHER VIEW
                    <section>
                        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
                            <h3>Derslerim</h3>
                            {isStudent && (
                                <button onClick={openEnrollModal} className="primary-btn" style={{ padding: '8px 16px', display: 'flex', gap: '8px' }}>
                                    <PlusCircle size={18} /> Yeni Derse Kayıt Ol
                                </button>
                            )}
                        </div>

                        {myCourses.length === 0 ? (
                            <p style={{ color: 'var(--text-gray)' }}>Henüz kayıtlı ders bulunmamaktadır.</p>
                        ) : (
                            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))', gap: '20px' }}>
                                {myCourses.map(course => (
                                    <motion.div
                                        key={course.id}
                                        whileHover={{ y: -5 }}
                                        className="glass-card"
                                        style={{ cursor: 'pointer' }}
                                        onClick={() => navigate(`/course/${course.id}`)}
                                    >
                                        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'start' }}>
                                            <div style={{ width: '40px', height: '40px', background: 'rgba(255,107,0,0.1)', borderRadius: '8px', display: 'flex', alignItems: 'center', justifyContent: 'center', color: 'var(--primary-orange)' }}>
                                                <BookOpen size={20} />
                                            </div>
                                            {/* <span style={{ fontSize: '0.8rem', color: 'var(--text-gray)' }}>{course.id.substring(0,6)}</span> */}
                                        </div>
                                        <h4 style={{ fontSize: '1.2rem', margin: '16px 0 8px' }}>{course.name || course.courseName}</h4>
                                        <p style={{ color: 'var(--text-gray)', fontSize: '0.9rem' }}>
                                            Eğitmen ID: {course.teacherId ? course.teacherId.substring(0, 8) : 'N/A'}...
                                        </p>
                                    </motion.div>
                                ))}
                            </div>
                        )}
                    </section>
                )}
            </main>

            {/* Enroll Modal */}
            {showEnrollModal && (
                <div style={{
                    position: 'fixed', top: 0, left: 0, right: 0, bottom: 0,
                    background: 'rgba(0,0,0,0.7)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000
                }}>
                    <motion.div
                        initial={{ scale: 0.9, opacity: 0 }}
                        animate={{ scale: 1, opacity: 1 }}
                        className="glass-card"
                        style={{ width: '100%', maxWidth: '500px', maxHeight: '80vh', overflowY: 'auto' }}
                    >
                        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
                            <h3>Mevcut Dersler</h3>
                            <X style={{ cursor: 'pointer' }} onClick={() => setShowEnrollModal(false)} />
                        </div>

                        {availableCourses.length === 0 ? (
                            <p>Kayıt olabileceğiniz yeni ders bulunmuyor.</p>
                        ) : (
                            <div style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
                                {availableCourses.map(c => (
                                    <div key={c.id} style={{
                                        padding: '16px', background: 'var(--bg-black)',
                                        borderRadius: '8px', border: '1px solid var(--border-color)',
                                        display: 'flex', justifyContent: 'space-between', alignItems: 'center'
                                    }}>
                                        <div>
                                            <div style={{ fontWeight: '600' }}>{c.name || c.courseName}</div>
                                        </div>
                                        <button onClick={() => handleEnroll(c.id)} className="primary-btn" style={{ padding: '6px 12px', fontSize: '0.8rem' }}>
                                            Kayıt Ol
                                        </button>
                                    </div>
                                ))}
                            </div>
                        )}
                    </motion.div>
                </div>
            )}
        </div>
    );
};

const SidebarItem = ({ icon, label, active, danger, onClick }) => (
    <div
        onClick={onClick}
        style={{
            display: 'flex',
            alignItems: 'center',
            gap: '12px',
            padding: '12px 16px',
            borderRadius: '10px',
            cursor: 'pointer',
            background: active ? 'rgba(255,107,0,0.1)' : 'transparent',
            color: active ? 'var(--primary-orange)' : danger ? '#FF4B4B' : 'var(--text-gray)',
            fontWeight: active ? '600' : '400',
            transition: 'all 0.3s ease'
        }}
    >
        {icon} <span>{label}</span>
    </div>
);

export default Dashboard;
