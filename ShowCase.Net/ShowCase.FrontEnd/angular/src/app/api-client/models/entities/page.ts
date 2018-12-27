import { Content } from '../base/content';

export class Page extends Content {
    content: string;

    createDateTime: string;
    updateDateTime: string;

    parent: Page;
    children: Page[];
}
