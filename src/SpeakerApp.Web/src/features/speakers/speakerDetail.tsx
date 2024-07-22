import React, { useState, useEffect } from 'react';
import { getSpeakers } from './speakerService';
import { Speaker } from './speaker';

interface SpeakerDetailProps {
  speakerId: string;
}

const SpeakerDetail: React.FC<SpeakerDetailProps> = ({ speakerId }) => {
  const [speaker, setSpeaker] = useState<Speaker | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const fetchSpeaker = async () => {
      try {
        const data = await getSpeakers();
        const foundSpeaker = data.find(s => s.id === speakerId);
        setSpeaker(foundSpeaker || null);
      } catch (error) {
        setError(error as Error);
      } finally {
        setLoading(false);
      }
    };

    fetchSpeaker();
  }, [speakerId]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (!speaker) {
    return <div>Speaker not found</div>;
  }

  return (
    <div>
      <h1>{speaker.firstName} {speaker.lastName}</h1>
      <p>{speaker.bio}</p>
    </div>
  );
};

export default SpeakerDetail;
