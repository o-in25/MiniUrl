import { ShortUrl } from "../types";

export const list = async (): Promise<ShortUrl[]> => {
  const response = await fetch('urlcondenser/list');
  if (response.ok) {
    const transaction = await response.json();
    return transaction.data;
  }
  return [];
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
export const getCondensedUrl = async(longUrl: string, customShortUrl: string | null = null): Promise<string> => {
  const response = await fetch('urlcondenser/minify', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ longUrl, customShortUrl }),
  });
  return response.text();
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
export const deleteCondensedUrl = async(hashKey: string): Promise<string> => {
  const response = await fetch(`urlcondenser/${hashKey}`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return response.text();
}
