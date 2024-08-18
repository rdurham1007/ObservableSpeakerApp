import React, { useState, useEffect } from "react";
import { Form } from "react-bootstrap";
import { getSpeakers } from "../speakers/speakerService";
import { Speaker } from "../speakers/speaker";
import CreateSpeakerForm from "../speakers/CreateSpeakerForm";
import { Talk } from "./talk";

interface CreateTalkFormProps {
  talkData: Talk;
  onTalkChange: (updatedTalk: Talk, updatedSpeaker: Speaker) => void;
}

const CreateTalkForm: React.FC<CreateTalkFormProps> = ({
  talkData,
  onTalkChange,
}) => {
  const [speakers, setSpeakers] = useState<Speaker[]>([]);
  const [isCreatingSpeaker, setCreateSpeaker] = useState(false);
  const [newSpeaker, setNewSpeaker] = useState<Speaker>({
    id: "",
    firstName: "",
    lastName: "",
    bio: "",
    email: ""
  });

  const handleSpeakerChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    if (e.target.value === "Create") {
      setCreateSpeaker(true);
    } else {
      setCreateSpeaker(false);
    }

    const speaker = speakers.find((speaker) => speaker.id === e.target.value);

    onTalkChange(
      {
        ...talkData,
        speakerId: speaker?.id || e.target.value,
        speakerName: speaker?.firstName + " " + speaker?.lastName || "",
      },
      speaker || newSpeaker
    );
  };

  const handleNewSpeakerChange = (updatedSpeaker: Speaker) => {
    
    setNewSpeaker(updatedSpeaker);
    
    onTalkChange(
        {
          ...talkData,
          speakerId: 'Created',
          speakerName: updatedSpeaker?.firstName + " " + updatedSpeaker?.lastName || "",
        },
        updatedSpeaker
      );
  }

  const handleTalkchange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;

    const speaker = speakers.find(
      (speaker) => speaker.id === talkData.speakerId
    );

    onTalkChange(
      {
        ...talkData,
        [name]: value,
      },
      speaker || newSpeaker
    );
  };

  useEffect(() => {
    const fetchSpeakers = async () => {
      const speakers = await getSpeakers();
      setSpeakers(speakers);
    };

    fetchSpeakers();
  }, []);

  return (
    <div>
      <Form>
        <div className="mb-3">
          <Form.Group controlId="formTitle">
            <Form.Label>Title</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter title"
              onChange={handleTalkchange}
              value={talkData.title}
              name="title"
            />
          </Form.Group>
        </div>
        <div className="mb-3">
          <Form.Group controlId="formAbstract">
            <Form.Label>Abstract</Form.Label>
            <Form.Control
              as="textarea"
              rows={4}
              placeholder="Enter abstract"
              onChange={handleTalkchange}
              value={talkData.abstract}
              name="abstract"
            />
          </Form.Group>
        </div>
        <div className="mb-3">
          <Form.Group controlId="formSpeaker">
            <Form.Label>Speaker</Form.Label>
            <Form.Control
              as="select"
              value={talkData.speakerId}
              onChange={handleSpeakerChange}
              name="speakerId"
            >
              <option value="">Select a speaker</option>
              <option value="Create">Create New Speaker</option>

              {speakers.map((speaker) => (
                <option key={speaker.id} value={speaker.id}>
                  {speaker.firstName} {speaker.lastName}
                </option>
              ))}
            </Form.Control>
          </Form.Group>
        </div>
      </Form>
        {isCreatingSpeaker && (
          <div>
            <h5>Create Speaker</h5>
            <CreateSpeakerForm
              speakerData={newSpeaker}
              onSpeakerChange={handleNewSpeakerChange}
            />
          </div>
        )}
    </div>
  );
};

export default CreateTalkForm;
