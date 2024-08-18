import { Speaker } from './speaker';

const apiUrl = '/api';

export const getSpeakers = async (): Promise<Speaker[]> => {
  try {
    const response = await fetch(`${apiUrl}/speakers`);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error('Fetch error:', error);
    throw error;
  }
};

export const addSpeaker = async (speaker: Speaker): Promise<Speaker> => {
  try {
    const response = await fetch(`${apiUrl}/speakers`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(speaker),
    });
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error('Fetch error:', error);
    throw error;
  }
};
