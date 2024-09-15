import { Route, Routes, Navigate } from "react-router-dom";
import { useEffect, useState } from "react";
import {jwtDecode} from "jwt-decode";
import HomePage from "./pages/HomePage";
import Login from "./components/Auth/Login/Login";
import Register from "./components/Auth/Register/Register";
import UsersPage from "./pages/Admin/UsersPage";
import DashboardPage from "./pages/Admin/DashboardPage";
import ChatDashboardPage from "./pages/Chat/ChatDashboardPage";
import AdminLayout from "./layouts/AdminLayout/AdminLayout";
import MainLayout from "./layouts/MainLayout/MainLayout";
import ChatLayout from "./layouts/ChatLayout/ChatLayout";
import UserUpdate from "./pages/Admin/UserUpdate";
import ChatPage from "./pages/Chat/ChatPage";

function App() {
  const [userRole, setUserRole] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        setUserRole(decodedToken.role);
      } catch (error) {
        console.error("Error decoding token", error);
      }
    }

    setLoading(false);
  }, []);

  if (loading) {
    return <div>Loading...</div>; // Or a loading spinner
  }

  return (
    <Routes>
      {/* Ana Sayfa ve Giriş/Üyelik Sayfaları */}
      <Route path="/" element={<MainLayout><HomePage /></MainLayout>} />
      <Route path="/login" element={<MainLayout><Login /></MainLayout>} />
      <Route path="/register" element={<MainLayout><Register /></MainLayout>} />

      {/* Admin sayfaları */}
      <Route
        path="/admin"
        element={userRole === "Superadmin" ? (
          <AdminLayout><DashboardPage /></AdminLayout>
        ) : (
          <Navigate to="/" />
        )}
      />
      <Route
        path="/admin/users"
        element={userRole === "Superadmin" ? (
          <AdminLayout><UsersPage /></AdminLayout>
        ) : (
          <Navigate to="/" />
        )}
      />
      <Route
        path="/admin/userupdate/:userId"
        element={userRole === "Superadmin" ? (
          <AdminLayout><UserUpdate /></AdminLayout>
        ) : (
          <Navigate to="/" />
        )}
      />
      

      {/* Chat sayfaları */}
     
      <Route
        path="/chat/"
        element={userRole === "Superadmin" || userRole === "OperationSupport" ? (
          <ChatLayout><ChatDashboardPage /></ChatLayout>
        ) : (
          <Navigate to="/" />
        )}
      />
      <Route
        path="/chat/:currentUserId/:recipientUserId"
        element={userRole === "Superadmin" || userRole === "OperationSupport" ? (
          <ChatLayout><ChatPage /></ChatLayout>
        ) : (
          <Navigate to="/" />
        )}
      />

      {/* Default Route */}
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
}

export default App;
