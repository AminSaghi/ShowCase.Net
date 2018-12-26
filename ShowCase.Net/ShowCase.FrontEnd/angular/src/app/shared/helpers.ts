import { MenuItem, TreeNode } from 'primeng/api';

import { Project } from 'src/app/api-client/models';

export class Helpers {
    public static createMenuItemsOf(elements: any[], preRoute: string): MenuItem[] {
        const pushedIds = [];
        const result: MenuItem[] = [];

        elements.forEach(function (item) {
            if (!pushedIds.includes(item.id)) {
                pushedIds.push(item.id);

                const menuItem = {
                    label: item.title,
                    routerLink: [preRoute + '/' + item.id],
                    items: (item.children && item.children.length > 0 ? this.createMenuItemsOf(item.children, preRoute) : [])
                };

                result.push(menuItem);
            }
        });

        return result;
    }

    public static createMenuItemsOfProjects(projects: Project[]): MenuItem[] {
        const pushedIds = [];
        const result: MenuItem[] = [];

        projects.forEach(function (project) {
            if (!pushedIds.includes(project.id)) {
                pushedIds.push(project.id);

                const menuItem = {
                    label: project.title,
                    // routerLink: ['project/' + project.id],
                    items: (project.features && project.features.length > 0 ? this.createMenuItemsOf(project.features, 'feature') : [])
                };

                result.push(menuItem);
            }
        });

        return result;
    }

    public static createTreeNodesOf(elements: any[]) {
        const pushedIds = [];
        const result: TreeNode[] = [];

        elements.forEach(function (node) {
            if (pushedIds.includes(node.id)) {
                pushedIds.push(node.id);

                const treeNode = {
                    'data': {
                        'id': node.id,
                        'orderIndex': node.orderIndex,
                        'title': node.title,
                        'slug': node.slug,
                        'updateDateTime': node.updateDateTime,
                        'published': node.published
                    },
                    'children': (node.children && node.children.length > 0 ? node.createTreeNodesOf(node.children) : [])
                };

                result.push(treeNode);
            }
        });

        return result;
    }
}
