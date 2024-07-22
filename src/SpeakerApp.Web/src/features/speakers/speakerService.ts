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
