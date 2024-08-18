import { EditTalk } from './EditTalk';
import { Talk } from './talk';

const apiUrl = '/api';

export const getTalks = async (speakerId?: string): Promise<Talk[]> => {
  try {
    
    let url = `${apiUrl}/talks`;

    if (speakerId) {
      url += `?speakerId=${speakerId}`;
    }

    const response = await fetch(url);

    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error('Fetch error:', error);
    throw error;
  }
};

export const createTalk = async (talk: EditTalk): Promise<Talk> => {
  try {

    //remove talk.speaker.id if it is empty

    const data = {
      ...talk,
      speaker: {
        ...talk.speaker
      }
    }

    if (!data.speaker.id) {
      delete data.speaker.id;
    }

    const response = await fetch(`${apiUrl}/talks`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error('Fetch error:', error);
    throw error;
  }
}
