/* eslint-disable react-hooks/immutability */
import { useState, useEffect } from 'react';
import { OrganizationApi } from './api/OrganizationApi';

function App() {
  const [organizations, setOrganizations] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadOrganizations();
    
  }, []);

  const loadOrganizations = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await OrganizationApi.getAll();
      setOrganizations(response.data);
    } catch (error) {
      console.error('Ошибка при загрузке:', error);
      setError(error)
    } finally {
      setLoading(false);
    }
  }

  return (
    <div style={styles.page}>
      <div style={styles.container}>
        {/* ШАПКА В СТИЛЕ 2000-Х */}
        <div style={styles.header}>
          <h1 style={styles.title}>📋 Организации</h1>
          <p style={styles.subtitle}>Добро пожаловать в систему учёта</p>
        </div>

        {/* ОШИБКИ */}
        {error && <div style={styles.error}>⚠️ {error}</div>}

        {/* ИНДИКАТОР ЗАГРУЗКИ */}
        {loading && <div style={styles.loading}>Загрузка данных...</div>}

        {/* ТАБЛИЦА В СТИЛЕ НУЛЕВЫХ */}
        {!loading && organizations.length === 0 && (
          <p style={styles.empty}>📭 Список организаций пуст.</p>
        )}

        {!loading && organizations.length > 0 && (
          <table style={styles.table}>
            <thead>
              <tr>
                <th style={styles.th}>ID</th>
                <th style={styles.th}>Название</th>
              </tr>
            </thead>
            <tbody>
              {organizations.map((org) => (
                <tr key={org.id} style={styles.tr}>
                  <td style={styles.td}>{org.id}</td>
                  <td style={styles.td}>{org.name}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}

        {/* ПОДВАЛ С КОПИРАЙТОМ (куда без него) */}
        <div style={styles.footer}>
          &copy; 2005–{new Date().getFullYear()} — Все права защищены 😄
        </div>
      </div>
    </div>
  );
}

// 📦 ОБЪЕКТ СО СТИЛЯМИ (как в старые добрые времена)
const styles = {
  page: {
    margin: 0,
    padding: 0,
    minHeight: '100vh',
    background: 'linear-gradient(to bottom, #b8e1fc, #d0f0fd)',
    fontFamily: '"Segoe UI", Tahoma, Geneva, Verdana, sans-serif',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  container: {
    maxWidth: '700px',
    width: '100%',
    margin: '40px 20px',
    padding: '24px 28px',
    backgroundColor: '#ffffff',
    borderRadius: '16px',
    boxShadow: '0 10px 30px rgba(0,0,0,0.15)',
    border: '1px solid #c0d9e8',
  },
  header: {
    textAlign: 'center',
    borderBottom: '2px solid #e5eef5',
    paddingBottom: '12px',
    marginBottom: '20px',
  },
  title: {
    fontSize: '32px',
    margin: 0,
    fontWeight: 600,
    color: '#1f3a4b',
    letterSpacing: '1px',
  },
  subtitle: {
    fontSize: '16px',
    color: '#3c6e8c',
    margin: '6px 0 0 0',
    fontStyle: 'italic',
  },
  table: {
    width: '100%',
    borderCollapse: 'collapse',
    marginTop: '12px',
    fontSize: '18px',
    borderRadius: '8px',
    overflow: 'hidden',
    boxShadow: '0 2px 8px rgba(0,0,0,0.05)',
  },
  th: {
    backgroundColor: '#d9e6f2',
    color: '#1f3a4b',
    padding: '12px 16px',
    textAlign: 'left',
    borderBottom: '2px solid #b3c9db',
    fontWeight: 600,
  },
  tr: {
    backgroundColor: '#f9fcff',
    borderBottom: '1px solid #dbe5ed',
  },
  td: {
    padding: '12px 16px',
    color: '#1f2e3b',
    borderBottom: '1px solid #dbe5ed',
  },
  loading: {
    textAlign: 'center',
    fontSize: '20px',
    color: '#1f4a6e',
    padding: '40px 0',
    fontWeight: 500,
  },
  error: {
    backgroundColor: '#ffe5e5',
    color: '#a13d3d',
    padding: '12px 18px',
    borderRadius: '8px',
    border: '1px solid #f5c2c2',
    marginBottom: '16px',
    textAlign: 'center',
    fontSize: '17px',
  },
  empty: {
    textAlign: 'center',
    fontSize: '20px',
    color: '#3e6d8c',
    padding: '32px 0',
    fontStyle: 'italic',
  },
  footer: {
    marginTop: '28px',
    paddingTop: '14px',
    borderTop: '1px solid #dbe5ed',
    textAlign: 'center',
    fontSize: '15px',
    color: '#3f6b86',
    letterSpacing: '0.3px',
  },
};

export default App;