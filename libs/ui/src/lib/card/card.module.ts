import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@spotacard/shared';
import { CardComponent } from './card.component';
import { CloseableComponent } from './closeable';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromCard from './+state/card.reducer';
import { CardFacade } from './+state/card.facade';
import { NxModule } from '@nrwl/angular';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '@env/environment';
@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NxModule.forRoot(),
    StoreModule.forFeature(fromCard.CARD_FEATURE_KEY, fromCard.reducer)
  ],
  declarations: [CardComponent, CloseableComponent],
  exports: [CardComponent, CloseableComponent],
  entryComponents: [CloseableComponent],
  providers: [CardFacade]
})
export class CardModule {}
