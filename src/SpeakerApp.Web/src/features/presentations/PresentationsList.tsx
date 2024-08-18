import React, { useState, useEffect } from 'react';
import { Presentation } from './Presentation';
import { getPresentations } from './PresentationService';

const PresentationList: React.FC = () => {

  const [presentations, setPresentations] = useState<Presentation[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | null>(null);

  useEffect(() => {
    
    const fetchSpeakers = async () => {
      try {
        const data = await getPresentations();
        setPresentations(data);
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
      <div className="row">
        {presentations.map(presentation => (
          <div className="col-md-4" key={presentation.id}>
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{presentation.title}</h5>
                <h6 className="card-subtitle mb-2 text-muted">Presenter: <a href={`/speakers/${presentation.speakerId}`}>{presentation.speakerName}</a></h6>
                <p className="card-text">{presentation.abstract}</p>
              </div>
              <div className="card-footer">
                <div className="float-start">
                    {presentation.location}
                </div>
                <div className="float-end">
                    { new Date(presentation.scheduledAt).toLocaleString('en-US') }
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default PresentationList;
