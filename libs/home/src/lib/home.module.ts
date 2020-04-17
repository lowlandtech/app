import { CardListModule } from '@lowlandtech/card-list';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { HomeEffects } from './+state/home.effects';
import { HomeFacade } from './+state/home.facade';
import { homeInitialState, homeReducer, HOMESTATE_FEATURE_KEY } from './+state/home.reducer';
import { HomeResolverService } from './home-resolver.service';
import { HomeComponent } from './home.component';
import { HomeService } from './home.service';
import { TagsListComponent } from './tags-list';
import { ApiModule } from '@lowlandtech/api';

@NgModule({
  imports: [
    CommonModule,
    CardListModule,
    ApiModule,
    RouterModule.forChild([
      {
        path: '',
        pathMatch: 'full',
        component: HomeComponent,
        resolve: { HomeResolverService },
      },
    ]),
    StoreModule.forFeature(HOMESTATE_FEATURE_KEY, homeReducer, {
      initialState: homeInitialState,
    }),
    EffectsModule.forFeature([HomeEffects]),
  ],
  declarations: [HomeComponent, TagsListComponent],
  providers: [HomeEffects, HomeResolverService, HomeService, HomeFacade],
  entryComponents: [
    TagsListComponent
  ]
})
export class HomeModule {}
