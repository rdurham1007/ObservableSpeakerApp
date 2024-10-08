import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SpeakersPage from './features/speakers/SpeakersPage';
import SpeakerDetailPage from './features/speakers/speakerDetail';
import TalksPage from './features/talks/TalksPage';
import CreateTalkPage from './features/talks/CreateTalkPage';
// import HomePage from './features/home/HomePage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/talks" element={<TalksPage />} />
        <Route path="/talks/create" element={<CreateTalkPage />} />
        <Route path="/speakers" element={<SpeakersPage />} />
        <Route path="/speakers/:id" element={<SpeakerDetailPage />} />
        <Route path="/" element={<SpeakersPage />} />
      </Routes>
    </Router>    
  );
};

export default App;
