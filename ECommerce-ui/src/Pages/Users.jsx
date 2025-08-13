import { useState, useEffect } from "react";
import axios from "axios";

const Users = () => {
  const [users, setUsers] = useState([]);
  const [filtered, setFiltered] = useState([]);
  const [loading, setLoading] = useState(false);

  const [filters, setFilters] = useState({
    name: "",
    email: "",
    userType: "",
    code: "",
    state: "",
  });

  const [newUser, setNewUser] = useState({
    name: "",
    eMail: "",
    phoneNumber: "",
    userType: "",
    code: 0,
    state: true,
  });

  const [showAddPanel, setShowAddPanel] = useState(false);
  const [userTypes, setUserTypes] = useState([]);

  const fetchUsers = async () => {
    setLoading(true);
    try {
      const res = await axios.get("http://localhost:5083/api/user/enum-all");
      setUsers(res.data);
      setFiltered(res.data);
    } catch (error) {
      console.error("Kullanıcılar çekilemedi:", error);
      alert("Kullanıcı listesi yüklenemedi.");
    } finally {
      setLoading(false);
    }
  };

  const fetchUserTypes = async () => {
    try {
      const res = await axios.get("http://localhost:5083/api/user/usertypes"); // enum endpoint'in varsa
      setUserTypes(res.data);
    } catch (error) {
      console.error("User type alınamadı:", error);
      setUserTypes(["Admin", "Customer", "Employee"]); // fallback değerler
    }
  };

  useEffect(() => {
    fetchUsers();
    fetchUserTypes();
  }, []);

  const handleFilterChange = (e) => {
    setFilters({ ...filters, [e.target.name]: e.target.value });
  };

  const handleFilter = () => {
    const filteredData = users.filter((u) => {
      const matchesName = u.name?.toLowerCase().includes(filters.name.toLowerCase()) || false;
      const matchesEmail = u.eMail?.toLowerCase().includes(filters.email.toLowerCase()) || false;
      const matchesUserType = filters.userType === "" || u.userType === filters.userType;
      const matchesCode = filters.code === "" || u.code.toString() === filters.code;
      const matchesState = filters.state === "" || u.state.toString() === filters.state;
      return matchesName && matchesEmail && matchesUserType && matchesCode && matchesState;
    });

    setFiltered(filteredData);
  };

  const clearFilter = () => {
    setFilters({
      name: "",
      email: "",
      userType: "",
      code: "",
      state: "",
    });
    setFiltered(users);
  };

  const handleAddChange = (e) => {
    const { name, value, type } = e.target;
    let val = value;

    if (type === "number") {
      val = value === "" ? 0 : Number(value);
    } else if (name === "state") {
      val = value === "true";
    }

    setNewUser({ ...newUser, [name]: val });
  };

  const handleAddUser = async () => {
    if (!newUser.name || !newUser.eMail || !newUser.userType || newUser.code === "") {
      alert("Lütfen gerekli alanları doldurun.");
      return;
    }

    try {
      setLoading(true);
      const res = await axios.post("http://localhost:5083/api/user", {
        name: newUser.name,
        eMail: newUser.eMail,
        phoneNumber: newUser.phoneNumber,
        userType: newUser.userType,
        code: newUser.code,
        state: Boolean(newUser.state),
      });

      console.log("Yeni kullanıcı eklendi:", res.data);
      await fetchUsers();
      setShowAddPanel(false);
      setNewUser({
        name: "",
        eMail: "",
        phoneNumber: "",
        userType: "",
        code: 0,
        state: true,
      });
      alert("Kullanıcı başarıyla eklendi!");
    } catch (error) {
      console.error("Kullanıcı ekleme hatası:", error);
      alert("Kullanıcı eklenemedi.");
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Yükleniyor...</div>;

  return (
      <div className="usersPages">
        <div className="users-header" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
          <h2>USERS</h2>
          <button onClick={() => setShowAddPanel(!showAddPanel)}>
            {showAddPanel ? "Close" : "Add"}
          </button>
        </div>

        {showAddPanel && (
            <div className="add-user-panel" style={{ margin: "20px 0", padding: "15px", border: "1px solid #ccc", borderRadius: "10px", background: "#fafafa" }}>
              <input
                  type="text"
                  name="name"
                  placeholder="Name"
                  value={newUser.name}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <input
                  type="email"
                  name="eMail"
                  placeholder="Email"
                  value={newUser.eMail}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <input
                  type="text"
                  name="phoneNumber"
                  placeholder="Phone Number"
                  value={newUser.phoneNumber}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <select
                  name="userType"
                  value={newUser.userType}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              >
                <option value="">Select User Type</option>
                {userTypes.map((type) => (
                    <option key={type} value={type}>{type}</option>
                ))}
              </select>
              <input
                  type="number"
                  name="code"
                  placeholder="Code"
                  value={newUser.code}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              />
              <select
                  name="state"
                  value={newUser.state.toString()}
                  onChange={handleAddChange}
                  style={{ marginRight: 10 }}
              >
                <option value="true">Active</option>
                <option value="false">Inactive</option>
              </select>
              <button onClick={handleAddUser} disabled={loading}>
                {loading ? "Adding..." : "Add User"}
              </button>
            </div>
        )}

        <div className="filter-bar" style={{ display: "flex", gap: "10px", marginBottom: "20px", flexWrap: "wrap" }}>
          <input
              type="text"
              placeholder="Name"
              name="name"
              value={filters.name}
              onChange={handleFilterChange}
          />
          <input
              type="text"
              placeholder="Email"
              name="email"
              value={filters.email}
              onChange={handleFilterChange}
          />
          <select name="userType" value={filters.userType} onChange={handleFilterChange}>
            <option value="">All User Types</option>
            {userTypes.map((type) => (
                <option key={type} value={type}>{type}</option>
            ))}
          </select>
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

        <table className="users-table">
          <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>User Type</th>
            <th>Code</th>
            <th>State</th>
          </tr>
          </thead>
          <tbody>
          {filtered.map((u) => (
              <tr key={u.id}>
                <td>{u.name}</td>
                <td>{u.eMail}</td>
                <td>{u.phoneNumber}</td>
                <td>{u.userType}</td>
                <td>{u.code}</td>
                <td>{u.state ? "Active" : "Inactive"}</td>
              </tr>
          ))}
          </tbody>
        </table>
      </div>
  );
};

export default Users;
