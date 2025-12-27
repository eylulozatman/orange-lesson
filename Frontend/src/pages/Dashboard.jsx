import { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import {
    LayoutDashboard, BookOpen, Clock, Settings, LogOut,
    Building2, Users, FileText, PlusCircle
} from 'lucide-react';
import { organizationService } from '../services/api';

const Dashboard = () => {
    const [user, setUser] = useState(null);
    const [organizations, setOrganizations] = useState([]);
    const [newOrgName, setNewOrgName] = useState('');

    useEffect(() => {
        const savedUser = JSON.parse(localStorage.getItem('user'));
        setUser(savedUser);

        if (savedUser?.role === 'admin') {
            fetchOrganizations();
        }
    }, []);

    const fetchOrganizations = async () => {
        const res = await organizationService.getAll();
        setOrganizations(res.data);
    };

    const handleCreateOrg = async (e) => {
        e.preventDefault();
        await organizationService.create({ name: newOrgName });
        setNewOrgName('');
        fetchOrganizations();
    };

    if (!user) return null;

    const isAdmin = user.role === 'admin';

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
                            <SidebarItem icon={<FileText size={20} />} label="Sistem Raporları" />
                        </>
                    ) : (
                        <>
                            <SidebarItem icon={<BookOpen size={20} />} label="Derslerim" />
                            <SidebarItem icon={<Clock size={20} />} label="Ödevler" />
                        </>
                    )}
                    <SidebarItem icon={<Settings size={20} />} label="Ayarlar" />
                </nav>

                <SidebarItem icon={<LogOut size={20} />} label="Çıkış Yap" danger onClick={() => { localStorage.removeItem('user'); window.location.href = '/'; }} />
            </aside>

            {/* Main Content */}
            <main style={{ flex: 1, padding: '40px', overflowY: 'auto' }}>
                <header style={{ marginBottom: '40px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <div>
                        <h1 style={{ fontSize: '1.8rem' }}>Merhaba, <span className="orange-text">{user.name}</span> 👋</h1>
                        <p style={{ color: 'var(--text-gray)' }}>
                            {isAdmin ? "Sistem yönetim paneline hoş geldiniz." : "Bugün öğrenmek için harika bir gün."}
                        </p>
                    </div>
                    <div style={{ display: 'flex', alignItems: 'center', gap: '16px' }}>
                        <div style={{ textAlign: 'right' }}>
                            <div style={{ fontSize: '0.9rem', fontWeight: '600' }}>{isAdmin ? "Süper Admin" : "Öğrenci"}</div>
                            <div style={{ fontSize: '0.8rem', color: 'var(--text-gray)' }}>OrangeLesson Cloud</div>
                        </div>
                        <div style={{ width: '48px', height: '48px', borderRadius: '50%', background: 'linear-gradient(45deg, var(--primary-orange), #FFA500)' }}></div>
                    </div>
                </header>

                {isAdmin ? (
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
                    <section style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '24px' }}>
                        {/* Standard Dashboard Content */}
                        <div className="glass-card" style={{ gridColumn: 'span 2' }}>
                            <h3>Derslerim</h3>
                            <p style={{ color: 'var(--text-gray)', marginTop: '10px' }}>Kayıtlı olduğunuz ders bulunmamaktadır.</p>
                        </div>
                    </section>
                )}
            </main>
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
