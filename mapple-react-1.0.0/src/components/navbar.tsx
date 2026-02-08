import { MenuIcon, XIcon } from "lucide-react";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export const Navbar = () => {
    const [isOpen, setIsOpen] = useState(false);
    const { isAuthenticated, user, logout } = useAuth();
    const navigate = useNavigate();

    const links = [
        { name: "Home", href: "/" },
        { name: "Products", href: "/products" },
    ];

    const handleLogout = () => {
        logout();
        navigate('/');
        setIsOpen(false);
    };

    return (
        <div className="flex items-center justify-between px-6 md:px-16 lg:px-24 xl:px-32 py-4 border-b border-gray-800">
            <Link to='/' className="flex items-center gap-2">
                <img src="/logo.svg" alt="Logo" width={127} height={32} />
            </Link>
            <ul className="max-md:hidden flex items-center gap-8">
                {links.map((link) => (
                    <li key={link.name}>
                        <Link to={link.href} className="hover:opacity-70 py-1">
                            {link.name}
                        </Link>
                    </li>
                ))}
            </ul>
            <div className="max-md:hidden flex items-center gap-4">
                {isAuthenticated ? (
                    <>
                        <span className="text-sm text-gray-400">Welcome, {user?.userName}</span>
                        <button
                            onClick={handleLogout}
                            className="bg-red-500/10 hover:bg-red-500/20 border border-red-500 text-red-500 px-6 py-2.5 rounded-lg transition duration-300"
                        >
                            Logout
                        </button>
                    </>
                ) : (
                    <>
                        <Link
                            to="/login"
                            className="hover:opacity-70 py-1"
                        >
                            Login
                        </Link>
                        <Link
                            to="/register"
                            className="bg-primary hover:bg-secondary transition duration-300 text-black px-6 py-2.5 rounded-lg"
                        >
                            Get Started
                        </Link>
                    </>
                )}
            </div>
            <button className="md:hidden" onClick={() => setIsOpen(!isOpen)}>
                <MenuIcon className="size-6" />
            </button>
            <div className={`flex flex-col items-center justify-center gap-6 text-lg fixed inset-0 z-50 bg-black/50 backdrop-blur-sm transition duration-300 ${isOpen ? 'translate-x-0' : '-translate-x-full'}`}>
                {links.map((link) => (
                    <Link
                        key={link.name}
                        to={link.href}
                        onClick={() => setIsOpen(false)}
                    >
                        {link.name}
                    </Link>
                ))}
                {isAuthenticated ? (
                    <>
                        <span className="text-sm text-gray-400">Welcome, {user?.userName}</span>
                        <button
                            onClick={handleLogout}
                            className="bg-red-500/10 hover:bg-red-500/20 border border-red-500 text-red-500 px-4 py-2 rounded-lg"
                        >
                            Logout
                        </button>
                    </>
                ) : (
                    <>
                        <Link to="/login" onClick={() => setIsOpen(false)}>Login</Link>
                        <Link to="/register" onClick={() => setIsOpen(false)}>Register</Link>
                    </>
                )}
                <button onClick={() => setIsOpen(false)}>
                    <XIcon className="size-6" />
                </button>
            </div>
        </div>
    );
};