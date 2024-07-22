import React, { useState, useEffect } from 'react';
import { getSpeakers } from './speakerService';
import { Speaker } from './speaker';

const SpeakerList: React.FC = () => {
  const [speakers, setSpeakers] = useState<Speaker[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

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

  return (
    <div>
      <h1>Speakers</h1>
      <ul>
        {speakers.map(speaker => (
          <li key={speaker.id}>{speaker.firstName} {speaker.lastName}</li>
        ))}
      </ul>
    </div>
  );
};

export default SpeakerList;
