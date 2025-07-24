import { useState, useEffect } from "react";
import axios from "axios";

const Brands = () => {
  const [brands, setBrands] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [loading, setLoading] = useState(false);

  const [filters, setFilters] = useState({
    name: "",
    code: "",
    state: "",
  });

  const [newBrand, setNewBrand] = useState({
    name: "",
    code: 0,
    description: "",
    state: true,
  });

  const [showAddPanel, setShowAddPanel] = useState(false);

  const fetchBrands = async () => {
    setLoading(true);
    try {
      const res = await axios.get("http://localhost:5432/api/brand/enum-all");
      setBrands(res.data);
      setFiltered(res.data);
    } catch (error) {
      console.error("Brand çekme hatası:", error);
      alert("Brand verileri yüklenemedi.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchBrands();
  }, []);

  const handleFilterChange = (e) => {
    setFilters({ ...filters, [e.target.name]: e.target.value });
  };

  const handleFilter = () => {
    const filteredData = brands.filter((b) => {
      const matchesName = b.name?.toLowerCase().includes(filters.name.toLowerCase()) || false;
      const matchesCode = filters.code === "" || b.code.toString() === filters.code;
      const matchesState = filters.state === "" || b.state.toString() === filters.state;
      return matchesName && matchesCode && matchesState;
    });

    setFiltered(filteredData);
  };

  const clearFilter = () => {
    setFilters({
      name: "",
      code: "",
      state: "",
    });
    setFiltered(brands);
  };

  const handleAddChange = (e) => {
    const { name, value, type } = e.target;
    let val = value;

    if (type === "number") {
      val = value === "" ? 0 : Number(value);
    } else if (name === "state") {
      val = value === "true";
    }

    setNewBrand({ ...newBrand, [name]: val });
  };

  const handleAddBrand = async () => {
    if (!newBrand.name || newBrand.code === "") {
      alert("Lütfen gerekli alanları doldurun.");
      return;
    }

    try {
      setLoading(true);
      const res = await axios.post("http://localhost:5432/api/brand", {
        name: newBrand.name,
        code: newBrand.code,
        description: newBrand.description,
        state: Boolean(newBrand.state),
      });

      console.log("Yeni marka eklendi:", res.data);
      await fetchBrands();
      setShowAddPanel(false);
      setNewBrand({
        name: "",
        code: 0,
        description: "",
        state: true,
      });
      alert("Marka başarıyla eklendi!");
    } catch (error) {
      console.error("Marka ekleme hatası:", error);
      alert("Marka eklenemedi.");
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Yükleniyor...</div>;

  return (
      <div className="brandsPages">
        <div className="brands-header" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
          <h2>BRANDS</h2>
          <button onClick={() => setShowAddPanel(!showAddPanel)}>
            {showAddPanel ? "Close" : "Add"}
          </button>
        </div>

        {showAddPanel && (
            <div className="add-brand-panel" style={{ margin: "20px 0", padding: "15px", border: "1px solid #ccc", borderRadius: "10px", background: "#fafafa" }}>
              <input
                  type="text"
                  name="name"
                  placeholder="Name"
                  value={newBrand.name}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <input
                  type="number"
                  name="code"
                  placeholder="Code"
                  value={newBrand.code}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <input
                  type="text"
                  name="description"
                  placeholder="Description"
                  value={newBrand.description}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <select
                  name="state"
                  value={newBrand.state.toString()}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              >
                <option value="true">Active</option>
                <option value="false">Inactive</option>
              </select>
              <button onClick={handleAddBrand} disabled={loading}>
                {loading ? "Adding..." : "Add Brand"}
              </button>
            </div>
        )}

        <div className="filter-bar" style={{ display: "flex", gap: "10px", marginBottom: "20px" }}>
          <input
              type="text"
              placeholder="Name"
              name="name"
              value={filters.name}
              onChange={handleFilterChange}
          />
          <input
              type="number"
              placeholder="Code"
              name="code"
              value={filters.code}
              onChange={handleFilterChange}
          />
          <select name="state" value={filters.state} onChange={handleFilterChange}>
            <option value="">All</option>
            <option value="true">Active</option>
            <option value="false">Inactive</option>
          </select>
          <button onClick={handleFilter}>Filter</button>
          <button onClick={clearFilter}>Clear</button>
        </div>

        <table className="brands-table">
          <thead>
          <tr>
            <th>Name</th>
            <th>Code</th>
            <th>Description</th>
            <th>State</th>
          </tr>
          </thead>
          <tbody>
          {filtered.map((b) => (
              <tr key={b.id}>
                <td>{b.name}</td>
                <td>{b.code}</td>
                <td>{b.description}</td>
                <td>{b.state ? "Active" : "Inactive"}</td>
              </tr>
          ))}
          </tbody>
        </table>
      </div>
  );
};

export default Brands;
