import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './layouts';

import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
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
                path: 'dashboard',
                loadChildren: './components/dashboard/dashboard.module#DashboardModule'
            },
            {
                path: 'marketview',
                loadChildren: './components/market-view/market-view.module#MarketViewModule',
                data: {
                    title: 'Market View'
                }
            },
            // {
            //     path: 'visualbots',
            //     loadChildren: './components/visual-bots/visual-bots.module#VisualBotsModule',
            //     data: {
            //         title: 'Visual Bots'
            //     }
            // },
            {
                path: 'csbot',
                loadChildren: './components/csharp-bot/csharp-bot.module#CSharpBotModule',
                data: {
                    title: 'C# Bots'
                }
            },
            {
                path: 'account',
                loadChildren: './components/account/account.module#AccountModule',
                data: {
                    title: 'Account Management'
                }
            },
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
