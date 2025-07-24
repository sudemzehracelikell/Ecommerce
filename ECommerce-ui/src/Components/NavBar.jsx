import { RiMenuLine, RiCloseLine } from "react-icons/ri";
import { useMainPanel } from "../Contexts/MainPanelContext";

const NavBar = () => {

    const {sideBarOpen, toggleSideBar} = useMainPanel();
    return(
        <nav className="navbar">
            <button onClick={toggleSideBar}>
                {sideBarOpen ? <RiCloseLine size={25} /> : <RiMenuLine size={25} />}
            </button>
            <h1>NAVBAR</h1>
        </nav>
    );
}

export default NavBar;