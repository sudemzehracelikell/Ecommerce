import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Login = () => {
  const [credentials, setCredentials] = useState({
    email: "",
    password: ""
  });
  const navigate = useNavigate();

  const handleChange = (e) => {
    setCredentials({ ...credentials, [e.target.name]: e.target.value });
  };

  const handleLogin = (e) => {
    e.preventDefault();

    if (credentials.email && credentials.password) {
      navigate("/"); 
    } else {
      alert("Lütfen e-posta ve şifre girin!");
    }
  };

  return (
    <div className="login-page">
      <form className="login-box" onSubmit={handleLogin}>
        <h2>Admin Giriş</h2>
        <input
          type="email"
          name="email"
          placeholder="E-posta"
          value={credentials.email}
          onChange={handleChange}
        />
        <input
          type="password"
          name="password"
          placeholder="Şifre"
          value={credentials.password}
          onChange={handleChange}
        />
        <button type="submit">Giriş Yap</button>
      </form>
    </div>
  );
};

export default Login;
