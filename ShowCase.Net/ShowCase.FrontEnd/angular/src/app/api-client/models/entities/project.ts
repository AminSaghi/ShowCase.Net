import { Feature } from './feature';

export class Project {
    id: number;
    name: string;
    description: string;
    imageUrl: string;

    features: Feature[];
}
