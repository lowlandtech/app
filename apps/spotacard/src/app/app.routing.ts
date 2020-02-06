import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent, SiteComponent } from './layouts';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: '',
    component: AdminComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'home',
        loadChildren: () =>
          import('@spotacard/home/src/lib/home.module').then(m => m.HomeModule)
      },
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./pages/dashboard/dashboard.module').then(
            m => m.DashboardModule
          )
      },
      {
        path: 'card/:slug',
        loadChildren: () =>
          import('@spotacard/card/src/lib/card.module').then(m => m.CardModule)
      },
      {
        path: 'settings',
        loadChildren: () =>
          import('@spotacard/settings/src/lib/settings.module').then(
            m => m.SettingsModule
          )
      },
      {
        path: 'editor',
        loadChildren: () =>
          import('@spotacard/editor/src/lib/editor.module').then(
            m => m.EditorModule
          )
      },
      {
        path: 'profile/:username',
        loadChildren: () =>
          import('@spotacard/profile/src/lib/profile.module').then(
            m => m.ProfileModule
          )
      },
      {
        path: 'playground',
        loadChildren: () =>
          import('@spotacard/playground/src/lib/playground.module').then(
            m => m.PlaygroundModule
          )
      },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      initialNavigation: 'enabled',
      useHash: true
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
