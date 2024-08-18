import { Presentation } from './Presentation';

const apiUrl = '/api';

export const getPresentations = async (speakerId?: string): Promise<Presentation[]> => {
  try {
    
    let url = `${apiUrl}/presentations`;

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
