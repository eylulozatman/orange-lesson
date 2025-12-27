import { motion } from 'framer-motion';
import { useNavigate } from 'react-router-dom';
import { ArrowRight, BookOpen, Users, Award } from 'lucide-react';

const LandingPage = () => {
    const navigate = useNavigate();

    return (
        <div className="landing-container">
            <nav style={{ padding: '24px 5%', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
                    <div style={{
                        width: '40px',
                        height: '40px',
                        background: 'var(--primary-orange)',
                        borderRadius: '50% 50% 0 50%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        color: 'white',
                        fontWeight: 'bold',
                        fontSize: '1.2rem'
                    }}>OL</div>
                    <span style={{ fontSize: '1.5rem', fontWeight: '700' }}>
                        Orange<span className="orange-text">Lesson</span>
                    </span>
                </div>
                <button className="primary-btn" onClick={() => navigate('/login')}>
                    Giriş Yap <ArrowRight size={18} />
                </button>
            </nav>

            <main style={{ padding: '80px 5%', textAlign: 'center' }}>
                <motion.h1
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.6 }}
                    style={{ fontSize: 'clamp(2.5rem, 6vw, 4.5rem)', maxWidth: '900px', margin: '0 auto 24px' }}
                >
                    OrangeLesson ile <span className="orange-gradient-text">Ödev Takibi</span> Artık Çok Kolay
                </motion.h1>

                <motion.p
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.6, delay: 0.2 }}
                    style={{ color: 'var(--text-gray)', fontSize: '1.2rem', maxWidth: '600px', margin: '0 auto 40px' }}
                >
                    Hemen üye ol, derslerini takip et ve ödevlerini kolayca gönder.
                </motion.p>

                <motion.div
                    initial={{ opacity: 0, scale: 0.9 }}
                    animate={{ opacity: 1, scale: 1 }}
                    transition={{ duration: 0.5, delay: 0.4 }}
                    className="glass-card"
                    style={{ maxWidth: '1000px', margin: '0 auto', display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(250px, 1fr))', gap: '24px' }}
                >
                    <FeatureCard
                        icon={<BookOpen className="orange-text" />}
                        title="Ders Yönetimi"
                        desc="Kişiselleştirilmiş ders programları ve kolay takip."
                    />
                    <FeatureCard
                        icon={<Users className="orange-text" />}
                        title="Öğrenci Takibi"
                        desc="Gelişmiş analizler ve performans raporları."
                    />
                    <FeatureCard
                        icon={<Award className="orange-text" />}
                        title="Ödev Sistemi"
                        desc="Saniyeler içinde ödev yayınlayın ve yanıtları toplayın."
                    />
                </motion.div>
            </main>

            <footer style={{ marginTop: '80px', padding: '40px', borderTop: '1px solid var(--border-color)', textAlign: 'center', color: 'var(--text-gray)' }}>
                © 2025 OrangeLesson - Her Hakkı Saklıdır.
            </footer>
        </div>
    );
};

const FeatureCard = ({ icon, title, desc }) => (
    <div style={{ textAlign: 'left', padding: '12px' }}>
        <div style={{ marginBottom: '16px', background: 'rgba(255,107,0,0.1)', width: 'fit-content', padding: '12px', borderRadius: '12px' }}>
            {icon}
        </div>
        <h3 style={{ marginBottom: '8px' }}>{title}</h3>
        <p style={{ fontSize: '0.95rem', color: 'var(--text-gray)' }}>{desc}</p>
    </div>
);

export default LandingPage;
