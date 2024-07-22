import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SpeakersPage from './features/speakers/SpeakersPage';
import HomePage from './features/home/HomePage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/speakers" element={<SpeakersPage/>} />
        {/* <Route path="/talks" component={TalksPage} />
        <Route path="/meetings" component={MeetingsPage} /> */}
        <Route path="/" element={<HomePage />} />
      </Routes>
    </Router>    
  );
};

export default App;
