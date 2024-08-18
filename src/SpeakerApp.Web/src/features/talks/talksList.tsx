import React, { useEffect, useState } from "react";
import { Talk } from "./talk";
import { getTalks } from "./talksService";

interface TalksListProps {
  speakerId?: string;
  showSpeaker?: boolean;
}

const TalksList: React.FC<TalksListProps> = ({ speakerId, showSpeaker = false }) => {
  const [talks, setTalks] = useState<Talk[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    // Fetch talks data based on speakerId
    // Replace this with your actual API call or data retrieval logic
    const fetchTalks = async () => {
      try {
        const response = await getTalks(speakerId);
        setTalks(response);
      } catch (error) {
        setError(error as Error);
      } finally {
        setLoading(false);
      }
    };

    fetchTalks();
  }, [speakerId, showSpeaker]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <div>
      <div className="row">
        {talks.map((talk) => (
          <div className="col-md-4" key={talk.id}>
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{talk.title}</h5>
                <p className="card-text">{talk.abstract}</p>
              </div>

              {showSpeaker && (
                <div className="card-footer">
                  Presenter:{" "}
                  <a href={`/speakers/${talk.speakerId}`}>{talk.speakerName}</a>
                </div>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default TalksList;
