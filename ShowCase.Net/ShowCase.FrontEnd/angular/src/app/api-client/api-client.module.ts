// Angular
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { Urls } from './urls';

import { AuthService } from './auth/auth.service';
import { AuthInterceptor } from './auth/auth.interceptor';

import { UserService } from './user/user.service';
import { ProjectService } from './project/project.service';
import { FeatureService } from './feature/feature.service';

import { SettingsService } from './settings/settings.service';

@NgModule({
    imports: [
        // BrowserAnimationsModule,
        HttpClientModule
    ],
    declarations: [
        Urls
    ],
    exports: [
        Urls
    ]
})
export class ApiClientModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ApiClientModule,
            providers: [
                {
                    provide: HTTP_INTERCEPTORS,
                    useClass: AuthInterceptor,
                    multi: true
                },
                AuthService,
                UserService,
                ProjectService,
                FeatureService,
                SettingsService
            ]
        };
    }
}
