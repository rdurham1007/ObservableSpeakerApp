import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SpeakersPage from './features/speakers/SpeakersPage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/speakers" element={<SpeakersPage/>} />
        {/* <Route path="/talks" component={TalksPage} />
        <Route path="/meetings" component={MeetingsPage} /> */}
      </Routes>
    </Router>
  );
};

export default App;
