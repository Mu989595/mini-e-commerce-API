import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { authService, AuthResponse } from '../services/authService';

interface AuthContextType {
  isAuthenticated: boolean;
  user: any | null;
  login: (userName: string, password: string) => Promise<AuthResponse>;
  register: (userName: string, email: string, password: string) => Promise<AuthResponse>;
  logout: () => void;
  loading: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<any | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = authService.getToken();
    const userData = authService.getUser();
    if (token && userData) {
      setIsAuthenticated(true);
      setUser(userData);
    }
    setLoading(false);
  }, []);

  const login = async (userName: string, password: string): Promise<AuthResponse> => {
    const response = await authService.login({ userName, password });
    if (response.isSuccess && response.data) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('user', JSON.stringify({
        userName: response.data.userName,
        email: response.data.email,
      }));
      setIsAuthenticated(true);
      setUser({
        userName: response.data.userName,
        email: response.data.email,
      });
    }
    return response;
  };

  const register = async (userName: string, email: string, password: string): Promise<AuthResponse> => {
    const response = await authService.register({ userName, email, password });
    return response;
  };

  const logout = () => {
    authService.logout();
    setIsAuthenticated(false);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, user, login, register, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
