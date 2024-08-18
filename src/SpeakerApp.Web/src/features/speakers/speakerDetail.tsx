import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getSpeakers } from "./speakerService";
import { Speaker } from "./speaker";
import Layout from "../layout/Layout";
import TalksList from "../talks/TalksList";

interface SpeakerDetailProps {}

const SpeakerDetail: React.FC<SpeakerDetailProps> = () => {
  const { id } = useParams<{ id: string }>();
  const [speaker, setSpeaker] = useState<Speaker | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    const fetchSpeaker = async () => {
      try {
        const data = await getSpeakers();
        console.log(data);
        console.log(id);
        const foundSpeaker = data.find((s) => s.id === id);
        setSpeaker(foundSpeaker || null);
      } catch (error) {
        setError(error as Error);
      } finally {
        setLoading(false);
      }
    };

    fetchSpeaker();
  }, [id]);

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
    <Layout>
      <div>
        <h1>
          {speaker.firstName} {speaker.lastName}
        </h1>
        <p>{speaker.bio}</p>
      </div>
      <div>
        <h2>Talks</h2>
        <TalksList speakerId={speaker.id} />
      </div>
    </Layout>
  );
};

export default SpeakerDetail;
