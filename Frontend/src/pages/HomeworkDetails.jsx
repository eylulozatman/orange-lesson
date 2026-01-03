import { useState, useEffect } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { motion } from 'framer-motion';
import { ArrowLeft, Upload, FileText, CheckCircle, User } from 'lucide-react';
import { homeworkService } from '../services/api';

const HomeworkDetails = () => {
    const { homeworkId } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    // We try to get homework from navigation state, otherwise we'd need to fetch (skipped for now)
    const [homework, setHomework] = useState(location.state?.homework || null);
    const [user, setUser] = useState(null);
    const [submissions, setSubmissions] = useState([]);

    // Form states
    const [content, setContent] = useState('');
    const [file, setFile] = useState(null);
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        const savedUser = JSON.parse(localStorage.getItem('user'));
        if (!savedUser) {
            navigate('/login');
            return;
        }
        setUser(savedUser);

        if (savedUser.role === 'teacher') {
            fetchSubmissions();
        }
    }, [homeworkId]);

    const fetchSubmissions = async () => {
        try {
            const res = await homeworkService.getSubmissions(homeworkId);
            setSubmissions(res.data);
        } catch (err) {
            console.error("Failed to fetch submissions", err);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSubmitting(true);

        const formData = new FormData();
        formData.append('homeworkId', homeworkId);
        formData.append('studentId', user.id);
        formData.append('content', content);
        if (file) {
            formData.append('file', file);
        }

        try {
            await homeworkService.submit(formData);
            alert("Ödev başarıyla gönderildi!");
            navigate(-1); // Go back
        } catch (err) {
            console.error("Submission failed", err);
            alert("Gönderim başarısız oldu.");
        } finally {
            setSubmitting(false);
        }
    };

    if (!homework) return <div style={{ padding: '40px', color: 'white' }}>Ödev bilgisi bulunamadı. Lütfen ders listesinden tekrar deneyin.</div>;

    const isStudent = user?.role === 'student';
    const isTeacher = user?.role === 'teacher';

    return (
        <div style={{ minHeight: '100vh', padding: '40px', maxWidth: '1000px', margin: '0 auto' }}>
            <button
                onClick={() => navigate(-1)}
                style={{
                    background: 'none', border: 'none', color: 'var(--text-gray)',
                    display: 'flex', alignItems: 'center', gap: '8px', cursor: 'pointer', marginBottom: '20px'
                }}
            >
                <ArrowLeft size={20} /> Geri Dön
            </button>

            <div className="glass-card" style={{ padding: '32px', marginBottom: '40px' }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'start', marginBottom: '24px' }}>
                    <div>
                        <h1 style={{ fontSize: '2rem', marginBottom: '8px' }}>{homework.title}</h1>
                        <div style={{ display: 'flex', gap: '16px', color: 'var(--text-gray)' }}>
                            <span>Son Teslim: {new Date(homework.dueDate).toLocaleDateString()}</span>
                            {/* <span>Puan: 100</span> */}
                        </div>
                    </div>
                </div>

                <div style={{ background: 'rgba(255,255,255,0.05)', padding: '20px', borderRadius: '8px', lineHeight: '1.6' }}>
                    <h3 style={{ marginBottom: '10px', fontSize: '1.1rem', color: 'var(--primary-orange)' }}>Açıklama</h3>
                    <p>{homework.description}</p>
                </div>
            </div>

            {isStudent && (
                <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} className="glass-card" style={{ padding: '32px' }}>
                    <h3 style={{ marginBottom: '24px' }}>Ödev Gönder</h3>
                    <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
                        <div>
                            <label style={{ display: 'block', marginBottom: '8px', color: 'var(--text-gray)' }}>Notunuz / Cevabınız</label>
                            <textarea
                                value={content}
                                onChange={(e) => setContent(e.target.value)}
                                style={{ width: '100%', minHeight: '120px', padding: '12px', background: 'var(--bg-black)', border: '1px solid var(--border-color)', borderRadius: '8px', color: 'white' }}
                                required
                                placeholder="Cevabınızı buraya yazabilirsiniz..."
                            />
                        </div>

                        <div>
                            <label style={{ display: 'block', marginBottom: '8px', color: 'var(--text-gray)' }}>Dosya Ekle (Opsiyonel)</label>
                            <div style={{ border: '2px dashed var(--border-color)', padding: '32px', borderRadius: '8px', textAlign: 'center', cursor: 'pointer' }}
                                onClick={() => document.getElementById('file-upload').click()}
                            >
                                <input
                                    id="file-upload"
                                    type="file"
                                    style={{ display: 'none' }}
                                    onChange={(e) => setFile(e.target.files[0])}
                                />
                                <Upload className="orange-text" size={32} style={{ marginBottom: '12px' }} />
                                <p>{file ? file.name : "Dosya seçmek için tıklayın"}</p>
                            </div>
                        </div>

                        <button type="submit" className="primary-btn" disabled={submitting} style={{ alignSelf: 'flex-start', padding: '12px 32px' }}>
                            {submitting ? 'Gönderiliyor...' : 'Ödevi Tamamla'}
                        </button>
                    </form>
                </motion.div>
            )}

            {isTeacher && (
                <div className="glass-card" style={{ padding: '32px' }}>
                    <h3 style={{ marginBottom: '24px' }}>Öğrenci Gönderimleri ({submissions.length})</h3>

                    {submissions.length === 0 ? (
                        <p style={{ color: 'var(--text-gray)' }}>Henüz gönderim yapılmamış.</p>
                    ) : (
                        <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
                            {submissions.map(sub => (
                                <div key={sub.id} style={{ padding: '16px', background: 'var(--bg-black)', borderRadius: '8px', border: '1px solid var(--border-color)' }}>
                                    <div style={{ display: 'flex', alignItems: 'center', gap: '12px', marginBottom: '12px' }}>
                                        <div style={{ width: '32px', height: '32px', borderRadius: '50%', background: 'var(--text-gray)', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
                                            <User size={16} color="black" />
                                        </div>
                                        <div>
                                            <div style={{ fontWeight: '600' }}>Öğrenci ID: {sub.studentId.substring(0, 8)}...</div>
                                            <div style={{ fontSize: '0.8rem', color: 'var(--text-gray)' }}>Gönderildi</div>
                                        </div>
                                    </div>
                                    <p style={{ marginBottom: '12px' }}>{sub.content}</p>
                                    {sub.filePath && (
                                        <a href={`http://localhost:5000${sub.filePath}`} target="_blank" rel="noreferrer" style={{ display: 'inline-flex', alignItems: 'center', gap: '8px', color: 'var(--primary-orange)', textDecoration: 'none' }}>
                                            <FileText size={16} /> Ekli Dosyayı Görüntüle
                                        </a>
                                    )}
                                </div>
                            ))}
                        </div>
                    )}
                </div>
            )}
        </div>
    );
};

export default HomeworkDetails;
