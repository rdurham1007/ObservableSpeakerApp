import React, { useState } from 'react';
import { Speaker } from './speaker';
import { Modal, Button } from 'react-bootstrap';
import CreateSpeakerForm from './CreateSpeakerForm';

interface AddSpeakerModalProps {
  show: boolean;
  onClose: () => void;
  onSave: (speaker: Speaker) => void;
}

const AddSpeakerModal: React.FC<AddSpeakerModalProps> = ({ show, onClose, onSave }) => {

  const [speaker, setSpeaker] = useState<Speaker>({ id: "", firstName: "", lastName: "", bio: "", email: ""});

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(speaker);
  };

  if (!show) {
    return null;
  }

  return (
    <Modal show={show} onHide={onClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>Add Speaker</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <CreateSpeakerForm speakerData={speaker} onSpeakerChange={(updatedSpeaker) => setSpeaker(updatedSpeaker)} />
      </Modal.Body>
      <Modal.Footer>
        <Button onClick={handleSubmit}>Save</Button>
      </Modal.Footer>
    </Modal>
  );
};

export default AddSpeakerModal;