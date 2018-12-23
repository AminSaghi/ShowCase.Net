import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { AdminLayoutComponent, DefaultLayoutComponent } from './layouts';

import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: '',
        component: DefaultLayoutComponent,
        data: {
            title: 'Home'
        },
        children: [
            {
                path: 'home',
                loadChildren: './components/home/home.module#HomeModule'
            }
        ]
    },
    {
        path: 'admin',
        redirectTo: 'dashboard',
        pathMatch: 'full',
    },
    {
        path: 'admin',
        component: AdminLayoutComponent,
        data: {
            title: 'Admin'
        },
        children: [
            {
                path: 'dashboard',
                loadChildren: './components/dashboard/dashboard.module#DashboardModule'
            },
            {
                path: 'pages',
                loadChildren: './components/page/page.module#PageModule',
                data: {
                    title: 'Pages'
                }
            },
            {
                path: 'projects',
                loadChildren: './components/project/project.module#ProjectModule',
                data: {
                    title: 'Projects'
                }
            },
            {
                path: 'users',
                loadChildren: './components/user/user.module#UserModule',
                data: {
                    title: 'Users'
                }
            },
            // {
            //     path: 'account',
            //     component: AccountComponent,
            //     data: {
            //         title: 'Account'
            //     }
            // },
        ]
    },
    {
        path: 'login',
        component: LoginComponent,
        data: {
            title: 'Login Page'
        }
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
