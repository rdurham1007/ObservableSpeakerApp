import React from "react";
import { Speaker } from "./speaker";
import { Form } from "react-bootstrap";

interface CreateSpeakerFormProps {
  speakerData: Speaker;
  onSpeakerChange: (updatedSpeaker: Speaker) => void;
}

const CreateSpeakerForm: React.FC<CreateSpeakerFormProps> = ({
  speakerData,
  onSpeakerChange,
}) => {
  

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;

    onSpeakerChange({
        ...speakerData,
        [name]: value,
      });
  };

  return (
    <Form>
      <Form.Group>
        <Form.Label>First Name</Form.Label>
        <Form.Control
          type="text"
          value={speakerData.firstName}
          onChange={handleChange}
          required
          name="firstName"
        />
      </Form.Group>
      <Form.Group>
        <Form.Label>Last Name</Form.Label>
        <Form.Control
          type="text"
          value={speakerData.lastName}
          onChange={handleChange}
          required
          name="lastName"
        />
      </Form.Group>
      <Form.Group>
        <Form.Label>Email</Form.Label>
        <Form.Control
          type="text"
          value={speakerData.email}
          onChange={handleChange}
          required
          name="email"
        />
      </Form.Group>
      <Form.Group>
        <Form.Label>Bio</Form.Label>
        <Form.Control
          as="textarea"
          value={speakerData.bio}
          onChange={handleChange}
          required
          name="bio"
        />
      </Form.Group>
    </Form>
  );
};

export default CreateSpeakerForm;
