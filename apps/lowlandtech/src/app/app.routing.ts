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
          import('@lowlandtech/home/src/lib/home.module').then(m => m.HomeModule)
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
          import('@lowlandtech/card/src/lib/card.module').then(m => m.CardModule)
      },
      {
        path: 'settings',
        loadChildren: () =>
          import('@lowlandtech/settings/src/lib/settings.module').then(
            m => m.SettingsModule
          )
      },
      {
        path: 'editor',
        loadChildren: () =>
          import('@lowlandtech/editor/src/lib/editor.module').then(
            m => m.EditorModule
          )
      },
      {
        path: 'profile/:username',
        loadChildren: () =>
          import('@lowlandtech/profile/src/lib/profile.module').then(
            m => m.ProfileModule
          )
      },
      {
        path: 'playground',
        loadChildren: () =>
          import('@lowlandtech/playground/src/lib/playground.module').then(
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
