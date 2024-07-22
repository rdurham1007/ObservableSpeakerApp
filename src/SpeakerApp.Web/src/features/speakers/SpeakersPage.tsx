import React from 'react';
import SpeakerList from './speakerList';
import Layout from '../layout/Layout';

const SpeakersPage: React.FC = () => {
  return (
    <Layout>
      <SpeakerList />
    </Layout>
  );
};

export default SpeakersPage;
