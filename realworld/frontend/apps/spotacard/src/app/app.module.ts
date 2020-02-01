import { ApiModule } from '@spotacard/api';
import { AuthModule } from '@spotacard/auth';
import { NgrxErrorModule } from '@spotacard/ngrx-error';
import { NgrxRouterModule } from '@spotacard/ngrx-router';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { NxModule } from '@nrwl/angular';

import { environment } from '../environments/environment';
import { AppComponent } from './app.component';
import { FooterComponent } from './layout/footer/footer.component';
import { NavbarComponent } from './layout/navbar/navbar.component';

@NgModule({
  imports: [
    ApiModule,
    AuthModule,
    BrowserModule,
    NxModule.forRoot(),
    RouterModule.forRoot(
      [
        {
          path: '',
          loadChildren: () =>
            import('@spotacard/home/src/lib/home.module').then(m => m.HomeModule),
        },
        {
          path: 'card/:slug',
          loadChildren: () =>
            import('@spotacard/card/src/lib/card.module').then(m => m.ArticleModule),
        },
        {
          path: 'settings',
          loadChildren: () =>
            import('@spotacard/settings/src/lib/settings.module').then(
              m => m.SettingsModule,
            ),
        },
        {
          path: 'editor',
          loadChildren: () =>
            import('@spotacard/editor/src/lib/editor.module').then(m => m.EditorModule),
        },
        {
          path: 'profile/:username',
          loadChildren: () =>
            import('@spotacard/profile/src/lib/profile.module').then(m => m.ProfileModule),
        },
      ],
      {
        initialNavigation: 'enabled',
        useHash: true,
      },
    ),
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    NgrxRouterModule,
    NgrxErrorModule,
  ],
  declarations: [AppComponent, FooterComponent, NavbarComponent],
  bootstrap: [AppComponent],
})
export class AppModule {}
