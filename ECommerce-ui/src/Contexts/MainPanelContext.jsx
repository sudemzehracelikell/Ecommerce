import { createContext, useContext, useState } from "react";


const MainPanelContext = createContext();

export const useMainPanel = () =>{

    const context = useContext(MainPanelContext);
    if (!context) {
        throw new Error('useMainPanel must be used within MainPanelProvider');
    }
    return context;
};

export const MainPanelProvider = ({children}) => {

    const [sideBarOpen, setSideBarOpen] = useState(true);

    const toggleSideBar = () =>{
        setSideBarOpen(!sideBarOpen);
    };

    const value = {
        sideBarOpen,
        setSideBarOpen,
        toggleSideBar,
    };

    return(
        <MainPanelContext.Provider value={value}>
            {children}
        </MainPanelContext.Provider>
    );

};