import { MenuItem, TreeNode, MessageService } from 'primeng/api';

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

    public static createTreeNodesOf(entityName: string, elements: any[], pushedIds: number[] = []) {
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
                    }
                };

                if (entityName === 'feature') {
                    treeNode['data']['project'] = node.project;
                }

                treeNode['children'] = node.children && node.children.length > 0 ?
                    Helpers.createTreeNodesOf(entityName, node.children, pushed) : [];

                result.push(treeNode);
            }
        });

        return result;
    }

    public static addCreatingToast(messageService: MessageService, entityName: string) {
        Helpers.addCrudToast(messageService, 'Creating', entityName);
    }
    public static addUpdatingToast(messageService: MessageService, entityName: string) {
        Helpers.addCrudToast(messageService, 'Updating', entityName);
    }
    public static addDeletingToast(messageService: MessageService, entityName: string) {
        Helpers.addCrudToast(messageService, 'Deleting', entityName);
    }
    private static addCrudToast(messageService: MessageService, operation: string, entityName: string) {
        Helpers.addToast(messageService, 'info', `${operation} ${entityName}`, 'Please wait ...');
    }
    public static addToast(messageService: MessageService, severity, summary, detail) {
        messageService.clear();

        messageService.add({
            severity: severity,
            summary: summary,
            detail: detail
        });
    }
}
