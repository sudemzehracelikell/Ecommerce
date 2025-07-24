import { useMainPanel } from "../Contexts/MainPanelContext";

const Content = ({children}) => {
    
    const {sideBarOpen} = useMainPanel();

    return(
        <main className={`page-content ${sideBarOpen ? 'shifted' : 'unshifted'}`}>
            <div className="content-component">
                {children}
            </div>
        </main>
    );

};

export default Content;