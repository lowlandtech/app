import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { UiModule } from '@lowlandtech/ui';
import { SharedModule } from '@lowlandtech/shared';

import { CardListEffects } from './+state';
import { CardListFacade } from './+state';
import {
  cardListInitialState,
  cardListReducer,
  CARDLIST_STATE_FEATURE_KEY,
  metaReducers
} from './+state';
import { CardListItemComponent } from './card-list-item';
import { CardListService } from './card-list.service';
import { CardListComponent } from './card-list';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    StoreModule.forFeature(CARDLIST_STATE_FEATURE_KEY, cardListReducer, {
      initialState: cardListInitialState,
      metaReducers: metaReducers
    }),
    EffectsModule.forFeature([CardListEffects]),
    UiModule,
  ],
  declarations: [
    CardListComponent,
    CardListItemComponent,
  ],
  providers: [
    CardListService,
    CardListEffects,
    CardListFacade,
  ],
  exports: [
    CardListComponent,
  ]
})
export class CardListModule {}
