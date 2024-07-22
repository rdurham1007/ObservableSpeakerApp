import React from "react";
import { Link } from "react-router-dom";
import Layout from "../layout/Layout";

const HomePage: React.FC = () => {
  return (
    <Layout>
      <div className="container my-5">
        <div className="jumbotron">
          <h1 className="display-4">Welcome to My Bootstrap App!</h1>
          <p className="lead">
            This is a simple hero unit, a simple jumbotron-style component for
            calling extra attention to featured content or information.
          </p>
          <hr className="my-4" />
          <p>
            It uses utility classes for typography and spacing to space content
            out within the larger container.
          </p>
          <Link
            to="/learn-more"
            className="btn btn-primary btn-lg"
            role="button"
          >
            Learn more
          </Link>
        </div>
      </div>
    </Layout>
  );
};

export default HomePage;
