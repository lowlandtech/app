import { SharedModule } from '@spotacard/shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { CardListEffects } from './+state/card-list.effects';
import { CardListFacade } from './+state/card-list.facade';
import { cardListInitialState, cardListReducer } from './+state/card-list.reducer';
import { CardListItemComponent } from './card-list-item/card-list-item.component';
import { CardListComponent } from './card-list.component';
import { CardListService } from './card-list.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    StoreModule.forFeature('cardList', cardListReducer, {
      initialState: cardListInitialState,
    }),
    EffectsModule.forFeature([CardListEffects]),
  ],
  declarations: [CardListComponent, CardListItemComponent],
  providers: [CardListService, CardListEffects, CardListFacade],
  exports: [CardListComponent],
})
export class CardListModule {}
