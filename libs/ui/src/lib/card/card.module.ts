import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@spotacard/shared';

import { StoreModule } from '@ngrx/store';
import * as fromCard from './+state/card.reducer';
import { EffectsModule } from '@ngrx/effects';
import { CardEffects } from './+state/card.effects';

import { CardComponent } from './card.component';
import { CloseableComponent } from './closeable';
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    StoreModule.forFeature(fromCard.cardFeatureKey, fromCard.reducer),
    EffectsModule.forFeature([CardEffects])
  ],
  declarations: [
    CardComponent,
    CloseableComponent
  ],
  exports: [
    CardComponent,
    CloseableComponent
  ],
  entryComponents:[
    CloseableComponent
  ]
})
export class CardModule {}
