export interface Profile {
  username: string;
  bio: string;
  image: string;
  following: boolean;
  loading: boolean;
}

export interface CardComment {
  id: number;
  body: string;
  createdAt: string;
  author: Profile;
}

export interface CardData {
  id?: string;
  slug: string;
  title: string;
  description: string;
  body: string;
  tagList: string[];
  createdAt: string;
  updatedAt: string;
  favorited: boolean;
  favoritesCount: number;
  author: Profile;
  children?: CardData[];
}

export interface User {
  email: string;
  token: string;
  username: string;
  bio: string;
  image: string;
}
