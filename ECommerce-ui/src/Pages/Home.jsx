
import { useEffect, useState } from "react";

const Home = () => {
  const [greeting, setGreeting] = useState("");

  useEffect(() => {
    const hour = new Date().getHours();
    if (hour < 12) setGreeting("Good Morning!");
    else if (hour < 18) setGreeting("Have a nice day!");
    else setGreeting("Godd evening!");
  }, []);

  const today = new Date().toLocaleDateString("tr-TR");

  const stats = {
    products: 128,
    brands: 12,
    categories: 24,
    users: 57,
  };

  const recentProducts = [
    { id: 1, name: "Laptop", category: "Electronics", price: "12.000₺" },
    { id: 2, name: "Phone", category: "Electronics", price: "9.500₺" },
    { id: 3, name: "T-Shirt", category: "Clothing", price: "300₺" },
  ];

  return (
    <div className="homePage">
      <h2>{greeting}</h2>
      <p>Bugün {today}</p>

      <div className="dashboard-cards">
        <div className="card">Ürünler: {stats.products}</div>
        <div className="card">Markalar: {stats.brands}</div>
        <div className="card">Kategoriler: {stats.categories}</div>
        <div className="card">Kullanıcılar: {stats.users}</div>
      </div>

      <div className="recent-products">
        <h3>Son Eklenen Ürünler</h3>
        <table>
          <thead>
            <tr>
              <th>Ad</th>
              <th>Kategori</th>
              <th>Fiyat</th>
            </tr>
          </thead>
          <tbody>
            {recentProducts.map((p) => (
              <tr key={p.id}>
                <td>{p.name}</td>
                <td>{p.category}</td>
                <td>{p.price}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Home;
