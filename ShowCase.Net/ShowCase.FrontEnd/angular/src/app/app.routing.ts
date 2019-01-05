import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { AdminLayoutComponent, DefaultLayoutComponent } from './layouts';
import { AuthGuardService } from './api-client/auth/auth-guard.service';
import { LoginComponent } from './components/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SettingsComponent } from './components/settings/settings.component';

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
        redirectTo: 'admin/dashboard',
        pathMatch: 'full',
    },
    {
        path: 'admin',
        component: AdminLayoutComponent,
        canActivate: [AuthGuardService],
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
                path: 'features',
                loadChildren: './components/feature/feature.module#FeatureModule',
                data: {
                    title: 'Features'
                }
            },
            {
                path: 'users',
                loadChildren: './components/user/user.module#UserModule',
                data: {
                    title: 'Users'
                }
            },
            {
                path: 'settings',
                component: SettingsComponent,
                data: {
                    title: 'Settings'
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
    {
        path: '**',
        component: NotFoundComponent,
        data: {
            title: 'Not Found!'
        }
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
