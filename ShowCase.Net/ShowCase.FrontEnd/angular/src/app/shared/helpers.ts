import { MenuItem, TreeNode } from 'primeng/api';

import { Project } from 'src/app/api-client/models';

export class Helpers {
    public static createMenuItemsOf(elements: any[], preRoute: string, pushedIds: number[] = []): MenuItem[] {
        const pushed = pushedIds;
        const result: MenuItem[] = [];

        elements.forEach(function (item) {
            if (!pushed.includes(item.id)) {
                pushed.push(item.id);

                const menuItem = {
                    label: item.title,
                    routerLink: preRoute + '/' + item.id,
                    items: (item.children && item.children.length > 0 ? Helpers.createMenuItemsOf(item.children, preRoute, pushed) : null)
                };

                result.push(menuItem);
            }
        });

        return result;
    }

    public static createMenuItemsOfProjects(projects: Project[]): MenuItem[] {
        const result: MenuItem[] = [];

        projects.forEach(function (project) {
            const menuItem = {
                label: project.title,
                // routerLink: ['project/' + project.id],
                items: (project.features && project.features.length > 0 ? Helpers.createMenuItemsOf(project.features, 'feature') : null)
            };

            result.push(menuItem);
        });

        return result;
    }

    public static createTreeNodesOf(elements: any[], pushedIds: number[] = []) {
        const pushed = pushedIds;
        const result: TreeNode[] = [];

        elements.forEach(function (node) {
            if (!pushed.includes(node.id)) {
                pushed.push(node.id);

                const treeNode = {
                    'data': {
                        'id': node.id,
                        'orderIndex': node.orderIndex,
                        'title': node.title,
                        'slug': node.slug,
                        'updateDateTime': node.updateDateTime,
                        'published': node.published
                    },
                    'children': (node.children && node.children.length > 0 ? Helpers.createTreeNodesOf(node.children, pushed) : [])
                };

                result.push(treeNode);
            }
        });

        return result;
    }
}
