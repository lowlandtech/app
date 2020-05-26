import { CardListModule } from '@lowlandtech/card-list';
import { AuthGuardService } from '@lowlandtech/auth';
import { SharedModule } from '@lowlandtech/shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { ProfileEffects } from './+state/profile.effects';
import { ProfileFacade } from './+state/profile.facade';
import { profileInitialState, profileReducer } from './+state/profile.reducer';
import { ProfileCardsComponent } from './profile-cards.component';
import {
  ProfileCardsResolverService,
  ProfileFavoritesResolverService,
  ProfileResolverService,
} from './profile-resolver.service';
import { ProfileComponent } from './profile.component';
import { ProfileService } from './profile.service';

@NgModule({
  imports: [
    CommonModule,
    CardListModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: '',
        component: ProfileComponent,
        resolve: { ProfileResolverService },
        canActivate: [AuthGuardService],
        children: [
          {
            path: '',
            component: ProfileCardsComponent,
            resolve: { ProfileCardsResolverService },
          },
          {
            path: 'favorites',
            component: ProfileCardsComponent,
            resolve: { ProfileFavoritesResolverService },
          },
        ],
      },
    ]),
    StoreModule.forFeature('profile', profileReducer, {
      initialState: profileInitialState,
    }),
    EffectsModule.forFeature([ProfileEffects]),
  ],
  providers: [
    ProfileEffects,
    ProfileService,
    ProfileResolverService,
    ProfileCardsResolverService,
    ProfileFavoritesResolverService,
    ProfileFacade,
  ],
  declarations: [ProfileComponent, ProfileCardsComponent],
})
export class ProfileModule {}
