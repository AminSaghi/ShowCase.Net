import { Content } from './content';
import { Project } from './project';

export class Feature extends Content {
    projectId: number;
    content: string;

    createDateTime: string;
    updateDateTime: string;

    project: Project;
    parent: Feature;
}
