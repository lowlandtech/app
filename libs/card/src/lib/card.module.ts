import { NgrxFormsModule } from '@spotacard/ngrx-forms';
import { SharedModule } from '@spotacard/shared';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { CardEffects } from './+state/card.effects';
import { CardFacade } from './+state/card.facade';
import { cardInitialState, cardReducer } from './+state/card.reducer';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { CardCommentComponent } from './card-comment/card-comment.component';
import { CardGuardService } from './card-guard.service';
import { CardMetaComponent } from './card-meta/card-meta.component';
import { CardComponent } from './card.component';
import { CardService } from './card.service';
import { MarkdownPipe } from './markdown.pipe';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: CardComponent,
        canActivate: [CardGuardService],
      },
    ]),
    StoreModule.forFeature('card', cardReducer, {
      initialState: cardInitialState,
    }),
    EffectsModule.forFeature([CardEffects]),
    NgrxFormsModule,
    SharedModule,
  ],
  providers: [CardEffects, CardService, CardGuardService, CardFacade],
  declarations: [CardComponent, CardMetaComponent, CardCommentComponent, MarkdownPipe, AddCommentComponent],
})
export class CardModule {}
