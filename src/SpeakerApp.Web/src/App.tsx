import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SpeakersPage from './features/speakers/SpeakersPage';
import SpeakerDetailPage from './features/speakers/speakerDetail';
// import HomePage from './features/home/HomePage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/speakers" element={<SpeakersPage />} />
        <Route path="/speakers/:id" element={<SpeakerDetailPage />} />
        <Route path="/" element={<SpeakersPage />} />
      </Routes>
    </Router>    
  );
};

export default App;
