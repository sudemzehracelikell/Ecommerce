import { useState, useEffect } from "react";
import axios from "axios";

const Categories = () => {
  const [categories, setCategories] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [loading, setLoading] = useState(false);

  const [filters, setFilters] = useState({
    name: "",
    code: "",
    state: "",
  });

  const [newCategory, setNewCategory] = useState({
    name: "",
    code: 0,
    description: "",
    state: true,
  });

  const [showAddPanel, setShowAddPanel] = useState(false);

  const fetchCategories = async () => {
    setLoading(true);
    try {
      const res = await axios.get("http://localhost:5432/api/category/enum-all");
      setCategories(res.data);
      setFiltered(res.data);
    } catch (error) {
      console.error("Kategori verileri alınamadı:", error);
      alert("Kategori listesi yüklenirken hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const handleFilterChange = (e) => {
    setFilters({ ...filters, [e.target.name]: e.target.value });
  };

  const handleFilter = () => {
    const filteredData = categories.filter((c) => {
      const matchesName = c.name?.toLowerCase().includes(filters.name.toLowerCase()) || false;
      const matchesCode = filters.code === "" || c.code.toString() === filters.code;
      const matchesState = filters.state === "" || c.state.toString() === filters.state;
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
    setFiltered(categories);
  };

  const handleAddChange = (e) => {
    const { name, value, type } = e.target;
    let val = value;

    if (type === "number") {
      val = value === "" ? 0 : Number(value);
    } else if (name === "state") {
      val = value === "true";
    }

    setNewCategory({ ...newCategory, [name]: val });
  };

  const handleAddCategory = async () => {
    if (!newCategory.name || newCategory.code === "") {
      alert("Lütfen gerekli alanları doldurun.");
      return;
    }

    try {
      setLoading(true);
      const res = await axios.post("http://localhost:5432/api/category", {
        name: newCategory.name,
        code: newCategory.code,
        description: newCategory.description,
        state: Boolean(newCategory.state),
      });

      console.log("Yeni kategori eklendi:", res.data);
      await fetchCategories();
      setShowAddPanel(false);
      setNewCategory({
        name: "",
        code: 0,
        description: "",
        state: true,
      });
      alert("Kategori başarıyla eklendi!");
    } catch (error) {
      console.error("Kategori ekleme hatası:", error);
      alert("Kategori eklenemedi.");
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Yükleniyor...</div>;

  return (
      <div className="categoriesPages">
        <div className="categories-header" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
          <h2>CATEGORIES</h2>
          <button onClick={() => setShowAddPanel(!showAddPanel)}>
            {showAddPanel ? "Close" : "Add"}
          </button>
        </div>

        {showAddPanel && (
            <div className="add-category-panel" style={{ margin: "20px 0", padding: "15px", border: "1px solid #ccc", borderRadius: "10px", background: "#fafafa" }}>
              <input
                  type="text"
                  name="name"
                  placeholder="Name"
                  value={newCategory.name}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <input
                  type="number"
                  name="code"
                  placeholder="Code"
                  value={newCategory.code}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <input
                  type="text"
                  name="description"
                  placeholder="Description"
                  value={newCategory.description}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <select
                  name="state"
                  value={newCategory.state.toString()}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              >
                <option value="true">Active</option>
                <option value="false">Inactive</option>
              </select>
              <button onClick={handleAddCategory} disabled={loading}>
                {loading ? "Adding..." : "Add Category"}
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

        <table className="categories-table">
          <thead>
          <tr>
            <th>Name</th>
            <th>Code</th>
            <th>Description</th>
            <th>State</th>
          </tr>
          </thead>
          <tbody>
          {filtered.map((c) => (
              <tr key={c.id}>
                <td>{c.name}</td>
                <td>{c.code}</td>
                <td>{c.description}</td>
                <td>{c.state ? "Active" : "Inactive"}</td>
              </tr>
          ))}
          </tbody>
        </table>
      </div>
  );
};

export default Categories;
