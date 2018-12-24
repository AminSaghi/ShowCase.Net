import { Feature } from './feature';

export class Project {
    id: number;
    orderIndex: number;
    title: string;
    slug: string;
    description: string;
    imageUrl: string;
    published: boolean;

    features: Feature[];
}
