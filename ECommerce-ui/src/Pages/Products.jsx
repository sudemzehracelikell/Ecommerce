import { useState, useEffect } from "react";
import axios from "axios";

const Products = () => {
  const [products, setProducts] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [brands, setBrands] = useState([]);
  const [categories, setCategories] = useState([]); 
  const [loading, setLoading] = useState(false);
  
  const [filters, setFilters] = useState({
    name: "",
    category: "",
    brand: "",
    minPrice: "",
    maxPrice: "",
    minStock: "",
    maxStock: "",
  });

  const [newProduct, setNewProduct] = useState({
    code: 0,
    name: "",
    brandId: "", 
    kdv: 0,
    basePrice: 0,
    stock: 0,
    state: true,
    description: "",
  });

  const [showAddPanel, setShowAddPanel] = useState(false);

  
  const fetchData = async () => {
    setLoading(true);
    try {
      console.log("API çağrısı başlatılıyor...");
      
      console.log("Products çekiliyor...");
      const productsRes = await axios.get("http://localhost:5083/api/product/enum-all");
      console.log("Products başarılı:", productsRes.data);
      
      if (productsRes.data.length > 0) {
        console.log("İlk product'ın key'leri:", Object.keys(productsRes.data[0]));
        console.log("İlk product detayı:", productsRes.data[0]);
      }
      
      setProducts(productsRes.data);
      setFiltered(productsRes.data);

      console.log("Brands çekiliyor...");
      try {
        const brandsRes = await axios.get("http://localhost:5083/api/brand/enum-all");
        console.log("Brands response:", brandsRes);
        console.log("Brands data:", brandsRes.data);
        console.log("Brands type:", typeof brandsRes.data);
        console.log("Brands length:", brandsRes.data?.length);
        setBrands(brandsRes.data || []);
      } catch (brandError) {
        console.error("Brand error:", brandError);
        console.error("Brand error response:", brandError.response?.data);
        console.error("Brand error status:", brandError.response?.status);
        setBrands([]); // Boş array set et
      }

      console.log("Tüm veriler başarıyla yüklendi!");

    } catch (error) {
      console.error("Detaylı hata:", error);
      console.error("Hata mesajı:", error.message);
      console.error("Response:", error.response?.data);
      console.error("Status:", error.response?.status);
      
      alert(`Veriler yüklenirken hata oluştu: ${error.message}`);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleFilterChange = (e) => {
    setFilters({ ...filters, [e.target.name]: e.target.value });
  };

  const handleFilter = () => {
    const filteredData = products.filter((p) => {
      const matchesName = p.name?.toLowerCase().includes(filters.name.toLowerCase()) || false;
      
      const matchesCategory = filters.category === "" || true; // Şimdilik skip
      const matchesBrand = filters.brand === "" || p.brand?.name === filters.brand;

      const minPrice = parseFloat(filters.minPrice) || 0;
      const maxPrice = parseFloat(filters.maxPrice) || Number.MAX_SAFE_INTEGER;
      const matchesPrice = p.basePrice >= minPrice && p.basePrice <= maxPrice;

      const minStock = parseInt(filters.minStock) || 0;
      const maxStock = parseInt(filters.maxStock) || Number.MAX_SAFE_INTEGER;
      const matchesStock = p.stock >= minStock && p.stock <= maxStock;

      return matchesName && matchesCategory && matchesBrand && matchesPrice && matchesStock;
    });
    setFiltered(filteredData);
  };

  const clearFilter = () => {
    setFilters({
      name: "",
      category: "",
      brand: "",
      minPrice: "",
      maxPrice: "",
      minStock: "",
      maxStock: "",
    });
    setFiltered(products);
  };

  const handleAddChange = (e) => {
    const { name, value, type } = e.target;

    let val = value;
    if (type === "number") {
      val = value === "" ? 0 : Number(value);
    } else if (name === "state") {
      val = value === "true";
    }

    setNewProduct({ ...newProduct, [name]: val });
  };

  const handleAddProduct = async () => {
    if (
      !newProduct.code ||
      !newProduct.name ||
      !newProduct.brandId ||
      newProduct.kdv === "" ||
      newProduct.basePrice === "" ||
      newProduct.stock === ""
    ) {
      alert("Lütfen tüm zorunlu alanları doldurun!");
      return;
    }

    try {
      setLoading(true);
      
     
      const response = await axios.post("http://localhost:5083/api/product", {
        code: parseInt(newProduct.code),
        name: newProduct.name,
        brandId: parseInt(newProduct.brandId),
        kdv: parseFloat(newProduct.kdv),
        basePrice: parseFloat(newProduct.basePrice),
        stock: parseInt(newProduct.stock),
        state: Boolean(newProduct.state),
        description: newProduct.description
      });

      console.log("Yeni ürün eklendi:", response.data);
      
      
      await fetchData();
      
      
      setNewProduct({
        code: 0,
        name: "",
        brandId: "",
        kdv: 0,
        basePrice: 0,
        stock: 0,
        state: true,
        description: "",
      });
      
      setShowAddPanel(false);
      alert("Ürün başarıyla eklendi!");

    } catch (error) {
      console.error("Ürün ekleme hatası:", error);
      alert("Ürün eklenirken hata oluştu!");
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div>Yükleniyor...</div>;
  }

  return (
    <div className="productsPages">
      <div className="products-header" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <h2>PRODUCTS</h2>
        <button className="addButton" onClick={() => setShowAddPanel(!showAddPanel)}>
          {showAddPanel ? "Close" : "Add"}
        </button>
      </div>

      {showAddPanel && (
        <div className="add-product-panel" style={{ margin: "20px 0", padding: "15px", border: "1px solid #ccc", borderRadius: "10px", background: "#fafafa" }}>
          <input
            type="number"
            name="code"
            placeholder="Code"
            value={newProduct.code}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          />
          <input
            type="text"
            name="name"
            placeholder="Name"
            value={newProduct.name}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          />
          <select
            name="brandId"
            value={newProduct.brandId}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          >
            <option value="">Select Brand</option>
            {brands.map((brand) => (
              <option key={brand.id} value={brand.id}>{brand.name}</option>
            ))}
          </select>
          <input
            type="number"
            name="kdv"
            placeholder="KDV"
            step="0.01"
            value={newProduct.kdv}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          />
          <input
            type="number"
            name="basePrice"
            placeholder="Base Price"
            step="0.01"
            value={newProduct.basePrice}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          />
          <input
            type="number"
            name="stock"
            placeholder="Stock"
            value={newProduct.stock}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          />
          <select
            name="state"
            value={newProduct.state.toString()}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          >
            <option value="true">Active</option>
            <option value="false">Inactive</option>
          </select>
          <input
            type="text"
            name="description"
            placeholder="Description"
            value={newProduct.description}
            onChange={handleAddChange}
            style={{ marginRight: 10 }}
          />
          <button onClick={handleAddProduct} disabled={loading}>
            {loading ? "Adding..." : "Add Product"}
          </button>
        </div>
      )}

      <div className="filter-bar" style={{ display: "flex", flexWrap: "wrap", gap: "10px", marginBottom: "25px" }}>
        <input
          type="text"
          placeholder="Name"
          name="name"
          value={filters.name}
          onChange={handleFilterChange}
          style={{ minWidth: 150 }}
        />
        {/* Category filtreleme - ProductCategory mantığına göre düzenlenecek */}
        {/* <select name="category" value={filters.category} onChange={handleFilterChange} style={{ minWidth: 150 }}>
          <option value="">All Categories</option>
          {categories.map((cat) => (
            <option key={cat.id} value={cat.name}>{cat.name}</option>
          ))}
        </select> */}
        <select name="brand" value={filters.brand} onChange={handleFilterChange} style={{ minWidth: 150 }}>
          <option value="">All Brands</option>
          {brands.map((brand) => (
            <option key={brand.id} value={brand.name}>{brand.name}</option>
          ))}
        </select>
        <input
          type="number"
          name="minPrice"
          placeholder="Min Price"
          value={filters.minPrice}
          onChange={handleFilterChange}
          style={{ width: 100 }}
        />
        <input
          type="number"
          name="maxPrice"
          placeholder="Max Price"
          value={filters.maxPrice}
          onChange={handleFilterChange}
          style={{ width: 100 }}
        />
        <input
          type="number"
          name="minStock"
          placeholder="Min Stock"
          value={filters.minStock}
          onChange={handleFilterChange}
          style={{ width: 100 }}
        />
        <input
          type="number"
          name="maxStock"
          placeholder="Max Stock"
          value={filters.maxStock}
          onChange={handleFilterChange}
          style={{ width: 100 }}
        />
        <button className="filterButton" onClick={handleFilter}>Filter</button>
        <button className="clearButton" onClick={clearFilter}>Clear</button>
      </div>

      <table className="products-table">
        <thead>
          <tr>
            <th>Code</th>
            <th>Name</th>
            <th>Brand</th>
            <th>KDV</th>
            <th>Base Price</th>
            <th>Stock</th>
            <th>State</th>
            <th>Description</th>
          </tr>
        </thead>
        <tbody>
          {filtered.map((p) => {
            // Brand ID'ye göre brand ismini bul
            const brand = brands.find(b => b.id === p.brandId);
            
            return (
              <tr key={p.id}>
                <td>{p.code}</td>
                <td>{p.name}</td>
                <td>{brand?.name || "—"}</td>
                <td>{p.kdv}</td>
                <td>{p.basePrice}</td>
                <td>{p.stock}</td>
                <td>{p.state ? "Active" : "Inactive"}</td>
                <td>{p.description}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default Products;