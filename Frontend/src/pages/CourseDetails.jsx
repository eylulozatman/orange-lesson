import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { motion } from 'framer-motion';
import { ArrowLeft, Clock, FileText, ChevronRight, PlusCircle } from 'lucide-react';
import { homeworkService } from '../services/api';

const CourseDetails = () => {
    const { courseId } = useParams();
    const navigate = useNavigate();
    const [user, setUser] = useState(null);
    const [homeworks, setHomeworks] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const savedUser = JSON.parse(localStorage.getItem('user'));
        if (!savedUser) {
            navigate('/login');
            return;
        }
        setUser(savedUser);
        fetchHomeworks(savedUser);
    }, [courseId, navigate]);

    const fetchHomeworks = async (currentUser) => {
        try {
            let res;
            if (currentUser.role === 'student') {
                res = await homeworkService.getForStudent(currentUser.id);
            } else if (currentUser.role === 'teacher') {
                res = await homeworkService.getForTeacher(currentUser.id);
            }

            if (res && res.data) {
                // Filter homeworks for this specific course
                const courseHomeworks = res.data.filter(hw => hw.courseId === courseId);
                setHomeworks(courseHomeworks);
            }
        } catch (err) {
            console.error("Failed to fetch homeworks", err);
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <div style={{ padding: '40px', color: 'white' }}>Yükleniyor...</div>;

    const isTeacher = user?.role === 'teacher';

    return (
        <div style={{ minHeight: '100vh', padding: '40px', maxWidth: '1200px', margin: '0 auto' }}>
            <button
                onClick={() => navigate('/dashboard')}
                style={{
                    background: 'none', border: 'none', color: 'var(--text-gray)',
                    display: 'flex', alignItems: 'center', gap: '8px', cursor: 'pointer', marginBottom: '20px'
                }}
            >
                <ArrowLeft size={20} /> Panele Dön
            </button>

            <header style={{ marginBottom: '40px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                    <h1 style={{ fontSize: '2rem', marginBottom: '8px' }}>Ders İçeriği</h1>
                    <p style={{ color: 'var(--text-gray)' }}>
                        Course ID: <span style={{ fontFamily: 'monospace' }}>{courseId}</span>
                    </p>
                </div>
                {/* Teachers can add homeworks here in future */}
                {isTeacher && (
                    <button className="primary-btn" disabled title="Not implemented in this demo">
                        <PlusCircle size={18} /> Ödev Ekle
                    </button>
                )}
            </header>

            <section>
                <h3 style={{ marginBottom: '20px', display: 'flex', alignItems: 'center', gap: '10px' }}>
                    <Clock className="orange-text" /> Aktif Ödevler
                </h3>

                {homeworks.length === 0 ? (
                    <div className="glass-card" style={{ textAlign: 'center', padding: '40px', color: 'var(--text-gray)' }}>
                        Bu ders için henüz ödev atanmamış.
                    </div>
                ) : (
                    <div style={{ display: 'grid', gap: '16px' }}>
                        {homeworks.map(hw => (
                            <motion.div
                                key={hw.id}
                                whileHover={{ scale: 1.01 }}
                                className="glass-card"
                                style={{
                                    display: 'flex', justifyContent: 'space-between', alignItems: 'center',
                                    cursor: 'pointer', padding: '24px'
                                }}
                                onClick={() => navigate(`/homework/${hw.id}`, { state: { homework: hw } })}
                            >
                                <div style={{ display: 'flex', alignItems: 'center', gap: '20px' }}>
                                    <div style={{
                                        width: '50px', height: '50px', borderRadius: '12px',
                                        background: 'rgba(255,107,0,0.1)', display: 'flex', alignItems: 'center', justifyContent: 'center',
                                        color: 'var(--primary-orange)'
                                    }}>
                                        <FileText size={24} />
                                    </div>
                                    <div>
                                        <h4 style={{ fontSize: '1.2rem', marginBottom: '4px' }}>{hw.title}</h4>
                                        <p style={{ color: 'var(--text-gray)', fontSize: '0.9rem' }}>
                                            Son Teslim: {new Date(hw.dueDate).toLocaleDateString()}
                                        </p>
                                    </div>
                                </div>
                                <ChevronRight style={{ color: 'var(--text-gray)' }} />
                            </motion.div>
                        ))}
                    </div>
                )}
            </section>
        </div>
    );
};

export default CourseDetails;
