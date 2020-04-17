import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { NxModule } from '@nrwl/angular';
import { SharedModule } from '@lowlandtech/shared';

import {
  initialState,
  metaReducers,
  CARD_FEATURE_KEY,
  reducer,
  CardFacade
} from './+state';

import { CardComponent } from './card';
import { CloseableComponent } from './closeable';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NxModule.forRoot(),
    StoreModule.forFeature(CARD_FEATURE_KEY, reducer, {
      initialState: initialState,
      metaReducers: metaReducers
    })
  ],
  declarations: [CardComponent, CloseableComponent],
  exports: [CardComponent, CloseableComponent],
  entryComponents: [CloseableComponent],
  providers: [CardFacade]
})
export class CardModule {}
