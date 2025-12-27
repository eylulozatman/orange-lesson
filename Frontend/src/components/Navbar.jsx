import { Search, Bell, Menu } from 'lucide-react';

const Navbar = () => {
    return (
        <div style={{
            padding: '16px 40px',
            background: 'var(--bg-black)',
            borderBottom: '1px solid var(--border-color)',
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            position: 'sticky',
            top: 0,
            zIndex: 100
        }}>
            <div style={{ display: 'flex', alignItems: 'center', gap: '24px', flex: 1 }}>
                <Menu size={20} style={{ cursor: 'pointer', color: 'var(--text-gray)' }} />
                <div style={{ position: 'relative', width: '100%', maxWidth: '400px' }}>
                    <Search size={16} style={{ position: 'absolute', left: '12px', top: '50%', transform: 'translateY(-50%)', color: 'var(--text-gray)' }} />
                    <input
                        type="text"
                        placeholder="Ders, ödev veya döküman ara..."
                        style={{ width: '100%', paddingLeft: '40px', background: '#1A1A1A', border: 'none' }}
                    />
                </div>
            </div>

            <div style={{ display: 'flex', alignItems: 'center', gap: '20px' }}>
                <div style={{ position: 'relative', cursor: 'pointer' }}>
                    <Bell size={20} style={{ color: 'var(--text-gray)' }} />
                    <div style={{
                        position: 'absolute',
                        top: '-2px',
                        right: '-2px',
                        width: '8px',
                        height: '8px',
                        background: 'var(--primary-orange)',
                        borderRadius: '50%',
                        border: '2px solid var(--bg-black)'
                    }}></div>
                </div>
                <button className="primary-btn" style={{ padding: '8px 16px', fontSize: '0.9rem' }}>
                    Hızlı Kayıt
                </button>
            </div>
        </div>
    );
};

export default Navbar;
