import './Styles/App.css';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';

import { MainPanelProvider } from './Contexts/MainPanelContext';
import MainPanel from './Components/MainPanel';

import Brands from './Pages/Brands';
import Categories from './Pages/Categories';
import Home from './Pages/Home';
import Products from './Pages/Products';
import Users from './Pages/Users';
import Login from './Pages/Login';

import Sidebar from './Components/Sidebar';
import NavBar from './Components/NavBar';
import Content from './Components/Content';


const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<Login />} />

        {/* Giriş yapıldıktan sonra gözükecekler */}
        <Route
          path="/dashboard/*"
          element={
            <MainPanelProvider>
              <MainPanel>
                <NavBar />
                <Sidebar />
                <Content>
                  <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="brands" element={<Brands />} />
                    <Route path="categories" element={<Categories />} />
                    <Route path="products" element={<Products />} />
                    <Route path="users" element={<Users />} />
                  </Routes>
                </Content>
              </MainPanel>
            </MainPanelProvider>
          }
        />
      </Routes>
    </Router>
  );
};

export default App;
