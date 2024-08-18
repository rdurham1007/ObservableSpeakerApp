import React, { useState, useEffect } from 'react';
import { getSpeakers, addSpeaker } from './speakerService';
import { Speaker } from './speaker';
import AddSpeakerModal from './addSpeakerModal';

const SpeakerList: React.FC = () => {
  const [speakers, setSpeakers] = useState<Speaker[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);
  const [showModal, setShowModal] = useState<boolean>(false);

  useEffect(() => {
    
    const fetchSpeakers = async () => {
      try {
        const data = await getSpeakers();
        setSpeakers(data);
      } catch (error) {
        setError(error as Error);
      } finally {
        setLoading(false);
      }
    };

    fetchSpeakers();
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  const handleAddSpeaker = async (speaker: Speaker) => {
    try {      
      speaker.createdAt = new Date().toISOString();
      const newSpeaker = await addSpeaker(speaker);
      setSpeakers([...speakers, newSpeaker]);
      setShowModal(false);
    } catch (error) {
      setError(error as Error);
    }
  };

  return (
    <div>
      <h1>Speakers</h1>
      <button className="btn btn-primary" style={{ float: 'right' }} onClick={() => setShowModal(true)}>Add Speaker</button>
      <AddSpeakerModal show={showModal} onClose={() => setShowModal(false)} onSave={handleAddSpeaker} />
      <div className="row">
        {speakers.map(speaker => (
          <div className="col-md-4" key={speaker.id}>
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{speaker.firstName} {speaker.lastName}</h5>
                <p className="card-text">{speaker.bio}</p>
              </div>
              <div className="card-footer">
                <a href={`/speakers/${speaker.id}`}>View Talks</a>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default SpeakerList;
