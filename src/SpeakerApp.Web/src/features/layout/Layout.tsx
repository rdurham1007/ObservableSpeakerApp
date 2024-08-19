import React, { ReactNode } from "react";
import { Link } from "react-router-dom";

// Define the props type for the Layout component
interface LayoutProps {
    children: ReactNode;
  }

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div>
      <header>
        <div className="navbar navbar-dark bg-dark box-shadow">
          <div className="container d-flex justify-content-between">
            <Link to="/" className="navbar-brand d-flex align-items-center">
              <strong>SpeakerApp</strong>
            </Link>
            <nav className="nav justify-content-end">
              <Link to="/speakers" className="nav-link">Speakers</Link>
              <Link to="/talks" className="nav-link">Talks</Link>
            </nav>
          </div>
        </div>
      </header>

      <div className="container my-5">{children}</div>
    </div>
  );
};

export default Layout;
