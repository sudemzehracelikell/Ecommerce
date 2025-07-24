import { RiUser3Line, RiHome6Line, RiProductHuntLine,  RiStore2Line, RiLayout2Line } from "react-icons/ri";
import { Link } from "react-router-dom";
import { useMainPanel } from "../Contexts/MainPanelContext";


const SideBar = () => {

    const {sideBarOpen} = useMainPanel();

    return(
        <aside className={`sidebar ${sideBarOpen ? "open" : "closed"}`}>
            <ul>
                <li>
                    <Link to="/dashboard">
                    <RiHome6Line size={20} style={{ marginRight: "12px" }} />
                    HOME
                    </Link>
                </li>
                <li>
                    <Link to="/dashboard/users">
                    <RiUser3Line size={20} style={{ marginRight: "12px" }} />
                    USERS
                    </Link>
                </li>
                <li>
                    <Link to="/dashboard/products">
                    <RiProductHuntLine size={20} style={{ marginRight: "12px" }} />
                    PRODUCTS
                    </Link>
                </li>
                <li>
                    <Link to="/dashboard/brands">
                    <RiStore2Line size={20} style={{ marginRight: "12px" }} />
                    BRANDS
                    </Link>
                </li>
                <li>
                    <Link to="/dashboard/categories">
                    <RiLayout2Line size={20} style={{ marginRight: "12px" }} />
                    CATEGORIES
                    </Link>
                </li>
            </ul>
        </aside>
    );
};

export default SideBar;