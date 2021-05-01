import { Photo } from "./Photo";

    export interface Member {
      id: number;
      userName: string;
      photosUrl: string;
      age: number;
      knownAs: string;
      create: Date;
      lastActive: Date;
      gender: string;
      interduction?: any;
      lookingFor: string;
      interests: string;
      city: string;
      country: string;
      photos: Photo[];
  }

