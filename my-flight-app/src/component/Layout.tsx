interface Props {
    children: JSX.Element;
}
const Layout: React.FC<Props> = ({ children}) => {
    return (
        <div className="container">
            {children}
        </div>
    )
}

export default Layout;