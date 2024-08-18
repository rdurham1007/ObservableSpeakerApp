import React from 'react';
import Layout from '../layout/Layout';
import PresentationList from './PresentationsList';

const PresentationsPage: React.FC = () => {
  return (
    <Layout>
        <h2>Presentations</h2>
        <PresentationList />
    </Layout>
  );
};

export default PresentationsPage;
