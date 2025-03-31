export interface ShortUrl {
	longUrl: string;
	customShortUrl?: string | null;
	clickCount: number;
	createdAt: string;
	hashKey: string;
}
