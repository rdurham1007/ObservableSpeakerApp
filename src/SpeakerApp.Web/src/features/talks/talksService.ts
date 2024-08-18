import { Talk } from './talk';

const apiUrl = '/api';

export const getTalks = async (speakerId: string): Promise<Talk[]> => {
  try {
    const response = await fetch(`${apiUrl}/talks?speakerId=${speakerId}`);
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return await response.json();
  } catch (error) {
    console.error('Fetch error:', error);
    throw error;
  }
};
