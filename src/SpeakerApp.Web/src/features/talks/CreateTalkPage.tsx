import React, { useState } from "react";
import Layout from "../layout/Layout";
import CreateTalkForm from "./CreateTalkForm";
import { Talk } from "./talk";
import { Button } from "react-bootstrap";
import { Speaker } from "../speakers/speaker";
import { EditTalk } from "./EditTalk";
import { createTalk } from "./talksService";
import { useNavigate } from "react-router-dom";

const CreateTalkPage: React.FC = () => {
  const [talkData, setTalkData] = useState<Talk>({
    title: "",
    abstract: "",
    speakerId: "",
    id: "",
    speakerName: "",
  });

  const navigate = useNavigate();

  const [speakerData, setSpeakerData] = useState<Speaker>({
    id: "",
    firstName: "",
    lastName: "",
    bio: "",
    email: "",
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const talk: EditTalk = {
      title: talkData.title,
      abstract: talkData.abstract,
      speaker: speakerData,
    };

    await createTalk(talk);

    console.log("Talk created");
    navigate("/talks");
  };

  const handleTalkUpdate = (updatedTalk: Talk, updatedSpeaker: Speaker) => {
    setTalkData(updatedTalk);
    setSpeakerData(updatedSpeaker);
  };

  return (
    <Layout>
      <div>
        <h2 className="float-start">Create Talk</h2>
      </div>
      <div className="clearfix"></div>
      <div>
        <CreateTalkForm talkData={talkData} onTalkChange={handleTalkUpdate} />

        <Button variant="primary" type="submit" onClick={handleSubmit}>
          Create
        </Button>
      </div>
    </Layout>
  );
};

export default CreateTalkPage;
